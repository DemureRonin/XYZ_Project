﻿using System.Collections;
using System.Collections.Generic;
using Scripts.Model.Definitions.Localisation;
using UI.Localisation;
using UnityEngine;

public class LocalizationWindow : AnimatedWindow
{
    [SerializeField] private Transform _container;
    [SerializeField] private LocaleItemWidget _prefab;

    private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;

    private readonly string[] _supportedLocales = {"en", "ru", "es"};

    protected override void Start()
    {
        base.Start();
        _dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);
        _dataGroup.SetData(ComposeData());
    }

    private List<LocaleInfo> ComposeData()
    {
        var data = new List<LocaleInfo>();
        foreach (var locale in _supportedLocales)
        {
            data.Add(new LocaleInfo {LocaleId = locale});
        }

        return data;
    }

    public void OnSelected(string selectedLocale)
    {
        LocalizationManager.I.SetLocale(selectedLocale);
    }
}
