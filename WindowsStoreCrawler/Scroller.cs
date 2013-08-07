using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsStoreCrawler
{
    class Scroller
    {
        private struct ScrollBarType
        {
            public static int SbHorz = 0;
            public static int SbVert = 1;
            public static int SbCtl = 2;
            public static int SbBoth = 3;
        }

        private static int MOUSEEVENTF_HWHEEL = 0x01000;  // move horizontally
        private static int MOUSEEVENTF_WHEEL = 0x0800;  // move vertically

        public static void HorScroll()
        {
            NativeMethods.mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, 100, IntPtr.Zero);
        }

        public static void HorScroll(int distance)
        {
            NativeMethods.mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, distance, IntPtr.Zero);
        }

        public static void HorScroll(IntPtr hwnd)
        {
            int pos = NativeMethods.GetScrollPos(hwnd, ScrollBarType.SbHorz);
            NativeMethods.mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, (pos + 100), IntPtr.Zero);
        }

        public static void HorScroll(IntPtr hwnd, int distance)
        {
            int pos = NativeMethods.GetScrollPos(hwnd, ScrollBarType.SbHorz);
            NativeMethods.mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, (pos + distance), IntPtr.Zero);
        }

        public static void VerScroll()
        {
            NativeMethods.mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 100, IntPtr.Zero);
        }

        public static void VerScroll(int distance)
        {
            NativeMethods.mouse_event(MOUSEEVENTF_WHEEL, 0, 0, distance, IntPtr.Zero);
        }

        public static void VerScroll(IntPtr hwnd)
        {
            int pos = NativeMethods.GetScrollPos(hwnd, ScrollBarType.SbVert);
            NativeMethods.mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (pos + 100), IntPtr.Zero);
        }

        public static void VerScroll(IntPtr hwnd, int distance)
        {
            int pos = NativeMethods.GetScrollPos(hwnd, ScrollBarType.SbVert);
            NativeMethods.mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (pos + distance), IntPtr.Zero);
        }
    }
}
