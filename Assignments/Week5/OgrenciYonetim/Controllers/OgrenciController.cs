using System;
using System.Web.Mvc;
using OgrenciYonetim.Models;

namespace OgrenciYonetim.Controllers
{
    /// <summary>
    /// Öğrenci yönetim controller'ı
    /// Tüm CRUD işlemleri IActionResult ile implement edilmiştir
    /// </summary>
    public class OgrenciController : Controller
    {
        private readonly OgrenciDB db = new OgrenciDB();

        /// <summary>
        /// Ana sayfa - Öğrenci listesi
        /// GET: /Ogrenci/Index
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                var ogrenciler = db.GetAll();
                ViewBag.ToplamOgrenci = ogrenciler.Count;
                return View(ogrenciler);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Öğrenci detayları
        /// GET: /Ogrenci/Details/5
        /// </summary>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "ID parametresi gerekli");
            }

            try
            {
                Ogrenci ogrenci = db.GetById(id.Value);
                
                if (ogrenci == null)
                {
                    return HttpNotFound("Öğrenci bulunamadı");
                }

                return View(ogrenci);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Yeni öğrenci ekleme formu
        /// GET: /Ogrenci/Create
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Yeni öğrenci ekleme işlemi
        /// POST: /Ogrenci/Create
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ogrenci ogrenci)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Numara kontrolü
                    if (db.NumaraExists(ogrenci.Numara))
                    {
                        ModelState.AddModelError("Numara", "Bu öğrenci numarası zaten kayıtlı!");
                        return View(ogrenci);
                    }

                    bool sonuc = db.Insert(ogrenci);
                    
                    if (sonuc)
                    {
                        TempData["Basari"] = "Öğrenci başarıyla eklendi!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Öğrenci eklenirken bir hata oluştu.");
                    }
                }

                return View(ogrenci);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ogrenci);
            }
        }

        /// <summary>
        /// Öğrenci düzenleme formu
        /// GET: /Ogrenci/Edit/5
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "ID parametresi gerekli");
            }

            try
            {
                Ogrenci ogrenci = db.GetById(id.Value);
                
                if (ogrenci == null)
                {
                    return HttpNotFound("Öğrenci bulunamadı");
                }

                return View(ogrenci);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Öğrenci düzenleme işlemi
        /// POST: /Ogrenci/Edit/5
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ogrenci ogrenci)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Numara kontrolü (kendi ID'si hariç)
                    if (db.NumaraExists(ogrenci.Numara, ogrenci.ID))
                    {
                        ModelState.AddModelError("Numara", "Bu öğrenci numarası başka bir öğrenciye ait!");
                        return View(ogrenci);
                    }

                    bool sonuc = db.Update(ogrenci);
                    
                    if (sonuc)
                    {
                        TempData["Basari"] = "Öğrenci başarıyla güncellendi!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Öğrenci güncellenirken bir hata oluştu.");
                    }
                }

                return View(ogrenci);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ogrenci);
            }
        }

        /// <summary>
        /// Öğrenci silme onay sayfası
        /// GET: /Ogrenci/Delete/5
        /// </summary>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "ID parametresi gerekli");
            }

            try
            {
                Ogrenci ogrenci = db.GetById(id.Value);
                
                if (ogrenci == null)
                {
                    return HttpNotFound("Öğrenci bulunamadı");
                }

                return View(ogrenci);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Öğrenci silme işlemi
        /// POST: /Ogrenci/Delete/5
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
                    TempData["Basari"] = "Öğrenci başarıyla silindi!";
                }
                else
                {
                    TempData["Hata"] = "Öğrenci silinirken bir hata oluştu.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Hata"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Bölüm istatistikleri (bonus özellik)
        /// GET: /Ogrenci/Stats
        /// </summary>
        public ActionResult Stats()
        {
            try
            {
                var stats = db.GetStatsByBolum();
                return View(stats);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Controller dispose
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Cleanup işlemleri buraya eklenebilir
            }
            base.Dispose(disposing);
        }
    }
}

