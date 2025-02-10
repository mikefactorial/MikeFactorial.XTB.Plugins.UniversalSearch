using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MikeFactorial.XTB.Plugins.Xsd;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    public partial class UniversalSearch
    {
        private void LoadMetadata(List<EntityMetadata> selectedEntities)
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
                    int errors = 0;
                    for (int i = 0; i < selectedEntities.Count; i++)
                    {
                        try
                        {
                            string textToSearch = searchTextBox.Text;
                            EntityMetadata selectedEntity = selectedEntities[i];
                            double percentage = (double)(i + 1) / (double)selectedEntities.Count;
                            percentage *= (double)100;

                            List<AttributeMetadata> attsToSearch = null;
                            worker.ReportProgress(Convert.ToInt32(percentage), $"Searching {selectedEntity.LogicalName} metadata.");
                            if (searchAttributes.Checked)
                            {
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
                                attsToSearch = resp.EntityMetadata.Attributes.Where(meta => (meta.AttributeType == AttributeTypeCode.String || meta.AttributeType == AttributeTypeCode.Memo) && meta.IsValidForRead.Value).ToList();
                            }
                            if (worker.CancellationPending)
                            {
                                return;
                            }
                            Guid guidValue;
                            if (Guid.TryParse(this.searchTextBox.Text, out guidValue))
                            {
                                textToSearch = guidValue.ToString();
                            }

                            EntityMetadata relationshipMetadata = null;
                            if (attsToSearch != null)
                            {
                                attsToSearch = attsToSearch.OrderBy(a => a.LogicalName).ToList();
                            }
                            if (searchRelationships.Checked)
                            {
                                RetrieveEntityRequest request = new RetrieveEntityRequest();
                                request.EntityFilters = EntityFilters.Relationships;
                                request.LogicalName = selectedEntity.LogicalName;

                                var relationshipResponse = (RetrieveEntityResponse)this.Service.Execute(request);
                                relationshipMetadata = relationshipResponse.EntityMetadata;
                            }
                            if (worker.CancellationPending)
                            {
                                return;
                            }
                            var formsToSearch = new List<FormXml>();
                            var fetchToSearch = new List<FetchXml>();
                            var layoutToSearch = new List<LayoutXml>();
                            if (searchFormsViews.Checked)
                            {
                                QueryExpression formQuery = new QueryExpression("systemform");
                                formQuery.ColumnSet = new ColumnSet("formxml", "name");
                                formQuery.Criteria = new FilterExpression(LogicalOperator.And);
                                formQuery.Criteria.AddCondition("objecttypecode", ConditionOperator.Equal, selectedEntity.ObjectTypeCode.Value);
                                var forms = this.Service.RetrieveMultiple(formQuery);
                                foreach (var form in forms.Entities)
                                {
                                    if (form.Contains("formxml"))
                                    {
                                        var formObject = FormXml.Deserialize(form["formxml"].ToString());
                                        if (form.Contains("name"))
                                        {
                                            formObject.FormName = form["name"].ToString();
                                        }
                                        else
                                        {
                                            formObject.FormName = "Unknown Form";
                                        }
                                        formsToSearch.Add(formObject);
                                    }
                                }

                                if (worker.CancellationPending)
                                {
                                    return;
                                }
                                QueryExpression userQueryQuery = new QueryExpression("savedquery");
                                userQueryQuery.ColumnSet = new ColumnSet("fetchxml", "layoutxml", "columnsetxml", "name");
                                userQueryQuery.Criteria = new FilterExpression(LogicalOperator.And);
                                userQueryQuery.Criteria.AddCondition("returnedtypecode", ConditionOperator.Equal, selectedEntity.ObjectTypeCode.Value);
                                var userViews = this.Service.RetrieveMultiple(userQueryQuery);
                                foreach (var view in userViews.Entities)
                                {
                                    if (view.Contains("fetchxml"))
                                    {
                                        var fetchObject = FetchXml.Deserialize(view["fetchxml"].ToString());
                                        if (view.Contains("name"))
                                        {
                                            fetchObject.ViewName = view["name"].ToString();
                                        }
                                        else
                                        {
                                            fetchObject.ViewName = "Unknown View";
                                        }
                                        fetchToSearch.Add(fetchObject);
                                    }

                                    if (view.Contains("layoutxml"))
                                    {
                                        var layoutObject = LayoutXml.Deserialize(view["layoutxml"].ToString());
                                        if (view.Contains("name"))
                                        {
                                            layoutObject.ViewName = view["name"].ToString();
                                        }
                                        else
                                        {
                                            layoutObject.ViewName = "Unknown View";
                                        }
                                        layoutToSearch.Add(layoutObject);
                                    }
                                }
                            }

                            if (worker.CancellationPending)
                            {
                                return;
                            }
                            List<MetadataSearchResult> results = new List<MetadataSearchResult>();

                            string itemIdentifier = "";
                            this.searchMetadataObject(selectedEntity, string.Empty, "Entity", selectedEntity, itemIdentifier, textToSearch, ref results);
                            if (formsToSearch.Count > 0)
                            {
                                this.searchMetadataObject(selectedEntity, "forms", "Form", formsToSearch, itemIdentifier, textToSearch, ref results);
                            }
                            if (fetchToSearch.Count > 0)
                            {
                                this.searchMetadataObject(selectedEntity, "views", "View Query", fetchToSearch, itemIdentifier, textToSearch, ref results);
                            }
                            if (layoutToSearch.Count > 0)
                            {
                                this.searchMetadataObject(selectedEntity, "views", "View Layout", layoutToSearch, itemIdentifier, textToSearch, ref results);
                            }
                            if (attsToSearch != null)
                            {
                                this.searchMetadataObject(selectedEntity, "fields", "Attribute", attsToSearch, itemIdentifier, textToSearch, ref results);
                            }
                            if (relationshipMetadata != null)
                            {
                                this.searchMetadataObject(selectedEntity, "relationships", "Relationship", relationshipMetadata, itemIdentifier, textToSearch, ref results);
                            }

                            recordCount += results.Count;

                            AddMetadataResultTab(selectedEntity.LogicalName, results);

                            entityCount++;
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
                        worker.ReportProgress(0, $"Search Complete. No metadata was found. Make sure you've selected the correct entities to search and that you are using * for wildcard searches otherwise the exact text will be matched.");
                    }
                    else
                    {
                        worker.ReportProgress(0, $"Search Complete. Found {recordCount} instances in {entityCount} entities with {errors} error(s).");
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

        private void searchMetadataObject(EntityMetadata entity, string linkType, string metadataType, object searchObject, string itemIdentifier, string searchText, ref List<MetadataSearchResult> results)
        {
            if (searchObject == null) return;
            itemIdentifier = this.BuildItemIdentifier(searchObject, itemIdentifier);
            var listObject = searchObject as System.Collections.IList;
            if (listObject != null)
            {
                foreach (var listItem in listObject)
                {
                    searchMetadataObject(entity, linkType, metadataType, listItem, itemIdentifier, searchText, ref results);
                }
            }
            else
            {
                Type objType = searchObject.GetType();
                PropertyInfo[] properties = objType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead).ToArray();
                foreach (PropertyInfo property in properties)
                {
                    object propValue = property.GetValue(searchObject, null);
                    var elems = propValue as System.Collections.IList;
                    if (elems != null)
                    {
                        foreach (var item in elems)
                        {
                            searchMetadataObject(entity, linkType, metadataType, item, itemIdentifier, searchText, ref results);
                        }
                    }
                    else
                    {
                        // This will not cut-off System.Collections because of the first check
                        if (property.PropertyType.Assembly == objType.Assembly)
                        {
                            searchMetadataObject(entity, linkType, metadataType, propValue, itemIdentifier, searchText, ref results);
                        }
                        else
                        {
                            if (!matchCaseMetadata.Checked)
                            {
                                if (Regex.IsMatch(property.Name, WildCardToRegular(searchText), RegexOptions.IgnoreCase) || (!string.IsNullOrEmpty(propValue?.ToString()) && Regex.IsMatch(propValue?.ToString(), WildCardToRegular(searchText), RegexOptions.IgnoreCase)))
                                {
                                    results.Add(new MetadataSearchResult()
                                    {
                                        Link = this.BuildLinkForMetadataItem(entity, linkType),
                                        Property = property.Name,
                                        Value = propValue?.ToString(),
                                        Item = itemIdentifier,
                                        Type = objType.ToString().Split('.').Last(),
                                        Metadata = metadataType
                                    });
                                }
                            }
                            else if (Regex.IsMatch(property.Name, WildCardToRegular(searchText)) || (!string.IsNullOrEmpty(propValue?.ToString()) && Regex.IsMatch(propValue?.ToString(), WildCardToRegular(searchText))))
                            {
                                results.Add(new MetadataSearchResult()
                                {
                                    Link = this.BuildLinkForMetadataItem(entity, linkType),
                                    Property = property.Name,
                                    Value = propValue?.ToString(),
                                    Item = itemIdentifier,
                                    Type = objType.ToString().Split('.').Last(),
                                    Metadata = metadataType
                                });
                            }
                        }
                    }
                }
            }
            return;
        }


        private void AddMetadataResultTab(string entityName, List<MetadataSearchResult> results)
        {
            if (results.Count > 0)
            {
                if (this.tabControl1.InvokeRequired)
                {
                    AddMetadataTabCallback d = new AddMetadataTabCallback(AddMetadataResultTab);
                    this.Invoke(d, new object[] { entityName, results });
                }
                else
                {
                    DataGridView gridData = new ResultsDataGridView();
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
                    gridData.CellDoubleClick += gridData_CellDoubleClick;
                    gridData.DataSource = results;
                    gridData.DataBindingComplete += GridData_DataBindingComplete;
                    TabPage tabPage = new TabPage(entityName);
                    tabPage.Controls.Add(gridData);
                    this.tabControl1.TabPages.Add(tabPage);
                }
            }
        }
        private void findInMetadata()
        {
            if (btnFind.Text.StartsWith("Search"))
            {
                if (this.EntitiesListView.CheckedEntities.Count == 0)
                {
                    MessageBox.Show("Please select at least one entity to search before continuing.", "No Entities Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(searchTextBox.Text))
                {
                    MessageBox.Show("Please enter your search criteria in the search box. Use asterisks * to perform a wildcard search", "No Search Criteria Entered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    this.btnFind.Text = "Cancel";
                    this.searchLocationList.Enabled = false;
                    this.openInExcelToolStripMenuItem.Enabled = false;
                    LoadMetadata(this.EntitiesListView.CheckedEntities);
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

        private void OpenMetadataReference(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Process.Start(url);
            }
        }

        private string BuildItemIdentifier(object searchObject, string currentIdentifier)
        {
            if (searchObject is EntityMetadata)
            {
                return ((EntityMetadata)searchObject).DisplayName?.UserLocalizedLabel?.Label;
            }
            else if (searchObject is AttributeMetadata)
            {
                return ((AttributeMetadata)searchObject).DisplayName?.UserLocalizedLabel?.Label;
            }
            else if (searchObject is OneToManyRelationshipMetadata)
            {
                return $"{((OneToManyRelationshipMetadata)searchObject)?.ReferencedEntity} ({((OneToManyRelationshipMetadata)searchObject)?.ReferencedAttribute}) - {((OneToManyRelationshipMetadata)searchObject)?.ReferencingEntity} ({((OneToManyRelationshipMetadata)searchObject)?.ReferencingAttribute})";
            }
            else if (searchObject is ManyToManyRelationshipMetadata)
            {
                return $"{((ManyToManyRelationshipMetadata)searchObject)?.Entity1LogicalName} ({((ManyToManyRelationshipMetadata)searchObject)?.Entity1IntersectAttribute}) - {((ManyToManyRelationshipMetadata)searchObject)?.Entity2LogicalName} ({((ManyToManyRelationshipMetadata)searchObject)?.Entity2IntersectAttribute})";
            }
            else if (searchObject is FormXml)
            {
                return ((FormXml)searchObject).FormName;
            }
            else if (searchObject is FetchXml)
            {
                return ((FetchXml)searchObject).ViewName;
            }
            else if (searchObject is LayoutXml)
            {
                return ((LayoutXml)searchObject).ViewName;
            }
            return currentIdentifier;
        }
        private string BuildLinkForMetadataItem(EntityMetadata entity, string type)
        {
            return $"https://make.powerapps.com/environments/{ConnectionDetail.Organization}/entities/{entity.MetadataId.ToString()}/{entity.LogicalName}#{type}";
        }


    }
}
