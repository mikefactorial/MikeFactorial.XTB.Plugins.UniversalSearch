﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

using McTools.Xrm.Connection;
using XrmToolBox.Extensibility;
using xrmtb.XrmToolBox.Controls;
using Subro.Controls;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System.Threading;

namespace Sample.XrmToolBox.TestPlugin
{
    public partial class ControlTesterPluginControl : PluginControlBase
    {
        public ControlTesterPluginControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On Load of the plugin control, perform some initialization of the Controls 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_Load(object sender, EventArgs args)
        {
            int width = (int)ClientSize.Width / 3;
            splitterAttribDropdown.SplitterDistance =
                splitterAttribList.SplitterDistance =
                splitterEntDropdown.SplitterDistance =
                splitterEntityList.SplitterDistance =
                splitterSolnDropdown.SplitterDistance =
                splitterViewDropdown.SplitterDistance =
                splitterGlobalOptsList.SplitterDistance =
                splitterCRMGridView.SplitterDistance =
                splitterXmlViewerControl.SplitterDistance =
                splitterXmlViewer.SplitterDistance = width =
                splitterBoundListView.SplitterDistance = width;

            // set up the properties detail
            SetPropertySelectedObject(radioEntListShowProps, propGridEntList, EntityListControl, null);
            SetPropertySelectedObject(radioEntDropdownShowProps, propGridEntDropdown, EntityDropdown, null);
            SetPropertySelectedObject(radioAttribDropdownShowProps, propGridAttribDropdown, AttributeDropdownBase, null);
            SetPropertySelectedObject(radioSolnDropdownShowProps, propGridSolutions, SolutionDropdown, null);
            SetPropertySelectedObject(radioAttribListShowProps, propGridAttrList, AttribListControl, null);
            SetPropertySelectedObject(radioViewDropdownShowProps, propGridViewDropdown, ViewDropdown, null);
            SetPropertySelectedObject(radioGlobalOptsListShowProps, propGridGlobalOptsList, GlobalOptionSetList, null);
            SetPropertySelectedObject(radioCRMGridViewShowProps, propCRMGridView, CrmGridView, null);
            SetPropertySelectedObject(radioEntLVBaseShowProps, propGridEntLVBase, EntityListViewBase, null);


            // set up service references
            UpdateAllServices(Service);

            //var items = new List<FilterInfo>()
            //{
            //    new FilterInfo(){ FilterMatchType=EnumFilterMatchType.RegEx, FilterString="One" },
            //    new FilterInfo(){ FilterMatchType=EnumFilterMatchType.RegEx, FilterString="Two" },
            //    new FilterInfo(){ FilterMatchType=EnumFilterMatchType.RegEx, FilterString="Three" },
            //    new FilterInfo(){ FilterMatchType=EnumFilterMatchType.RegEx, FilterString="Four" },
            //    new FilterInfo(){ FilterMatchType=EnumFilterMatchType.RegEx, FilterString="Five" },
            //    new FilterInfo(){ FilterMatchType=EnumFilterMatchType.RegEx, FilterString="Six" }
            //};
        }

        private void AllControls_NotificationMessage(object sender, NotificationEventArgs e)
        {
            var msg = $"{e.Message}";

            if ((e.Exception != null))
            {
                msg += $": {e.Exception.Message}";
            }

            ShowErrorNotification(msg, null);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        #region Call external methods on control
        private void ToolButtonLoadData_Click(object sender, EventArgs e)
        {
            // do stuff based on active tab
            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageEntList":
                    EntityListControl.LoadData();
                    break;
                case "tabPageEntDropdown":
                    EntityDropdown.LoadData();
                    break;
                case "tabPageAttrList":
                    AttribListControl.ClearData();
                    EntityDropdownAttribList.LoadData();

                    break;
                case "tabPageAttrDropDown":
                    AttributeDropdownBase.ClearData();
                    EntityDropdownAttribs.LoadData();

                    break;
                case "tabPageSolution":
                    SolutionDropdown.LoadData();
                    break;

                case "tabPageViewsDropdown":
                    ViewDropdown.ClearData();
                    EntityDropdownViews.LoadData();
                    break;

                case "tabPageGlobalOptSets":
                    GlobalOptionSetList.LoadData();
                    break;

            }
        }
        private void ToolButtonClearData_Click(object sender, EventArgs e)
        {
            // do stuff based on active tab
            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageEntList":
                    EntityListControl.ClearData();
                    break;
                case "tabPageEntDropdown":
                    EntityDropdown.ClearData();
                    break;
                case "tabPageAttrList":
                    AttribListControl.ClearData();
                    EntityDropdownAttribList.ClearData();

                    break;
                case "tabPageAttrDropDown":
                    AttributeDropdownBase.ClearData();
                    EntityDropdownAttribs.ClearData();

                    break;
                case "tabPageSolution":
                    SolutionDropdown.ClearData();
                    break;
                case "tabPageViewsDropdown":
                    ViewDropdown.ClearData();
                    EntityDropdownViews.ClearData();
                    break;

                case "tabPageGlobalOptSets":
                    GlobalOptionSetList.ClearData();
                    break;
            }
        }

