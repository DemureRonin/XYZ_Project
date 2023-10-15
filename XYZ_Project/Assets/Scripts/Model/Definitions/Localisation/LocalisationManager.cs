using System;
using System.Collections.Generic;
using Model.Data.Properties;
using UnityEngine;

namespace Scripts.Model.Definitions.Localisation
{
    public class LocalisationManager
    {
        public readonly static LocalisationManager I;
        private Dictionary<string, string> _localization;
        public event Action OnLocaleChanged;

        private StringPersistentProperties _localeKey = new StringPersistentProperties("en", "localisation/current");
        static LocalisationManager ()
        {
            I = new LocalisationManager();
        }

        public LocalisationManager()
        {
            LoadLocale(_localeKey.Value);
        }

        private void LoadLocale(string localeToLoad)
        {
            var def  =  Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
            _localization = def.GetData();
            OnLocaleChanged?.Invoke();
        }


        public string Localize(string key)
        {
            return _localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }
    }
}