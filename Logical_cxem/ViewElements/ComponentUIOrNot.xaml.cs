using System.Windows;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUI.xaml
    /// </summary>
    public partial class ComponentUIOrNot : BaseComponentUI
    {
        public ComponentUIOrNot() : base(ETypeOfElement.OrNot)
        {
            InitializeComponent();
            DataContext = new ElementOrNot(BaseElement.IdCounter) {CheckVisibility = false};
        }

        public ComponentUIOrNot(Point point, int id) : base(point, ETypeOfElement.OrNot, id)
        {
            InitializeComponent();
        }

        private void ComponentUIPin_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}