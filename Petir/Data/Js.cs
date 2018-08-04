using System;

namespace Petir
{
    public class Js
    {
        public static string inject
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"storage\inject.js";
            }
        }
        public static string autofill
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"storage\autofill.js";
            }
        }
    }
}
