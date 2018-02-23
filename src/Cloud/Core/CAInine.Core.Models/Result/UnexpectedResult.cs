using System;
using System.Collections.Generic;

namespace CAInine.Core.Models.Results
{
    /// <summary>
    /// Unexpected result.
    /// </summary>
    public class UnexpectedResult<T> : Result<T>
    {
        public override ResultType Type => ResultType.Unexpected;

        public override List<string> Errors => new List<string> { "There was an unexpected problem" };

        public override T Data => default(T);
    }
}
