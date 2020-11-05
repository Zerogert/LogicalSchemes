using System.Windows.Controls;
using System.Windows.Input;

namespace Logical_cxem.ViewElements
{
    /// <summary>
    ///     Interaction logic for ElementUICheck.xaml
    /// </summary>
    public partial class ElementUICheck : UserControl
    {
        public ElementUICheck()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (DataContext != null) State_text.Text = "1" == State_text.Text ? "0" : "1";
        }
    }
}