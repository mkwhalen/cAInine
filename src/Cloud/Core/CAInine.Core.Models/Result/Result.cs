using System;
using System.Collections.Generic;

namespace CAInine.Core.Models.Results
{
    /// <summary>
    /// Result model to contain data, result type, and errors
    /// </summary>
    public abstract class Result<T>
    {
        public abstract ResultType Type { get; }
        public abstract List<string> Errors { get; }
        public abstract T Data { get; }
    }
}
