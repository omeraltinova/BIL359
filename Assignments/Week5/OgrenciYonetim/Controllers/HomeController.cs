using System;
using System.Web.Mvc;

namespace OgrenciYonetim.Controllers
{
    /// <summary>
    /// Ana sayfa controller'ı
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Ana sayfa (herkese açık)
        /// GET: /Home/Index
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// Kitaplar sayfası (herkese açık - sadece görüntüleme)
        /// GET: /Home/Kitaplar
        /// </summary>
        public ActionResult Kitaplar()
        {
            try
            {
                var db = new Models.KitapDB();
                var kitaplar = db.GetAll();
                return View(kitaplar);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Hakkında sayfası
        /// GET: /Home/About
        /// </summary>
        public ActionResult About()
        {
            return View();
        }
    }
}

