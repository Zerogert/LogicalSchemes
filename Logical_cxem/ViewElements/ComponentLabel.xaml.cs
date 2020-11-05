using System.Windows;
using System.Windows.Input;
using Logical_cxem.Enumerations;
using Logical_cxem.ViewModels.Component;

namespace Logical_cxem.ViewElements
{
    /// <summary>
    ///     Interaction logic for ComponentLabel.xaml
    /// </summary>
    public partial class ComponentLabel : BaseComponentUI
    {
        public delegate void _OnEdit(bool b);

        public ComponentLabel() : base(ETypeOfElement.Label)
        {
            InitializeComponent();
            DataContext = new ViewModelLabel();
        }

        public ComponentLabel(Point point, int id) : base(point, ETypeOfElement.Label, id)
        {
            InitializeComponent();
        }

        public virtual event _OnEdit OnEdit;


        protected void OnEdited(object sender, RoutedEventArgs routedEventArgs)
        {
	        OnEdit?.Invoke(true);
        }
    }
}