using Microsoft.AspNetCore.Mvc;

namespace DocOffice.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}