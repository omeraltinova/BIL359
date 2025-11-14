using System;
using System.ComponentModel.DataAnnotations;

namespace OgrenciYonetim.Models
{
    /// <summary>
    /// Kullanıcı entity sınıfı
    /// </summary>
    public class Kullanici
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir")]
        [Display(Name = "Kullanıcı Adı")]
        public string kulad { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [StringLength(50, ErrorMessage = "Şifre en fazla 50 karakter olabilir")]
        [Display(Name = "Şifre")]
        public string sifre { get; set; }

        [Display(Name = "Yetki")]
        public string yetki { get; set; }

        [Display(Name = "Durum")]
        public string durum { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime kayit_tarihi { get; set; }
    }
}

