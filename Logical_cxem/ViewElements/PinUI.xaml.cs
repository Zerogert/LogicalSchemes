using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logical_cxem.ViewElements
{
    /// <summary>
    /// Interaction logic for PinUI.xaml
    /// </summary>
    public partial class PinUI : BaseComponentUI
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public override event _clickOnPin ClickOnPin;
        public PinUI():base("Pin")
        {
            InitializeComponent();
        }

        public PinUI(Point point) : base(point, "Pin")
        {
            InitializeComponent();
            Margin = new Thickness(point.X, point.Y, 0, 0);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            ClickOnPin(1, 2);

            base.OnMouseLeftButtonDown(e);
        }
    }
}
