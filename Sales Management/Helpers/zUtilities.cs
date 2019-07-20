using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Management;
using System.Globalization;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;


namespace zUtilities
{

    #region Generator Operations
    public static class KeyGeneratorOp
    {
        public static Random random = new Random();
        public enum RandomType { Numbers, Characters, All }
        public static string GetUniqueKey(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">
        /// Number Of String 
        /// </param>
        /// <param name="randomType">
        /// All: All Charcaters AZ-az and Numbers, 
        /// Numbers: Numbers Only, 
        /// Characters: Characters Only 
        /// </param>
        /// <returns></returns>
        public static string GetUniqueKey(int length, RandomType randomType)
        {
            const string all = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";

            string strReturn;

            if (randomType == RandomType.All)
                strReturn = new string(Enumerable.Repeat(all, length).Select(s => s[random.Next(s.Length)]).ToArray());
            else if (randomType == RandomType.Characters)
                strReturn = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            else
                strReturn = new string(Enumerable.Repeat(numbers, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return strReturn;
        }

        public static string GetUniqueKey2(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        //public static string GetUniqueKey3(int length)
        //{
        //    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //    char[] stringChars = new char[length];
        //    string finalString;
        //    var random = new Random();

        //    for (int i = 0; i < stringChars.Length; i++)
        //    {
        //        stringChars[i] = chars[random.Next(chars.Length)];
        //    }

        //    return finalString = new String(stringChars);
        //}
    }
    #endregion


    #region DecryptEncrypt Operations
    public static class DecryptEncryptOp
    {

    }
    #endregion


    #region Services Operations
    public static class ServicesOp
    {
        #region "=-=-=-= =-=-=-= =-=-=-= HDD Serial =-=-=-= =-=-=-= =-=-=-="
        public static string GetHDSerial()
        {
            string sn = "";
            ManagementObjectSearcher mgmt =
                new ManagementObjectSearcher("select * from win32_DiskDrive where DeviceID='\\\\.\\PHYSICALDRIVE1'");
            foreach (var obj in mgmt.Get())
            {
                sn = obj["SerialNumber"].ToString().Trim();
            }
            return sn;
        }
        #endregion

        #region "=-=-=-= =-=-=-= =-=-=-= GetActivationKey =-=-=-= =-=-=-= =-=-=-="
        public static string GetActivationKey(string value)
        {
            MD5CryptoServiceProvider md5;
            using (md5 = new MD5CryptoServiceProvider())
            {
                Byte[] result = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(value));
                string str = Convert.ToBase64String(result);
                str = Regex.Replace(str, @"[`~!@#\$%\^&\*\(\)_\-\+=\{\}\[\]\\\|:;""'<>,\.\?/]", String.Empty);
                str = (str.Substring(0, 5).ToUpper() + "-" + str.Substring(5, 5) + "-" +
                         str.Substring(10, 5) + "-" + str.Substring(15, 5));
                return str;
            }
        }
        public static string GetActivationKey2(string value)
        {
            string str = Regex.Replace(Encrypt(value),
                @"[`~!@#\$%\^&\*\(\)_\-\+=\{\}\[\]\\\|:;""'<>,\.\?/]", String.Empty);
            str = (str.Substring(0, 5).ToUpper() + "-" + str.Substring(5, 5) + "-" +
                     str.Substring(10, 5) + "-" + str.Substring(15, 5));
            return str;
        }
        #endregion


        #region "=-=-=-= =-=-=-= =-=-=-= Encrypt =-=-=-= =-=-=-= =-=-=-="
        public static string Encrypt(string value)
        {
            MD5CryptoServiceProvider md5;
            using (md5 = new MD5CryptoServiceProvider())
            {
                Byte[] result = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(value));
                return Convert.ToBase64String(result);
            }
        }
        #endregion


    }
    #endregion


    #region Databases Operations
    public static class DatabasesOp
    {
        static SqlCommand cmd;
        static SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");
        public static void CreateDatabase(string databaseName, string scriptFile)
        {
            cmd = new SqlCommand("select 1 from sys.Databases Where name = '" + databaseName + "'");
            cmd.Connection = conn;
            conn.Open();
            if (cmd.ExecuteNonQuery() != 1)
            {

                try
                {
                    var file = System.IO.File.ReadAllText(scriptFile);
                    var sqlQuery = file.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                    cmd = new SqlCommand();
                    foreach (var query in sqlQuery)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            conn.Close();
        }
    }
    #endregion


    #region Files Operations
    public static class FilesOp
    {
        //using (var sr = new StreamReader(Application.StartupPath + @"\sqlscript.sql", Encoding.UTF8))
        public static string ReadAllText(String path)
        {
            using (var sReader = new StreamReader(path, Encoding.UTF8))
            {
                return sReader.ReadToEnd();
            }
        }
    }
    #endregion


    #region Security Operations
    public static class SecurityOp
    {
        public static string MD5Hash(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
            byte[] result = md5.Hash;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));//It formats the string as two uppercase hexadecimal characters.
            }
            string sn = sb.ToString();
            string serial = sn.Substring(0, 5) + "-" + sn.Substring(5, 5) + "-" + sn.Substring(10, 5) + "-" + sn.Substring(15, 5) + "-" + sn.Substring(20, 5);
            return serial.ToString();
        }

        public static void SendEmail(string from, string to, string subject, string body, string displayName)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(from, displayName);
            msg.To.Add(to);
            msg.Subject = subject;
            msg.Body = body;

            string[] Titles = { "dndshafndy07@gmail.com", "hussainafndy@gmail.com" };
            string rand_email = Titles[new Random().Next(0, Titles.Length)];

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(rand_email, "2ccd*44cc");
            smtp.EnableSsl = true;

            new Thread(() => smtp.Send(msg)).Start();
        }
        public static void SendEmail(string from, string to, string subject, string body, string displayName, string email, string password)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(from, displayName);
            msg.To.Add(to);
            msg.Subject = subject;
            msg.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(email, password);
            smtp.EnableSsl = true;

