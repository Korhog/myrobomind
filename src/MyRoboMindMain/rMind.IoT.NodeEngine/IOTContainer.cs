using rMind.Elements;
using rMind.SmartHome;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using rMind.Content.Row;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace rMind.IoT.NodeEngine
{
    class IOTContainer<T> : Content.rMindHeaderRowContainer where T : IIOTDevice
    {
        Guid m_guid;
        public Guid GUID { get { return m_guid; } }

        public IOTContainer(rMindBaseController parent, Guid guid) : base(parent)
        {
            CanExpand = true;
            CanEdit = true;
            m_guid = guid;

            BorderRadius = new Windows.UI.Xaml.CornerRadius(3);

            AccentColor = Colors.SteelBlue;
            Build();
        }

        void Build()
        {
            var type = typeof(T);

            Header = type.GetCustomAttribute<DisplayName>()?.Value ?? "IOT";

            foreach(var method in type.GetMethods().Where(x => x.IsPublic))
            {
                var attrs = method.GetCustomAttributes();
                AddLogicRow(attrs, method.Name);
                AddValueRow(attrs, method);
            }
        }

        void AddLogicRow(IEnumerable<Attribute> attrs, string name)
        {
            if (attrs.Where(x => x is SmartHome.Activator || x is Receiver).Any())
            {
                var row = new rMindRow
                {
                    OutputNodeType = rMind.Nodes.rMindNodeConnectionType.None,
                    InputNodeType = rMind.Nodes.rMindNodeConnectionType.None
                };

                if (attrs.Where(x => x is Receiver).Any())
                {
                    row.InputNodeType = rMind.Nodes.rMindNodeConnectionType.Container;
                    row.InputNode = new Nodes.rMindBaseNode(this)
                    {
                        AttachMode = rMind.Nodes.rMindNodeAttachMode.Single,
                        ConnectionType = rMind.Nodes.rMindNodeConnectionType.Container,
                        NodeOrientation = rMind.Nodes.rMindNodeOriantation.Left,
                        Theme = new Nodes.rMindNodeTheme
                        {
                            BaseFill = new SolidColorBrush(Colors.Black),
                            BaseStroke = new SolidColorBrush(Colors.Pink)
                        },
                        NodeType = rMind.Nodes.rMindNodeType.Input
                    };
                }

                if (attrs.Where(x => x is SmartHome.Activator).Any())
                {
                    row.OutputNodeType = rMind.Nodes.rMindNodeConnectionType.Container;
                    row.OutputNode = new Nodes.rMindBaseNode(this)
                    {
                        AttachMode = rMind.Nodes.rMindNodeAttachMode.Multi,
                        ConnectionType = rMind.Nodes.rMindNodeConnectionType.Container,
                        NodeOrientation = rMind.Nodes.rMindNodeOriantation.Right,
                        Theme = new Nodes.rMindNodeTheme
                        {
                            BaseFill = new SolidColorBrush(Colors.Black),
                            BaseStroke = new SolidColorBrush(Colors.LimeGreen)
                        },
                        NodeType = rMind.Nodes.rMindNodeType.Output
                    };
                }

                row.Content = new TextBlock
                {
                    Text = name,
                    Foreground = new SolidColorBrush(Colors.White),
                    Opacity = 0.5,
                    IsHitTestVisible = false,
                    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                    Margin = new Windows.UI.Xaml.Thickness(10, 0, 10, 0)
                };

                AddRow(row);
            }
        }

        void AddValueRow(IEnumerable<Attribute> attrs, MethodInfo method)
        {
            if (attrs.Where(x => x is Getter || x is Setter).Any())
            {
                var row = new rMindRow
                {
                    OutputNodeType = rMind.Nodes.rMindNodeConnectionType.None,
                    InputNodeType = rMind.Nodes.rMindNodeConnectionType.None
                };

                if (attrs.Where(x => x is Setter).Any())
                {
                    row.InputNodeType = rMind.Nodes.rMindNodeConnectionType.Value;
                    row.InputNode = new Nodes.rMindBaseNode(this)
                    {
                        AttachMode = rMind.Nodes.rMindNodeAttachMode.Single,
                        ConnectionType = rMind.Nodes.rMindNodeConnectionType.Value,
                        NodeOrientation = rMind.Nodes.rMindNodeOriantation.Left,
                        Theme = new Nodes.rMindNodeTheme
                        {
                            BaseFill = new SolidColorBrush(Colors.Black),
                            BaseStroke = new SolidColorBrush(ColorMapper.Get(method.ReturnType))
                        },
                        NodeType = rMind.Nodes.rMindNodeType.Input
                    };
                }

                if (attrs.Where(x => x is Getter).Any())
                {
                    row.OutputNodeType = rMind.Nodes.rMindNodeConnectionType.Value;
                    row.OutputNode = new Nodes.rMindBaseNode(this)
                    {
                        AttachMode = rMind.Nodes.rMindNodeAttachMode.Multi,
                        ConnectionType = rMind.Nodes.rMindNodeConnectionType.Value,
                        NodeOrientation = rMind.Nodes.rMindNodeOriantation.Right,
                        Theme = new Nodes.rMindNodeTheme
                        {
                            BaseFill = new SolidColorBrush(Colors.Black),
                            BaseStroke = new SolidColorBrush(Colors.BlueViolet)
                        },
                        NodeType = rMind.Nodes.rMindNodeType.Output
                    };
                }

                row.Content = new TextBlock
                {
                    Text = method.Name,
                    Foreground = new SolidColorBrush(Colors.White),
                    Opacity = 0.5,
                    IsHitTestVisible = false,
                    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                    Margin = new Windows.UI.Xaml.Thickness(10, 0, 10, 0)
                };

                AddRow(row);
            }
        }
    }
}
