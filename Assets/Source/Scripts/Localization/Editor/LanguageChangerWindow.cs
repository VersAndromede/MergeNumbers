using Scripts.Localization;
using UnityEditor;
using UnityEngine;

namespace EditorWindows
{
    public class LanguageChangerWindow : EditorWindow
    {
        private LocalizationSetter _localizationSetter;

        [MenuItem("Window/Language Changer")]
        public static void ShowLanguageChangerWindow()
        {
            LanguageChangerWindow languageChangerWindow = GetWindow<LanguageChangerWindow>();
            languageChangerWindow.titleContent = new GUIContent("Language Changer");
        }

        private void OnEnable()
        {
            _localizationSetter = new LocalizationSetter();
        }

        private void OnGUI()
        {
            if (GUILayout.Button(LocalizationSetter.EnLanguage))
                _localizationSetter.Set(LocalizationSetter.EnLocale);

            if (GUILayout.Button(LocalizationSetter.RuLanguage))
                _localizationSetter.Set(LocalizationSetter.RuLocale);

            if (GUILayout.Button(LocalizationSetter.TrLanguage))
                _localizationSetter.Set(LocalizationSetter.TrLocale);
        }
    }
}