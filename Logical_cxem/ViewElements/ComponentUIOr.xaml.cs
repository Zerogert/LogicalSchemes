using System.Windows;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUI.xaml
    /// </summary>
    public partial class ComponentUIOr : BaseComponentUI
    {
        public ComponentUIOr() : base(ETypeOfElement.Or)
        {
            InitializeComponent();
            DataContext = new ElementOr(BaseElement.IdCounter) {CheckVisibility = false};
        }

        public ComponentUIOr(Point point, int id) : base(point, ETypeOfElement.Or, id)
        {
            InitializeComponent();
        }

        private void ComponentUIPin_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}