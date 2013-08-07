using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WindowsStoreCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            // one thread to download appx from Windows Store
            UIOperator uiOperator = new UIOperator();
            Thread uiThread = new Thread(new ThreadStart(uiOperator.execute));
            uiThread.Start();

            Thread.Sleep(10 * 1000);

            // another thread to pack appx 
            // then submit to MARS
            AppxPacker packer = new AppxPacker();
            Thread packThread = new Thread(new ThreadStart(packer.Pack));
            packThread.Start();            
        }
    }
}
