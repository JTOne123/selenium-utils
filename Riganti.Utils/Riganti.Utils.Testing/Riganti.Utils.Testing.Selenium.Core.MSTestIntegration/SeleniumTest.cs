﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Represents implementation of base for selenium tests for MSTest. 
    /// </summary>
    public class SeleniumTest : SeleniumTestBase
    {
        private ITestContext testContext;

        public override ITestContext TestContext
        {
            get { return testContext ?? (testContext = TestingContext?.Wrap()); }
            set { testContext = value; }
        }

        public TestContext TestingContext { get; set; }

        [TestCleanup]
        public override void TestCleanUp()
        {
            base.TestCleanUp();
        }

        [ClassCleanup]
        public override void TotalCleanUp()
        {
            base.TotalCleanUp();
        }
    }
}