using System;
using Xamarin.Forms;

namespace XForms.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        public BaseViewModel()
        {
        }

        public virtual void OnAppearing()
        {
            try
            {

            }
            catch (Exception ex)
            {
                //Logger?.LogError(ex);
            }
            finally
            {

            }
        }
    }
}
