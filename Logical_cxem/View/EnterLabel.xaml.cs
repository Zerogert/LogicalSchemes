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
using System.Windows.Shapes;

namespace Logical_cxem.View {
	/// <summary>
	/// Interaction logic for EnterLabel.xaml
	/// </summary>
	public partial class EnterLabel : Window {
		public EnterLabel() {
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Label.Text.Length>0)
			{
				Close();
			}
			else
			{
				MessageBox.Show("Вы не ввели значение", "Ошибка", MessageBoxButton.OK);
			}
		}
	}
}
