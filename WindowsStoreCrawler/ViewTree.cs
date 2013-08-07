using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UIAutomation;

namespace WindowsStoreCrawler
{
    class ViewTree
    {
        public IUIAutomation automation = null;
        public TreeNode root=null;

        public ViewTree()
        {
        }

        public ViewTree(IUIAutomationElement element, IUIAutomation automation)
        {
            this.root = new TreeNode(element);
            this.root.parent = null;
            IUIAutomationElementArray array = element.FindAll(TreeScope.TreeScope_Children, automation.CreateTrueCondition());
            if (0 == array.Length)
            {
                this.root.children = null;
                this.root.isLeaf = true;
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    IUIAutomationElement e = array.GetElement(i);
                    TreeNode n = new TreeNode(e);
                    this.root.children.Add(n);
                }
            }
        }

        private void loadChildren(TreeNode node)
        {
            IUIAutomationElementArray array = node.element.FindAll(TreeScope.TreeScope_Children, automation.CreateTrueCondition());
            if (0 == array.Length)
            {
                node.children = null;
                node.isLeaf = true;
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    IUIAutomationElement e = array.GetElement(i);
                    TreeNode n = new TreeNode(e);
                    node.children.Add(n);
                    this.loadChildren(n);
                }
            }
        }

        public void BuildTree()
        {
            if (null == this.root)
            {
                return;
            }

            if (null == this.root.children)
            {
                return;
            }

            foreach (TreeNode node in this.root.children)
            {
                this.loadChildren(node);
            }
        }
        

        public class TreeNode
        {
            public TreeNode parent;
            public List<TreeNode> children;
            public bool isLeaf;

            public IUIAutomationElement element;

            public string name;
            public string className;
            public string frameworkId;
            public string automationId;

            public string controlType;

            public bool enabled;
            public bool onScreen;
            public bool focusable;

            public BoundingRectangle rect;
            public Location location;

            public TreeNode(IUIAutomationElement element)
            {
                this.element = element;
                this.name = element.CurrentName;
                this.className = element.CurrentClassName;
                this.frameworkId = element.CurrentFrameworkId;
                this.automationId = element.CurrentAutomationId;
                
                this.controlType = ControlTypeConverter.convert2string(element.CurrentControlType);
                this.enabled = element.CurrentIsEnabled==0 ? true : false;  //#define S_OK  ((HRESULT)0x00000000L)
                this.onScreen = element.CurrentIsOffscreen==0 ? false : true;
                this.focusable = element.CurrentIsKeyboardFocusable==0 ? true : false;

                this.rect.left = element.CurrentBoundingRectangle.left;
                this.rect.right = element.CurrentBoundingRectangle.right;
                this.rect.top = element.CurrentBoundingRectangle.top;
                this.rect.bottom = element.CurrentBoundingRectangle.bottom;

                this.location.X = (this.rect.left + this.rect.right) / 2;
                this.location.Y = (this.rect.top + this.rect.bottom) / 2;
            }            

        }

        public struct BoundingRectangle
        {
            public int left;
            public int right;
            public int top;
            public int bottom;
        }

        public struct Location
        {
            public int X;
            public int Y;
        }
    }
}
