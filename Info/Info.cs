using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Resources;
using System.Windows;

namespace FishRandomSelector.Info
{
    static class Info
    {
        public static string Version = "1.0.0.0";
        public static string Productname = "FishRandomSelector1.0";
        public static string VerifyLink = @"http://www.liziyu0714.tk/Fish/FishRandomSelector/FishRandomSelector1.0";
        public static string GetLicense ()
        {
            byte[] LicenseText_byte = new byte[100 * 1024 * 1024];
            string LicenseText = null;
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("LICENSE", UriKind.Relative));
            UnmanagedMemoryStream stream = (UnmanagedMemoryStream)sri.Stream;
            int tmp = 0 , pos = 0;
            tmp = stream.ReadByte();
            while(tmp!=-1)
            {
                LicenseText_byte[pos++] = (byte)tmp;
                tmp = stream.ReadByte();
            }
            LicenseText = System.Text.Encoding.UTF8.GetString(LicenseText_byte);
            return LicenseText;
        }
        public static string GetEULA()
        {
            byte[] EULAText_byte = new byte[100 * 1024 * 1024];
            string EULAText = null;
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("EULA", UriKind.Relative));
            UnmanagedMemoryStream stream = (UnmanagedMemoryStream)sri.Stream;
            int tmp = 0, pos = 0;
            tmp = stream.ReadByte();
            while (tmp != -1)
            {
                EULAText_byte[pos++] = (byte)tmp;
                tmp = stream.ReadByte();
            }
            EULAText = System.Text.Encoding.UTF8.GetString(EULAText_byte);
            return EULAText;
        }
    }
}
