﻿using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrew.Utils.Disposables;
using Scripts.Model;
using UnityEngine;

public class QuickInventoryModel : IDisposable
{
    private readonly PlayerData _data;

    public InventoryItemData[] Inventory { get; private set; }

    public readonly IntProperties SelectedIndex = new IntProperties();

    public event Action OnChanged;

    public InventoryItemData SelectedItem => Inventory[SelectedIndex.Value];

    public QuickInventoryModel(PlayerData data)
    {
        _data = data;

        Inventory = _data.Inventory.GetAll(ItemTag.Usable);
        _data.Inventory.OnChanged += OnChangedInventory;
    }

    public IDisposable Subscribe(Action call)
    {
        OnChanged += call;
        return new ActionDisposable(() => OnChanged -= call);
    }

    private void OnChangedInventory(string id, int value)
    {
        Inventory = _data.Inventory.GetAll(ItemTag.Usable);
        SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
        OnChanged?.Invoke();
    }

    public void SetNextItem()
    {
        SelectedIndex.Value = (int) Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
    }

    public void Dispose()
    {
        _data.Inventory.OnChanged -= OnChangedInventory;
    }
}
