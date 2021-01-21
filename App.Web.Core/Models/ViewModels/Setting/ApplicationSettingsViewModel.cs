using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Web.Core.Models.Entities.Setting
{
    public class ApplicationSettingsViewModel
    {
        [Required(ErrorMessage = "You must enter a Name")]
        public string Name { get; set; }
        public string Url { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Copyright { get; set; }
        public string Code { get; set; }
        public byte[] LogoData { get; set; }
        public byte[] FaviconData { get; set; }
        public string FaviconUrl { get; set; }
        public string LogoType { get; set; }
        public string FaviconType { get; set; }
        public HttpPostedFileBase Logo { get; set; }
        public HttpPostedFileBase Favicon { get; set; }
    }
}
