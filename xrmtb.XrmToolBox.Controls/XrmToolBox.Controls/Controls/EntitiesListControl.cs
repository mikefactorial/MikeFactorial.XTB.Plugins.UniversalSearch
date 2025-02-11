﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace xrmtb.XrmToolBox.Controls
{
    /// <summary>
    /// Shared XrmToolBox Control that will load a list of entities into a ListView control
    /// </summary>
    public partial class EntitiesListControl : FilteredListViewBaseControl
    {
        #region Private items
        private ConfigurationInfo _config = null;
        private string _solutionFilter = null;
        #endregion

        #region Public properties

        /// <summary>
        /// Defines which Entity types should be loaded on retrieve.
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Entity Types")]
        [Description("Defines which Entity types should be loaded on retrieve.")]
        public EnumEntityTypes EntityTypes
        {
            get { return _config.EntityTypes; }
            set { _config.EntityTypes = value; }
        }
        
        /// <summary>
        /// Defines which Entity types should be loaded on retrieve.
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Display Solution Dropdown")]
        [Description("Flag indicating whether to display the Solution Filter Dropdown.")]
        public bool DisplaySolutionDropdown
        {
            get => solutionsDropdown.Visible;
            set {
                solutionsDropdown.Visible = value;

                if (!solutionsDropdown.Visible)
                {
                    solutionsDropdown.ClearData();
                    SolutionFilter = null;
                }
                else {
                    solutionsDropdown.UpdateConnection(Service);
                    if (Service != null) {
                        solutionsDropdown.LoadData();
                    }
                }
            }
        }

        /// <summary>
        /// Defines which Entity types should be loaded on retrieve.
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Solution Filter")]
        [Description("Specifies a Solution Unique Name filter to be used when retrieving Entities.")]
        public string SolutionFilter {
            get 
            {
                if (DisplaySolutionDropdown) {
                    _solutionFilter = solutionsDropdown.SelectedSolution?.Attributes["uniquename"].ToString();
                }
                else {
                    _solutionFilter = null;
                }
                return _solutionFilter;
            }
            set {
                // TODO - update the dropdown!
                _solutionFilter = value;
            }
        }

        /// <summary>
        /// List of Entities to excluded upon retrieval.
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Entity Filters")]
        [Description("List of Entities to excluded upon retrieval.")]
        [Browsable(false)]
        public List<FilterInfo> EntityFilters
        {
            get { return _config.EntityFilters; }
            internal set { _config.EntityFilters = value; }
        }

        /// <summary>
        /// List of EntityFilters to be applied on the 
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Entity Request Filters")]
        [Description("List of EntityFilters applied to Entity retrieval. This is the EntityFilter structure passed with the RetrieveAllEntitiesRequest")]
        public List<EntityFilters> EntityRequestFilters
        {
            get { return _config.EntityRequestFilters; }
            internal set { _config.EntityRequestFilters = value; }
        }

        /// <summary>
        /// Flag indicating whether to retrieve the metadata that has not been published
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Retrieve As If Published")]
        [Description("Flag indicating whether to retrieve the metadata that has not been published")]
        public bool RetrieveAsIfPublished
        {
            get { return _config.RetrieveAsIfPublished; }
            set { _config.RetrieveAsIfPublished = value; }
        }
        #endregion

        #region Runtime Properties
        /// <summary>
        /// List of all checked EntityMetadata objects in the ListView
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Checked Entities List")]
        [Description("List of all Entities that are checked in the control.")]
        [Browsable(false)]
        public List<EntityMetadata> CheckedEntities { get => CheckedItems?.Select(i => i as EntityMetadata).ToList<EntityMetadata>(); }

        /// <summary>
        /// The currently selected EntityMetadata object in the ListView
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Selected Entity")]
        [Description("The Entity that is currently selected the control.")]
        [Browsable(false)]
        public EntityMetadata SelectedEntity { get => SelectedItem as EntityMetadata; }

        /// <summary>
        /// List of all loaded EntityMetadata objects for the current connection
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("All Entities List")]
        [Description("List of all Entities that have been loaded.")]
        [Browsable(false)]
        public List<EntityMetadata> AllEntities
        {
            get => AllItems?.ConvertAll<EntityMetadata>(new Converter<object, EntityMetadata>((item) => { return item as EntityMetadata; }));
        }

        /// <summary>
        /// Set up the ListViewColumnDef defaults
        /// </summary>
        protected override void ResetListViewColDefs()
        {
            // default the col defs for the 
            ListViewColDefs = new ListViewColumnDef[] {
                new ListViewColumnDef("DisplayName", 1, "Display Name") { Width = 250 },
                new ListViewColumnDef("LogicalName", 2, "Logical Name") { IsFilterColumn = true },
                new ListViewColumnDef( "SchemaName", 0, "Schema Name") { Width = 150 },
                new ListViewColumnDef( "IsManaged", 4, "Managed State") { IsGroupColumn = true },
                new ListViewColumnDef("Description", 3) { Width = 250 }
            };
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public EntitiesListControl() : base()
        {
            InitializeComponent();

            // set up some default values and uI state
            _config = new ConfigurationInfo();

            // init the col defs items
            ResetListViewColDefs();
        }

        #region Public methods

        #region IXrmToolBoxControl methods

        /// <summary>
        ///  Override to account for solution dropdown
        /// </summary>
        protected override void ToggleMainControlsEnabled()
        {
            base.ToggleMainControlsEnabled();
            solutionsDropdown.Enabled = buttonELVBaseLoadItems.Enabled;
        }

        /// <summary>
        ///  Override to account for solution dropdown 
        /// </summary>
        /// <param name="newService">New service reference</param>
        public override void UpdateConnection(IOrganizationService newService)
        {
            base.UpdateConnection(newService);

            if (DisplaySolutionDropdown) {
                solutionsDropdown.UpdateConnection(newService);
            }
        }
        /// <summary>
        /// Load the Attributes using the current IOrganizationService.
        /// The call is asynchronous and will leverage the WorkAsync object on the parent XrmToolBox control
        /// </summary>
        public override void LoadData()
        {
            // this is for developers!  If LoadData is called from within WorkAsync, the controls will be 
            // inaccessible because of the worker thread
            if (this.InvokeRequired)
                throw new InvalidOperationException("This method cannot be invoked from WorkAsync");

            LoadData(true);
        }

        /// <summary>
        /// Private method that will rethrow an Exception if specified in the parameter.
        /// This is meant to allow for external programmatic calls to load vs those from the built in controls
        /// </summary>
        /// <param name="throwException">Flag indicating whether to rethrow a captured exception</param>
        protected override void LoadData(bool throwException)
        {
            if (Service == null)
            {
                var ex = new InvalidOperationException("The Service reference must be set before loading the Entities list");

                // raise the error event and if set, throw error
                OnNotificationMessage(ex.Message, MessageLevel.Exception, ex);

                if (throwException) {
                    throw ex;
                }
                return;
            }
            try
            {
                OnProgressChanged(0, "Loading Entities from CRM");

                OnBeginLoadData();

                ToggleMainControlsEnabled(false);

                // first clear out all data currently loaded
                this.ClearData();

                var worker = new BackgroundWorker();

                worker.DoWork += (w, e) => {

                    var entities = new List<EntityMetadata>();

                    if (SolutionFilter != null)
                    {
                        entities = CrmActions.RetrieveEntitiesForSolution(Service, SolutionFilter);
                    }
                    else
                    {
                        entities = CrmActions.RetrieveAllEntities(Service, _config);
                    }
                    e.Result = entities;
                };

                worker.RunWorkerCompleted += (s, e) =>
                {
                    var entities = e.Result as List<EntityMetadata>;

                    // reset the list of all entities 
                    var allEntities = new List<EntityMetadata>();

                    double counter = 0;
                    double total = entities.Count;

                    OnProgressChanged(1, $"{entities.Count} Entities loaded");

                    foreach (var entity in entities)
                    {
                        counter++;
                        if (counter % 50 == 0) {
                            OnProgressChanged((int)(100 * counter / total), "Loading Entities ...");
                        }
                        // filter based on configuration settings
                        if (_config.FilterEntity(entity.LogicalName)) {
                            continue;
                        }
                        // see if we are filtering by system and custom
                        else if (_config.EntityTypes != EnumEntityTypes.BothCustomAndSystem)
                        {
                            if ((_config.EntityTypes == EnumEntityTypes.Custom) && (!entity.IsCustomEntity.Value)) {
                                continue;
                            }
                            else if ((_config.EntityTypes == EnumEntityTypes.System) && (entity.IsCustomEntity.Value)) {
                                continue;
                            }
                        }

                        allEntities.Add(entity);
                    }

                    // now that the entities are loaded, populate the list view.
                    SetAllItems<EntityMetadata>(allEntities);

                    ToggleMainControlsEnabled();

                    OnProgressChanged(100, "Loading Entities from CRM Complete!");

                    base.LoadData();
                };

                // kick off the worker thread!
                worker.RunWorkerAsync();
            }
            catch (System.ServiceModel.FaultException ex)
            {
                OnNotificationMessage($"An error occured attetmpting to load the list of Entities", MessageLevel.Exception, ex);

                if (throwException) {
                    throw ex;
                }
            }
        }
        #endregion
        #endregion
    }
}
