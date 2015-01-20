using System;
using System.Linq.Expressions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;

namespace NetworkTracker.WindowsPhone.ViewModel
{
    /// <summary>
    /// Base ViweModel class
    /// </summary>
    public class BaseViewModel : ViewModelBase
    {
        protected void SetProperty<T>(Expression<Func<T>> expression, ref T storageValue, T newValue)
        {
            if (!Equals(storageValue, newValue))
            {
                storageValue = newValue;
                InvokeOnUI(() => RaisePropertyChanged(expression));
            }
        }

        protected void InvokeOnUI(Action action)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(action);
        }
    }
}
