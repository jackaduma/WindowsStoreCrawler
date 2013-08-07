using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Threading;

namespace WindowsStoreCrawler
{
    class AppxPacker
    {
        private string srcPath = @"C:\Program Files\WindowsApps\";
        private string deleteDir = @"C:\Program Files\WindowsApps\Deleted";

        private string desPath = @"E:\WinStoreApps\";

        private HashSet<string> packedAppSet = new HashSet<string>();

        public HashSet<string> GetPackedAppSet()
        {
            return this.packedAppSet;
        }

        public AppxPacker() 
        {
            if (!Directory.Exists(this.srcPath))
            {
                Console.WriteLine("The app installation directory is not exist or cannot be accessd!");
                throw new Exception();
            }
            if (!Directory.Exists(this.desPath))
            {
                Directory.CreateDirectory(this.desPath);
            }
        }

        public void Pack(string appxName)
        {
            string startPath = this.srcPath + appxName;
            string zipPath = this.desPath + appxName + ".appx";

            if (!new FileInfo(zipPath).Exists)
            {
                ZipFile.CreateFromDirectory(startPath, zipPath);
            }
        }

        public void Pack(string srcDir, string des)
        {
            ZipFile.CreateFromDirectory(srcDir, des);
        }

        public void UnPack(string appx, string desDir)
        {
            ZipFile.ExtractToDirectory(appx, desDir);
        }

        public bool IsFileInUse(string filePath)
        {
            bool locked = false;
            FileStream fs = null;
            try
            {
                fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                locked = true;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return locked;
        }

        public bool IsAppxDownload(string appxDir)
        {

            return false;
        }

        /*
         * get appx unique flag from Manifest file
         * not completed
        */
        public string GetAppxSignature(string appx)
        {
            string xmlFilePath = this.srcPath + appx + @"\AppxManifest.xml";
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeReader reader = null;

            //string appId = null;
            string platform = null;
            string publisher = null;
            string version = null;
            string name = null;

            try
            {
                xmldoc.Load(xmlFilePath);
                XmlElement root = xmldoc.DocumentElement;
                root = xmldoc.DocumentElement;

                // using Node Reader
                reader = new XmlNodeReader(xmldoc);
                while (reader.Read())
                {
                    if (reader.NodeType.Equals(XmlNodeType.Element)
                        && reader.Name.Equals("Identity"))
                    {
                        platform = reader.GetAttribute("ProcessorArchitecture");
                        publisher = reader.GetAttribute("Publisher");
                        version = reader.GetAttribute("Version");
                        name = reader.GetAttribute("Name");
                        break;
                    }
                    //if (reader.NodeType.Equals(XmlNodeType.Element)
                    //    && reader.Name.Equals("Application"))
                    //{
                    //    appId = reader.GetAttribute("Id");                     
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (null != reader)
                {
                    reader.Close();
                }
            }           
            
            return string.Join("<br>", platform, publisher, version, name);
        }

        public void Pack()
        {
            while (true)
            {
                // this variable is for demo
                int round = 0;
                foreach (string dirPath in Directory.GetDirectories(this.srcPath))
                {
                    string appxName = Path.GetFileName(dirPath);
                    
                    if (dirPath.Equals(this.deleteDir))
                    {
                        continue;
                    }
                    if (this.packedAppSet.Contains(dirPath))
                    {
                        continue;
                    }

                    DirectoryInfo di = new DirectoryInfo(dirPath);
                    DateTime dt = di.LastAccessTime;
                    DateTime now = DateTime.Now;
                    TimeSpan delta = now - dt;
                    double deltaMinutes = delta.TotalMinutes;
                    double deltaHours = delta.TotalHours;

                    if (round <= 5)
                    {
                        if (deltaMinutes < 10)
                        {
                            string signature = this.GetAppxSignature(appxName);
                            Console.WriteLine(signature);
                            this.Pack(appxName);
                            this.packedAppSet.Add(dirPath);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    
                    if (deltaHours >= 1.0)
                    {
                        string signature = this.GetAppxSignature(appxName);
                        Console.WriteLine(signature);
                        this.Pack(appxName);
                        this.packedAppSet.Add(dirPath);
                    }
                    else
                    {
                        continue;
                    }                   
                   
                }
                // this is for demo
                round += 1;
                // sleep
                Thread.Sleep(5 * 60 * 1000);
            }
        }
    }
}
