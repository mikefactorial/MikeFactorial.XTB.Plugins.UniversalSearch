using Futurez.XrmToolBox.Controls;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MikeFactorial.XTB.Plugins
{
    public partial class GlobalSearch : PluginControlBase, IStatusBarMessenger, IMessageBusHost, IAboutPlugin, IGitHubPlugin, IHelpPlugin
    {
        public string RepositoryName => "D365SatSamples";

        public string UserName => "mikefactorial";

        public string HelpUrl => "https://www.mikefactorial.com/";

        public GlobalSearch()
        {
            InitializeComponent();
            EntitiesListViewControl1.Initialize(this, Service);
        }

        public event EventHandler<MessageBusEventArgs> OnOutgoingMessage;
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;
        delegate void AddTabCallback(EntityCollection collection);
        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);
            foreach (TabPage tabPage in this.tabControl1.TabPages)
            {
                foreach (Cinteros.Xrm.CRMWinForm.CRMGridView view in tabPage.Controls)
                {
                    view.OrganizationService = detail.ServiceClient;
                }
            }
            EntitiesListViewControl1.Service = detail.ServiceClient;
        }

        private void LoadData(List<EntityMetadata> selectedEntities)
        {
            this.tabControl1.TabPages.Clear();
            WorkAsync(new WorkAsyncInfo
            {
                IsCancelable = true,
                Message = "Searching...",
                AsyncArgument = selectedEntities,
                Work = (worker, args) =>
                {
                    selectedEntities = selectedEntities.OrderBy(e => e.LogicalName).ToList();
                    for (int i = 0; i < selectedEntities.Count; i++)
                    {
                        EntityMetadata selectedEntity = selectedEntities[i];
                        double percentage = (double)(i + 1) / (double)selectedEntities.Count;
                        percentage = percentage * (double)100;

                        worker.ReportProgress(Convert.ToInt32(percentage), $"Searching for {selectedEntity.LogicalName} records.");
                        // Retrieve the attribute metadata
                        var req = new RetrieveEntityRequest()
                        {
                            LogicalName = selectedEntity.LogicalName,
                            EntityFilters = EntityFilters.Attributes,
                            RetrieveAsIfPublished = true
                        };
                        var resp = (RetrieveEntityResponse)this.Service.Execute(req);

                        List<AttributeMetadata> attsToSearch = resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.String || meta.AttributeType == AttributeTypeCode.Memo) && meta.IsValidForRead.Value && meta.IsSearchable.Value).ToList();
                        Guid guidValue;
                        if (Guid.TryParse(this.searchTextBox.Text, out guidValue))
                        {
                            attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.Lookup || meta.AttributeType == AttributeTypeCode.Owner || meta.AttributeType == AttributeTypeCode.Uniqueidentifier || meta.AttributeType == AttributeTypeCode.Customer) && meta.IsValidForRead.Value && meta.IsSearchable.Value).ToList());
                        }

                        long intValue;
                        if (long.TryParse(this.searchTextBox.Text, out intValue))
                        {
                            attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.BigInt || meta.AttributeType == AttributeTypeCode.Integer || meta.AttributeType.Value == AttributeTypeCode.Picklist || meta.AttributeType.Value == AttributeTypeCode.State || meta.AttributeType.Value == AttributeTypeCode.Status) && meta.IsValidForRead.Value && meta.IsSearchable.Value).ToList());
                        }

                        double doubleValue;
                        if (double.TryParse(this.searchTextBox.Text, out doubleValue))
                        {
                            attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.Money || meta.AttributeType == AttributeTypeCode.Decimal || meta.AttributeType.Value == AttributeTypeCode.Double) && meta.IsValidForRead.Value && meta.IsSearchable.Value).ToList());
                        }
                        QueryExpression query = new QueryExpression(selectedEntity.LogicalName);
                        query.ColumnSet = new ColumnSet();
                        FilterExpression fe = new FilterExpression(LogicalOperator.Or);
                        query.ColumnSet.AddColumn(selectedEntity.PrimaryIdAttribute);
                        AttributeMetadata primaryNameAttribute = resp.EntityMetadata.Attributes.FirstOrDefault(a => a.LogicalName == selectedEntity.PrimaryNameAttribute);
                        if (primaryNameAttribute != null)
                        {
                            if (primaryNameAttribute.IsValidForRead.Value && primaryNameAttribute.IsSearchable.Value)
                            {
                                query.ColumnSet.AddColumn(selectedEntity.PrimaryNameAttribute);
                            }
                            if (primaryNameAttribute.IsSearchable.Value)
                            {
                                fe.AddCondition(selectedEntity.PrimaryNameAttribute, ConditionOperator.Like, this.searchTextBox.Text.Replace("*", "%"));
                            }
                        }
                        if (guidValue != Guid.Empty)
                        {
                            fe.AddCondition(selectedEntity.PrimaryIdAttribute, ConditionOperator.Equal, guidValue);
                        }
                        attsToSearch = attsToSearch.OrderBy(a => a.LogicalName).ToList();
                        foreach (AttributeMetadata att in attsToSearch)
                        {
                            if (att.LogicalName != selectedEntity.PrimaryNameAttribute && att.LogicalName != selectedEntity.PrimaryIdAttribute)
                            {
                                query.ColumnSet.AddColumn(att.LogicalName);
                                if (att.AttributeType == AttributeTypeCode.Lookup || att.AttributeType == AttributeTypeCode.Owner || att.AttributeType == AttributeTypeCode.Uniqueidentifier || att.AttributeType == AttributeTypeCode.Customer)
                                {
                                    fe.AddCondition(att.LogicalName, ConditionOperator.Equal, guidValue);
                                }
                                else if (att.AttributeType == AttributeTypeCode.BigInt)
                                {
                                    fe.AddCondition(att.LogicalName, ConditionOperator.Equal, intValue);
                                }
                                else if (att.AttributeType == AttributeTypeCode.Integer || att.AttributeType.Value == AttributeTypeCode.Picklist || att.AttributeType.Value == AttributeTypeCode.State || att.AttributeType.Value == AttributeTypeCode.Status)
                                {
                                    fe.AddCondition(att.LogicalName, ConditionOperator.Equal, (Int32)intValue);
                                }
                                else if (att.AttributeType.Value == AttributeTypeCode.Double)
                                {
                                    fe.AddCondition(att.LogicalName, ConditionOperator.Equal, doubleValue);
                                }
                                else if (att.AttributeType == AttributeTypeCode.Money || att.AttributeType == AttributeTypeCode.Decimal)
                                {
                                    fe.AddCondition(att.LogicalName, ConditionOperator.Equal, (decimal)doubleValue);
                                }
                                else
                                {
                                    fe.AddCondition(att.LogicalName, ConditionOperator.Like, this.searchTextBox.Text.Replace("*", "%"));
                                }
                            }
                        }
                        query.Criteria.AddFilter(fe);
                        AddTab(Service.RetrieveMultiple(query));


                    }
                },
                ProgressChanged = (args) =>
                {
                    SetWorkingMessage(args.UserState?.ToString());
                    SendMessageToStatusBar(this, new StatusBarMessageEventArgs(args.ProgressPercentage, args.UserState.ToString().Replace("\r\n", " ")));
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.Message, "Oh crap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            });
        }

        private void AddTab(EntityCollection collection)
        {
            if (this.tabControl1.InvokeRequired)
            {
                AddTabCallback d = new AddTabCallback(AddTab);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                if (collection.Entities.Count > 0)
                {
                    Cinteros.Xrm.CRMWinForm.CRMGridView gridData = new Cinteros.Xrm.CRMWinForm.CRMGridView();
                    gridData.AllowUserToAddRows = false;
                    gridData.AllowUserToDeleteRows = false;
                    gridData.AllowUserToOrderColumns = true;
                    gridData.AllowUserToResizeRows = false;
                    gridData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                    gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    gridData.Dock = System.Windows.Forms.DockStyle.Fill;
                    gridData.EntityReferenceClickable = true;
                    gridData.FilterColumns = "";
                    gridData.Location = new System.Drawing.Point(8, 38);
                    gridData.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
                    gridData.Name = "gridData";
                    gridData.ReadOnly = true;
                    gridData.RowHeadersVisible = false;
                    gridData.ShowIndexColumn = false;
                    gridData.Size = new System.Drawing.Size(1587, 1090);
                    gridData.TabIndex = 0;
                    gridData.RecordClick += new Cinteros.Xrm.CRMWinForm.CRMRecordEventHandler(gridData_RecordClick);
                    gridData.RecordDoubleClick += new Cinteros.Xrm.CRMWinForm.CRMRecordEventHandler(this.gridData_RecordDoubleClick);
                    gridData.DataSource = collection;
                    TabPage tabPage = new TabPage(collection.EntityName);
                    tabPage.Controls.Add(gridData);

                    this.tabControl1.TabPages.Add(tabPage);
                }
            }
        }
        private void OpenEntityReference(EntityReference entref)
        {
            if (!string.IsNullOrEmpty(entref.LogicalName) && !entref.Id.Equals(Guid.Empty))
            {
                var url = ConnectionDetail.WebApplicationUrl;
                if (string.IsNullOrEmpty(url))
                {
                    url = string.Concat(ConnectionDetail.ServerName, "/", ConnectionDetail.Organization);
                    if (!url.ToLower().StartsWith("http"))
                    {
                        url = string.Concat("http://", url);
                    }
                }
                url = string.Concat(url,
                    url.EndsWith("/") ? "" : "/",
                    "main.aspx?etn=",
                    entref.LogicalName,
                    "&pagetype=entityrecord&id=",
                    entref.Id.ToString());
                if (!string.IsNullOrEmpty(url))
                {
                    Process.Start(url);
                }
            }
        }

        private void gridData_RecordDoubleClick(object sender, Cinteros.Xrm.CRMWinForm.CRMRecordEventArgs e)
        {
            OpenEntityReference(e.Entity.ToEntityReference());
        }

        private void gridData_RecordClick(object sender, Cinteros.Xrm.CRMWinForm.CRMRecordEventArgs e)
        {
            if (e.Value is EntityReference entref)
            {
                OpenEntityReference(entref);
            }
        }

        public void OnIncomingMessage(MessageBusEventArgs message)
        {
            MessageBox.Show("Simple Data Viewer cannot yet handle calls from other tools.");
        }

        public void ShowAboutDialog()
        {
            MessageBox.Show("Simple Data Viewer\n\nD365 Saturday Sample project");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData(this.EntitiesListViewControl1.CheckedEntities);
        }
    }
}