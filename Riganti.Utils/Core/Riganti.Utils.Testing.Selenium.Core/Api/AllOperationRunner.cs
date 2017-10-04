﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public class AllOperationRunner<T> : OperationRunnerBase<T>
    {
        private readonly T[] wrappers;

        public AllOperationRunner(T[] wrappers)
        {
            this.wrappers = wrappers;
        }

        public override void Evaluate<TException>(ICheck<T> check)
        {
            var checkResults = wrappers.Select(check.Validate).ToArray();
            var checkResult = CreateCheckResult(checkResults);
            EvaluateResult<TException>(checkResult);
        }

        private static CheckResult CreateCheckResult(CheckResult[] checkResults)
        {
            var isSucceeded = checkResults.All(result => result.IsSucceeded);
            if (isSucceeded)
            {
                return CheckResult.Succeeded;
            }
            return new CheckResult(
                $"The check doesn't match on all elements. See {nameof(CheckResult.InnerResults)} for more details",
                checkResults);
        }
    }
}