using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace XForms.Models
{
    public partial class REFItem
    {
        public long Id { get; set; }
        public string Name { get; set; }

    }

    // [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class REFItem : BindableObject
    {
        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    base.OnPropertyChanged(propertyName);

        //    if (propertyName == nameof(IsSelected))
        //    {
        //        OnPropertyChanged(nameof(BackgroundColor));
        //    }
        //}
        //public string Icon { get; set; }

        private bool isSelected;

        public bool IsSelected {
            get { return isSelected; }
            set {
                isSelected = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }

        }

        //private Color backgroundColor = Color.Gray;

        public Color BackgroundColor => IsSelected ? Color.FromHex("#0C53A4") : Color.FromHex("#D4DBEA");
        public Color CertaficateBackgroundColor => IsSelected ? Color.White : Color.FromHex("#0D69B6");

        public Color BackgroundColorAdmin => IsSelected ? Color.White : Color.FromHex("#a855f7");
        public Color TextColorAdmin => IsSelected ? Color.FromHex("#690DB1") : Color.White;
        public Color TextColor => IsSelected ? AppHelpers.LookupColor("EndGradient") : Color.White;




        //{
        //    get { return IsSelected ? Color.FromHex("#0C53A4") : Color.Gray; }
        //    set
        //    {
        //        backgroundColor = value;

        //        OnPropertyChanged();
        //    }
        //}

        //=> 


        //public Color TextColor => IsSelected ? Color.White : Color.Gray;


    }


}
