using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using FluentValidation;
using Xamarin.Forms;
using XForms.Interfaces;
using XForms.Models;

namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class WalkthroughViewModel : BaseViewModel
    {
        public List<WalkthroughModel> WalkthroughList { get; set; }
        public int WalkthroughPosition { get; set; }

        // public ImageSource WalkthroughImageSource => ImageSource.FromResource(WalkthroughList[WalkthroughPosition].Image, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

        public WalkthroughViewModel()
        {
            WalkthroughList = new List<WalkthroughModel>()
            {
                new WalkthroughModel()
                {
                    Title = "Demande congé",
                    Description  = "Vous souhaitez partir en vacances et utiliserles congés payés",
                    Image = ImageSource.FromResource("XForms.Resources.Images." + "Walkthrough_1.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly)
                },
                new WalkthroughModel()
                {
                    Title = "Demande Attestation",
                    Description  = "Nous vous proposons plusieurs modèles d'attestationset de demande d'attestation.",
                    Image = ImageSource.FromResource("XForms.Resources.Images." +"Walkthrough_2.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly)

                },
                new WalkthroughModel()
                {
                    Title = "Amélioration interne",
                    Description  = "Aucune entreprise moderne ne peut ignorer le bien-être de ses collaborateurs.",
                    Image = ImageSource.FromResource("XForms.Resources.Images." + "Walkthrough_1.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                    IsLastStep = true
                }
            };
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

        }

        #region Commands
        public bool CanCommence { get; set; } = true;
        public ICommand CommenceCommand => new Command(() =>
        {
            try
            {
                CanCommence = false;

                AppPreferences.ClearCache();

                AppHelpers.SetInitialView();
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex);
            }
            finally
            {
                CanCommence = true;
            }
        }, () => CanCommence);
        #endregion
    }
}