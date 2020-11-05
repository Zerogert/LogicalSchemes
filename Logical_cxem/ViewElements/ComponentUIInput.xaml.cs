using System.Windows;
using System.Windows.Input;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUIInput.xaml
    /// </summary>
    public partial class ComponentUIInput : BaseComponentUI
    {
        public ComponentUIInput() : base(ETypeOfElement.Input)
        {
            InitializeComponent();
            DataContext = new ElementInput(BaseElement.IdCounter) {CheckVisibility = false};
            Label.DataContext = null;
        }

        public ComponentUIInput(Point point, int id) : base(point, ETypeOfElement.Input, id)
        {
            InitializeComponent();
        }
        //protected override void OnMouseDown(MouseButtonEventArgs e)
        //{
        //    if (DataContext is BaseElement)
        //    {
        //        State_text.Text = ("1" == State_text.Text) ? "0" : "1";
        //    }
        //}

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (!IsDrag)
                if ((DataContext != null) & !Construct)
                    State_text.Text = "1" == State_text.Text ? "0" : "1";
            base.OnMouseUp(e);
            IsDrag = false;
        }
    }
}