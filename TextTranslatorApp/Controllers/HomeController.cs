using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TextTranslatorApp.OnException;
using TextTranslatorApp.Service;

namespace TextTranslatorApp.Controllers
{
    /// <summary>
    ///This is homecontroller which contains method to generate initial view and methods which calls to businessleyer translation method.
    ///Here concept of inheritance implemented.
    /// </summary>
    public class HomeController : Controller
    {
        public Itranslator _translator;
        public HomeController(Itranslator translator)
        {
            _translator = translator;
        }

        /// <summary>
        /// Home index, initial view
        /// </summary>
        /// <returns>view-index</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lngCode"></param>
        /// <param name="inputText"></param>
        /// <returns>Json</returns>
        [HttpPost]
        [OnExceptions]
        public async Task<JsonResult> TranslationAsync(string lngCode, string inputText)
        {
            return Json(await _translator.Trans(inputText, lngCode));
        }
    }
}