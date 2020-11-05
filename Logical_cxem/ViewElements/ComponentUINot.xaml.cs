using System.Windows;
using System.Windows.Input;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;

namespace Logical_cxem
{
    /// <summary>
    ///     Interaction logic for ComponentUIInput.xaml
    /// </summary>
    public partial class ComponentUINot : BaseComponentUI
    {
        public ComponentUINot() : base(ETypeOfElement.Not)
        {
            InitializeComponent();
            DataContext = new ElementNot(BaseElement.IdCounter) {CheckVisibility = false};
        }

        public ComponentUINot(Point point, int id) : base(point, ETypeOfElement.Not, id)
        {
            InitializeComponent();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
        }
    }
}