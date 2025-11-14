using System;
using System.ComponentModel.DataAnnotations;

namespace OgrenciYonetim.Models
{
    /// <summary>
    /// Kitap entity sınıfı
    /// </summary>
    public class Kitap
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Kitap adı zorunludur")]
        [StringLength(200, ErrorMessage = "Kitap adı en fazla 200 karakter olabilir")]
        [Display(Name = "Kitap Adı")]
        public string kitap_adi { get; set; }

        [Required(ErrorMessage = "Yazar adı zorunludur")]
        [StringLength(100, ErrorMessage = "Yazar adı en fazla 100 karakter olabilir")]
        [Display(Name = "Yazar")]
        public string yazar { get; set; }

        [StringLength(20, ErrorMessage = "ISBN en fazla 20 karakter olabilir")]
        [Display(Name = "ISBN")]
        public string isbn { get; set; }

        [Display(Name = "Yayın Yılı")]
        public int? yil { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime kayit_tarihi { get; set; }
    }
}

