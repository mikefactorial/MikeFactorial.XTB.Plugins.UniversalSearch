using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    public partial class UniversalSearch
    {
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
                    long recordCount = 0;
                    int entityCount = 0;
                    List<EntityMetadata> nonSelectedEntities = new List<EntityMetadata>();
                    Dictionary<Guid, string> matchedLookups = new Dictionary<Guid, string>();
                    int errors = 0;
                    for (int i = 0; i < selectedEntities.Count; i++)
                    {
                        try
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
                            if (worker.CancellationPending)
                            {
                                return;
                            }
                            List<AttributeMetadata> attsToSearch = resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.String || meta.AttributeType == AttributeTypeCode.Memo) && meta.IsValidForRead.Value).ToList();


                            if (worker.CancellationPending)
                            {
                                return;
                            }
                            Guid guidValue;
                            if (Guid.TryParse(this.searchTextBox.Text, out guidValue))
                            {
                                attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.Lookup || meta.AttributeType == AttributeTypeCode.Owner || meta.AttributeType == AttributeTypeCode.Uniqueidentifier || meta.AttributeType == AttributeTypeCode.Customer) && meta.IsValidForRead.Value).ToList());
                            }
                            else if (searchLookupText.Checked)
                            {
                                attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.Lookup || meta.AttributeType == AttributeTypeCode.Owner || meta.AttributeType == AttributeTypeCode.Customer) && meta.IsValidForRead.Value).ToList());
                            }

                            long intValue;
                            if (long.TryParse(this.searchTextBox.Text, out intValue))
                            {
                                attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.BigInt || meta.AttributeType == AttributeTypeCode.Integer || meta.AttributeType.Value == AttributeTypeCode.Picklist || meta.AttributeType.Value == AttributeTypeCode.State || meta.AttributeType.Value == AttributeTypeCode.Status) && meta.IsValidForRead.Value).ToList());
                            }
                            else
                            {
                                if (searchOptionSetText.Checked)
                                {
                                    attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.Picklist) && meta.IsValidForRead.Value));
                                }
                                intValue = long.MinValue;
                            }

                            double doubleValue;
                            if (double.TryParse(this.searchTextBox.Text, out doubleValue))
                            {
                                attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.Money || meta.AttributeType == AttributeTypeCode.Decimal || meta.AttributeType.Value == AttributeTypeCode.Double) && meta.IsValidForRead.Value).ToList());
                            }
                            DateTime dateTimeValue;
                            if (DateTime.TryParse(this.searchTextBox.Text, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTimeValue))
                            {
                                attsToSearch.AddRange(resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.DateTime) && meta.IsValidForRead.Value).ToList());
                            }

                            QueryExpression query = new QueryExpression(selectedEntity.LogicalName);
                            query.ColumnSet = new ColumnSet();
                            FilterExpression fe = new FilterExpression(LogicalOperator.Or);
                            query.ColumnSet.AddColumn(selectedEntity.PrimaryIdAttribute);
                            AttributeMetadata primaryNameAttribute = resp.EntityMetadata.Attributes.FirstOrDefault(a => a.LogicalName == selectedEntity.PrimaryNameAttribute);
                            if (primaryNameAttribute != null)
                            {
                                if (primaryNameAttribute.IsValidForRead.Value && string.IsNullOrEmpty(primaryNameAttribute.AttributeOf))
                                {
                                    query.ColumnSet.AddColumn(primaryNameAttribute.LogicalName);
                                }
                                fe.AddCondition(primaryNameAttribute.LogicalName, ConditionOperator.Like, this.searchTextBox.Text.Replace("*", "%"));
                            }
                            if (guidValue != Guid.Empty)
                            {
                                fe.AddCondition(selectedEntity.PrimaryIdAttribute, ConditionOperator.Equal, guidValue);
                            }
                            attsToSearch = attsToSearch.OrderBy(a => a.LogicalName).ToList();
                            bool conditionAdded = false;
                            EntityMetadata relationshipMetadata = null;
                            if (searchLookupText.Checked)
                            {
                                RetrieveEntityRequest request = new RetrieveEntityRequest();
                                request.EntityFilters = EntityFilters.Relationships;
                                request.LogicalName = selectedEntity.LogicalName;

                                var relationshipResponse = (RetrieveEntityResponse)this.Service.Execute(request);
                                relationshipMetadata = relationshipResponse.EntityMetadata;
                            }

                            foreach (AttributeMetadata att in attsToSearch)
                            {
                                if (att.LogicalName != selectedEntity.PrimaryNameAttribute && att.LogicalName != selectedEntity.PrimaryIdAttribute)
                                {
                                    if (string.IsNullOrEmpty(att.AttributeOf))
                                    {
                                        query.ColumnSet.AddColumn(att.LogicalName);
                                        if (att.AttributeType == AttributeTypeCode.Lookup || att.AttributeType == AttributeTypeCode.Owner || att.AttributeType == AttributeTypeCode.Uniqueidentifier || att.AttributeType == AttributeTypeCode.Customer)
                                        {
                                            if (searchLookupText.Checked && (att.AttributeType == AttributeTypeCode.Lookup || att.AttributeType == AttributeTypeCode.Owner || att.AttributeType == AttributeTypeCode.Customer) && guidValue == Guid.Empty)
                                            {
                                                //Here we are going to search for the picklist display value rather than the value
                                                LookupAttributeMetadata lookup = (LookupAttributeMetadata)att;
                                                OneToManyRelationshipMetadata relationship = relationshipMetadata.ManyToOneRelationships.FirstOrDefault(r => r.ReferencingAttribute == lookup.LogicalName);

                                                if (relationship != null)
                                                {
                                                    QueryExpression lookupQuery = new QueryExpression(relationship.ReferencedEntity);
                                                    EntityMetadata relatedEntityMetadata = selectedEntities.FirstOrDefault(m => m.LogicalName == relationship.ReferencedEntity);
                                                    if (relatedEntityMetadata == null)
                                                    {
                                                        relatedEntityMetadata = nonSelectedEntities.FirstOrDefault(m => m.LogicalName == relationship.ReferencedEntity);
                                                        if (relatedEntityMetadata == null)
                                                        {
                                                            RetrieveEntityRequest request = new RetrieveEntityRequest();
                                                            request.EntityFilters = EntityFilters.Entity;
                                                            request.LogicalName = relationship.ReferencedEntity;

                                                            var relationshipResponse = (RetrieveEntityResponse)this.Service.Execute(request);
                                                            nonSelectedEntities.Add(relationshipResponse.EntityMetadata);
                                                            relatedEntityMetadata = nonSelectedEntities.FirstOrDefault(m => m.LogicalName == relationship.ReferencedEntity);
                                                        }
                                                    }
                                                    FilterExpression filter = new FilterExpression();
                                                    filter.AddCondition(relatedEntityMetadata.PrimaryNameAttribute, ConditionOperator.Like, this.searchTextBox.Text.Replace("*", "%"));
                                                    lookupQuery.Criteria.AddFilter(filter);
                                                    lookupQuery.ColumnSet = new ColumnSet(new string[] { relatedEntityMetadata.PrimaryIdAttribute, relatedEntityMetadata.PrimaryNameAttribute });

                                                    EntityCollection parentEntities = this.Service.RetrieveMultiple(lookupQuery);
                                                    foreach (var parentEntity in parentEntities.Entities)
                                                    {
                                                        fe.AddCondition(lookup.LogicalName, ConditionOperator.Equal, parentEntity[relatedEntityMetadata.PrimaryIdAttribute]);
                                                        if (!matchedLookups.ContainsKey((Guid)parentEntity[relatedEntityMetadata.PrimaryIdAttribute]))
                                                        {
                                                            matchedLookups.Add((Guid)parentEntity[relatedEntityMetadata.PrimaryIdAttribute], (string)parentEntity[relatedEntityMetadata.PrimaryNameAttribute]);
                                                        }
                                                        conditionAdded = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                fe.AddCondition(att.LogicalName, ConditionOperator.Equal, guidValue);
                                                conditionAdded = true;
                                            }
                                        }
                                        else if (att.AttributeType == AttributeTypeCode.BigInt)
                                        {
                                            fe.AddCondition(att.LogicalName, ConditionOperator.Equal, intValue);
                                            conditionAdded = true;
                                        }
                                        else if (att.AttributeType == AttributeTypeCode.Integer || att.AttributeType.Value == AttributeTypeCode.Picklist || att.AttributeType.Value == AttributeTypeCode.State || att.AttributeType.Value == AttributeTypeCode.Status)
                                        {
                                            if (searchOptionSetText.Checked && (att.AttributeType.Value == AttributeTypeCode.Picklist && intValue == long.MinValue))
                                            {
                                                //Here we are going to search for the picklist display value rather than the value
                                                PicklistAttributeMetadata pick = (PicklistAttributeMetadata)att;
                                                if (pick.OptionSet != null)
                                                {
                                                    foreach (var option in pick.OptionSet.Options)
                                                    {
                                                        if (option.Label != null && option.Label.UserLocalizedLabel != null && option.Value != null)
                                                        {
                                                            if (!matchCaseCheckBox.Checked)
                                                            {
                                                                if (Regex.IsMatch(option.Label.UserLocalizedLabel.Label, WildCardToRegular(this.searchTextBox.Text), RegexOptions.IgnoreCase))
                                                                {
                                                                    fe.AddCondition(att.LogicalName, ConditionOperator.Equal, (Int32)option.Value.Value);
                                                                    conditionAdded = true;
                                                                }
                                                            }
                                                            else if (Regex.IsMatch(option.Label.UserLocalizedLabel.Label, WildCardToRegular(this.searchTextBox.Text)))
                                                            {
                                                                fe.AddCondition(att.LogicalName, ConditionOperator.Equal, (Int32)option.Value.Value);
                                                                conditionAdded = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                fe.AddCondition(att.LogicalName, ConditionOperator.Equal, (Int32)intValue);
                                                conditionAdded = true;
                                            }
                                        }
                                        else if (att.AttributeType.Value == AttributeTypeCode.DateTime)
                                        {
                                            fe.AddCondition(att.LogicalName, ConditionOperator.On, dateTimeValue.Date);
                                            conditionAdded = true;
                                        }
                                        else if (att.AttributeType.Value == AttributeTypeCode.Double)
                                        {
                                            fe.AddCondition(att.LogicalName, ConditionOperator.Equal, doubleValue);
                                            conditionAdded = true;
                                        }
                                        else if (att.AttributeType == AttributeTypeCode.Money || att.AttributeType == AttributeTypeCode.Decimal)
                                        {
                                            fe.AddCondition(att.LogicalName, ConditionOperator.Equal, (decimal)doubleValue);
                                            conditionAdded = true;
                                        }
                                        else
                                        {
                                            fe.AddCondition(att.LogicalName, ConditionOperator.Like, this.searchTextBox.Text.Replace("*", "%"));
                                            conditionAdded = true;
                                        }
                                    }
                                }
                            }
                            query.Criteria.AddFilter(fe);
                            if (worker.CancellationPending)
                            {
                                return;
                            }
                            if (worker.CancellationPending)
                            {
                                return;
                            }
                            if (conditionAdded)
                            {
                                EntityCollection results = Service.RetrieveMultiple(query);
                                if (results.Entities != null && results.Entities.Count > 0)
                                {
                                    //Here we are going to replace the integer value with the display value for the picklist we returned in the results and convert UTC date/time to local for the results to display correctly..
                                    UpdateDisplayTexts(attsToSearch, intValue, matchedLookups, guidValue, results);

                                    if (this.matchCaseCheckBox.Checked || dateTimeValue != DateTime.MinValue)
                                    {
                                        for (int j = 0; j < results.Entities.Count; j++)
                                        {
                                            Entity e = results.Entities[j];
                                            if (this.matchCaseCheckBox.Checked && !e.Attributes.Any(a => (a.Value != null && LikeOperator.LikeString(a.Value.ToString(), searchTextBox.Text, Microsoft.VisualBasic.CompareMethod.Binary))))
                                            {
                                                results.Entities.RemoveAt(j);
                                                j--;
                                            }
                                            else if (dateTimeValue != DateTime.MinValue)
                                            {
                                                bool found = false;
                                                foreach (var att in attsToSearch.Where(a => a.AttributeType != null && a.AttributeType.Value == AttributeTypeCode.DateTime))
                                                {
                                                    if (results.Entities[j].Contains(att.LogicalName) && ((DateTime)results.Entities[j][att.LogicalName]).Date == dateTimeValue.Date)
                                                    {
                                                        found = true;
                                                        break;
                                                    }
                                                }
                                                if (!found)
                                                {
                                                    results.Entities.RemoveAt(j);
                                                    j--;
                                                }
                                            }
                                        }
                                        recordCount += results.Entities.Count;
                                        entityCount++;
                                        AddRecordResultTab(results);
                                    }
                                    else
                                    {
                                        recordCount += results.Entities.Count;
                                        entityCount++;
                                        AddRecordResultTab(results);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            errors++;
                            try
                            {
                                this.LogError(e.ToString());
                            }
                            catch { }
                            worker.ReportProgress(0, $"Search Completed with Errors. Error Message: " + e.Message);
                        }
                    }
                    if (recordCount == 0)
                    {
                        worker.ReportProgress(0, $"Search Complete. No records were found. Make sure you've selected the correct entities to search and that you are using * for wildcard searches otherwise the exact text will be matched.");
                    }
                    else
                    {
                        worker.ReportProgress(0, $"Search Complete. Found {recordCount} records in {entityCount} entities with {errors} error(s).");
                    }

                },
                ProgressChanged = (args) =>
                {
                    SetWorkingMessage(args.UserState?.ToString());
                    SendMessageToStatusBar(this, new StatusBarMessageEventArgs(args.ProgressPercentage, args.UserState.ToString().Replace("\r\n", " ")));
                },
                PostWorkCallBack = (args) =>
                {
                    this.searchLocationList.Enabled = true;
                    this.openInExcelToolStripMenuItem.Enabled = (tabControl1.TabPages.Count > 0);
                    this.btnFind.Text = "Search";
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.Message, "Uh oh.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            });
        }
        private void AddSolutionResultTab(string directoryName, List<SolutionSearchResult> results)
        {
            if (results.Count > 0)
            {
                if (this.tabControl1.InvokeRequired)
                {
                    AddSolutionTabCallback d = new AddSolutionTabCallback(AddSolutionResultTab);
                    this.Invoke(d, new object[] { directoryName, results });
                }
                else
                {
                    DataGridView gridData = new DataGridView();
                    gridData.AllowUserToAddRows = false;
                    gridData.AllowUserToDeleteRows = false;
                    gridData.AllowUserToOrderColumns = true;
                    gridData.AllowUserToResizeRows = false;
                    gridData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                    gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    gridData.Dock = System.Windows.Forms.DockStyle.Fill;
                    gridData.Location = new System.Drawing.Point(8, 38);
                    gridData.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
                    gridData.Name = "gridData";
                    gridData.ReadOnly = true;
                    gridData.RowHeadersVisible = false;
                    gridData.Size = new System.Drawing.Size(1587, 1090);
                    gridData.TabIndex = 0;
                    gridData.CellDoubleClick += gridData_SolutionCellDoubleClick;
                    gridData.DataSource = results;
                    gridData.DataBindingComplete += GridData_DataBindingComplete;
                    TabPage tabPage = new TabPage(directoryName);
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
        private void findInRecords()
        {
            if (btnFind.Text.StartsWith("Search"))
            {
                if (this.EntitiesListView.CheckedEntities.Count == 0)
                {
                    MessageBox.Show("Please select at least one entity to search before continuing.", "No Entities Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(searchTextBox.Text))
                {
                    MessageBox.Show("Please enter your search criteria in the search box. Use asterisks * to perform a wildcard search.", "No Search Criteria Entered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    this.searchLocationList.Enabled = false;
                    this.openInExcelToolStripMenuItem.Enabled = false;
                    this.btnFind.Text = "Cancel";
                    LoadData(this.EntitiesListView.CheckedEntities);
                }
            }
            else
            {
                CancelWorker(); // PluginBaseControl method that calls the Background Workers CancelAsync method.
                this.btnFind.Text = "Search";
                this.searchLocationList.Enabled = true;
                this.openInExcelToolStripMenuItem.Enabled = (tabControl1.TabPages.Count > 0);

            }
        }

    }
}
