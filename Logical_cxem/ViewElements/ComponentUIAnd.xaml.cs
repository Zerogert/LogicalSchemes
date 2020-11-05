using System.Windows;
using Logical_cxem.Enumerations;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUI.xaml
    /// </summary>
    public partial class ComponentUIAnd : BaseComponentUI
    {
        public ComponentUIAnd() : base(ETypeOfElement.And)
        {
            InitializeComponent();
            DataContext = new ElementAnd(BaseElement.IdCounter) {CheckVisibility = false};
        }

        public ComponentUIAnd(Point point, int id) : base(point, ETypeOfElement.And, id)
        {
            InitializeComponent();
        }
    }
}