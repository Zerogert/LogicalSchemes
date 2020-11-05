using System.Windows;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUI.xaml
    /// </summary>
    public partial class ComponentUIXorNot : BaseComponentUI
    {
        public ComponentUIXorNot() : base(ETypeOfElement.XorNot)
        {
            InitializeComponent();
            DataContext = new ElementXorNot {CheckVisibility = false};
        }

        public ComponentUIXorNot(Point point, int id) : base(point, ETypeOfElement.XorNot, id)
        {
            InitializeComponent();
        }

        private void ComponentUIPin_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}