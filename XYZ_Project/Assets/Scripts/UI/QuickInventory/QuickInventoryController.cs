﻿using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrew.Utils.Disposables;
using Scripts.Model;
using UI.HUD;
using UnityEngine;

public class QuickInventoryController : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private InventoryItemWidget _prefab;
    
    [SerializeField] private CoinHud _coinHud;

    private readonly CompositeDisposable _trash = new CompositeDisposable();
    private GameSession _session;
    private List<InventoryItemWidget> _createdItems = new List<InventoryItemWidget>();

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();

        _trash.Retain(_session.QuickInventory.Subscribe(Rebuild));
        Rebuild();
    }

    private void Rebuild()
    {
        
        var inventory = _session.QuickInventory.Inventory;
        for (var i = _createdItems.Count; i < inventory.Length; i++)
        {
            var item = Instantiate(_prefab, _container);
            _createdItems.Add(item);
        }
        for (var i = 0; i < inventory.Length; i++)
        {
            _createdItems[i].SetData(inventory[i], i);
            _createdItems[i].gameObject.SetActive(true);
        }
        for (var i = inventory.Length; i < _createdItems.Count; i++)
        {
            _createdItems[i].gameObject.SetActive(false);
        }
        _coinHud.OnValueChanged();
    }
}
