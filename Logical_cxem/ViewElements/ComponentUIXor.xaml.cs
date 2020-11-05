using System.Windows;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUI.xaml
    /// </summary>
    public partial class ComponentUIXor : BaseComponentUI
    {
        public ComponentUIXor() : base(ETypeOfElement.Xor)
        {
            InitializeComponent();
            DataContext = new ElementXor(BaseElement.IdCounter) {CheckVisibility = false};
        }

        public ComponentUIXor(Point point, int id) : base(point, ETypeOfElement.Xor, id)
        {
            InitializeComponent();
        }

        private void ComponentUIPin_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}