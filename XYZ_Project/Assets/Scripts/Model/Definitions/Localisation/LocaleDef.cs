using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Defs/LocaleDef", fileName = "LocaleDef")]
public class LocaleDef : ScriptableObject
{
   [SerializeField] private string _url;
   [SerializeField] private List<LocaleItems> _localeItems;

   private UnityWebRequest _request;
   [ContextMenu("Update Locale")]
   public void LoadLocales()
   {
       if (_request != null) return;
       _request = UnityWebRequest.Get(_url);
       _request.SendWebRequest().completed += OnDataLoaded;

   }

   public Dictionary<string, string> GetData()
   {
       var dictionary = new Dictionary<string, string>();
       foreach (var localeItem in _localeItems)
       {
           dictionary.Add(localeItem.key, localeItem.value);
       }

       return dictionary;

   }

   private void OnDataLoaded(AsyncOperation operation)
   {
       if (operation.isDone)
       {
           var rows = _request.downloadHandler.text.Split('\n');
           _localeItems.Clear();
           var items = new List<LocaleItems>();
           foreach (var row in rows)
           {
               AddLocaleItem(row);
           }
       }
   }

   private void AddLocaleItem(string row)
   {
       try
       {
            var parts = row.Split('\t');
            _localeItems.Add(new LocaleItems{key = parts[0], value = parts[1]});
       }
       catch (Exception exception)
       {
           Debug.LogError($"Can`t parse row {row}.\n {exception}");
           throw;
       }
   }

   [Serializable]
   private class LocaleItems
   {
       public string key;
       public string value;
   }
}
