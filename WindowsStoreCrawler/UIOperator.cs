using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using Microsoft.UIAutomation;


namespace WindowsStoreCrawler
{
    class UIOperator
    {
        IUIAutomation _automation;

        public UIOperator()
        {
            this._automation = new CUIAutomation();
        }

        public void execute()
        {
            IntPtr storeHwnd = this.StartWinStore();
            if (null == storeHwnd)
            {
                return;  // failed to start Windows Store
            }
            Thread.Sleep(3000);

            IUIAutomationElement updateDocumentElement = this.GetUpdateDocumentElement(this.GetRootElement(storeHwnd));
            IUIAutomationElement mainScrollingContainerElement = this.GetMainScrollingContainerElement(this.GetRootElement(storeHwnd));
            IUIAutomationElement topHome1 = this.GetTopFreeHome(mainScrollingContainerElement);
            
            this.ClickElement(topHome1);
            //refresh
            IUIAutomationElement scrollingContainerElement = this.GetScrollingContainerElement(this.GetRootElement(storeHwnd));
            IUIAutomationElement[] appsArray = this.GetAppsArray(scrollingContainerElement);

            IUIAutomationElement appInfoElement;
            IUIAutomationElement installButtonElement;
            for (int i = 0; i < appsArray.Length; i++)
            {
                if (appsArray[i].CurrentBoundingRectangle.Equals(null))
                { 
                    // scroll and refresh

                }
                this.ClickElement(appsArray[i]);
                Thread.Sleep(3000);

                //refresh 
                appInfoElement = this.GetAppInfoElement(this.GetRootElement(storeHwnd));
                installButtonElement = this.GetInstallButtonElement(appInfoElement);
                if (installButtonElement != null)
                {
                    this.ClickElement(installButtonElement);
                    Thread.Sleep(3000);
                    //refresh

                }

                // windows store will go back automatically
                //this.GoBack(updateDocumentElement);
                Thread.Sleep(3000);

                //refresh
                scrollingContainerElement = this.GetScrollingContainerElement(this.GetRootElement(storeHwnd));
                appsArray = this.GetAppsArray(scrollingContainerElement);
            }

            this.ScrollToRight(scrollingContainerElement);
            Thread.Sleep(1000);

            this.GoBack(updateDocumentElement);        
        }

        public IntPtr StartWinStore()
        {
            int retryTime = 10;
            while (retryTime>0)
            {
                //IntPtr windowPtr = NativeMethods.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", "Program Manager"); //"ImmersiveLauncher", "Start menu");
                //NativeMethods.SetForegroundWindow(windowPtr);

                KeyBoard.KeyPress(VirtualKeyCodes.VK_LWIN);
                IntPtr startMenuHwnd = NativeMethods.FindWindow("ImmersiveLauncher", "Start menu");
                Console.WriteLine(startMenuHwnd);

                NativeStructs.Point oldPoint;
                NativeMethods.GetCursorPos(out oldPoint);

                IntPtr t = NativeMethods.SetFocus(startMenuHwnd);
                Console.WriteLine(t);

                bool b = NativeMethods.SetForegroundWindow(startMenuHwnd);
                Console.WriteLine(b);

                int curWindow = NativeMethods.GetFocus();
                Console.WriteLine(curWindow);

                //NativeMethods.SetCursorPos(500, 628);
                //Mouse.LeftClick();
                //NativeMethods.SetCursorPos(oldPoint.X, oldPoint.Y);

                KeyBoard.KeyPress(VirtualKeyCodes.VK_S);
                KeyBoard.KeyPress(VirtualKeyCodes.VK_T);
                KeyBoard.KeyPress(VirtualKeyCodes.VK_O);
                KeyBoard.KeyPress(VirtualKeyCodes.VK_R);
                KeyBoard.KeyPress(VirtualKeyCodes.VK_E);
                //KeyBoard.InputStr();
                Thread.Sleep(5000);
                KeyBoard.KeyPress(VirtualKeyCodes.VK_RETURN);

                IntPtr storeHwnd = NativeMethods.FindWindow("Windows.UI.Core.CoreWindow", "Store");
                if (storeHwnd != IntPtr.Zero)
                {
                    return storeHwnd;
                }
                else
                {
                    retryTime -= 1;
                }
            }

            return IntPtr.Zero;
        }

        public void ClickElement(IUIAutomationElement element)
        {
            int left = element.CurrentBoundingRectangle.left;
            int right = element.CurrentBoundingRectangle.right;
            int top = element.CurrentBoundingRectangle.top;
            int bottom = element.CurrentBoundingRectangle.bottom;
            int x = (left + right) / 2;
            int y = (top + bottom) / 2;

            NativeMethods.SetCursorPos(x, y);
            Mouse.LeftClick();

            Thread.Sleep(3000);
            
        }

        public void ScrollToRight(IUIAutomationElement element)
        {
            Scroller.HorScroll(element.CurrentNativeWindowHandle, 200);
            Thread.Sleep(1000);
        }

        public void GoBack(IUIAutomationElement updateElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_ButtonControlTypeId);
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Back");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "backButton");
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement backElement = updateElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            this.ClickElement(backElement);
            Thread.Sleep(2000);
        }

        //public void RefreshScrollingContainer(IntPtr hwnd)
        //{ 

        //}

