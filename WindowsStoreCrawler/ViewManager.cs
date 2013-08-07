using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UIAutomation;

namespace WindowsStoreCrawler
{
    class ViewManager
    {
        IUIAutomation _automation = new CUIAutomation();

        IUIAutomationElement _rootElement;
        ViewTree vt;

        KeyNotFoundException exception = new KeyNotFoundException();

        public ViewManager()
        {
        }

        public ViewManager(IntPtr hwnd)
        {
            _rootElement = _automation.ElementFromHandle(hwnd);
            if (null == _rootElement)
            {
                throw this.exception;
            }

            this.vt = new ViewTree(_rootElement, _automation);
            this.vt.BuildTree();
        }

        //public ViewTree.TreeNode GetDocument()
        //{
           
        //}

        //public ViewTree.TreeNode GetElementByCondition(string name, int controlType)
        //{

        //}

    }
}
