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
            this.metadataSearchRadio = new System.Windows.Forms.RadioButton();
            this.recordSearchRadio = new System.Windows.Forms.RadioButton();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.metadataSearchGroup = new System.Windows.Forms.GroupBox();
            this.matchCaseMetadata = new System.Windows.Forms.CheckBox();
            this.searchRelationships = new System.Windows.Forms.CheckBox();
            this.searchFormsViews = new System.Windows.Forms.CheckBox();
            this.searchAttributes = new System.Windows.Forms.CheckBox();
            this.searchEntities = new System.Windows.Forms.CheckBox();
            this.btnFindMetadata = new System.Windows.Forms.Button();
            this.recordSearchGroup = new System.Windows.Forms.GroupBox();
            this.searchLookupText = new System.Windows.Forms.CheckBox();
            this.searchOptionSetText = new System.Windows.Forms.CheckBox();
            this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.searchTextToolTip = new System.Windows.Forms.ToolTip(this.components);
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
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer1.Size = new System.Drawing.Size(1050, 537);
            this.splitContainer1.SplitterDistance = 284;
            this.splitContainer1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EntitiesListView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 537);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entity";
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
            this.EntitiesListView.Location = new System.Drawing.Point(3, 16);
            this.EntitiesListView.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.EntitiesListView.Name = "EntitiesListView";
            this.EntitiesListView.RetrieveAsIfPublished = true;
            this.EntitiesListView.Service = null;
            this.EntitiesListView.Size = new System.Drawing.Size(278, 518);
            this.EntitiesListView.SolutionFilter = null;
            this.EntitiesListView.TabIndex = 1;
            // 
            // resultsGroup
            // 
            this.resultsGroup.Controls.Add(this.tabControl1);
            this.resultsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsGroup.Location = new System.Drawing.Point(0, 82);
            this.resultsGroup.Name = "resultsGroup";
            this.resultsGroup.Size = new System.Drawing.Size(762, 455);
            this.resultsGroup.TabIndex = 3;
            this.resultsGroup.TabStop = false;
            this.resultsGroup.Text = "Records";
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(756, 436);
            this.tabControl1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.metadataSearchGroup);
            this.groupBox3.Controls.Add(this.recordSearchGroup);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(762, 82);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Criteria (Use asterisks * to perform a wildcard search)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.metadataSearchRadio);
            this.groupBox2.Controls.Add(this.recordSearchRadio);
            this.groupBox2.Controls.Add(this.searchTextBox);
            this.groupBox2.Location = new System.Drawing.Point(10, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 43);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // metadataSearchRadio
            // 
            this.metadataSearchRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metadataSearchRadio.AutoSize = true;
            this.metadataSearchRadio.Location = new System.Drawing.Point(630, 17);
            this.metadataSearchRadio.Name = "metadataSearchRadio";
            this.metadataSearchRadio.Size = new System.Drawing.Size(107, 17);
            this.metadataSearchRadio.TabIndex = 7;
            this.metadataSearchRadio.Text = "Metadata Search";
            this.metadataSearchRadio.UseVisualStyleBackColor = true;
            this.metadataSearchRadio.CheckedChanged += new System.EventHandler(this.metadataSearchRadio_CheckedChanged);
            // 
            // recordSearchRadio
            // 
            this.recordSearchRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.recordSearchRadio.AutoSize = true;
            this.recordSearchRadio.Checked = true;
            this.recordSearchRadio.Location = new System.Drawing.Point(533, 17);
            this.recordSearchRadio.Name = "recordSearchRadio";
            this.recordSearchRadio.Size = new System.Drawing.Size(97, 17);
            this.recordSearchRadio.TabIndex = 6;
            this.recordSearchRadio.TabStop = true;
            this.recordSearchRadio.Text = "Record Search";
            this.recordSearchRadio.UseVisualStyleBackColor = true;
            this.recordSearchRadio.CheckedChanged += new System.EventHandler(this.recordSearchRadio_CheckedChanged);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(3, 16);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(516, 20);
            this.searchTextBox.TabIndex = 5;
            this.searchTextToolTip.SetToolTip(this.searchTextBox, "Enter the value to search. Use wildcard (*) to perform a wildcard search.");
            // 
            // metadataSearchGroup
            // 
            this.metadataSearchGroup.Controls.Add(this.matchCaseMetadata);
            this.metadataSearchGroup.Controls.Add(this.searchRelationships);
            this.metadataSearchGroup.Controls.Add(this.searchFormsViews);
            this.metadataSearchGroup.Controls.Add(this.searchAttributes);
            this.metadataSearchGroup.Controls.Add(this.searchEntities);
            this.metadataSearchGroup.Controls.Add(this.btnFindMetadata);
            this.metadataSearchGroup.Location = new System.Drawing.Point(9, 50);
            this.metadataSearchGroup.Name = "metadataSearchGroup";
            this.metadataSearchGroup.Size = new System.Drawing.Size(740, 32);
            this.metadataSearchGroup.TabIndex = 11;
            this.metadataSearchGroup.TabStop = false;
            this.metadataSearchGroup.Visible = false;
            // 
            // matchCaseMetadata
            // 
            this.matchCaseMetadata.AutoSize = true;
            this.matchCaseMetadata.Location = new System.Drawing.Point(7, 9);
            this.matchCaseMetadata.Name = "matchCaseMetadata";
            this.matchCaseMetadata.Size = new System.Drawing.Size(83, 17);
            this.matchCaseMetadata.TabIndex = 12;
            this.matchCaseMetadata.Text = "Match Case";
            this.matchCaseMetadata.UseVisualStyleBackColor = true;
            // 
            // searchRelationships
            // 
            this.searchRelationships.AutoSize = true;
            this.searchRelationships.Checked = true;
            this.searchRelationships.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchRelationships.Location = new System.Drawing.Point(326, 10);
            this.searchRelationships.Name = "searchRelationships";
            this.searchRelationships.Size = new System.Drawing.Size(126, 17);
            this.searchRelationships.TabIndex = 11;
            this.searchRelationships.Text = "Search Relationships";
            this.searchRelationships.UseVisualStyleBackColor = true;
            // 
            // searchFormsViews
            // 
            this.searchFormsViews.AutoSize = true;
            this.searchFormsViews.Checked = true;
            this.searchFormsViews.Location = new System.Drawing.Point(463, 10);
            this.searchFormsViews.Name = "searchFormsViews";
            this.searchFormsViews.Size = new System.Drawing.Size(143, 17);
            this.searchFormsViews.TabIndex = 10;
            this.searchFormsViews.Text = "Search Forms and Views";
            this.searchFormsViews.UseVisualStyleBackColor = true;
            // 
            // searchAttributes
            // 
            this.searchAttributes.AutoSize = true;
            this.searchAttributes.Checked = true;
            this.searchAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchAttributes.Location = new System.Drawing.Point(207, 9);
            this.searchAttributes.Name = "searchAttributes";
            this.searchAttributes.Size = new System.Drawing.Size(107, 17);
            this.searchAttributes.TabIndex = 9;
            this.searchAttributes.Text = "Search Attributes";
            this.searchAttributes.UseVisualStyleBackColor = true;
            // 
            // searchEntities
            // 
            this.searchEntities.AutoSize = true;
            this.searchEntities.Checked = true;
            this.searchEntities.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchEntities.Location = new System.Drawing.Point(102, 10);
            this.searchEntities.Name = "searchEntities";
            this.searchEntities.Size = new System.Drawing.Size(95, 17);
            this.searchEntities.TabIndex = 8;
            this.searchEntities.Text = "Search Entites";
            this.searchEntities.UseVisualStyleBackColor = true;
            // 
            // btnFindMetadata
            // 
            this.btnFindMetadata.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFindMetadata.Location = new System.Drawing.Point(618, 8);
            this.btnFindMetadata.Name = "btnFindMetadata";
            this.btnFindMetadata.Size = new System.Drawing.Size(119, 23);
            this.btnFindMetadata.TabIndex = 7;
            this.btnFindMetadata.Text = "Search Metadata";
            this.btnFindMetadata.UseVisualStyleBackColor = true;
            this.btnFindMetadata.Click += new System.EventHandler(this.btnFindMetadata_Click);
            // 
            // recordSearchGroup
            // 
            this.recordSearchGroup.Controls.Add(this.searchLookupText);
            this.recordSearchGroup.Controls.Add(this.searchOptionSetText);
            this.recordSearchGroup.Controls.Add(this.matchCaseCheckBox);
            this.recordSearchGroup.Controls.Add(this.btnFind);
            this.recordSearchGroup.Location = new System.Drawing.Point(10, 50);
            this.recordSearchGroup.Name = "recordSearchGroup";
            this.recordSearchGroup.Size = new System.Drawing.Size(740, 32);
            this.recordSearchGroup.TabIndex = 7;
            this.recordSearchGroup.TabStop = false;
            // 
            // searchLookupText
            // 
            this.searchLookupText.AutoSize = true;
            this.searchLookupText.Location = new System.Drawing.Point(373, 10);
            this.searchLookupText.Name = "searchLookupText";
            this.searchLookupText.Size = new System.Drawing.Size(167, 17);
            this.searchLookupText.TabIndex = 10;
            this.searchLookupText.Text = "Search Lookup Text (slowest)";
            this.searchLookupText.UseVisualStyleBackColor = true;
            // 
            // searchOptionSetText
            // 
            this.searchOptionSetText.AutoSize = true;
            this.searchOptionSetText.Location = new System.Drawing.Point(171, 9);
            this.searchOptionSetText.Name = "searchOptionSetText";
            this.searchOptionSetText.Size = new System.Drawing.Size(159, 17);
            this.searchOptionSetText.TabIndex = 9;
            this.searchOptionSetText.Text = "Search Picklist Text (slower)";
            this.searchOptionSetText.UseVisualStyleBackColor = true;
            // 
            // matchCaseCheckBox
            // 
            this.matchCaseCheckBox.AutoSize = true;
            this.matchCaseCheckBox.Location = new System.Drawing.Point(6, 10);
            this.matchCaseCheckBox.Name = "matchCaseCheckBox";
            this.matchCaseCheckBox.Size = new System.Drawing.Size(113, 17);
            this.matchCaseCheckBox.TabIndex = 8;
            this.matchCaseCheckBox.Text = "Match Case (slow)";
            this.matchCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(618, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(119, 23);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Search Records";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // searchTextToolTip
            // 
            this.searchTextToolTip.AutoPopDelay = 1000;
            this.searchTextToolTip.InitialDelay = 500;
            this.searchTextToolTip.ReshowDelay = 100;
            // 
            // UniversalSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UniversalSearch";
            this.PluginIcon = ((System.Drawing.Icon)(resources.GetObject("$this.PluginIcon")));
            this.Size = new System.Drawing.Size(1050, 537);
            this.TabIcon = ((System.Drawing.Image)(resources.GetObject("$this.TabIcon")));
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.resultsGroup.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.metadataSearchGroup.ResumeLayout(false);
            this.metadataSearchGroup.PerformLayout();
            this.recordSearchGroup.ResumeLayout(false);
            this.recordSearchGroup.PerformLayout();
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
        private System.Windows.Forms.GroupBox recordSearchGroup;
        private System.Windows.Forms.CheckBox matchCaseCheckBox;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.CheckBox searchLookupText;
        private System.Windows.Forms.CheckBox searchOptionSetText;
        private System.Windows.Forms.RadioButton metadataSearchRadio;
        private System.Windows.Forms.RadioButton recordSearchRadio;
        private System.Windows.Forms.GroupBox metadataSearchGroup;
        private System.Windows.Forms.CheckBox searchFormsViews;
        private System.Windows.Forms.CheckBox searchAttributes;
        private System.Windows.Forms.CheckBox searchEntities;
        private System.Windows.Forms.Button btnFindMetadata;
        private System.Windows.Forms.CheckBox searchRelationships;
        private System.Windows.Forms.CheckBox matchCaseMetadata;
    }
}
