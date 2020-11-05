using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Logical_cxem.Commands;
using Logical_cxem.Models;
using Logical_cxem.View;

namespace Logical_cxem.ViewModels.Component
{
    public class BaseViewModelComponent : BaseViewModel, IDisposable
    {
        public delegate void _elementDelete(int id, UserControl element);

        public delegate void _onPinElementDropped(DataLine dataLine);

        private ICommand _deleteCommand;
        public string strength = "Подпись";
        public BaseElement element;
        public string Strength {
	        get => strength;
	        set {
		        strength = value;
		        OnPropertyChanged();
	        }
        }
        public int Id => element.Id;

        public ICommand DeleteCommand
        {
            get
            {
                _deleteCommand = _deleteCommand ?? new Command(DeleteProcessCommand);
                return _deleteCommand;
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        public event _onPinElementDropped OnPinElementDropped;
        public event _elementDelete OnElementDelete;

        public void OnDelete(int id, UserControl element)
        {
            OnElementDelete(Id, element);
        }

        private void DeleteProcessCommand()
        {
        }

        public List<ConnectionLine> GetAllLine()
        {
            var lines = new List<ConnectionLine>();

            return lines;
        }

        public void Delete()
        {
            element.Delete();
        }

        public void OnPinDrop(DataLine dataLine)
        {
            OnPinElementDropped(dataLine);
        }

        public BaseElement GetElementModel()
        {
            return element;
        }

        ~BaseViewModelComponent()
        {
            ReleaseUnmanagedResources();
        }
        public void OnEdit(bool b) {
	        var form = new EnterLabel();
	        form.Label.Text = Strength;
	        form.ShowDialog();
	        Strength = form.Label.Text;
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }
    }
}