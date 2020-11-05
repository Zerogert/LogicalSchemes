using System.Windows;
using System.Windows.Controls;
using Logical_cxem.Models;
using Logical_cxem.View;
using Logical_cxem.ViewElements;

namespace Logical_cxem.ViewModels.Component
{
    internal class ViewModelLabel : BaseViewModelComponent
    {
        private bool isEditing = false;

        public ViewModelLabel()
        {
        }

        public ViewModelLabel(Point point, Canvas surface, int id = -1)
        {
            BaseComponentUI componentUi;
            if (id < 0)
            {
                componentUi = new ComponentLabel(point, BaseElement.IdCounter);
                element = new ElementLabel(BaseElement.IdCounter);
            }
            else
            {
                componentUi = new ComponentLabel(point, id);
                element = new ElementLabel(id);
            }

            componentUi.DataContext = this;
            Panel.SetZIndex(componentUi, -10000);
            surface.Children.Add(componentUi);
            componentUi.OnPinDropped += OnPinDrop;
            componentUi.OnDeleteElement += OnDelete;
            ((ComponentLabel) componentUi).OnEdit += OnEdit;
        }

        public string Strength
        {
            get => strength;
            set
            {
                strength = value;
                OnPropertyChanged();
            }
        }


        public void OnEdit(bool b)
        {
	        var form = new EnterLabel();
	        form.Label.Text = Strength;
	        form.ShowDialog();
	        Strength = form.Label.Text;
        }
    }
}