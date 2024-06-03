using System;
using System.Collections.Generic;

namespace TestSolution.DataProviders
{
    public interface IDataProvider
    {
        IEnumerable<string> GetSimpleData();
    }
}