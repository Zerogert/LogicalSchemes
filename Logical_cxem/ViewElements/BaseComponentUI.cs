using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logical_cxem.Enumerations;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUI.xaml
    /// </summary>
    public class BaseComponentUI : UserControl
    {
        public delegate void _OnDeleteElement(int id, UserControl uiElement);

        public delegate void _OnPinDropped(DataLine dataLine);

        protected bool IsDrag;

        public BaseComponentUI(ETypeOfElement nameElement)
        {
            Construct = true;
            AllowDrop = true;
            NameElement = nameElement;
        }


        public BaseComponentUI(Point point, ETypeOfElement nameElement, int id)
        {
            Margin = new Thickness(point.X, point.Y, 0, 0);
            Construct = false;
            NameElement = nameElement;
            ID = id;
            AllowDrop = true;
        }

        public Point relativeMouse { get; set; }
        public Point oldPosition { get; set; }
        public bool Construct { get; }
        public int ID { get; }
        public ETypeOfElement NameElement { get; }
        public virtual event _OnPinDropped OnPinDropped;
        public virtual event _OnDeleteElement OnDeleteElement;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            relativeMouse = e.GetPosition(this);
            oldPosition = new Point(Margin.Left, Margin.Top);
        }

        protected void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !Construct && AllowDrop)
            {
                // Package the dataLine.
                var data = new DataObject();
                data.SetData("ComponentUI", this);
                IsDrag = true;
                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }

            if (e.LeftButton == MouseButtonState.Pressed && Construct)
            {
                // Package the dataLine.
                var data = new DataObject();
                data.SetData("ComponentUI", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }

            IsDrag = false;
            base.OnMouseMove(e);
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            if (!Construct) OnDeleteElement(ID, this);
        }

        protected void ComponentUI_Drag(DataLine dataLine)
        {
            if (!Construct)
            {
                var dat = new DataObject();
                dataLine.point0 = new PointPin(Margin.Left + dataLine.point0.X, Margin.Top + dataLine.point0.Y,
                    dataLine.point0.Direction, dataLine.point0.Type, ID, dataLine.point0.Id);
                dat.SetData("DataLine", dataLine);
                DragDrop.DoDragDrop(this, dat, DragDropEffects.Copy);
            }
        }

        protected void ComponentUI_DropOnPin(DataLine dataLine)
        {
            if (!Construct)
            {
                dataLine.point1 = new PointPin(Margin.Left + dataLine.point1.X, Margin.Top + dataLine.point1.Y,
                    dataLine.point1.Direction, dataLine.point1.Type, ID, dataLine.point1.Id);
                if (OnPinDropped != null) OnPinDropped(dataLine);
            }
        }
    }
}