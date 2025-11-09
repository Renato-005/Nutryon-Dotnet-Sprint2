
using Microsoft.AspNetCore.Mvc;

namespace Nutryon.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
