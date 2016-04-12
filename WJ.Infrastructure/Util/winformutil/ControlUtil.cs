using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WJ.Infrastructure.Util
{
    public class ControlUtil
    {
        private List<TreeNode> endSelectedNodes = new List<TreeNode>();
        public string windowName;
        #region TreeView
        public void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {

            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Checked)
                {
                    windowName += e.Node.Text + "";
                    //取消节点选中状态之后，取消所有父节点的选中状态
                    setChildNodeCheckedState(e.Node, true);
                }
                else
                {
                    //取消节点选中状态之后，取消所有父节点的选中状态
                    setChildNodeCheckedState(e.Node, false);
                    //如果节点存在父节点，取消父节点的选中状态
                    //if (e.Node.Parent != null)
                    //{
                    //    setParentNodeCheckedState(e.Node, false);
                    //}
                }
            }
        }

        public List<TreeNode> getEndSelectedNodes(TreeView treeview)
        {
            foreach (TreeNode node in treeview.Nodes)
            {
                getEndSelectedNodesByNode(node);
            }
            return endSelectedNodes;
        }

        public List<TreeNode> getEndSelectedNodesByNode(TreeNode treenode)
        {
            foreach (TreeNode node in treenode.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    getEndSelectedNodesByNode(node);
                }
                else
                {
                    if (node.Checked)
                    {
                        endSelectedNodes.Add(node);
                    }
                }
            }

            return endSelectedNodes;
        }
        //取消节点选中状态之后，取消所有父节点的选中状态
        private void setParentNodeCheckedState(TreeNode currNode, bool state)
        {
            TreeNode parentNode = currNode.Parent;
            parentNode.Checked = state;
            if (currNode.Parent.Parent != null)
            {
                setParentNodeCheckedState(currNode.Parent, state);
            }
        }
        //选中节点之后，选中节点的所有子节点
        private void setChildNodeCheckedState(TreeNode currNode, bool state)
        {
            TreeNodeCollection nodes = currNode.Nodes;
            if (nodes.Count > 0)
                foreach (TreeNode tn in nodes)
                {
                    tn.Checked = state;
                    setChildNodeCheckedState(tn, state);
                }
        }
        #endregion
    }
}
