using System;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    public class AlertTextEqualsValidator : IValidator<IBrowserWrapper>
    {
        private readonly string expectedValue;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public AlertTextEqualsValidator(string expectedValue, bool caseSensitive = false, bool trim = true)
        {
            this.expectedValue = expectedValue;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var alert = wrapper.GetAlert();
            string alertText = alert.Text;
            var tempExpectedValue = expectedValue;
            if (trim)
            {
                alertText = alert.Text?.Trim();
                tempExpectedValue = expectedValue.Trim();
            }

            var isSucceeded = string.Equals(alertText, tempExpectedValue,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Alert does not contain expected value. Expected value: '{tempExpectedValue}', provided value: '{alertText}'");
        }
    }
}