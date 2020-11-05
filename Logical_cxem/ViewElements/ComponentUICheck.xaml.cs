using System.Windows;
using System.Windows.Input;
using Logical_cxem.Enumerations;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUIOutput.xaml
    /// </summary>
    public partial class ComponentUICheck : BaseComponentUI
    {
        public ComponentUICheck() : base(ETypeOfElement.Check)
        {
            InitializeComponent();
            State_text.Text = "0";
        }

        public ComponentUICheck(Point point, int id) : base(point, ETypeOfElement.Check, id)
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsDrag)
                if (DataContext != null)
                    State_text.Text = "1" == State_text.Text ? "0" : "1";
            base.OnMouseUp(e);
            IsDrag = false;
        }
    }
}