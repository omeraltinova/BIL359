using System;
using System.Web.Mvc;
using OgrenciYonetim.Models;

namespace OgrenciYonetim.Controllers
{
    /// <summary>
    /// Kitap yönetim controller'ı
    /// Tüm CRUD işlemleri IActionResult ile implement edilmiştir
    /// </summary>
    public class KitapController : Controller
    {
        private readonly KitapDB db = new KitapDB();

        /// <summary>
        /// Admin kontrolü
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Admin kontrolü
            if (Session["yetki"] == null || Session["yetki"].ToString() != "admin")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Auth", action = "Login" }
                    )
                );
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Ana sayfa - Kitap listesi
        /// GET: /Kitap/Index
        /// </summary>
        public ActionResult Index()
        {
            try
            {
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
        /// Yeni kitap ekleme formu
        /// GET: /Kitap/Create
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Yeni kitap ekleme işlemi
        /// POST: /Kitap/Create
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kitap kitap)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool sonuc = db.Insert(kitap);
                    
                    if (sonuc)
                    {
                        TempData["Basari"] = "Kitap başarıyla eklendi!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kitap eklenirken bir hata oluştu.");
                    }
                }

                return View(kitap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(kitap);
            }
        }

        /// <summary>
        /// Kitap düzenleme formu
        /// GET: /Kitap/Edit/5
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "ID parametresi gerekli");
            }

            try
            {
                Kitap kitap = db.GetById(id.Value);
                
                if (kitap == null)
                {
                    return HttpNotFound("Kitap bulunamadı");
                }

                return View(kitap);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Kitap düzenleme işlemi
        /// POST: /Kitap/Edit/5
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kitap kitap)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool sonuc = db.Update(kitap);
                    
                    if (sonuc)
                    {
                        TempData["Basari"] = "Kitap başarıyla güncellendi!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kitap güncellenirken bir hata oluştu.");
                    }
                }

                return View(kitap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(kitap);
            }
        }

        /// <summary>
        /// Kitap silme onay sayfası
        /// GET: /Kitap/Delete/5
        /// </summary>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "ID parametresi gerekli");
            }

            try
            {
                Kitap kitap = db.GetById(id.Value);
                
                if (kitap == null)
                {
                    return HttpNotFound("Kitap bulunamadı");
                }

                return View(kitap);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Kitap silme işlemi
        /// POST: /Kitap/Delete/5
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                bool sonuc = db.Delete(id);
                
                if (sonuc)
                {
                    TempData["Basari"] = "Kitap başarıyla silindi!";
                }
                else
                {
                    TempData["Hata"] = "Kitap silinirken bir hata oluştu.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Hata"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}

