using System;
using System.ComponentModel.DataAnnotations;

namespace OgrenciYonetim.Models
{
    /// <summary>
    /// Öğrenci entity sınıfı
    /// </summary>
    public class Ogrenci
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Öğrenci numarası zorunludur")]
        [StringLength(20, ErrorMessage = "Öğrenci numarası en fazla 20 karakter olabilir")]
        [Display(Name = "Öğrenci Numarası")]
        public string Numara { get; set; }

        [Required(ErrorMessage = "Ad zorunludur")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Bölüm zorunludur")]
        [StringLength(100, ErrorMessage = "Bölüm en fazla 100 karakter olabilir")]
        [Display(Name = "Bölüm")]
        public string Bolum { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime Kayit_Tarihi { get; set; }

        /// <summary>
        /// Tam adı döndürür
        /// </summary>
        public string TamAd
        {
            get { return Ad + " " + Soyad; }
        }
    }
}

