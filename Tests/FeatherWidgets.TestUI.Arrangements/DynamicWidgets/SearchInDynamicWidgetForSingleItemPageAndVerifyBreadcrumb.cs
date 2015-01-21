﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SearchForSingleItemPageAndVerifyBreadcrumb arrangement class.
    /// </summary>
    public class SearchInDynamicWidgetForSingleItemPageAndVerifyBreadcrumb : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);

            Guid parentPageId = ServerOperations.Pages().CreatePage(PageName);

            Guid currentChildPageId = Guid.NewGuid();
            for (int i = 0; i < PageHierarchyLevelsCount; i++)
            {
                ServerOperations.Pages().CreatePage(ChildPagesPrefix + i, currentChildPageId, parentPageId);
                parentPageId = currentChildPageId;

                currentChildPageId = Guid.NewGuid();
            }
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        private const string PageName = "TestPage";
        private const string ChildPagesPrefix = "ChildPage";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string TransactionName = "Module Installations";

        private const int PageHierarchyLevelsCount = 10; 
    }
}