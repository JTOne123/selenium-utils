using System;
using System.Net;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    public class UrlIsAccessibleValidator : IValidator<IBrowserWrapper>
    {
        private readonly string url;
        private readonly UrlKind urlKind;

        public UrlIsAccessibleValidator(string url, UrlKind urlKind)
        {
            this.url = url;
            this.urlKind = urlKind;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var currentUri = new Uri(wrapper.CurrentUrl);
            var tempUrl = url;

            if (urlKind == UrlKind.Relative)
            {
                tempUrl = wrapper.GetAbsoluteUrl(tempUrl);
            }

            if (urlKind == UrlKind.Absolute && tempUrl.StartsWith("//"))
            {
                if (!string.IsNullOrWhiteSpace(currentUri.Scheme))
                {
                    tempUrl = currentUri.Scheme + ":" + tempUrl;
                }
            }

            HttpWebResponse response = null;
            //($"UrlIsAccessibleValidator: Checking of url: '{tempUrl}'", 10);
            var request = (HttpWebRequest)WebRequest.Create((string) tempUrl);
            request.Method = "HEAD";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                return new CheckResult($"Unable to access {tempUrl}! {e.Status}");
            }
            finally
            {
                response?.Close();
            }

            return CheckResult.Succeeded;
        }
    }
}