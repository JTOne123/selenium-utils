﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class FunctionsUiTests : SeleniumTest
    {
        [TestMethod]
        public void GetFullSelector()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                TestContext.WriteLine(
                    browser.First("#displayed").FindElements("div p")
                        .FullSelector);
            });
        }

        [TestMethod]
        public void SearchInElementsCollection()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.FindElements("form").FindElements("div").ThrowIfSequenceEmpty();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NoSuchElementException))]
        public void NoParentTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("NoParentTest.aspx");
                var parent = browser.First("html").ParentElement;
            });
        }

        [TestMethod]
        public void UrlComparisonTest1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/NoParentTest.aspx");
                browser.CheckUrl(url => url.Contains("NoParentTest.aspx"));
            });
        }

        [TestMethod]
        [ExpectedException(typeof(AlertException))]
        public void ExpectedExceptionTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(AlertException))]
        public void ExpectedExceptionTest2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        public void ConfirmTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Confirm.aspx");

                var button = browser.First("#button");
                button.Click();
                browser.ConfirmAlert().First("#message").CheckIfInnerTextEquals("Accept", false);

                button.Click();
                browser.DismissAlert().First("#message").CheckIfInnerTextEquals("Dismiss", false);
            });
        }

        [TestMethod]
        public void SelectMethodTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/SelectMethod.aspx");

                Func<string, By> sMethod = s => By.CssSelector($"[data-ui='{s}']");
                browser.SelectMethod = sMethod;

                var d = browser.First("d");
                d.SetCssSelectMethod();
                var c = d.First("#c");
                c.ParentElement.CheckIfHasAttribute("data-ui");

                //select css method - test switching
                browser.SetCssSelector();

                var a = browser.First("#a");
                a.SelectMethod = sMethod;
                var e = a.First("e");
                e.First("#b");

                a.SetBrowserSelectMethod();
                a.First("#c");
            });
        }

        [TestMethod]
        public void FileDialogTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("FileDialog.aspx");

                var tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, "test content");

                browser.FileUploadDialogSelect(browser.First("input[type=file]"), tempFile);
                browser.First("input[type=file]").CheckAttribute("value", s => !string.IsNullOrWhiteSpace(s));

                File.Delete(tempFile);
            });
        }

        [TestMethod]
        public void TextTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("text.aspx");
                browser.First("#button").CheckIfTextEquals("text", false);
                browser.First("#input").CheckIfTextEquals("text", false);
                browser.First("#area").CheckIfTextEquals("text", false);

                browser.First("#button").CheckIfText(s => s.ToLower().Contains("text"));
                browser.First("#input").CheckIfText(s => s.Contains("text"));
                browser.First("#area").CheckIfText(s => s.Contains("text"));
            });
        }

        [TestMethod]
        public void JsInnerTextTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("JsTestSite.aspx");
                var elm = browser.First("#hiddenElement");
                Assert.IsTrue(string.Equals(elm.GetJsInnerText()?.Trim(), "InnerText",
                    StringComparison.OrdinalIgnoreCase));
                elm.CheckIfJsPropertyInnerText(c => c == "InnerText")
                    .CheckIfJsPropertyInnerTextEquals("InnerText", false);
            });
        }

        [TestMethod]
        public void JsInnerHtmlTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("JsHtmlTest.aspx");
                var elm = browser.First("#htmlTest");
                var content = elm.GetJsInnerHtml()?.Trim() ?? "";
                Assert.IsTrue(content.Contains("<span>") && content.Contains("</span>"));
                elm.CheckIfJsPropertyInnerHtml(c => c.Contains("<span>") && c.Contains("</span>"));
            });
        }

        [TestMethod]
        public void ElementAtFirst1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("elementatfirst.aspx");
                Assert.AreEqual(
                    browser
                        .ElementAt("div > div", 0)
                        .First("#first0")
                        .GetInnerText()?.ToLower(), "first0");
            });
        }

        [TestMethod]
        public void ElementAtFirst2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("elementatfirst.aspx");
                Assert.AreEqual(browser
                    .ElementAt("#divs > div", 1)
                    .First("div")
                    .GetInnerText()?.ToLower(), "first1");
            });
        }

        [TestMethod]
        public void ElementAtFirst3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("elementatfirst.aspx");
                Assert.AreEqual(browser
                    .ElementAt("#divs > div", 2)
                    .ParentElement.First("#first2")
                    .GetInnerText()?.ToLower(), "first2");
            });
        }

        [TestMethod]
        public void First()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("TemporarySelector.aspx");
                browser.SelectMethod = s => SelectBy.CssSelector(s, "[data-ui='{0}']");
                browser.First("p", By.TagName).CheckIfTextEquals("p");
                browser.First("id", By.Id).CheckIfTextEquals("id");
                browser.First("id").CheckIfTextEquals("data");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(EmptySequenceException))]
        public void ElementContained_NoElement_ExpectedFailure()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ElementContained.aspx");

                var a = browser.First("#no");
                a.CheckIfContainsElement("span");
            });
        }

        [TestMethod]
        public void ElementContained_NoElement()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ElementContained.aspx");
                browser.First("#no").CheckIfNotContainsElement("span");
            });
        }

        [TestMethod]
        public void ElementContained_OneElement_ExpectedFailure()
        {
            try
            {
                RunInAllBrowsers(browser =>
                {
                    browser.NavigateToUrl("ElementContained.aspx");
                    browser.First("#one").CheckIfNotContainsElement("span");
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("children"))
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void ElementContained_OneElement()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ElementContained.aspx");
                browser.First("#one").CheckIfContainsElement("span");
            });

            try
            {
            }
            catch (Exception ex)
            {
                var message = ex.ToString();

                if (message.Contains("child") || !message.Contains("2"))
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void ElementContained_TwoElement_ExpectedFailure()
        {
            try
            {
                RunInAllBrowsers(browser =>
                {
                    browser.NavigateToUrl("ElementContained.aspx");
                    browser.First("#two").CheckIfNotContainsElement("span");
                });
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                if (!message.Contains("children") || !message.Contains("2"))
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void ElementContained_TwoElement()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ElementContained.aspx");
                browser.First("#two").CheckIfContainsElement("span");
            });
        }

        [TestMethod]
        public void CheckValueTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("valuetest.aspx");
                browser.First("#input-radio").CheckIfValue("radio1");
                browser.First("#input-radio2").CheckIfValue("radio2");
                browser.First("#checkbox1").CheckIfValue("checkboxvalue1");
                browser.First("#checkbox2").CheckIfValue("checkboxvalue2");
                browser.First("#area").CheckIfValue("areavalue");
                browser.First("#input-text").CheckIfValue("text1");
                browser.First("#input-text").CheckIfValue("texT1", true);
                browser.First("#input-text").CheckIfValue("   texT1   ", true);
            });
        }

        [TestMethod]
        public void SetJsInputPropertyTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("JSPropertySetTest.aspx");
                var input = browser.First("#input1");
                const string inputValue = "4012 5770 5655";
                input.SetJsElementProperty("value", inputValue);
                input.CheckIfValue(inputValue);
                Assert.AreEqual(input.GetJsElementPropertyValue("value"), inputValue);
            });
        }

        [TestMethod]
        public void CookieTest()
        {
            

                Action<BrowserWrapper> test = browser =>
                {
                    browser.NavigateToUrl("CookiesTest.aspx");
                    browser.First("#CookieIndicator").CheckIfTextEquals("False");
                    browser.Click("#SetCookies").Wait();
                    browser.NavigateToUrl("CookiesTest.aspx");
                    browser.First("#CookieIndicator").CheckIfTextEquals("True");

                };
                RunInAllBrowsers(browser =>
                {
                    browser.Dispose();
                });
                RunInAllBrowsers(test);
                RunInAllBrowsers(test);
            
        }

        [TestMethod]
        public void TextNotEquals()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("CookiesTest.aspx");
                var label = browser.First("#CookieIndicator");
                label.CheckIfTextNotEquals("True");
                label.CheckIfTextEquals("False");
                try
                {
                    label.CheckIfTextNotEquals("False");
                    throw new Exception("Exception was expected.");
                }
                catch (UnexpectedElementStateException)
                {
                }
            });
        }

        [TestMethod]
        public void CheckHyperLink()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");
                browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema", "/path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#RelativeLink", "/path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#RelativeLink", "/path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.AbsoluteUri);
                browser.CheckIfHyperLinkEquals("#RelativeLink", "path/test?query=test#fragmentasd", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#RelativeLink", "path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.AbsoluteUri);
                browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema", "//localhost:1234/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema",
                    "//localhostads:1234/path/test?query=test#fragment", UrlKind.Absolute, UriComponents.PathAndQuery);
            });
        }

        [TestMethod]
        public void CheckHyperLink_Failure1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");

                Assert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#RelativeLink", "/path0/test?query=test#fragment", UrlKind.Relative,
                        UriComponents.PathAndQuery);
                });
            });
        }

        [TestMethod]
        public void CheckHyperLink_Failure2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");

                Assert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#RelativeLink", "https://www.google.com/path/test?query=test#fragment",
                        UrlKind.Absolute);
                });
            });
        }

        [TestMethod]
        public void CheckHyperLink_Failure3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");

                Assert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.googles.com/path/test?query=test#fragment",
                        UrlKind.Absolute, UriComponents.AbsoluteUri);
                });
            });
        }

        [TestMethod]
        public void CheckHyperLink_Failure4()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");
                try
                {
                    browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema", "https://localhost:1234/path/test?query=test#fragment", UrlKind.Absolute, UriComponents.AbsoluteUri);
                }
                catch (UnexpectedElementStateException)
                {
                }
            });
        }

        [TestMethod]
        public void SingleExceptionTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");

                Assert.ThrowsException<EmptySequenceException>(() =>
                {
                    browser.Single("asdasd");
                });
            });
        }

        [TestMethod]
        public void CheckIfUrlExistsTest()
        {
            RunInAllBrowsers(browser =>
            {
                Assert.ThrowsException<EmptySequenceException>(() =>
                {
                    browser.CheckIfUrlIsAccessible("hyperlink.aspx", UrlKind.Relative);
                });
            });
        }

        [TestMethod]
        public void CheckIfUrlExistsTest2()
        {
            RunInAllBrowsers(browser =>
            {
                Assert.ThrowsException<SeleniumTestFailedException>(() =>
                {
                    browser.CheckIfUrlIsAccessible("NonExistent359.aspx", UrlKind.Relative);
                });
            });
        }
    }
}