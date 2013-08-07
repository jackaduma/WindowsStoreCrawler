using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowsStoreCrawler
{
    public static partial class KeyBoard
    {
        public static void KeyDown(int KeyCode)
        {
            NativeMethods.keybd_event((Byte)KeyCode, 0x45, NativeConstants.KEF_EXTENDEDKEY, (UIntPtr)0);
        }

        public static void KeyUp(int KeyCode)
        {
            NativeMethods.keybd_event((Byte)KeyCode, 0x45, NativeConstants.KEF_EXTENDEDKEY | NativeConstants.KEF_KEYUP, (UIntPtr)0);
        }

        public static void KeyPress(int KeyCode)
        {
            KeyDown(KeyCode);
            KeyUp(KeyCode);
        }

        /// <summary>
        /// send a string
        /// </summary>
        public static void InputStr()
        {
            //NativeMethods.SendMessage(myIntPtr, NativeContansts.WM_CHAR, BitConverter.ToInt32(ch, 0), 0);
            NativeStructs.INPUT input = new NativeStructs.INPUT();
            input.type = 1; //keyboard_input
            input.ki.wVk = VirtualKeyCodes.VK_T;
            input.ki.dwFlags = 0;
            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            NativeStructs.INPUT input1 = new NativeStructs.INPUT();
            input1.type = 1; //keyboard_input
            input1.ki.wVk = VirtualKeyCodes.VK_T;
            input1.ki.dwFlags = 2;
            NativeMethods.SendInput(1, ref input1, Marshal.SizeOf(input1));
        }
    }
}
