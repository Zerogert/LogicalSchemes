using System.Windows;
using Logical_cxem.Enumerations;

namespace Logical_cxem.ViewElements
{
    /// <summary>
    ///     Interaction logic for ComponentUIConnectionLine.xaml
    /// </summary>
    public partial class ComponentUIConnectionLine : BaseComponentUI
    {
        public ComponentUIConnectionLine() : base(ETypeOfElement.Line)
        {
            InitializeComponent();
        }

        public ComponentUIConnectionLine(int id) : base(new Point(), ETypeOfElement.Line, id)
        {
            InitializeComponent();
        }
    }
}