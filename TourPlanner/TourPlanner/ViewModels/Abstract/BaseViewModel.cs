using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.ViewModels.Abstract
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;
	    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	    {
		    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
    }
}
