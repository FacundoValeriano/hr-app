﻿namespace HrApp
{
    public static class Constants
    {
        public static string AppName = "HrApp";

        // Google OAuth
        // For Google login, configure at https://console.developers.google.com/

        // These values do not need changing
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string GoogleiOSRedirectUrl = "<insert IOS redirect URL here>:/oauth2redirect";
        public static string GoogleAndroidRedirectUrl = "com.googleusercontent.apps.456602862781-74ndmnl7a4d5i6hfp56eovmk9428066v:/oauth2redirect";

        //-------------------------------------------------------------------------------------------------------

        // Magic strings
        public static string ValidatedUserToken = "Token";
        public static string BaseApiUrl = "BaseApiUrl";
        public static string GoogleAndroidClientId = "GoogleAndroidClientId";
        public static string GoogleiOSClientId = "GoogleiOSClientId";
    }
}