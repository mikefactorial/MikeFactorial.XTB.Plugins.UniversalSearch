using System.Windows.Forms;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    partial class UniversalSearch
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UniversalSearch));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EntitiesListView = new xrmtb.XrmToolBox.Controls.EntitiesListControl();
            this.resultsGroup = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchLocationList = new System.Windows.Forms.ComboBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.metadataSearchGroup = new System.Windows.Forms.GroupBox();
            this.matchCaseMetadata = new System.Windows.Forms.CheckBox();
            this.searchRelationships = new System.Windows.Forms.CheckBox();
            this.searchFormsViews = new System.Windows.Forms.CheckBox();
            this.searchAttributes = new System.Windows.Forms.CheckBox();
            this.searchEntities = new System.Windows.Forms.CheckBox();
            this.recordSearchGroup = new System.Windows.Forms.GroupBox();
            this.searchLookupText = new System.Windows.Forms.CheckBox();
            this.searchOptionSetText = new System.Windows.Forms.CheckBox();
            this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.solutionSearchGroup = new System.Windows.Forms.GroupBox();
            this.solutionMatchCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.alwaysGetLatestSolutionCheckBox = new System.Windows.Forms.CheckBox();
            this.searchTextToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openInExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.resultsGroup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.metadataSearchGroup.SuspendLayout();
            this.recordSearchGroup.SuspendLayout();
            this.solutionSearchGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resultsGroup);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1400, 661);
            this.splitContainer1.SplitterDistance = 378;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EntitiesListView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(378, 661);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entities";
            // 
            // EntitiesListView
            // 
            this.EntitiesListView.AutoLoadData = true;
            this.EntitiesListView.AutosizeColumns = System.Windows.Forms.ColumnHeaderAutoResizeStyle.None;
            this.EntitiesListView.Checkboxes = true;
            this.EntitiesListView.DisplaySolutionDropdown = true;
            this.EntitiesListView.DisplayToolbar = true;
            this.EntitiesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntitiesListView.EntityTypes = xrmtb.XrmToolBox.Controls.EnumEntityTypes.BothCustomAndSystem;
            this.EntitiesListView.LanguageCode = 1033;
            this.EntitiesListView.ListViewColDefs = new xrmtb.XrmToolBox.Controls.ListViewColumnDef[] {
        ((xrmtb.XrmToolBox.Controls.ListViewColumnDef)(resources.GetObject("EntitiesListView.ListViewColDefs"))),
        ((xrmtb.XrmToolBox.Controls.ListViewColumnDef)(resources.GetObject("EntitiesListView.ListViewColDefs1"))),
        ((xrmtb.XrmToolBox.Controls.ListViewColumnDef)(resources.GetObject("EntitiesListView.ListViewColDefs2"))),
        ((xrmtb.XrmToolBox.Controls.ListViewColumnDef)(resources.GetObject("EntitiesListView.ListViewColDefs3"))),
        ((xrmtb.XrmToolBox.Controls.ListViewColumnDef)(resources.GetObject("EntitiesListView.ListViewColDefs4")))};
            this.EntitiesListView.Location = new System.Drawing.Point(4, 19);
            this.EntitiesListView.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.EntitiesListView.Name = "EntitiesListView";
            this.EntitiesListView.RetrieveAsIfPublished = true;
            this.EntitiesListView.Service = null;
            this.EntitiesListView.Size = new System.Drawing.Size(370, 638);
            this.EntitiesListView.SolutionFilter = null;
            this.EntitiesListView.TabIndex = 1;
            // 
            // resultsGroup
            // 
            this.resultsGroup.Controls.Add(this.tabControl1);
            this.resultsGroup.Controls.Add(this.menuStrip1);
            this.resultsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsGroup.Location = new System.Drawing.Point(0, 101);
            this.resultsGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resultsGroup.Name = "resultsGroup";
            this.resultsGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resultsGroup.Size = new System.Drawing.Size(1017, 560);
            this.resultsGroup.TabIndex = 3;
            this.resultsGroup.TabStop = false;
            this.resultsGroup.Text = "Records";
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 43);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1009, 513);
            this.tabControl1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.metadataSearchGroup);
            this.groupBox3.Controls.Add(this.recordSearchGroup);
            this.groupBox3.Controls.Add(this.solutionSearchGroup);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(1017, 101);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Criteria (Use asterisks * to perform a wildcard search)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.searchTextBox);
            this.groupBox2.Controls.Add(this.searchLocationList);
            this.groupBox2.Controls.Add(this.btnFind);
            this.groupBox2.Location = new System.Drawing.Point(13, 17);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(987, 53);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(4, 20);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(687, 22);
            this.searchTextBox.TabIndex = 5;
            this.searchTextToolTip.SetToolTip(this.searchTextBox, "Enter the value to search. Use wildcard (*) to perform a wildcard search.");
            // 
            // searchLocationList
            // 
            this.searchLocationList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchLocationList.DropDownWidth = 250;
            this.searchLocationList.FormattingEnabled = true;
            this.searchLocationList.Items.AddRange(new object[] {
            "Records",
            "Metadata",
            "Solution"});
            this.searchLocationList.Location = new System.Drawing.Point(697, 20);
            this.searchLocationList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchLocationList.Name = "searchLocationList";
            this.searchLocationList.Size = new System.Drawing.Size(132, 24);
            this.searchLocationList.TabIndex = 8;
            // 
            // btnFind
            // 
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(840, 20);
            this.btnFind.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(133, 26);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Search Records";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // metadataSearchGroup
            // 
            this.metadataSearchGroup.Controls.Add(this.matchCaseMetadata);
            this.metadataSearchGroup.Controls.Add(this.searchRelationships);
            this.metadataSearchGroup.Controls.Add(this.searchFormsViews);
            this.metadataSearchGroup.Controls.Add(this.searchAttributes);
            this.metadataSearchGroup.Controls.Add(this.searchEntities);
            this.metadataSearchGroup.Location = new System.Drawing.Point(12, 62);
            this.metadataSearchGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metadataSearchGroup.Name = "metadataSearchGroup";
            this.metadataSearchGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metadataSearchGroup.Size = new System.Drawing.Size(987, 39);
            this.metadataSearchGroup.TabIndex = 11;
            this.metadataSearchGroup.TabStop = false;
            this.metadataSearchGroup.Visible = false;
            // 
            // matchCaseMetadata
            // 
            this.matchCaseMetadata.AutoSize = true;
            this.matchCaseMetadata.Location = new System.Drawing.Point(9, 11);
            this.matchCaseMetadata.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.matchCaseMetadata.Name = "matchCaseMetadata";
            this.matchCaseMetadata.Size = new System.Drawing.Size(98, 20);
            this.matchCaseMetadata.TabIndex = 12;
            this.matchCaseMetadata.Text = "Match Case";
            this.matchCaseMetadata.UseVisualStyleBackColor = true;
            // 
            // searchRelationships
            // 
            this.searchRelationships.AutoSize = true;
            this.searchRelationships.Checked = true;
            this.searchRelationships.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchRelationships.Location = new System.Drawing.Point(435, 12);
            this.searchRelationships.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchRelationships.Name = "searchRelationships";
            this.searchRelationships.Size = new System.Drawing.Size(155, 20);
            this.searchRelationships.TabIndex = 11;
            this.searchRelationships.Text = "Search Relationships";
            this.searchRelationships.UseVisualStyleBackColor = true;
            // 
            // searchFormsViews
            // 
            this.searchFormsViews.AutoSize = true;
            this.searchFormsViews.Checked = true;
            this.searchFormsViews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchFormsViews.Location = new System.Drawing.Point(617, 12);
            this.searchFormsViews.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchFormsViews.Name = "searchFormsViews";
            this.searchFormsViews.Size = new System.Drawing.Size(176, 20);
            this.searchFormsViews.TabIndex = 10;
            this.searchFormsViews.Text = "Search Forms and Views";
            this.searchFormsViews.UseVisualStyleBackColor = true;
            // 
            // searchAttributes
            // 
            this.searchAttributes.AutoSize = true;
            this.searchAttributes.Checked = true;
            this.searchAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchAttributes.Location = new System.Drawing.Point(276, 11);
            this.searchAttributes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchAttributes.Name = "searchAttributes";
            this.searchAttributes.Size = new System.Drawing.Size(128, 20);
            this.searchAttributes.TabIndex = 9;
            this.searchAttributes.Text = "Search Attributes";
            this.searchAttributes.UseVisualStyleBackColor = true;
            // 
            // searchEntities
            // 
            this.searchEntities.AutoSize = true;
            this.searchEntities.Checked = true;
            this.searchEntities.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchEntities.Location = new System.Drawing.Point(136, 12);
            this.searchEntities.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchEntities.Name = "searchEntities";
            this.searchEntities.Size = new System.Drawing.Size(113, 20);
            this.searchEntities.TabIndex = 8;
            this.searchEntities.Text = "Search Entites";
            this.searchEntities.UseVisualStyleBackColor = true;
            // 
            // recordSearchGroup
            // 
            this.recordSearchGroup.Controls.Add(this.searchLookupText);
            this.recordSearchGroup.Controls.Add(this.searchOptionSetText);
            this.recordSearchGroup.Controls.Add(this.matchCaseCheckBox);
            this.recordSearchGroup.Location = new System.Drawing.Point(13, 62);
            this.recordSearchGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.recordSearchGroup.Name = "recordSearchGroup";
            this.recordSearchGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.recordSearchGroup.Size = new System.Drawing.Size(987, 39);
            this.recordSearchGroup.TabIndex = 7;
            this.recordSearchGroup.TabStop = false;
            // 
            // searchLookupText
            // 
            this.searchLookupText.AutoSize = true;
            this.searchLookupText.Location = new System.Drawing.Point(497, 12);
            this.searchLookupText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchLookupText.Name = "searchLookupText";
            this.searchLookupText.Size = new System.Drawing.Size(203, 20);
            this.searchLookupText.TabIndex = 10;
            this.searchLookupText.Text = "Search Lookup Text (slowest)";
            this.searchLookupText.UseVisualStyleBackColor = true;
            // 
            // searchOptionSetText
            // 
            this.searchOptionSetText.AutoSize = true;
            this.searchOptionSetText.Location = new System.Drawing.Point(228, 11);
            this.searchOptionSetText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchOptionSetText.Name = "searchOptionSetText";
            this.searchOptionSetText.Size = new System.Drawing.Size(194, 20);
            this.searchOptionSetText.TabIndex = 9;
            this.searchOptionSetText.Text = "Search Picklist Text (slower)";
            this.searchOptionSetText.UseVisualStyleBackColor = true;
            // 
            // matchCaseCheckBox
            // 
            this.matchCaseCheckBox.AutoSize = true;
            this.matchCaseCheckBox.Location = new System.Drawing.Point(8, 12);
            this.matchCaseCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.matchCaseCheckBox.Name = "matchCaseCheckBox";
            this.matchCaseCheckBox.Size = new System.Drawing.Size(136, 20);
            this.matchCaseCheckBox.TabIndex = 8;
            this.matchCaseCheckBox.Text = "Match Case (slow)";
            this.matchCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // solutionSearchGroup
            // 
            this.solutionSearchGroup.Controls.Add(this.solutionMatchCaseCheckBox);
            this.solutionSearchGroup.Controls.Add(this.alwaysGetLatestSolutionCheckBox);
            this.solutionSearchGroup.Location = new System.Drawing.Point(12, 62);
            this.solutionSearchGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.solutionSearchGroup.Name = "solutionSearchGroup";
            this.solutionSearchGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.solutionSearchGroup.Size = new System.Drawing.Size(987, 39);
            this.solutionSearchGroup.TabIndex = 11;
            this.solutionSearchGroup.TabStop = false;
            this.solutionSearchGroup.Visible = false;
            // 
            // solutionMatchCaseCheckBox
            // 
            this.solutionMatchCaseCheckBox.AutoSize = true;
            this.solutionMatchCaseCheckBox.Location = new System.Drawing.Point(8, 12);
            this.solutionMatchCaseCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.solutionMatchCaseCheckBox.Name = "solutionMatchCaseCheckBox";
            this.solutionMatchCaseCheckBox.Size = new System.Drawing.Size(98, 20);
            this.solutionMatchCaseCheckBox.TabIndex = 8;
            this.solutionMatchCaseCheckBox.Text = "Match Case";
            this.solutionMatchCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // alwaysGetLatestSolutionCheckBox
            // 
            this.alwaysGetLatestSolutionCheckBox.AutoSize = true;
            this.alwaysGetLatestSolutionCheckBox.Checked = true;
            this.alwaysGetLatestSolutionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alwaysGetLatestSolutionCheckBox.Location = new System.Drawing.Point(133, 12);
            this.alwaysGetLatestSolutionCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.alwaysGetLatestSolutionCheckBox.Name = "alwaysGetLatestSolutionCheckBox";
            this.alwaysGetLatestSolutionCheckBox.Size = new System.Drawing.Size(155, 20);
            this.alwaysGetLatestSolutionCheckBox.TabIndex = 8;
            this.alwaysGetLatestSolutionCheckBox.Text = "Export Latest Solution";
            this.alwaysGetLatestSolutionCheckBox.UseVisualStyleBackColor = true;
            // 
            // searchTextToolTip
            // 
            this.searchTextToolTip.AutoPopDelay = 1000;
            this.searchTextToolTip.InitialDelay = 500;
            this.searchTextToolTip.ReshowDelay = 100;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInExcelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(4, 19);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1009, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openInExcelToolStripMenuItem
            // 
            this.openInExcelToolStripMenuItem.Enabled = false;
            this.openInExcelToolStripMenuItem.Image = global::MikeFactorial.XTB.Plugins.UniversalSearch.Properties.Resources.icons8_excel_24;
            this.openInExcelToolStripMenuItem.Name = "openInExcelToolStripMenuItem";
            this.openInExcelToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.openInExcelToolStripMenuItem.Text = "Open in Excel";
            this.openInExcelToolStripMenuItem.Click += new System.EventHandler(this.openInExcelToolStripMenuItem_Click);
            // 
            // UniversalSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UniversalSearch";
            this.PluginIcon = ((System.Drawing.Icon)(resources.GetObject("$this.PluginIcon")));
            this.Size = new System.Drawing.Size(1400, 661);
            this.TabIcon = ((System.Drawing.Image)(resources.GetObject("$this.TabIcon")));
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.resultsGroup.ResumeLayout(false);
            this.resultsGroup.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.metadataSearchGroup.ResumeLayout(false);
            this.metadataSearchGroup.PerformLayout();
            this.recordSearchGroup.ResumeLayout(false);
            this.recordSearchGroup.PerformLayout();
            this.solutionSearchGroup.ResumeLayout(false);
            this.solutionSearchGroup.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.SplitContainer splitContainer1;
        private xrmtb.XrmToolBox.Controls.EntitiesListControl EntitiesListView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox resultsGroup;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip searchTextToolTip;
        private System.Windows.Forms.ComboBox searchLocationList;
        private System.Windows.Forms.GroupBox recordSearchGroup;
        private System.Windows.Forms.CheckBox matchCaseCheckBox;
        private System.Windows.Forms.CheckBox solutionMatchCaseCheckBox;
        private System.Windows.Forms.CheckBox alwaysGetLatestSolutionCheckBox;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.CheckBox searchLookupText;
        private System.Windows.Forms.CheckBox searchOptionSetText;
        private System.Windows.Forms.GroupBox solutionSearchGroup;
        private System.Windows.Forms.GroupBox metadataSearchGroup;
        private System.Windows.Forms.CheckBox searchFormsViews;
        private System.Windows.Forms.CheckBox searchAttributes;
        private System.Windows.Forms.CheckBox searchEntities;
        private System.Windows.Forms.CheckBox searchRelationships;
        private System.Windows.Forms.CheckBox matchCaseMetadata;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem openInExcelToolStripMenuItem;
    }
}
