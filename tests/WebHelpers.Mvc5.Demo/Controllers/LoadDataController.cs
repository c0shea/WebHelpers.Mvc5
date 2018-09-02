using System.Web.Mvc;

namespace WebHelpers.Mvc5.Demo.Controllers
{
    public class LoadDataController : Controller
    {
        public ActionResult Json()
        {
            return View();
        }

        public ActionResult JsonData()
        {
            return new JsonNetResult(new {key = "value"});
        }

        public ActionResult Array()
        {
            return View();
        }
    }
}