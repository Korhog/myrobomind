using rMind.Draw;
using rMind.Types;
using rMind.Theme;

using System.Linq;
using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Input;
using System;

namespace rMind.Elements
{
    using Nodes;
    /// <summary>
    /// Base scheme element 
    /// </summary>
    public class rMindBaseElement : rMindBaseItem, IDrawContainer
    {
        // Properties
        protected Border m_base;
        protected bool m_selected;
        protected Dictionary<string, rMindBaseNode> m_nodes_link;

        public rMindBaseElement(rMindBaseController parent) : base(parent)
        {
            m_nodes_link = new Dictionary<string, rMindBaseNode>();

            Init();
            SubscribeInput();
        }

        public void Delete()
        {
            UnsubscribeInput();
            foreach(var node in m_nodes_link.Values)
            {
                node.Detach();
            }

            m_parent.RemoveElement(this);
        }

        public override void Init()
        {
            base.Init();

            m_base = new Border()
            {
                Background = rMindScheme.Get().MainContainerBrush()
            };

            Template.Children.Add(m_base);
        }

        #region input        
        private void onPointerEnter(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            Parent.SetOveredItem(this);
            if (m_selected)
                return;        
        }


        private void onPointerExit(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            Parent.SetOveredItem(null);
            if (m_selected)
                return;         
        }

        private void onPointerUp(object sender, PointerRoutedEventArgs e)
        {            
            if (Parent.CheckIsOvered(this))
            {
                Parent.SetDragItem(null, e);
                SetSelected(true);
                Parent.SetSelectedItem(this, e.KeyModifiers == Windows.System.VirtualKeyModifiers.Shift);
            }            
        }

        private void onPointerPress(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            if (Parent.CheckIsOvered(this))
            {
                Parent.SetDragItem(this, e);
            }            
        }

        void SubscribeInput()
        {
            m_base.PointerEntered += onPointerEnter;
            m_base.PointerExited += onPointerExit;
            m_base.PointerPressed += onPointerPress;
            m_base.PointerReleased += onPointerUp;
        }

        void UnsubscribeInput()
        {
            m_base.PointerEntered -= onPointerEnter;
            m_base.PointerExited -= onPointerExit;
            m_base.PointerPressed -= onPointerPress;
            m_base.PointerReleased -= onPointerUp;
        }
        #endregion

        #region nodes

        /// <summary> create new node for connection </summary>
        public virtual rMindBaseNode CreateNode()
        {
            var desc = new rMindNodeDesc();
            return CreateNode(desc);
        }

        public virtual rMindBaseNode CreateNode(rMindNodeDesc desc)
        {
            var node = new rMindBaseNode(this)
            {
                ConnectionType = desc.ConnectionType
            };

            node.IDS = "node" + m_nodes_link.Count.ToString();
            m_nodes_link[node.IDS] = node;
            Template.Children.Add(node.Template);

            return node;
        }

        public void RemoveNode(string ids)
        {
            if (!m_nodes_link.ContainsKey(ids))
                return;

            var node = m_nodes_link[ids];
            RemoveNode(node);
        }

        public void RemoveNode(rMindBaseNode node)
        {
            if (node == null)
                return;

            if (m_nodes_link.ContainsKey(node.IDS) )
            {
                node.Detach();
                m_nodes_link.Remove(node.IDS);
                UpdateNodes();

                Template.Children.Remove(node.Template);
            }
        }

        /// <summary> update IDSs nodes after remove </summary>
        void UpdateNodes()
        {
            var nodes = m_nodes_link.Values.ToList();
            m_nodes_link.Clear();
            foreach(var node in nodes)
            {
                node.IDS = "node" + m_nodes_link.Count.ToString();
                m_nodes_link[node.IDS] = node;    
            }
        }


        #endregion

        public void SetSelected(bool state)
        {
            m_selected = state;
        }

        public override Vector2 GetOffset()
        {
            return Position;           
        }

        public override Vector2 SetPosition(Vector2 newPos)
        {
            var translation = base.SetPosition(newPos);

            foreach (var node in m_nodes_link.Values)
            {
                node.Update();
            }


            return translation;
        }

        protected CornerRadius m_border_radius;
        protected virtual void SetBorderRadius(CornerRadius value)
        {
            m_border_radius = value;
            m_base.CornerRadius = value;
        }
        public CornerRadius BorderRadius { get { return m_border_radius; } set { SetBorderRadius(value); } }
    }
}
