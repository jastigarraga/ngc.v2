
using System;
using System.Linq.Expressions;

namespace NGC.Common.Classes.Filters
{
    public interface IMerakiFilter<T> where T : class
    {
        Expression<Func<T,bool>> FilterExpression { get; }
    }
}
