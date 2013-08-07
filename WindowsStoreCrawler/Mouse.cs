using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsStoreCrawler
{
    public static partial class Mouse
    {

        public static void LeftDown()
        {
            NativeMethods.mouse_event(NativeConstants.MEF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
        }

        public static void LeftUp()
        {
            NativeMethods.mouse_event(NativeConstants.MEF_LEFTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void LeftClick()
        {
            LeftDown();
            LeftUp();
        }


        public static void RightDown()
        {
            NativeMethods.mouse_event(NativeConstants.MEF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero);
        }

        public static void RightUp()
        {
            NativeMethods.mouse_event(NativeConstants.MEF_RIGHTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void RightClick()
        {
            RightDown();
            RightUp();
        }


        public static void MiddleDown()
        {
            NativeMethods.mouse_event(NativeConstants.MEF_MIDDLEDOWN, 0, 0, 0, IntPtr.Zero);
        }

        public static void MiddleUp()
        {
            NativeMethods.mouse_event(NativeConstants.MEF_MIDDLEUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void MiddleClick()
        {
            MiddleDown();
            MiddleUp();
        }
    }
}
