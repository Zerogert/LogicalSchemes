using System.Windows;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUI.xaml
    /// </summary>
    public partial class ComponentUIAndNot : BaseComponentUI
    {
        public ComponentUIAndNot() : base(ETypeOfElement.AndNot)
        {
            InitializeComponent();
            DataContext = new ElementAndNot(BaseElement.IdCounter) {CheckVisibility = false};
        }

        public ComponentUIAndNot(Point point, int id) : base(point, ETypeOfElement.AndNot, id)
        {
            InitializeComponent();
        }
    }
}