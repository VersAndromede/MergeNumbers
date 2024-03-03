using Lean.Localization;
using System;

namespace Scripts.Localization
{
    public class LocalizationSetter
    {
        public const string EnLanguage = "English";
        public const string RuLanguage = "Russian";
        public const string TrLanguage = "Turkish";

        public const string EnLocale = "en";
        public const string RuLocale = "ru";
        public const string TrLocale = "tr";

        public void Set(string locale)
        {
            switch (locale)
            {
                case EnLocale:
                    SetLanguage(EnLanguage);
                    break;
                case RuLocale:
                    SetLanguage(RuLanguage);
                    break;
                case TrLocale:
                    SetLanguage(TrLanguage);
                    break;
                default:
                    throw new ArgumentException(locale);
            }
        }

        private void SetLanguage(string language)
        {
            LeanLocalization.SetCurrentLanguageAll(language);
        }
    }
}