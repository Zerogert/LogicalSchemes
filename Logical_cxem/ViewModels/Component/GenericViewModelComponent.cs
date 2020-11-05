using System;
using System.Windows;
using System.Windows.Controls;

namespace Logical_cxem.ViewModels.Component
{
    public class GenericViewModelComponent<T, U> : BaseViewModelComponent
        where T : BaseComponentUI where U : BaseElement
    {
        public GenericViewModelComponent(Point point, Canvas surface, int id = -1)
        {
            BaseComponentUI componentUi;
            if (id < 0)
            {
                componentUi = (T) Activator.CreateInstance(typeof(T), point, BaseElement.IdCounter);
                element = (U) Activator.CreateInstance(typeof(U), BaseElement.IdCounter);
            }
            else
            {
                componentUi = (T) Activator.CreateInstance(typeof(T), point, id);
                element = (U) Activator.CreateInstance(typeof(U), id);
            }

            componentUi.DataContext = element;
            surface.Children.Add(componentUi);
            componentUi.OnPinDropped += OnPinDrop;
            componentUi.OnDeleteElement += OnDelete;
        }
    }
}