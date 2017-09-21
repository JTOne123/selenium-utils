﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Riganti.Utils.Testing.Selenium.Core.Abstractions
{
    public interface IElementWrapper
    {
        int ActionWaitTime { get; set; }
        string BaseUrl { get; set; }
        IBrowserWrapper BrowserWrapper { get; }
        string FullSelector { get; }
        IElementWrapperCollection Children { get; }
        IElementWrapper ParentElement { get; }
        ISeleniumWrapper ParentWrapper { get; set; }
        Func<string, By> SelectMethod { get; set; }
        string Selector { get; set; }
        IWebElement WebElement { get; }

        void ActivateScope();
        //CheckIElementWrapper Check();
        IElementWrapper CheckAttribute(string attributeName, Func<string, bool> expression, string failureMessage = null);
        IElementWrapper CheckAttribute(string attributeName, string value, bool caseInsensitive = false, bool trimValue = true, string failureMessage = null);
        IElementWrapper CheckAttribute(string attributeName, string[] allowedValues, bool caseInsensitive = false, bool trimValue = true, string failureMessage = null);
        IElementWrapper CheckClassAttribute(Func<string, bool> expression, string failureMessage = "");
        IElementWrapper CheckClassAttribute(string value, bool caseInsensitive = false, bool trimValue = true);
        IElementWrapper CheckIfContainsElement(string cssSelector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper CheckIfContainsText();
        IElementWrapper CheckIfDoesNotContainsText();
        IElementWrapper CheckIfHasAttribute(string name);
        IElementWrapper CheckIfHasClass(string value, bool caseInsensitive = false);
        IElementWrapper CheckIfHasNotAttribute(string name);
        IElementWrapper CheckIfHasNotClass(string value, bool caseInsensitive = false);
        IElementWrapper CheckIfHyperLinkEquals(string url, UrlKind kind, params UriComponents[] components);
        IElementWrapper CheckIfInnerText(Func<string, bool> expression, string failureMessage = null);
        IElementWrapper CheckIfInnerTextEquals(string text, bool caseSensitive = true, bool trim = true);
        IElementWrapper CheckIfIsChecked();
        IElementWrapper CheckIfIsClickable();
        IElementWrapper CheckIfIsDisplayed();
        IElementWrapper CheckIfIsElementInView(IElementWrapper element);
        IElementWrapper CheckIfIsElementNotInView(IElementWrapper element);
        IElementWrapper CheckIfIsEnabled();
        IElementWrapper CheckIfIsNotChecked();
        IElementWrapper CheckIfIsNotClickable();
        IElementWrapper CheckIfIsNotDisplayed();
        IElementWrapper CheckIfIsNotEnabled();
        IElementWrapper CheckIfIsNotSelected();
        IElementWrapper CheckIfIsSelected();
        IElementWrapper CheckIfJsPropertyInnerHtml(Func<string, bool> expression, string failureMessage = null);
        IElementWrapper CheckIfJsPropertyInnerHtmlEquals(string text, bool caseSensitive = true, bool trim = true);
        IElementWrapper CheckIfJsPropertyInnerText(Func<string, bool> expression, string failureMesssage = null, bool trim = true);
        IElementWrapper CheckIfJsPropertyInnerTextEquals(string text, bool caseSensitive = true, bool trim = true);
        IElementWrapper CheckIfNotContainsElement(string cssSelector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper CheckIfTagName(Func<string, bool> expression, string failureMessage = null);
        IElementWrapper CheckIfTagName(string expectedTagName, string failureMessage = null);
        IElementWrapper CheckIfTagName(string[] expectedTagNames, string failureMessage = null);
        IElementWrapper CheckIfText(Func<string, bool> expression, string failureMessage = null);
        IElementWrapper CheckIfTextEquals(string text, bool caseSensitive = true, bool trim = true);
        IElementWrapper CheckIfTextNotEquals(string text, bool caseSensitive = true, bool trim = true);
        IElementWrapper CheckIfValue(string value, bool caseInsensitive = false, bool trimValue = true);
        IElementWrapper CheckTagName(Func<string, bool> expression, string failureMessage = null);
        IElementWrapper CheckTagName(string expectedTagName, string failureMessage = null);
        IElementWrapper Clear();
        IElementWrapper Click();
        IElementWrapper DropTo(IElementWrapper dropToElement, int offsetX = 0, int offsetY = 0);
        IElementWrapper ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null);
        IElementWrapperCollection FindElements(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        string GetAttribute(string name);
        string GetInnerText();
        string GetJsElementPropertyValue(string elementPropertyName);
        string GetJsInnerHtml();
        string GetJsInnerText(bool trim = true);
        string GetTagName();
        string GetText();
        string GetValue();
        bool HasAttribute(string name);
        bool HasCssClass(string cssClass);
        bool IsClickable();
        bool IsDisplayed();
        bool IsDisplayedAndHasSizeGreaterThanZero();
        bool IsElementInView(IElementWrapper element);
        bool IsEnabled();
        bool IsSelected();
        IElementWrapper Last(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper PerformActionOnSelectElement(Action<SelectElement> process);
        IElementWrapper ScrollTo(IElementWrapper element);
        IElementWrapper Select(Action<SelectElement> process);
        IElementWrapper Select(int index);
        IElementWrapper Select(string value);
        IElementWrapper SendEnterKey();
        IElementWrapper SendKeys(string text);
        void SetBrowserSelectMethod();
        void SetCssSelectMethod();
        IElementWrapper SetJsElementProperty(string propertyName, object propertyValue);
        IElementWrapper Single(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper Submit();
        T ThrowIfIsNull<T>(T obj, string message);
        IElementWrapper Wait();
        IElementWrapper Wait(int milliseconds);
        IElementWrapper Wait(TimeSpan interval);
        IElementWrapper WaitFor(Action<IElementWrapper> checkExpression, int maxTimeout, string failureMessage, int checkInterval = 500);
        IElementWrapper WaitFor(Func<IElementWrapper, bool> condition, int maxTimeout, string failureMessage, bool ignoreCertainException = true, int checkInterval = 500);
    }
}