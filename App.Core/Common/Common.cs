using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Common
{
    public static class Common
    {
        public static string ConvertByteToBase64String(byte[] data, string type = null)
        {
            type = type ?? "image/gif";
            if (data != null && data.Length > 0)
            {
                string byteString = Convert.ToBase64String(data);
                return string.Format("data:{0};base64,{1}", type, byteString);
            }
            return null;
        }
    }
}
