using System;
using Xamarin.Essentials;

namespace XForms
{
    public static class AppPreferences
    {

        public static string Token
        {
            get { return Preferences.Get(nameof(Token), string.Empty); }
            set { Preferences.Set(nameof(Token), value); }
        }

        public static string UserId
        {
            get { return Preferences.Get(nameof(UserId), string.Empty); }
            set { Preferences.Set(nameof(UserId), value); }
        }


        public static bool IsVerified
        {
            get { return Preferences.Get(nameof(IsVerified), false); }
            set { Preferences.Set(nameof(IsVerified), value); }
        }

        public static string PrixDiagnostic
        {
            get { return Preferences.Get(nameof(PrixDiagnostic), string.Empty); }
            set { Preferences.Set(nameof(PrixDiagnostic), value); }
        }

        public static string Email
        {
            get { return Preferences.Get(nameof(Email), string.Empty); }
            set { Preferences.Set(nameof(Email), value); }
        }

        public static string FullName
        {
            get { return Preferences.Get(nameof(FullName), string.Empty); }
            set { Preferences.Set(nameof(FullName), value); }
        }


        public static string UserRole
        {
            get { return Preferences.Get(nameof(UserRole), string.Empty); }
            set { Preferences.Set(nameof(UserRole), value); }
        }

        public static string FirebaseToken
        {
            get { return Preferences.Get(nameof(PrixDiagnostic), string.Empty); }
            set { Preferences.Set(nameof(FirebaseToken), value); }
        }

        public static string HashPass
        {
            get { return Preferences.Get(nameof(HashPass), string.Empty); }
            set { Preferences.Set(nameof(HashPass), value); }
        }

        public static string CurrentSiteId
        {
            get { return Preferences.Get(nameof(CurrentSiteId), string.Empty); }
            set { Preferences.Set(nameof(CurrentSiteId), value); }
        }

        public static string StoredDemarqueProductList
        {
            get { return Preferences.Get(nameof(StoredDemarqueProductList), string.Empty); }
            set { Preferences.Set(nameof(StoredDemarqueProductList), value); }
        }

        public static string SelectedDemarqueTypeId
        {
            get { return Preferences.Get(nameof(SelectedDemarqueTypeId), string.Empty); }
            set { Preferences.Set(nameof(SelectedDemarqueTypeId), value); }
        }

        public static string SelectedDemarqueTypeName
        {
            get { return Preferences.Get(nameof(SelectedDemarqueTypeName), string.Empty); }
            set { Preferences.Set(nameof(SelectedDemarqueTypeName), value); }
        }

        public static bool IsSignIn
        {
            get { return Preferences.Get(nameof(IsSignIn), false); }
            set { Preferences.Set(nameof(IsSignIn), value); }
        }

        public static bool IsHasNotchScreen
        {
            get { return Preferences.Get(nameof(IsHasNotchScreen), false); }
            set { Preferences.Set(nameof(IsHasNotchScreen), value); }
        }

        public static bool IsAleardyCheckHasNotchScreen
        {
            get { return Preferences.Get(nameof(IsAleardyCheckHasNotchScreen), false); }
            set { Preferences.Set(nameof(IsAleardyCheckHasNotchScreen), value); }
        }

        public static string StoredSitesData
        {
            get { return Preferences.Get(nameof(StoredSitesData), string.Empty); }
            set { Preferences.Set(nameof(StoredSitesData), value); }
        }

        public static DateTime ValidSession
        {
            get { return Preferences.Get(nameof(ValidSession), new DateTime()); }
            set { Preferences.Set(nameof(ValidSession), value); }
        }

        public static string PictureUrl
        {
            get { return Preferences.Get(nameof(PictureUrl), string.Empty); }
            set { Preferences.Set(nameof(PictureUrl), value); }
        }

        public static string RefFunctionLabel
        {
            get { return Preferences.Get(nameof(RefFunctionLabel), string.Empty); }
            set { Preferences.Set(nameof(RefFunctionLabel), value); }
        }

        public static double LastKnownLatitude
        {
            get { return Preferences.Get(nameof(LastKnownLatitude), 0); }
            set { Preferences.Set(nameof(LastKnownLatitude), value); }
        }

        public static double LastKnownLongitude
        {
            get { return Preferences.Get(nameof(LastKnownLongitude), 0); }
            set { Preferences.Set(nameof(LastKnownLongitude), value); }
        }
        public static bool EnableFaceID
        {
            get { return Preferences.Get(nameof(EnableFaceID), false); }
            set { Preferences.Set(nameof(EnableFaceID), value); }
        }

        public static bool IsAleardyLoggedIn
        {
            get { return Preferences.Get(nameof(IsAleardyLoggedIn), false); }
            set { Preferences.Set(nameof(IsAleardyLoggedIn), value); }
        }


        public static bool IsDigitalPrintActived
        {
            get { return Preferences.Get(nameof(IsDigitalPrintActived), false); }
            set { Preferences.Set(nameof(IsDigitalPrintActived), value); }
        }

        public static bool IsLoggedIn
        {
            get { return Preferences.Get(nameof(IsLoggedIn), false); }
            set { Preferences.Set(nameof(IsLoggedIn), value); }
        }

        



        public static void ClearCache()
        {
            //Preferences.Remove(nameof(CurrentSiteId));
            //Preferences.Remove(nameof(IsSignIn));
            //Preferences.Remove(nameof(StoredSitesData));
            //Preferences.Remove(nameof(HashPass));
            //Preferences.Remove(nameof(Token));
            //Preferences.Remove(nameof(PictureUrl));
            Preferences.Clear();



            //Preferences.Remove(nameof(ProductsInCartList));
        }
    }
}