            new Thread(() =>
            {
                smtp.Send(msg);
            }).Start();
        }

        #region "MD5_UTF"
        public enum UTF { ASCII, Unicode, UTF7, UTF8, UTF32 }
        public static string MD5_UTF(string value, UTF utf)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //md5.ComputeHash(ASCIIEncoding.UTF32.GetBytes(value));
            switch (utf)
            {
                case UTF.Unicode:
                    md5.ComputeHash(ASCIIEncoding.Unicode.GetBytes(value));
                    break;
                case UTF.UTF8: // ASCII
                    md5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(value));
                    break;
                case UTF.UTF32:
                    md5.ComputeHash(ASCIIEncoding.UTF32.GetBytes(value));
                    break;
                default:
                    md5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(value));
                    break;
            }


            byte[] result = md5.Hash;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }
            string sn = sb.ToString();
            string serial = sn.Substring(0, 5) + "-" + sn.Substring(5, 5) + "-" + sn.Substring(10, 5) + "-" + sn.Substring(15, 5) + "-" + sn.Substring(20, 5);
            return serial.ToString();
        }
        #endregion

        #region SendEmail Not Needed
        /*
        public static void SendEmail(string from, string to, string subject, string body)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("hussainafndy@gmail.com", "2ccd*44cc");
            smtp.EnableSsl = true;
            smtp.Send(from, to, subject, body);
        }
        public static void SendEmail(string from, string to, string subject, string body, string email, string password)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(email, password);
            smtp.EnableSsl = true;
            smtp.Send(from, to, subject, body);
        }
        */
        #endregion
    }
