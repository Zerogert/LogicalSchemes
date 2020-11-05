using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Logical_cxem.Models
{
    public abstract class BaseModel : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}