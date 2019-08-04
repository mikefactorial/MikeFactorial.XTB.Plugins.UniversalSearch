namespace MikeFactorial.XTB.Plugins
{
    partial class UniversalSearch
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UniversalSearch));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EntitiesListViewControl1 = new Futurez.XrmToolBox.Controls.EntitiesListControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.searchLookupText = new System.Windows.Forms.CheckBox();
            this.searchOptionSetText = new System.Windows.Forms.CheckBox();
            this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchTextToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 537);
            this.splitContainer1.SplitterDistance = 284;
            this.splitContainer1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EntitiesListViewControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 537);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entity";
            // 
            // EntitiesListViewControl1
            // 
            this.EntitiesListViewControl1.AutoLoadData = true;
            this.EntitiesListViewControl1.Checkboxes = true;
            this.EntitiesListViewControl1.ColumnDisplayMode = Futurez.XrmToolBox.Controls.ListViewColumnDisplayMode.Compact;
            this.EntitiesListViewControl1.DisplayToolbar = true;
            this.EntitiesListViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntitiesListViewControl1.EntityTypes = Futurez.XrmToolBox.Controls.EnumEntityTypes.BothCustomAndSystem;
            this.EntitiesListViewControl1.GroupByType = false;
            this.EntitiesListViewControl1.Location = new System.Drawing.Point(3, 16);
            this.EntitiesListViewControl1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.EntitiesListViewControl1.Name = "EntitiesListViewControl1";
            this.EntitiesListViewControl1.ParentBaseControl = this;
            this.EntitiesListViewControl1.RetrieveAsIfPublished = true;
            this.EntitiesListViewControl1.Service = null;
            this.EntitiesListViewControl1.Size = new System.Drawing.Size(278, 518);
            this.EntitiesListViewControl1.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tabControl1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 82);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(762, 455);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Records";
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
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(762, 82);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Criteria (Use asterisks * to perform a wildcard search)";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.searchLookupText);
            this.groupBox5.Controls.Add(this.searchOptionSetText);
            this.groupBox5.Controls.Add(this.matchCaseCheckBox);
            this.groupBox5.Controls.Add(this.btnFind);
            this.groupBox5.Location = new System.Drawing.Point(10, 50);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(740, 32);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            // 
            // searchLookupText
            // 
            this.searchLookupText.AutoSize = true;
            this.searchLookupText.Location = new System.Drawing.Point(373, 10);
            this.searchLookupText.Name = "searchLookupText";
            this.searchLookupText.Size = new System.Drawing.Size(199, 17);
            this.searchLookupText.TabIndex = 10;
            this.searchLookupText.Text = "Search Lookup Text (slowest)";
            this.searchLookupText.UseVisualStyleBackColor = true;
            // 
            // searchOptionSetText
            // 
            this.searchOptionSetText.AutoSize = true;
            this.searchOptionSetText.Location = new System.Drawing.Point(171, 9);
            this.searchOptionSetText.Name = "searchOptionSetText";
            this.searchOptionSetText.Size = new System.Drawing.Size(196, 17);
            this.searchOptionSetText.TabIndex = 9;
            this.searchOptionSetText.Text = "Search Picklist Text (slower)";
            this.searchOptionSetText.UseVisualStyleBackColor = true;
            // 
            // matchCaseCheckBox
            // 
            this.matchCaseCheckBox.AutoSize = true;
            this.matchCaseCheckBox.Location = new System.Drawing.Point(6, 10);
            this.matchCaseCheckBox.Name = "matchCaseCheckBox";
            this.matchCaseCheckBox.Size = new System.Drawing.Size(159, 17);
            this.matchCaseCheckBox.TabIndex = 8;
            this.matchCaseCheckBox.Text = "Match Case (slow)";
            this.matchCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(666, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(71, 23);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Search";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.searchTextBox);
            this.groupBox2.Location = new System.Drawing.Point(10, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 43);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTextBox.Location = new System.Drawing.Point(3, 16);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(734, 20);
            this.searchTextBox.TabIndex = 5;
            this.searchTextBox.KeyUp += SearchTextBox_KeyUp;
            this.searchTextToolTip.SetToolTip(this.searchTextBox, "Enter the value to search. Use wildcard (*) to perform a wildcard search.");
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Futurez.XrmToolBox.Controls.EntitiesListControl EntitiesListViewControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip searchTextToolTip;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox matchCaseCheckBox;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.CheckBox searchLookupText;
        private System.Windows.Forms.CheckBox searchOptionSetText;
    }
}