#endregion


    #region String Operations
    public static class StringOp
    {
        /// <summary>
        /// Reverse String Letter by Letter
        /// </summary>
        /// <param name="stringWords"></param>
        /// <returns></returns>
        public static string ReverseLetters(string stringWords)
        {
            string reversestring = "";
            int length;
            var stringInput = stringWords;
            length = stringInput.Length - 1;
            while (length >= 0)
            {
                reversestring += stringInput[length];
                length--;
            }
            return reversestring;
        }

        
        /// <summary>
        /// Reverse String Words by Words
        /// </summary>
        /// <param name="stringWords"></param>
        /// <returns></returns>
        public static string ReverseWords(string stringWords)
        {
            return stringWords.Split(' ').Aggregate((a, b) => b + " " + a);
        }

        /// <summary>
        /// Reverse String by Letters or Words
        /// </summary>
        /// <param name="stringWords"></param>
        /// <param name="letters"></param>
        /// <returns></returns>
        public static string Reverse(string stringWords,bool letters)
        {
            if (letters)
                return new string(stringWords.Reverse().ToArray());
            else
                return stringWords.Split(' ').Aggregate((a, b) => b + " " + a);
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
                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n2.ToString(CultureInfo.InvariantCulture);
                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n1.ToString(CultureInfo.InvariantCulture);
                if ((i + 1) != bt.Count && (i + 1) % 2 == 0) s += "-";
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
    #endregion


    #region Hardware Operations
    public static class HardwareOp
    {

        /// <summary>
        /// Retrieving Motherboard Manufacturer.
        /// </summary>
        /// <returns></returns>
        public static string BoardMaker()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }

                catch { }

            }

            return "Board Maker: Unknown";

        }
        /// <summary>
        /// Retrieving Motherboard Product Id.
        /// </summary>
        /// <returns></returns>
        public static string BoardProductId()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Product").ToString();

                }

                catch { }

            }

            return "Product: Unknown";

        }
        public static string BaseBoardId()
        {
            return Identifier("Win32_BaseBoard", "Model") + Identifier("Win32_BaseBoard", "Manufacturer") + Identifier("Win32_BaseBoard", "Name") + Identifier("Win32_BaseBoard", "SerialNumber");
        }


        /// <summary>
        /// Retrieving BIOS Maker.
        /// </summary>
        /// <returns></returns>
        public static string BIOSmaker()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }
                catch { }
            }
            return "BIOS Maker: Unknown";
        }
        public static string BiosId()
        {
            return Identifier("Win32_BIOS", "Manufacturer") + Identifier("Win32_BIOS", "SMBIOSBIOSVersion") + Identifier("Win32_BIOS", "IdentificationCode") + Identifier("Win32_BIOS", "SerialNumber") + Identifier("Win32_BIOS", "ReleaseDate") + Identifier("Win32_BIOS", "Version");
        }
        /// <summary>
        /// Retrieving BIOS Serial No.
        /// </summary>
        /// <returns></returns>
        public static string BIOSserNo()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("SerialNumber").ToString();

                }

                catch { }

            }

            return "BIOS Serial Number: Unknown";

        }
        /// <summary>
        /// Retrieving BIOS Caption.
        /// </summary>
        /// <returns></returns>
        public static string BIOScaption()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Caption").ToString();

                }
                catch { }
            }
            return "BIOS Caption: Unknown";
        }


        /// <summary>
        /// Retrieving Processor Identifier
        /// </summary>
        /// <returns></returns>
        /// 
        public static string CpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string cpuInfo = Identifier("Win32_Processor", "UniqueId");
            if (cpuInfo != "") return cpuInfo;
            cpuInfo = Identifier("Win32_Processor", "ProcessorId");
            if (cpuInfo != "") return cpuInfo;
            cpuInfo = Identifier("Win32_Processor", "Name");
            if (cpuInfo == "") //If no Name, use Manufacturer
            {
                cpuInfo = Identifier("Win32_Processor", "Manufacturer");
            }
            //Add clock speed for extra security
            cpuInfo += Identifier("Win32_Processor", "MaxClockSpeed");
            return cpuInfo;
        }
        public static string ProcessorId()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            String Id = String.Empty;
            foreach (ManagementObject mo in moc)
            {

                Id = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return Id;
        }
        /// <summary>
        /// Retrieving Processor Information.
        /// </summary>
        /// <returns></returns>
        public static string ProcessorInformation()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                string name = (string)mo["Name"];
                name = name.Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®").Replace("(C)", "©").Replace("(c)", "©").Replace("    ", " ").Replace("  ", " ");

                info = name + ", " + (string)mo["Caption"] + ", " + (string)mo["SocketDesignation"];
                //mo.Properties["Name"].Value.ToString();
                //break;
            }
            return info;
        }
        //Get CPU Temprature.
        /// <summary>
        /// method for retrieving the CPU Manufacturer
        /// using the WMI class
        /// </summary>
        /// <returns>CPU Manufacturer</returns>
        public static string CPUManufacturer()
        {
            string cpuMan = String.Empty;
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection objCol = mgmt.GetInstances();
            //start our loop for all processors found
            foreach (ManagementObject obj in objCol)
            {
                if (cpuMan == String.Empty)
                {
                    // only return manufacturer from first CPU
                    cpuMan = obj.Properties["Manufacturer"].Value.ToString();
                }
            }
            return cpuMan;
        }
        /// <summary>
        /// method to retrieve the CPU's current
        /// clock speed using the WMI class
        /// </summary>
        /// <returns>Clock speed</returns>
        public static int CPUCurrentClockSpeed()
        {
            int cpuClockSpeed = 0;
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection objCol = mgmt.GetInstances();
            //start our loop for all processors found
            foreach (ManagementObject obj in objCol)
            {
                if (cpuClockSpeed == 0)
                {
                    // only return cpuStatus from first CPU
                    cpuClockSpeed = Convert.ToInt32(obj.Properties["CurrentClockSpeed"].Value.ToString());
                }
            }
            //return the status
            return cpuClockSpeed;
        }
        /// <summary>
        /// Retrieve CPU Speed.
        /// </summary>
        /// <returns></returns>
        public static double? CpuSpeedInGHz()
        {
            double? GHz = null;
            using (ManagementClass mc = new ManagementClass("Win32_Processor"))
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    GHz = 0.001 * (UInt32)mo.Properties["CurrentClockSpeed"].Value;
                    break;
                }
            }
            return GHz;
        }


        /// <summary>
        ///  Retrieving Primary Video Controller ID.
        /// </summary>
        /// <returns></returns>
        public static string VideoId()
        {
            return Identifier("Win32_VideoController", "DriverVersion") + Identifier("Win32_VideoController", "Name");
        }


        /// <summary>
        /// Retrieving Physical Ram Memory.
        /// </summary>
        /// <returns></returns>
        public static string PhysicalMemory()
        {
            ManagementScope oMs = new ManagementScope();
            ObjectQuery oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oCollection = oSearcher.Get();

            long MemSize = 0;
            long mCap = 0;

            // In case more than one Memory sticks are installed
            foreach (ManagementObject obj in oCollection)
            {
                mCap = Convert.ToInt64(obj["Capacity"]);
                MemSize += mCap;
            }
            MemSize = (MemSize / 1024) / 1024;
            return MemSize.ToString() + "MB";
        }
        /// <summary>
        /// Retrieving No of Ram Slot on Motherboard.
        /// </summary>
        /// <returns></returns>
        public static string RamSlotsNo()
        {

            int MemSlots = 0;
            ManagementScope oMs = new ManagementScope();
            ObjectQuery oQuery2 = new ObjectQuery("SELECT MemoryDevices FROM Win32_PhysicalMemoryArray");
            ManagementObjectSearcher oSearcher2 = new ManagementObjectSearcher(oMs, oQuery2);
            ManagementObjectCollection oCollection2 = oSearcher2.Get();
            foreach (ManagementObject obj in oCollection2)
            {
                MemSlots = Convert.ToInt32(obj["MemoryDevices"]);

            }
            return MemSlots.ToString();
        }


        /// <summary>
        /// HDDSerialNo By ManagementObjectSearcher
        /// </summary>
        /// <returns></returns>
        public static string GetHDDSerialNo()
        {
            string val = "";
            ManagementObjectSearcher mgmt = new ManagementObjectSearcher("select * from win32_DiskDrive");
            try
            {
                foreach (ManagementObject obj in mgmt.Get())
                    return val = obj["SerialNumber"].ToString().Trim();
            }
            catch (ManagementException) { return ""; }
            return val;
        }
        public static string HDDSerialNo(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }
        /// <summary>
        /// Retrieving HDD Serial No.
        /// </summary>
        /// <returns></returns>    
        public static string HDDSerialNo()
        {
            ManagementClass mc = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection moc = mc.GetInstances();
            string result = "";
            foreach (ManagementObject strt in moc)
            {
                result += Convert.ToString(strt["VolumeSerialNumber"]);
            }
            return result;
        }
        public static string DiskId()
        {
            return Identifier("Win32_DiskDrive", "Model") + Identifier("Win32_DiskDrive", "Manufacturer") + Identifier("Win32_DiskDrive", "Signature") + Identifier("Win32_DiskDrive", "TotalHeads");
        }

        /// <summary>
        /// Retrieving CD-DVD Drive Path.
        /// </summary>
        /// <returns></returns>
        public static string CdRomDrive()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive");

            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Drive").ToString();

                }

                catch { }

            }

            return "CD ROM Drive Letter: Unknown";

        }


        /// <summary>
        /// Retrieving System MAC Address.
        /// </summary>
        /// <returns></returns>
        public static string MACAddress()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            string MACAddress = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                if (MACAddress == String.Empty)
                {
                    if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }

            MACAddress = MACAddress.Replace(":", "");
            return MACAddress;
        }
        /// <summary>
        /// Retrieving First Enabled Network Card ID.
        /// </summary>
        /// <returns></returns>
        public static string MacId()
        {
            return Identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }
        /// <summary>
        /// method to retrieve the network adapters
        /// default IP gateway using WMI
        /// </summary>
        /// <returns>adapters default IP gateway</returns>
        public static string DefaultIPGateway()
        {
            //create out management class object using the
            //Win32_NetworkAdapterConfiguration class to get the attributes
            //of the network adapter
            ManagementClass mgmt = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //create our ManagementObjectCollection to get the attributes with
            ManagementObjectCollection objCol = mgmt.GetInstances();
            string gateway = String.Empty;
            //loop through all the objects we find
            foreach (ManagementObject obj in objCol)
            {
                if (gateway == String.Empty)  // only return MAC Address from first card
                {
                    //grab the value from the first network adapter we find
                    //you can change the string to an array and get all
                    //network adapters found as well
                    //check to see if the adapter's IPEnabled
                    //equals true
                    if ((bool)obj["IPEnabled"] == true)
                    {
                        gateway = obj["DefaultIPGateway"].ToString();
                    }
                }
                //dispose of our object
                obj.Dispose();
            }
            //replace the ":" with an empty space, this could also
            //be removed if you wish
            gateway = gateway.Replace(":", "");
            //return the mac address
            return gateway;
        }






        /// <summary>
        /// Retrieving System Account Name.
        /// </summary>
        /// <returns></returns>
        public static string AccountName()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_UserAccount");

            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {

                    return wmi.GetPropertyValue("Name").ToString();
                }
                catch { }
            }
            return "User Account Name: Unknown";

        }
        
        /// <summary>
        /// Retrieving Current Language
        /// </summary>
        /// <returns></returns>
        public static string CurrentLanguage()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("CurrentLanguage").ToString();

                }

                catch { }

            }

            return "BIOS Maker: Unknown";

        }
        
        /// <summary>
        /// Retrieving Current Language.
        /// </summary>
        /// <returns></returns>
        public static string OSInformation()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return ((string)wmi["Caption"]).Trim() + ", " + (string)wmi["Version"] + ", " + (string)wmi["OSArchitecture"];
                }
                catch { }
            }
            return "BIOS Maker: Unknown";
        }
        
        /// <summary>
        /// Retrieving Computer Name.
        /// </summary>
        /// <returns></returns>
        public static String ComputerName()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                info = (string)mo["Name"];
                //mo.Properties["Name"].Value.ToString();
                //break;
            }
            return info;
        }





        /// <summary>
        /// Return a Hardware Identifier.
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <returns></returns>
        public static string Identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementBaseObject mo in moc)
            {
                //Only get the first one
                if (result != "") continue;
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch
                {
                }
            }
            return result;
        }
        
        /// <summary>
        /// Return a Hardware Identifier.
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <param name="wmiMustBeTrue"></param>
        /// <returns></returns>
        public static string Identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementBaseObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() != "True") continue;
                //Only get the first one
                if (result != "") continue;
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch
                {
                }
            }
            return result;
        }
    }
    #endregion 



}





/*
  * If not exists(select 1 from sys.Databases Where name = 'Sales_System2')
  Begin
      Drop Database Sales_System2
  End
  Go

  Create database Sales_System2


    If not exists (select 1 from sys.Databases Where name = 'Sales_System22')
    Begin
    Create database Sales_System22
    End
    Go


*/


//Random _random = new Random();
//textBox1.Text = _random.Next(0, 9999).ToString("D4");
