﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;

namespace SeleniumCore.UnitTests
{
    [TestClass]
    public class BrowserWrapperTests
    {
        [TestMethod]
        public void ExactUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrlEquals("https://localhost:12345/path1/path2?query=1#fragment");
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void ExactUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrlEquals("https://localhost:12345/spath1/path2?query=1#fragment");
        }

        [TestMethod]
        public void HostUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("https://localhost:12345/", UriKind.Absolute, UriComponents.Host);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void HostUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("https://localhosst:12345/", UriKind.Absolute, UriComponents.Host);
        }

        [TestMethod]
        public void PathUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("/path1/path2", UriKind.Relative, UriComponents.Path);
        }

        [TestMethod]
        public void PathUrlMatchTest3()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("https://localhost:12345/path1/path2", UriKind.Absolute, UriComponents.Path);
        }

        [ExpectedException(typeof(BrowserLocationException))]
        [TestMethod]
        public void PathUrlMatchTest4()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("https://localhost:12345/path1/path2/nonon", UriKind.Absolute, UriComponents.Path);
        }

        [TestMethod]
        public void PathUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("/path1/path2", UriKind.Relative, UriComponents.Path);
        }

        [TestMethod]
        public void PathAndHostUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("//localhost/path1/path2", UriKind.Absolute, UriComponents.Path, UriComponents.Host);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void PathAndHostUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("//localhost/path10/path2", UriKind.Absolute, UriComponents.Path, UriComponents.Host);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void PathAndHostUrlMatchTest4()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("//localhosta/path1/path2", UriKind.Absolute, UriComponents.Path, UriComponents.Host);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void PathAndHostUrlMatchTest3()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("//localhost0/path10/path2", UriKind.Absolute, UriComponents.Path, UriComponents.Host);
        }

        [TestMethod]
        public void PathHostAndPortUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("//localhost:12345/path1/path2", UriKind.Absolute, UriComponents.Path, UriComponents.HostAndPort);
        }

        [TestMethod]
        public void QueryUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("//localhost:12345/path1/path2?query=1", UriKind.Relative, UriComponents.Query);
        }

        [TestMethod]
        public void QueryUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("?query=1", UriKind.Relative, UriComponents.Query);
        }

        [TestMethod]
        public void FragmentUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("//localhost:12345/path1/path2#fragment", UriKind.Absolute, UriComponents.Fragment);
        }

        [TestMethod]
        public void FragmentUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("#fragment", UriKind.Relative, UriComponents.Fragment);
        }

        [TestMethod]
        public void SchemaUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("https://ex.com/", UriKind.Absolute, UriComponents.Scheme);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void SchemaUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("http://ex.com/", UriKind.Absolute, UriComponents.Scheme);
        }

        [TestMethod]
        public void PortUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("//localhost:12345", UriKind.Absolute, UriComponents.Port);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void PortUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.CheckUrl("https://ex.com/", UriKind.Absolute, UriComponents.Port);
        }
    }
}