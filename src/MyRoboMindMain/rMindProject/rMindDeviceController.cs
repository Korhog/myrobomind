using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Project
{
    using rMind.Elements;
    using rMind.CanvasEx;
    using rMind.Content;
    using rMind.Content.Quad;
    using Windows.UI.Xaml.Controls;

    class rMindDeviceController : rMindBaseController
    {
        rMindQuadContainer m_board;

        public rMindDeviceController(rMindCanvasController parent) : base(parent)
        {
            InitBoard();
        }

        void InitBoard()
        {
            m_board = new rMindQuadContainer(this)
            {
                Locked = true,
                Storable = false
            };

            m_board.SetPosition(5000, 3000);

            rMindHorizontalLine line;
            m_board.CreateLine<rMindVerticalLine>();

            line = m_board.CreateLine<rMindHorizontalLine>() as rMindHorizontalLine;
            line?.AddLeftNode();
            line?.AddRightNode();

            line = m_board.CreateLine<rMindHorizontalLine>() as rMindHorizontalLine;
            line?.AddLeftNode();
            line?.AddRightNode();

            line = m_board.CreateLine<rMindHorizontalLine>() as rMindHorizontalLine;
            line?.AddLeftNode();
            line?.AddRightNode();

            line = m_board.CreateLine<rMindHorizontalLine>() as rMindHorizontalLine;
            line?.AddLeftNode();
            line?.AddRightNode();

            AddElement(m_board);
        }

        protected override void DrawElements()
        {
            if (!m_subscribed)
                return;

            Draw(m_board);

            base.DrawElements();
        }

        public override void Reset()
        {
            base.Reset();
            if (!m_items.Contains(m_board))
                m_items.Add(m_board);
        }
    }
}
