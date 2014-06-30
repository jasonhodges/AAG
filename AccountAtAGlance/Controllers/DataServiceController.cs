using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AccountAtAGlance.Model;
using AccountAtAGlance.Repository;
using Microsoft.Practices.Unity;

namespace AccountAtAGlance.Controllers
{
    public class DataServiceController : ApiController
    {
        IAccountRepository _AccountRepository;
        ISecurityRepository _SecurityRepository;
        IMarketsAndNewsRepository _MarketRepository;


        public DataServiceController(IAccountRepository acctRepo, ISecurityRepository secRepo, IMarketsAndNewsRepository marketRepo)
        {
            _AccountRepository = acctRepo;
            _SecurityRepository = secRepo;
            _MarketRepository = marketRepo;
        }

        [HttpGet]
        public BrokerageAccount Account(string acctNumber)
        {
            var acct = _AccountRepository.GetAccount(acctNumber);
            if (acct == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return acct;
        }


        [HttpGet]
        public Security Quote(string symbol)
        {
            var security = _SecurityRepository.GetSecurity(symbol);
            if (security == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return security;
        }

        [HttpGet]
        public MarketQuotes MarketIndices()
        {
            var marketQuotes = _MarketRepository.GetMarkets();
            if (marketQuotes == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return marketQuotes;
        }

        [HttpGet]
        public MarketsAndNews TickerQuotes()
        {
            var marketQuotes = _MarketRepository.GetMarketTickerQuotes();
            var securityQuotes = _SecurityRepository.GetSecurityTickerQuotes();
            marketQuotes.AddRange(securityQuotes);
            var news = _MarketRepository.GetMarketNews();

            if (marketQuotes == null && securityQuotes == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return new MarketsAndNews { Markets = marketQuotes, News = news };
        }

        //Sample method - not actually used in the application
        public HttpResponseMessage PostAccount(BrokerageAccount acct)
        {
            var opStatus = _AccountRepository.InsertAccount(acct);

            if (!opStatus.Status)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            //Generate success response
            var response = Request.CreateResponse<BrokerageAccount>(HttpStatusCode.Created, acct);
            string uri = Url.Link("DefaultApi", new { id = acct.AccountNumber });
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}
