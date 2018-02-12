using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace rMind.Driver
{
    using rMind.Driver.Entities;

    public enum NodeType
    {
        Driver
    }
    /// <summary> Узел дерева </summary>
    public class TreeNode
    {
        public string Caption { get; set; }
        public NodeType Type { get; set; }             
    }

    public class TreeNodeBuilder
    {
        public static ObservableCollection<TreeNode> Build(List<Driver> drivers)
        {
            if ((drivers?.Count ?? 0) == 0)
                return null;

            var result = new ObservableCollection<TreeNode>();
            foreach (var driver in drivers)
            {
                result.Add(BuildNode(driver));
            }

            return result;
        }

        static TreeNode BuildNode(Driver driver)
        {
            var result = new TreeNode
            {
                Caption = driver.Name,
                Type = NodeType.Driver
            };

            return result;
        }
    }
}
