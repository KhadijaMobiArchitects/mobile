using System;
namespace XForms.Interface
{
    public interface IToast
    {
        void Alert(string message, ToastLength toastLength, bool showActionButtons);
    }

    public enum ToastLength
    {
        SHORT = 0,
        LONG = 1,
    }
}
