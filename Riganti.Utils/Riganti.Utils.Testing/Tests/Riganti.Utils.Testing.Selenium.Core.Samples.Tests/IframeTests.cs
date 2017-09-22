﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class IframeTests : SeleniumTest
    {
        [TestMethod]
        public void IFrameTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("frametest1.aspx");
                browser.First("#top");
                browser.First("#topframe");

                var frame = browser.GetFrameScope("#topframe");
                frame.First("#frame2_text");
            });
        }
        [TestMethod]
        public void IFrameTest2()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("frametest1.aspx");
                var elm = browser.First("#top");
                browser.First("#topframe");

                var frame = browser.GetFrameScope("#topframe");
                frame.First("#frame2_text");
                elm.First("#child");

            });
        }
        [TestMethod]
        public void IFrameExceptionMessageTest()
        {
            // test the exception message
            try
            {
                this.RunInAllBrowsers(browser =>
                {
                    browser.NavigateToUrl("frametest1.aspx");
                    var frame = browser.GetFrameScope("#top");
                });
            }
            catch (Exception ex)
            {
                if (!ex.ToString().Contains("is not a iframe element"))
                {
                    throw;
                }
            }

        }
    }
}