        public IUIAutomationElement[] GetAppsArray(IUIAutomationElement homeElement)
        {
            IUIAutomationCondition condition = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId,
                UIA_ControlTypeIds.UIA_ListItemControlTypeId);
            IUIAutomationElementArray appsList = homeElement.FindAll(TreeScope.TreeScope_Children, condition);
            Console.WriteLine(appsList.Length);
            IUIAutomationElement[] appsArray = new IUIAutomationElement[appsList.Length];
            for (int i = 0; i < appsList.Length; i++)
            {
                appsArray[i] = appsList.GetElement(i);
                Console.WriteLine(appsArray[i].CurrentAutomationId);
            }
            return appsArray;
        }

        public IUIAutomationElement[] GetTopFreeHomeArray(IUIAutomationElement element)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[2];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Top free");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_ListItemControlTypeId);

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElementArray topFreeList = element.FindAll(TreeScope.TreeScope_Children, conditions);

            IUIAutomationElement[] topFreeArray = new IUIAutomationElement[topFreeList.Length];
            for (int i = 0; i < topFreeList.Length; i++)
            {
                topFreeArray[i] = topFreeList.GetElement(i);
            }

            return topFreeArray;            
        }

        public IUIAutomationElement GetTopFreeHome(IUIAutomationElement element)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[2];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Top free");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_ListItemControlTypeId);

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement topFreeElement = element.FindFirst(TreeScope.TreeScope_Subtree, conditions);
            Console.WriteLine(topFreeElement.CurrentName);

            return topFreeElement;
        }

        public IUIAutomationElement GetInstallButtonElement(IUIAutomationElement appInfoElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Install");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "pdpBuyButton");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_ButtonControlTypeId);

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement installButtonElement = appInfoElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            return installButtonElement;
        }

        public IUIAutomationElement GetAppInfoElement(IUIAutomationElement rootElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[3];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_DocumentControlTypeId);
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "wsFrame");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement appInfoElement = rootElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            return appInfoElement;
        }

        public IUIAutomationElement GetScrollingContainerElement(IUIAutomationElement rootElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "wsFrame");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_DocumentControlTypeId);
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "document");

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement frameElement = rootElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            conditionArray = new IUIAutomationCondition[3];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "mainContent");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_ListControlTypeId);

            conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement contentElement = frameElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Scrolling Container");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_GroupControlTypeId);
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "group");

            conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement scrollingContainerElement = contentElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            return scrollingContainerElement;
        }

        public IUIAutomationElement GetMainScrollingContainerElement(IUIAutomationElement rootElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "wsHomeFrame");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_DocumentControlTypeId);
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "document");

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement homeFrameElement = rootElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            conditionArray = new IUIAutomationCondition[5];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "mainContentHome");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "");
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_SemanticZoomControlTypeId);
            conditionArray[4] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "semantic zoom");

            conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement contentHomeElement = homeFrameElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "mainContentZoomedIn");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_ListControlTypeId);
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "list");

            conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement listBoxElement = contentHomeElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Scrolling Container");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_GroupControlTypeId);
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "group");

            conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement scrollingContainerElement = listBoxElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            return scrollingContainerElement;
        }

        public IUIAutomationElement GetReportElement(IUIAutomationElement rootElement)
        { 
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Report this review");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_GroupControlTypeId);
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "pdpReportReviewMenu");
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement reportElement = rootElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            return reportElement;
        }

        public IUIAutomationElement GetUpdateLinkElement(IUIAutomationElement updateDocumentElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[3];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_HyperlinkControlTypeId);
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "headerLinkUpdates");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement updateLinkElement = updateDocumentElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            return updateLinkElement;
        }

        public IUIAutomationElement GetUpdateDocumentElement(IUIAutomationElement rootElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[4];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_DocumentControlTypeId);
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "frameHeader");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            conditionArray[3] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "document");

            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement updateElement = rootElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            return updateElement;
        }

        public IUIAutomationElement GetRootElement(IntPtr storeHwnd)
        {            
            //IUIAutomationCacheRequest cacheRequest = automation.CreateCacheRequest();

            IUIAutomationElement storeElement = _automation.ElementFromHandle(storeHwnd);            
            //int[] runtimeId = storeElement.GetRuntimeId();
            //for (int i = 0; i < runtimeId.Length; i++)
            //{
            //    Console.WriteLine(runtimeId[i]);
            //}

            IntPtr webPlatformHwnd = NativeMethods.FindWindowEx(storeHwnd, IntPtr.Zero, "Web Platform Embedding", "");
            Console.WriteLine(webPlatformHwnd);

            IntPtr IEServerHwnd = NativeMethods.FindWindowEx(webPlatformHwnd, IntPtr.Zero, "Internet Explorer_Server", "");
            Console.WriteLine(IEServerHwnd);
            // last native window handler
            IUIAutomationElement IEServerElement = _automation.ElementFromHandle(IEServerHwnd);


            IUIAutomationCondition condition1 = _automation.CreatePropertyCondition(
                UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_PaneControlTypeId);
            IUIAutomationCondition condition2 = _automation.CreatePropertyCondition(
                UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");

            IUIAutomationCondition conditions = _automation.CreateAndCondition(condition1, condition2);

            IUIAutomationElement IEElement = IEServerElement.FindFirst(TreeScope.TreeScope_Children, conditions);
            String id = IEElement.CurrentFrameworkId;
            Console.WriteLine(id);

            condition1 = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Store");
            condition2 = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_PaneControlTypeId);

            conditions = _automation.CreateAndCondition(condition1, condition2);
            IUIAutomationElement storePaneElement = IEElement.FindFirst(TreeScope.TreeScope_Children, conditions);

            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[3];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_PaneControlTypeId);
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "");
            conditionArray[2] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_FrameworkIdPropertyId, "InternetExplorer");
            
            conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement rootElement = storePaneElement.FindFirst(TreeScope.TreeScope_Children, conditions);            

            return rootElement;
        }

    }
}