        private void ToolButtonUpdateConnection_Click(object sender, EventArgs e)
        {
            // do stuff based on active tab
            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageEntList":
                    EntityListControl.UpdateConnection(Service);
                    break;
                case "tabPageEntDropdown":
                    EntityDropdown.UpdateConnection(Service);
                    break;
                case "tabPageAttrList":
                    AttribListControl.UpdateConnection(Service);
                    EntityDropdownAttribList.UpdateConnection(Service);

                    break;
                case "tabPageAttrDropDown":
                    AttributeDropdownBase.UpdateConnection(Service);
                    EntityDropdownAttribs.UpdateConnection(Service);
                    break;
                case "tabPageSolution":
                    SolutionDropdown.UpdateConnection(Service);
                    break;
                case "tabPageViewsDropdown":
                    ViewDropdown.UpdateConnection(Service);
                    EntityDropdownViews.UpdateConnection(Service);
                    break;
                case "tabPageGlobalOptSets":
                    GlobalOptionSetList.UpdateConnection(Service);
                    break;
            }
        }
        private void ToolButtonClose_Click(object sender, EventArgs e)
        {
            // do stuff based on active tab
            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageEntList":
                    EntityListControl.Close();
                    break;
                case "tabPageEntDropdown":
                    EntityDropdown.Close();
                    break;
                case "tabPageAttrList":
                    AttribListControl.Close();
                    EntityDropdownAttribList.Close();
                    break;

                case "tabPageAttrDropDown":
                    AttributeDropdownBase.Close();
                    EntityDropdownAttribs.Close();
                    break;
                case "tabPageSolution":
                    SolutionDropdown.Close();
                    break;
                case "tabPageViewsDropdown":
                    ViewDropdown.Close();
                    EntityDropdownViews.Close();
                    break;
                case "tabPageGlobalOptSets":
                    GlobalOptionSetList.Close();
                    break;

            }
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripTextFilter.Clear();
            bool enabled = false;

            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageEntList":
                case "tabPageAttrList":
                    enabled = true;
                    break;
                default:
                    enabled = false;
                    break;
            }
            toolStripLabelFilter.Enabled = enabled;
            toolStripTextFilter.Enabled = enabled;
        }
        #endregion

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            UpdateAllServices(newService);

            LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);

        }

        private void UpdateAllServices(IOrganizationService newService)
        {
            // ENTITIES LIST - update the connection 
            EntityListControl.UpdateConnection(newService);
            EntityDropdown.UpdateConnection(newService);
            AttributeDropdownBase.UpdateConnection(newService);
            EntityDropdownAttribs.UpdateConnection(newService);
            SolutionDropdown.UpdateConnection(newService);

            AttribListControl.UpdateConnection(newService);
            EntityDropdownAttribList.UpdateConnection(newService);

            ViewDropdown.UpdateConnection(newService);
            EntityDropdownViews.UpdateConnection(newService);

            GlobalOptionSetList.UpdateConnection(newService);

            // SolutionDropdownGridView.UpdateConnection(newService);
            CrmGridView.OrganizationService = newService;
            CrmGridViewDesignedCols.OrganizationService = newService;
            cdsDataComboBox.OrganizationService = newService;
            cdsDataTextBox.OrganizationService = newService;
            cdsLookupDialog1.Service = newService;

            EntityListViewBase.UpdateConnection(newService);
            listViewEntCollection.UpdateConnection(newService);

            cdsDataComboRetrieve.OrganizationService = newService;

        }

        #region Entity Listview Control event handlers
        private void EntitiesListControl_CloseComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textEntListLog, $"CloseComplete");
        }
        private void EntitiesListControl_InitializeComplete(object sender, EventArgs e)
        {
            // ENTITIES LIST - log event
            UpdateControlLogger(textEntListLog, $"InitializeComplete");
        }

        private void EntitiesListControl_LoadDataComplete(object sender, EventArgs e)
        {
            // ENTITIES LIST - log event
            UpdateControlLogger(textEntListLog, $"LoadDataComplete - All Entities count: {EntityListControl.AllEntities.Count}");
        }

        private void EntitiesListControl_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateControlLogger(textEntListLog, $"{e.UserState} : {e.ProgressPercentage}");
        }

        private void EntitiesListControl_CheckedItemsChanged(object sender, EventArgs e)
        {
            UpdateControlLogger(textEntListLog, $"CheckedItemsChanged - Checked Entities count: {EntityListControl.CheckedEntities.Count}");

            EntityMetadata ent = null;

            if (EntityListControl.CheckedEntities.Count > 0)
            {
                ent = EntityListControl.CheckedEntities[0];
            }

            SetPropertySelectedObject(radioEntListShowProps, propGridEntList, EntityListControl, ent);
        }
        private void EntitiesListControl_SelectedItemChanged(object sender, EventArgs e)
        {
            var ent = EntityListControl.SelectedEntity;

            UpdateControlLogger(textEntListLog, $"SelectedItemChanged: {ent?.SchemaName}");
            if (ent != null)
            {
                ent = CrmActions.RetrieveEntity(Service, ent.LogicalName, true, new List<EntityFilters>() { EntityFilters.All });
            }
            SetPropertySelectedObject(radioEntListShowProps, propGridEntList, EntityListControl, ent);

        }
        private void EntitiesListControl_ClearDataComplete(object sender, EventArgs e)
        {
            // ENTITIES LIST - log event
            SetPropertySelectedObject(radioEntListShowProps, propGridEntList, EntityListControl, null);

            UpdateControlLogger(textEntListLog, $"ClearDataComplete - All Entities count: {EntityListControl.AllEntities.Count}");
        }

        private void RadioEntitiesList_CheckedChanged(object sender, EventArgs e)
        {
            SetPropertySelectedObject(radioEntListShowProps, propGridEntList, EntityListControl, EntityListControl.SelectedEntity);
        }

        private void EntitiesListControl_BeginLoadData(object sender, EventArgs e)
        {
            if (this.textEntListLog.InvokeRequired)
            {
            }
            else
            {
                textEntListLog.Clear();
            }
        }
        #endregion

        #region Entity Dropdown events

        private void EntityDropdown_SelectedItemChanged(object sender, EventArgs e)
        {
            UpdateControlLogger(textEntDropdownLog, $"SelectedItemChanged: {EntityDropdown.SelectedEntity?.SchemaName}");

            SetPropertySelectedObject(radioEntDropdownShowProps, propGridEntDropdown, EntityDropdown, EntityDropdown.SelectedEntity);
        }

        private void RadioEntDropdown_CheckedChanged(object sender, EventArgs e)
        {
            SetPropertySelectedObject(radioEntDropdownShowProps, propGridEntDropdown, EntityDropdown, EntityDropdown.SelectedEntity);
        }

        private void EntityDropdown_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateControlLogger(textEntDropdownLog, $"ProgressChanged: {e.UserState} : {e.ProgressPercentage}");
        }

        private void EntityDropdown_LoadDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textEntDropdownLog, $"LoadDataComplete, Count: {EntityDropdown.AllEntities.Count}");

            listBoxEntities.DataSource = null;
            listBoxEntities.Items.Clear();
            listBoxEntities.DataSource = EntityDropdown.AllEntitiesBindable;
            listBoxEntities.DisplayMember = "DisplayName";
            listBoxEntities.ValueMember = "Object";

            SetPropertySelectedObject(radioEntDropdownShowProps, propGridEntDropdown, EntityDropdown, EntityDropdown.SelectedEntity);
        }

        private void EntityDropdown_BeginLoadData(object sender, EventArgs e)
        {
            UpdateControlLogger(textEntDropdownLog, $"BeginLoadData");
        }

        private void EntityDropdown_BeginClearData(object sender, EventArgs e)
        {
            UpdateControlLogger(textEntDropdownLog, $"BeginClearData");
        }

        private void EntityDropdown_ClearDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textEntDropdownLog, $"ClearDataComplete");
        }
        #endregion

        #region Attrib Dropdown events

        private void RadioAttribDropdown_CheckedChanged(object sender, EventArgs e)
        {
            SetPropertySelectedObject(radioAttribDropdownShowProps, propGridAttribDropdown, AttributeDropdownBase, AttributeDropdownBase.SelectedAttribute);
        }

        private void EntityDropdownAttribs_SelectedItemChanged(object sender, EventArgs e)
        {
            AttributeDropdownBase.ParentEntity = EntityDropdownAttribs.SelectedEntity;
            UpdateControlLogger(textAttribDropdownLog, $"SelectedItemChanged (Entity): {EntityDropdownAttribs.SelectedEntity?.LogicalName}");
        }

        private void EntityDropdownAttribs_LoadDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"LoadDataComplete (Entities), Count: {EntityDropdownAttribs.AllEntities.Count}");
        }

        private void AttributeDropdown_SelectedItemChanged(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"SelectedItemChanged (Attribute): {AttributeDropdownBase.SelectedAttribute?.LogicalName}");
            SetPropertySelectedObject(radioAttribDropdownShowProps, propGridAttribDropdown, AttributeDropdownBase, AttributeDropdownBase.SelectedAttribute);
        }

        private void AttributeDropdown_LoadDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"LoadDataComplete (Attribute), Count: {AttributeDropdownBase.AllAttributes.Count}");

            listBoxAttributes.DataSource = null;
            listBoxAttributes.Items.Clear();
            listBoxAttributes.DataSource = AttributeDropdownBase.AllAttributesBindable;
            listBoxAttributes.DisplayMember = "DisplayName";
            listBoxAttributes.ValueMember = "Object";

            SetPropertySelectedObject(radioAttribDropdownShowProps, propGridAttribDropdown, AttributeDropdownBase, AttributeDropdownBase.SelectedAttribute);
        }

        private void AttributeDropdown_ClearDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"ClearDataComplete (Attribute) - Count: {AttributeDropdownBase.AllAttributes.Count}");
        }

        private void AttributeDropdown_BeginLoadData(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"BeginLoadData (Attribute)");
        }

        private void AttributeDropdown_BeginClearData(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"BeginClearData (Attribute)");
        }

        private void AttributeDropdown_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"ProgressChanged (Attribute): {e.UserState} : {e.ProgressPercentage}");
        }

        private void EntityDropdownAttribs_BeginLoadData(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"BeginLoadData (Entities)");
        }

        private void EntityDropdownAttribs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"ProgressChanged (Entities): {e.UserState} : {e.ProgressPercentage}");
        }
        #endregion

        #region Attrib ListControl events

        private void EntityDropdownAttribList_SelectedItemChanged(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribListLog, $"SelectedItemChanged (Entity): {EntityDropdownAttribList.SelectedEntity?.LogicalName}");

            AttribListControl.ParentEntity = EntityDropdownAttribList.SelectedEntity;
        }

        private void AttribListControl_SelectedItemChanged(object sender, EventArgs e)
        {
            var attr = AttribListControl.SelectedAttribute;
            UpdateControlLogger(textAttribListLog, $"SelectedItemChanged (Attribute): {attr?.LogicalName}");

            object attrObj = attr;
            if (attr != null)
            {
                switch (attr.AttributeType)
                {
                    case AttributeTypeCode.Lookup:
                        attrObj = (LookupAttributeMetadata)attr;
                        break;
                    default:
                        break;
                }
            }
            SetPropertySelectedObject(radioAttribListShowProps, propGridAttrList, AttribListControl, attrObj);
        }

        private void AttribListControl_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateControlLogger(textAttribListLog, $"ProgressChanged (Attributes): {e.UserState} : {e.ProgressPercentage}");
        }

        private void AttribListControl_CheckedItemsChanged(object sender, EventArgs e)
        {
            AttributeMetadata attr = null;
            var count = AttribListControl.CheckedAttributes?.Count;
            if (count > 0)
            {
                attr = AttribListControl.CheckedAttributes[count.Value - 1];
            }
            UpdateControlLogger(textAttribListLog, $"CheckedItemsChanged (Attributes) - {AttribListControl.CheckedAttributes?.Count}");
            SetPropertySelectedObject(radioAttribListShowProps, propGridAttrList, AttribListControl, attr);
        }

        private void AttribListControl_FilterListComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribListLog, $"FilterListComplete (Attributes) - {AttribListControl.AllAttributes.Count}");
        }

        private void AttribListControl_LoadDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribListLog, $"LoadDataComplete (Attributes) - {AttribListControl.AllAttributes.Count}");
        }

        private void RadioAttribList_CheckedChanged(object sender, EventArgs e)
        {
            var attr = AttribListControl.SelectedAttribute;
            SetPropertySelectedObject(radioAttribListShowProps, propGridAttrList, AttribListControl, attr);
        }

        #endregion

        #region Solution Dropdown events
        private void SolutionsDropdown_LoadDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textSolnDropdownLog, $"LoadDataComplete - Count: {SolutionDropdown.AllSolutions.Count}");

            listBoxSolutions.DataSource = null;
            listBoxSolutions.Items.Clear();
            listBoxSolutions.DataSource = SolutionDropdown.AllSolutionsBindable;
            listBoxSolutions.DisplayMember = "DisplayName";
            listBoxSolutions.ValueMember = "Object";

            SetPropertySelectedObject(radioSolnDropdownShowProps, propGridSolutions, SolutionDropdown, SolutionDropdown.SelectedSolution);
        }

        private void SolutionDropdown_BeginLoadData(object sender, EventArgs e)
        {
            UpdateControlLogger(textSolnDropdownLog, $"BeginLoadData");
        }

        private void SolutionDropdown_SelectedItemChanged(object sender, EventArgs e)
        {
            UpdateControlLogger(textSolnDropdownLog, $"SelectedItemChanged: {SolutionDropdown.SelectedSolution?.Attributes["friendlyname"]}");
            SetPropertySelectedObject(radioSolnDropdownShowProps, propGridSolutions, SolutionDropdown, SolutionDropdown.SelectedSolution);
        }

        private void SolutionDropdown_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateControlLogger(textSolnDropdownLog, $"ProgressChanged: {e.UserState} : {e.ProgressPercentage}");
        }

        private void SolutionDropdown_BeginClearData(object sender, EventArgs e)
        {
            UpdateControlLogger(textSolnDropdownLog, $"BeginClearData");
        }

        private void SolutionDropdown_ClearDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textSolnDropdownLog, $"ClearDataComplete");
            SetPropertySelectedObject(radioSolnDropdownShowProps, propGridSolutions, SolutionDropdown, SolutionDropdown.SelectedSolution);
        }
        private void RadioSolnDropdownShowProps_CheckedChanged(object sender, EventArgs e)
        {
            SetPropertySelectedObject(radioSolnDropdownShowProps, propGridSolutions, SolutionDropdown, SolutionDropdown.SelectedSolution);
        }
        #endregion

        #region View Dropdown Events

        private void EntityDropdownViews_SelectedItemChanged(object sender, EventArgs e)
        {
            UpdateControlLogger(textViewsDropdownLog, $"SelectedItemChanged: {EntityDropdownViews.SelectedEntity?.SchemaName}");

            ViewDropdown.ParentEntity = EntityDropdownViews.SelectedEntity;

            SetPropertySelectedObject(radioViewDropdownShowProps, propGridViewDropdown, ViewDropdown, ViewDropdown.SelectedView);
        }

        private void ViewDropdown_SelectedItemChanged(object sender, EventArgs e)
        {
            UpdateControlLogger(textViewsDropdownLog, $"SelectedItemChanged (View): {ViewDropdown.SelectedView?.LogicalName}");

            SetPropertySelectedObject(radioViewDropdownShowProps, propGridViewDropdown, ViewDropdown, ViewDropdown.SelectedView);
        }

        private void ViewDropdown_LoadDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textAttribDropdownLog, $"LoadDataComplete (Views), Count: {ViewDropdown.AllViews.Count}");

            var v = listBoxViews;
            v.DataSource = null;
            v.Items.Clear();
            v.DataSource = ViewDropdown.AllViewsBindable;
            v.DisplayMember = "Name";
            v.ValueMember = "Object";

            SetPropertySelectedObject(radioViewDropdownShowProps, propGridViewDropdown, ViewDropdown, ViewDropdown.SelectedView);
        }

        private void ViewDropdown_ClearDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textViewsDropdownLog, $"ClearDataComplete (Views) - Count: {ViewDropdown.AllViews.Count}");
        }

        private void ViewDropdown_BeginLoadData(object sender, EventArgs e)
        {
            UpdateControlLogger(textViewsDropdownLog, $"BeginLoadData (Views)");
        }

        private void ViewDropdown_BeginClearData(object sender, EventArgs e)
        {
            UpdateControlLogger(textViewsDropdownLog, $"BeginClearData (Views)");
        }

        private void ViewDropdown_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateControlLogger(textViewsDropdownLog, $"ProgressChanged (Views): {e.UserState} : {e.ProgressPercentage}");
        }


        private void RadioViewsDropdown_CheckedChanged(object sender, EventArgs e)
        {
            SetPropertySelectedObject(radioViewDropdownShowProps, propGridViewDropdown, ViewDropdown, ViewDropdown.SelectedView);
        }
        #endregion 

        #region Helpers

        private void UpdateControlLogger(TextBox ctl, string message, bool clear = false)
        {
            if (clear)
            {
                ctl.Clear();
            }
            else
            {
                ctl.Text += message + Environment.NewLine;
            }
        }

        private void SetPropertySelectedObject(RadioButton radio, PropertyGrid grid, object targetCtl, object targetObj)
        {
            if (radio.Checked)
            {
                grid.SelectedObject = targetCtl;
            }
            else
            {
                grid.SelectedObject = targetObj;
            }
        }

        /// <summary>
        /// Apply the text filter programmatically 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTextFilter_TextChanged(object sender, EventArgs e)
        {
            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageEntList":
                    EntityListControl.FilterList(toolStripTextFilter.Text);
                    break;
                case "tabPageAttrList":
                    AttribListControl.FilterList(toolStripTextFilter.Text);
                    break;
            }
        }
        #endregion

        #region Global OptionSet Listview Control event handlers
        private void GlobalOptionSetList_CloseComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textGlobalOptsListLog, $"CloseComplete");
        }
        private void GlobalOptionSetList_InitializeComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textGlobalOptsListLog, $"InitializeComplete");
        }

        private void GlobalOptionSetList_LoadDataComplete(object sender, EventArgs e)
        {
            UpdateControlLogger(textGlobalOptsListLog, $"LoadDataComplete - All Global OptionSets  count: {GlobalOptionSetList.AllOptionSets?.Count}");
        }

        private void GlobalOptionSetList_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateControlLogger(textGlobalOptsListLog, $"{e.UserState} : {e.ProgressPercentage}");
        }

        private void GlobalOptionSetList_CheckedItemsChanged(object sender, EventArgs e)
        {
            UpdateControlLogger(textGlobalOptsListLog, $"CheckedItemsChanged - Checked Global OptionSets count: {GlobalOptionSetList.CheckedOptionSets?.Count}");

            OptionSetMetadataBase opt = null;

            if (GlobalOptionSetList.CheckedOptionSets.Count > 0)
            {
                opt = GlobalOptionSetList.CheckedOptionSets[0];
            }

            SetPropertySelectedObject(radioGlobalOptsListShowProps, propGridGlobalOptsList, GlobalOptionSetList, opt);
        }
        private void GlobalOptionSetList_SelectedItemChanged(object sender, EventArgs e)
        {
            var opt = GlobalOptionSetList.SelectedOptionSet;

            SetPropertySelectedObject(radioGlobalOptsListShowProps, propGridGlobalOptsList, GlobalOptionSetList, opt);

            UpdateControlLogger(textGlobalOptsListLog, $"SelectedItemChanged: {opt?.Name}");
        }
        private void GlobalOptionSetList_ClearDataComplete(object sender, EventArgs e)
        {
            // ENTITIES LIST - log event
            SetPropertySelectedObject(radioGlobalOptsListShowProps, propGridGlobalOptsList, GlobalOptionSetList, null);

            UpdateControlLogger(textGlobalOptsListLog, $"ClearDataComplete - All Global OptionSets count: {GlobalOptionSetList.AllOptionSets?.Count}");
        }

        private void RadioGlobalOptionSetList_CheckedChanged(object sender, EventArgs e)
        {
            SetPropertySelectedObject(radioGlobalOptsListShowProps, propGridGlobalOptsList, GlobalOptionSetList, EntityListControl.SelectedEntity);
        }

        private void GlobalOptionSetList_BeginLoadData(object sender, EventArgs e)
        {
            if (this.textEntListLog.InvokeRequired)
            {
            }
            else
            {
                textGlobalOptsListLog.Clear();
            }
        }
        #endregion

        #region CRMGridView event handlers

        private void RadioCRMGridViewShowProps_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCRMGridViewShowProps.Checked)
                propCRMGridView.SelectedObject = CrmGridView;
            else if (radioCRMGridViewSelEntity.Checked)
                propCRMGridView.SelectedObject = CrmGridView.SelectedCellRecords?.FirstOrDefault();
            else if (radioCRMGridViewRightShowProps.Checked)
                propCRMGridView.SelectedObject = CrmGridViewDesignedCols;
            else if (radioCRMGridViewCmbBx.Checked)
                propCRMGridView.SelectedObject = cdsDataComboBox;
            else if (radioCRMGridViewTxtBx.Checked)
                propCRMGridView.SelectedObject = cdsDataTextBox;
            else if (radioCRMGridViewLkpDlg.Checked)
                propCRMGridView.SelectedObject = cdsLookupDialog1;
        }

        #endregion CRMGridView event handlers

        private void crmGridView1_RecordClick(object sender, CRMRecordEventArgs e)
        {
            SetPropertySelectedObject(radioCRMGridViewShowProps, propCRMGridView, CrmGridView, e.Entity);
            cdsDataTextBox.Entity = e.Entity;
        }

        private void buttonExecFetch_Click(object sender, EventArgs e)
        {
            // exec the fetch and bind to the grid
            if (XmlViewerCRMDataGrid.IsValidXml)
            {
                var fetchReq = new RetrieveMultipleRequest
                {
                    Query = new FetchExpression(XmlViewerCRMDataGrid.Text)
                };

                var fetchResult = Service.Execute(fetchReq) as RetrieveMultipleResponse;

                CrmGridView.DataSource = fetchResult.EntityCollection;
                cdsDataComboBox.DataSource = fetchResult.EntityCollection;
                cdsDataComboBox.DisplayFormat = textCdsDataComboBoxFormat.Text;

                // MessageBox.Show(fetchResult.EntityCollection.EntityName);
            }
        }

        private void XmlViewerCRMDataGrid_TextChanged(object sender, EventArgs e)
        {
            buttonExecFetch.Enabled = XmlViewerCRMDataGrid.IsValidXml;
        }

        private void RadioBoundListShowProps_CheckedChanged(object sender, EventArgs e)
        {
            SetPropertySelectedObject(radioBoundListShowProps, propGridBoundListView, listViewEntCollection, null);
        }

        private void EntLVBaseLoadItems_Click(object sender, EventArgs e)
        {
            EntityListViewBase.LoadData();
        }

        private void EntityListViewBase_SelectedItemChanged(object sender, EventArgs e)
        {
            SetPropertySelectedObject(radioEntLVBaseShowProps, propGridEntLVBase, EntityListViewBase, EntityListViewBase.SelectedEntity);
        }

        private void solutionsDropdownControl1_LoadDataComplete(object sender, EventArgs e)
        {
            //BoundListView.ClearData();
            var defs = new ListViewColumnDef[] {
                new ListViewColumnDef("DisplayName", 1, "Display Name") { Width = 250 },
                new ListViewColumnDef("LogicalName", 2, "Logical Name") { IsFilterColumn = true },
                new ListViewColumnDef( "SchemaName", 0, "Schema Name") { Width = 150 },
                new ListViewColumnDef( "IsManaged", 4, "Managed State") { IsGroupColumn = true },
                new ListViewColumnDef("Description", 3) { Width = 250 }
            };

            defs = new ListViewColumnDef[] {
                new ListViewColumnDef("uniquename", 1, "Unique Name") { Width = 250 },
                new ListViewColumnDef("friendlyname", 2, "Friendly Name") { IsFilterColumn = true },
                new ListViewColumnDef( "description", 3, "Description") { Width = 150 }
            };

            // listViewEntCollection.ListViewColDefs = defs;
            // listViewEntCollection.LoadData<Entity>(solutionsDropdownControl1.AllSolutions);

        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            AttributeDropdownBase.LoadData();
        }

        private void textCdsDataComboBoxFormat_TextChanged(object sender, EventArgs e)
        {
            cdsDataComboBox.DisplayFormat = textCdsDataComboBoxFormat.Text;
            cdsDataTextBox.DisplayFormat = textCdsDataComboBoxFormat.Text;
        }

        private void cdsDataComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entity = cdsDataComboBox.SelectedEntity;
            //MessageBox.Show(entity.Id.ToString());
        }

        private void buttonExecFetchBoundList_Click(object sender, EventArgs e)
        {
            // exec the fetch and bind to the grid
            if (xmlViewerEntColl.IsValidXml)
            {
                listViewEntCollection.LoadData(xmlViewerEntColl.Text);
            }
        }

        private void buttonCDSComboRetrieve_Click(object sender, EventArgs e)
        {
            // 
            var infoPanel = InformationPanel.GetInformationPanel(this, "CDS Combo Box Retrieve Example", 340, 150);
            infoPanel.BringToFront();
            Refresh();

            textBoxCDSComboProgress.Text = "";

            cdsDataComboRetrieve.RetrieveMultiple(xmlViewerFetchCDSCombo.Text,
                (string message) => {

                    textBoxCDSComboProgress.Text += $"{message}{Environment.NewLine}";
                    InformationPanel.ChangeInformationPanelMessage(infoPanel, message);
                    Refresh();

                },
                (int itemCount, Entity FirstItem) =>
                {
                    // Thread.Sleep(2000);
                    textBoxCDSComboProgress.Text += $"Count: {itemCount}{Environment.NewLine}";

                    InformationPanel.ChangeInformationPanelMessage(infoPanel, $"Count: {itemCount}, Entity: {FirstItem?.Attributes.First().ToString()}");
                    Refresh();

                    // Thread.Sleep(2000);

                    if (Controls.Contains(infoPanel)) {
                        infoPanel.Dispose();
                        Controls.Remove(infoPanel);
                    }
                });
        }

        private void buttonExecPredefinedCrmGridViewQuery_Click(object sender, EventArgs e)
        {
            var fex = new FetchExpression(@"<fetch>
  <entity name='account' >
    <attribute name='accountid' />
    <attribute name='name' />
    <attribute name='telephone1' />
    <attribute name='address1_city' />
    <attribute name='primarycontactid' />
    <attribute name='revenue' />
    <attribute name='address1_postofficebox' />
    <filter>
      <condition attribute='statecode' operator='eq' value='0' />
    </filter>
    <link-entity name='contact' from='contactid' to='primarycontactid' link-type='outer' alias='C' >
      <attribute name='emailaddress1' />
      <attribute name='address1_telephone3' />
    </link-entity>
    <attribute name='statecode' />
  </entity>
</fetch>");
            var res = Service.RetrieveMultiple(fex);
            CrmGridViewDesignedCols.DataSource = res;
        }

        private void CrmGridViewDesignedCols_RecordClick(object sender, CRMRecordEventArgs e)
        {
            cdsDataTextBox.Entity = e.Entity;
        }

        private void cdsDataTextBox_RecordClick(object sender, CDSRecordEventArgs e)
        {
            if (e.Entity != null)
            {
                MessageBox.Show($"Entity: {e.Entity.LogicalName} Id: {e.Entity.Id} Attributes: {e.Entity.Attributes.Count}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            switch (cdsLookupDialog1.ShowDialog(this))
            {
                case DialogResult.OK:
                    cdsDataTextBox.Entity = cdsLookupDialog1.Entity;
                    break;
                case DialogResult.Abort:
                    cdsDataTextBox.Entity = null;
                    break;
            }
            Cursor = Cursors.Default;
        }
    }
}