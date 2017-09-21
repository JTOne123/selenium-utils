using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsEnabled : ICheck<IElementWrapper>
    {
        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsEnabled();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not enabled. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}