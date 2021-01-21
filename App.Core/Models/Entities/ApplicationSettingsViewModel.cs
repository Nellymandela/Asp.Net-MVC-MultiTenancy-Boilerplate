namespace App.Core.Models.Entities
{
    public class ApplicationSettingsViewModel
    {
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
    }
}
