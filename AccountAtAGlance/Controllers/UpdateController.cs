using System.Web.Mvc;
using AccountAtAGlance.Repository;
using AccountAtAGlance.Repository.Helpers;
using Microsoft.Practices.Unity;

namespace AccountAtAGlance.Controllers
{
    public class UpdateController : Controller
    {
        IAccountRepository _AccountRepository;
        ISecurityRepository _SecurityRepository;
        IMarketsAndNewsRepository _MarketRepository;
        IStockEngine _StockEngine;

        public UpdateController(IAccountRepository acctRepo, ISecurityRepository secRepo, IMarketsAndNewsRepository marketRepo)
        {
            _AccountRepository = acctRepo;
            _SecurityRepository = secRepo;
            _MarketRepository = marketRepo;
        }

        public ActionResult Index()
        {
            var opStatus = _SecurityRepository.InsertSecurityData();

            if (opStatus.Status)
            {
                opStatus = _MarketRepository.InsertMarketData();

                if (opStatus.Status)
                {
                    opStatus = _AccountRepository.RefreshAccountsData();
                }
            }

            ViewBag.OperationStatus = opStatus;

            return View();
        }

    }
}
