using McTools.Xrm.Connection;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xrmtb.XrmToolBox.Controls;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    public partial class UniversalSearch : PluginControlBase, IStatusBarMessenger, IAboutPlugin, IGitHubPlugin, IHelpPlugin
    {
        public string RepositoryName => "MikeFactorial.XTB.Plugins.UniversalSearch";

        public string UserName => "mikefactorial";

        public string HelpUrl => "https://mikefactorial.com/dynamics-365-universal-search-for-xrmtoolbox/";

        private bool solutionsSorted = false;
        public UniversalSearch()
        {
            InitializeComponent();
            this.SolutionDropDownComboBox.DropDownWidth = 400;
            this.SolutionDropDownComboBox.DropDownHeight = 300;
            this.SplitContainerToolbar.SplitterDistance = (int)(this.SplitContainerToolbar.ClientSize.Width * .4);
            this.SolutionDropDownComboBox.DataSourceChanged += SolutionsDropDown_DataSourceChanged;
            EntitiesListView.SortList(0, SortOrder.Ascending);
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;
        delegate void AddTabCallback(EntityCollection collection);
        delegate void AddMetadataTabCallback(string entityName, List<MetadataSearchResult> results);
        delegate void AddSolutionTabCallback(string directoryName, List<SolutionSearchResult> results);

        public System.Windows.Forms.CheckBox CheckBoxCheckAllNone
        {
            get
            {
                return this.Controls.Find("checkBoxCheckAllNone", true).ToList().FirstOrDefault() as System.Windows.Forms.CheckBox;
            }
        }
        public ListView EntityListView
        {
            get
            {
                return this.Controls.Find("ListViewMain", true).ToList().FirstOrDefault() as ListView;
            }
        }
        public SplitContainer SplitContainerToolbar
        {
            get
            {
                return this.Controls.Find("splitContainerToolbar", true).ToList().FirstOrDefault() as SplitContainer;
            }
        }
        public SolutionsDropdownControl SolutionsDropDown
        {
            get
            {
                return this.Controls.Find("solutionsDropDown", true).ToList().FirstOrDefault() as SolutionsDropdownControl;
            }
        }
        public ComboBox SolutionDropDownComboBox
        {
            get
            {
                return this.Controls.Find("comboSolutions", true).ToList().FirstOrDefault() as ComboBox;
            }
        }
        private void SolutionsDropDown_DataSourceChanged(object sender, EventArgs e)
        {
            sortSolutionList();
            if (this.SolutionsDropDown != null && this.SolutionDropDownComboBox.DataSource != null)
            {
                var defaultSolution = ((List<ListDisplayItem>)this.SolutionDropDownComboBox.DataSource).FirstOrDefault(i => i.Name.ToLower() == "default");
                this.SolutionDropDownComboBox.SelectedItem = defaultSolution;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            foreach (TabPage tabPage in this.tabControl1.TabPages)
            {
                foreach (object tab in tabPage.Controls)
                {
                    var view = tab as xrmtb.XrmToolBox.Controls.CRMGridView;
                    if (view != null)
                    {
                        view.OrganizationService = newService;
                    }
                }
            }
            EntitiesListView.Service = newService;
            base.UpdateConnection(newService, detail, actionName, parameter);
        }

        private void UpdateDisplayTexts(List<AttributeMetadata> attsToSearch, long intValue, Dictionary<Guid, string> matchedLookups, Guid guidValue, EntityCollection results)
        {
            if (searchOptionSetText.Checked && intValue == long.MinValue)
            {
                foreach (AttributeMetadata att in attsToSearch.Where(att => att.AttributeType != null && att.AttributeType.Value == AttributeTypeCode.Picklist))
                {
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
                                        for (int j = 0; j < results.Entities.Count; j++)
                                        {
                                            if (results.Entities[j].Contains(pick.LogicalName))
                                            {
                                                results.Entities[j][pick.LogicalName] = option.Label.UserLocalizedLabel.Label;
                                            }
                                        }
                                    }
                                }
                                else if (Regex.IsMatch(option.Label.UserLocalizedLabel.Label, WildCardToRegular(this.searchTextBox.Text)))
                                {
                                    for (int j = 0; j < results.Entities.Count; j++)
                                    {
                                        if (results.Entities[j].Contains(pick.LogicalName))
                                        {
                                            results.Entities[j][pick.LogicalName] = option.Label.UserLocalizedLabel.Label;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (searchLookupText.Checked && guidValue == Guid.Empty)
            {
                var lookupAtts = attsToSearch.Where(att => att.AttributeType != null && (att.AttributeType.Value == AttributeTypeCode.Lookup || att.AttributeType.Value == AttributeTypeCode.Customer || att.AttributeType.Value == AttributeTypeCode.Owner)).ToList();
                foreach (AttributeMetadata att in lookupAtts)
                {
                    for (int j = 0; j < results.Entities.Count; j++)
                    {
                        var iEnum = matchedLookups.GetEnumerator();
                        while (iEnum.MoveNext())
                        {
                            if (results.Entities[j].Contains(att.LogicalName) && ((EntityReference)results.Entities[j][att.LogicalName]).Id == iEnum.Current.Key)
                            {
                                results.Entities[j][att.LogicalName] = iEnum.Current.Value;
                            }

                        }
                    }
                }
            }
        }

        private static string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
        }
        private void AddRecordResultTab(EntityCollection collection)
        {
            if (collection != null && collection.Entities != null)
            {
                if (this.tabControl1.InvokeRequired)
                {
                    AddTabCallback d = new AddTabCallback(AddRecordResultTab);
                    this.Invoke(d, new object[] { collection });
                }
                else
                {
                    if (collection.Entities.Count > 0)
                    {
                        xrmtb.XrmToolBox.Controls.CRMGridView gridData = new xrmtb.XrmToolBox.Controls.CRMGridView();
                        gridData.ShowLocalTimes = false;
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
                        gridData.RecordDoubleClick += new xrmtb.XrmToolBox.Controls.CRMRecordEventHandler(this.gridData_RecordDoubleClick);
                        gridData.DataSource = collection;
                        gridData.DataBindingComplete += GridData_DataBindingComplete;
                        TabPage tabPage = new TabPage(collection.EntityName);
                        tabPage.Controls.Add(gridData);

                        this.tabControl1.TabPages.Add(tabPage);
                    }
                }
            }
        }

        private void gridData_SolutionCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
                string filePath = row.Cells[0].Value.ToString();
                var fullFilePath = Path.GetFullPath(filePath);
                Process.Start(fullFilePath);
            }
        }

        private void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
                string link = row.Cells[0].Value.ToString();
                OpenMetadataReference(link);

            }
        }

        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(0, 10, 0, 10);
            }
        }
        private void GridData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).Columns[0].Visible = false;
            foreach (DataGridViewRow row in ((DataGridView)sender).Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    Guid guidValue;
                    Guid guidCellValue;
                    if (Guid.TryParse(this.searchTextBox.Text, out guidValue) && Guid.TryParse(cell.Value.ToString(), out guidCellValue) && guidValue == guidCellValue)
                    {
                        cell.Style.BackColor = Color.Yellow;
                    }
                    long intValue;
                    long intCellValue;
                    if (long.TryParse(this.searchTextBox.Text, out intValue) && long.TryParse(cell.Value.ToString(), out intCellValue) && intValue == intCellValue)
                    {
                        cell.Style.BackColor = Color.Yellow;
                    }
                    double doubleValue;
                    double doubleCellValue;
                    if (double.TryParse(this.searchTextBox.Text, out doubleValue) && double.TryParse(cell.Value.ToString(), out doubleCellValue) && doubleValue == doubleCellValue)
                    {
                        cell.Style.BackColor = Color.Yellow;
                    }
                    DateTime dateTimeValue;
                    DateTime dateTimeCellValue;
                    if (DateTime.TryParse(this.searchTextBox.Text, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTimeValue) && DateTime.TryParse(cell.Value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTimeCellValue) && dateTimeValue.Date == dateTimeCellValue.Date)
                    {
                        cell.Style.BackColor = Color.Yellow;
                    }
                    if (cell != null && cell.Value != null)
                    {
                        if (this.matchCaseCheckBox.Checked)
                        {
                            if (LikeOperator.LikeString(cell.Value.ToString(), searchTextBox.Text, Microsoft.VisualBasic.CompareMethod.Binary))
                            {
                                cell.Style.BackColor = Color.Yellow;
                            }
                        }
                        else
                        {
                            if (LikeOperator.LikeString(cell.Value.ToString().ToLower(), searchTextBox.Text.ToLower(), Microsoft.VisualBasic.CompareMethod.Binary))
                            {
                                cell.Style.BackColor = Color.Yellow;
                            }
                        }
                    }
                }
            }
        }

        private void gridData_RecordDoubleClick(object sender, xrmtb.XrmToolBox.Controls.CRMRecordEventArgs e)
        {
            OpenEntityReference(e.Entity.ToEntityReference());
        }

        private void SearchTextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind.PerformClick();
            }
        }

        public void OnIncomingMessage(MessageBusEventArgs message)
        {
            MessageBox.Show("Universal Search cannot yet handle calls from other tools.");
        }

        public void ShowAboutDialog()
        {
            MessageBox.Show("Universal Search by Mike!");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            openInExcelToolStripMenuItem.Enabled = false;
            if (searchLocationList.SelectedIndex == 0)
            {
                findInRecords();
            }
            else if (searchLocationList.SelectedIndex == 1)
            {
                findInMetadata();
            }
            else if (searchLocationList.SelectedIndex == 2)
            {
                findInSolution();
            }
        }

        private void SearchLocationList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.updateSearchVisibility();
        }
        private void sortSolutionList()
        {
            if (this.SolutionDropDownComboBox.DataSource != null)
            {
                ((List<ListDisplayItem>)this.SolutionDropDownComboBox.DataSource).Sort(delegate (ListDisplayItem x, ListDisplayItem y)
                {
                    if (x.DisplayName == null && y.DisplayName == null) return 0;
                    else if (x.DisplayName == null) return -1;
                    else if (y.DisplayName == null) return 1;
                    else return x.DisplayName.CompareTo(y.DisplayName);
                });
            }
        }
        private void updateSearchVisibility()
        {
            recordSearchGroup.Visible = (searchLocationList.SelectedIndex == 0);
            metadataSearchGroup.Visible = (searchLocationList.SelectedIndex == 1);
            solutionSearchGroup.Visible = (searchLocationList.SelectedIndex == 2);
            resultsGroup.Text = (searchLocationList.SelectedIndex == 1) ? "Metadata" : (searchLocationList.SelectedIndex == 2) ? "Files" : "Records";
            btnFind.Text = $"Search {searchLocationList.Text}";
            if(searchLocationList.SelectedIndex == 2) {
                this.EntityListView.Visible = false;
                this.SplitContainerToolbar.Panel1Collapsed = true;
                var items = (List<ListDisplayItem>)this.SolutionDropDownComboBox.DataSource;
                if (items != null)
                {
                    //Filter the solution list
                    items = items.Where(i => !((Entity)i.Object).GetAttributeValue<bool>("ismanaged") && i.Name.ToLower() != "active" && i.Name.ToLower() != "default").ToList();
                    this.SolutionDropDownComboBox.DataSource = items;

                }
                //this.SolutionsDropDown.SolutionType = CrmActions.SolutionType.Unmanaged;
                this.groupBox1.Text = "Solutions";
            }
            else
            {
                this.EntityListView.Visible = true;
                this.SplitContainerToolbar.Panel1Collapsed = false;
                //this.SolutionsDropDown.SolutionType = CrmActions.SolutionType.Both;
                this.SolutionsDropDown.LoadData();
                this.groupBox1.Text = "Entities";
            }
        }

        private void openInExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                openInExcelToolStripMenuItem.Enabled = false;
                var xlexcel = new Microsoft.Office.Interop.Excel.Application();
                xlexcel.Visible = true;
                var xlWorkBook = xlexcel.Workbooks.Add(System.Reflection.Missing.Value);
                int worksheetNumber = 0;
                dynamic xlResultSheet = null;
                foreach (TabPage tabPage in this.tabControl1.TabPages)
                {
                    foreach (var control in tabPage.Controls)
                    {
                        if (control is xrmtb.XrmToolBox.Controls.CRMGridView gridData)
                        {
                            worksheetNumber++;
                            var tempCopyMode = gridData.ClipboardCopyMode;
                            gridData.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                            gridData.SelectAll();
                            var dataObj = gridData.GetClipboardContent();
                            gridData.ClearSelection();
                            if (dataObj == null)
                            {
                                return;
                            }
                            Clipboard.SetDataObject(dataObj);

                            // Create sheet for results
                            if (worksheetNumber == 1) 
                            {
                                xlResultSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                            }
                            else
                            {
                                xlResultSheet = (Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, (Worksheet)xlResultSheet, Type.Missing, Type.Missing);
                            }
                            xlResultSheet.Name = tabPage.Text;
                            // Paste all data
                            var cellResultA1 = (Range)xlResultSheet.Cells[1, 1];
                            cellResultA1.Select();
                            xlResultSheet.PasteSpecial(cellResultA1, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                            // Format width and headers
                            var header = (Range)xlResultSheet.Rows[1];
                            header.Font.Bold = true;
                            header.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                            header.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThick;
                            header.AutoFilter(1, Type.Missing, XlAutoFilterOperator.xlAnd, Type.Missing, true);
                            xlResultSheet.Activate();
                            xlResultSheet.Application.ActiveWindow.SplitRow = 1;
                            try { xlResultSheet.Application.ActiveWindow.FreezePanes = true; } catch { }
                            xlResultSheet.Columns.AutoFit();
                        }
                    }
                }
                xlResultSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlResultSheet.Activate();
                xlResultSheet.Range["A1", "A1"].Select();
            }
            catch (Exception ex)
            {
                //this.ShowErrorDialog(ex, "Open Excel");
            }
            finally
            {
                Cursor = Cursors.Default;
                openInExcelToolStripMenuItem.Enabled = true;
            }

        }
    }
}