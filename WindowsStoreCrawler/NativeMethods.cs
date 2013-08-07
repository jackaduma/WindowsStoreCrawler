using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowsStoreCrawler
{
    internal static class NativeMethods
    {
        #region #####Mouse#####
        [STAThread]
        [DllImport("User32")]
        public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        [DllImport("User32")]
        public extern static bool GetCursorPos(out NativeStructs.Point p);
        [DllImport("User32")]
        public extern static int ShowCursor(bool bShow);
        #endregion


        #region #####Window#####
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("USER32.DLL")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern int BringWindowToTop(IntPtr hWnd);
        #endregion

        #region #####Keyboard#####
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(UInt16 virtualKeyCode);
        #endregion

        #region #####Art#####

        //[DllImport("shell32.dll")]
        //public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref Art.GetIncoIntPtrStruct.SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        //[DllImport("user32.dll")]
        //public static extern bool ClientToScreen(IntPtr hWnd, ref Art.DismantleElmentStruct.POINT lpPoint);
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref NativeStructs.Point point);
        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref NativeStructs.Point point);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        #endregion

        #region #####Device#####
        //[DllImport("User32.dll")]
        //public static extern bool GetLastInputInfo(ref Device.LASTINPUTINFO info);
        #endregion

        #region #####Hook#####

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern int SetWindowsHookEx(int idHook, NativeStructs.HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        #endregion

        #region #####Window#####
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(int hWnd, String str, Int32 count);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(int hWnd, string lpString);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(int hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(NativeStructs.Point Point);
        [DllImport("user32.dll")]
        public static extern int GetFocus();
        #endregion

        #region #####Input Method Editor#####
        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);
        [DllImport("Imm32.dll")]
        public static extern Boolean ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);
        #endregion

        [DllImport("user32")]
        public static extern int GetDoubleClickTime();
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInput, ref NativeStructs.INPUT pInput, int cbSize);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref NativeStructs.RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        //Scroll
        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        [DllImport("user32.dll")]
        public static extern int GetScrollPos(IntPtr hwnd, int nBar);
    }
}
