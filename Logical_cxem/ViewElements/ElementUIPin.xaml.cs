using System.Windows;
using System.Windows.Input;
using Logical_cxem.Enumerations;

namespace Logical_cxem.ViewElements
{
    /// <summary>
    ///     Interaction logic for ElementUIPin.xaml
    /// </summary>
    public partial class ElementUIPin : BaseComponentUI
    {
        public delegate void _DragOnPin(DataLine dataLine);

        public delegate void _DropOnPin(DataLine dataLine);

        public ElementUIPin() : base(ETypeOfElement.Pin)
        {
            InitializeComponent();
        }

        public int id { get; set; }
        public int ParentId { get; set; }
        public EDirection Direction { get; set; }
        public ETypePin Type { get; set; }
        public virtual event _DropOnPin DropOnPin;
        public virtual event _DragOnPin DragOnPin;

        protected override void OnDrop(DragEventArgs e)
        {
            var data = e;
            if (data.Handled == false)
            {
                var Data = data.Data.GetData("DataLine");
                if (Data != null)
                {
                    var dataLine = (DataLine) data.Data.GetData("DataLine");
                    dataLine.point1 = new PointPin(Margin.Left + 6.5, Margin.Top + 6.5, Direction, Type, 0, id);
                    DropOnPin(dataLine);
                }
            }

            base.OnDrop(e);
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var dataLine = new DataLine();
                dataLine.point0 = new PointPin(Margin.Left + 5, Margin.Top + 5, Direction, Type, 0, id);
                DragOnPin(dataLine);
            }
        }
    }
}