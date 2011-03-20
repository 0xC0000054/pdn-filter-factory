using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace PdnFF
{
    class TreeNodeItemComparer : IComparer
    {
        private SortOrder order;
        public TreeNodeItemComparer() 
        {
            this.order = SortOrder.Ascending;
        }
       
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = ns.StringLogicalComparer.Compare(((TreeNode)x).Text,
                                    ((TreeNode)y).Text);
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }
    }

}
