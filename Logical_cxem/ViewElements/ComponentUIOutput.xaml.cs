using System.Windows;
using System.Windows.Input;
using Logical_cxem.Enumerations;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUIOutput.xaml
    /// </summary>
    public partial class ComponentUIOutput : BaseComponentUI
    {
        public ComponentUIOutput() : base(ETypeOfElement.Output)
        {
            InitializeComponent();
            State_text.Text = "0";
        }

        public ComponentUIOutput(Point point, int id) : base(point, ETypeOfElement.Output, id)
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }
    }
}