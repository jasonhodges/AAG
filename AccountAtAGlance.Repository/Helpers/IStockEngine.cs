using AccountAtAGlance.Model;
using System;
using System.Collections.Generic;
namespace AccountAtAGlance.Repository.Helpers
{
    public interface IStockEngine
    {
        List<MarketIndex> GetMarketQuotes(params string[] symbols);
        List<Security> GetSecurityQuotes(params string[] symbols);
    }
}
