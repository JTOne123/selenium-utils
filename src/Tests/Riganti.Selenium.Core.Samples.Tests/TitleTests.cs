﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{

    [TestClass]
    public class TitleTests : SeleniumTest
    {
        [TestMethod]
        public void Title_CheckIfTitleEquals()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Title");
                browser.CheckIfTitleEquals("This is title       ");
            });
        }
        [TestMethod]
        public void Title_CheckIfTitleNotEquals()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Title");
                browser.CheckIfTitleNotEquals("This is not title            ");
            });
        }
        [TestMethod]
        public void Title_CheckIfTitleEquals_NoTrim_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Title");

                MSAssert.ThrowsException<BrowserException>(() =>
                {
                    browser.CheckIfTitleEquals("This is title           ", trim: false);
                });
            });
        }
        [TestMethod]
        public void Title_CheckIfTitleNotEquals_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Title");
                MSAssert.ThrowsException<BrowserException>(() =>
                {
                    browser.CheckIfTitleNotEquals("This is title");
                });
            });
        }
        [TestMethod]
        public void Title_CheckIfTitleEquals_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Title");
                MSAssert.ThrowsException<BrowserException>(() =>
                {
                    browser.CheckIfTitleEquals("This is not title");
                });
            });
        }
        [TestMethod]
        public void Title_CheckIfTitle_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Title");
                MSAssert.ThrowsException<BrowserException>(() =>
                {
                    browser.CheckIfTitle(c => c == "This is not title");
                });
            });
        }
    }
}
