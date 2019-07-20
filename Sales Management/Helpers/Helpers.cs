using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace zUtilities
{
    class Helpers
    {
        public static string GetProductID(string drive)//use:: getUniqueID("C");
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }
            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = HardwareOp.HDDSerialNo(drive);
            string cpuID = HardwareOp.CpuId();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        public static string GetProductID()
        {
            return GetHash(
                "CPU >> " + HardwareOp.CpuId() +
                "\nBIOS >> " + HardwareOp.BiosId() +
                "\nBASE >> " + HardwareOp.BaseBoardId() +
                "\nDISK >> " + HardwareOp.DiskId() +
                "\nVIDEO >> " + HardwareOp.VideoId() +
                "\nMAC >> " + HardwareOp.MacId());
        }

        public static string GetHexString(IList<byte> bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Count; i++)
            {
                byte b = bt[i];
                int n = b;
                int n1 = n & 15;
                int n2 = (n >> 4) & 15;

                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n1.ToString(CultureInfo.InvariantCulture);

                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n2.ToString(CultureInfo.InvariantCulture);


                if ((i + 1) != bt.Count && (i + 1) % 2 == 0)
                    s += "-";
            }
            return s;
        }

        public static string GetHash(string s)
        {
            //Initialize a new MD5 Crypto Service Provider in order to generate a hash
            MD5 sec = new MD5CryptoServiceProvider();
            //Grab the bytes of the variable 's'
            byte[] bt = Encoding.ASCII.GetBytes(s);
            //Grab the Hexadecimal value of the MD5 hash
            return GetHexString(sec.ComputeHash(bt));
        }

       
    }
}
