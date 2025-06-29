﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Model.Definitions.Repositories;
using Scripts.Model;
using UnityEngine;
[Serializable]
public class InventoryData 
{
    [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

    public delegate void OnInventoryChanged(string id, int value);

    public OnInventoryChanged OnChanged;
    public void AddItem(string id, int value)
    {
        var item = GetItem(id);


        var itemDef = DefsFacade.I.Items.Get(id);
        if (itemDef.SetMaxValue && item?.Value >= itemDef.MaxValue) return;
        if (value <= 0) return;
        if (itemDef.IsVoid) return;


        if (item == null || !itemDef.HasTag(ItemTag.Stackable))
        {
            item = new InventoryItemData(id);
            _inventory.Add(item);
        }

        item.Value += value;
        OnChanged?.Invoke(id, Count(id));
    }

    public void RemoveItem(string id, int value)
    {
        var itemDef = DefsFacade.I.Items.Get(id);
        if (itemDef.IsVoid) return;

        var item = GetItem(id);
        if (item == null)
        {
            return;
        }
        item.Value -= value;
        if (item.Value <= 0)
        {
            _inventory.Remove(item);
        }
        OnChanged?.Invoke(id, Count(id));

    }
    public InventoryItemData[] GetAll(params ItemTag[] tags)
    {
        var returnValue = new List<InventoryItemData>();
        foreach (var item in _inventory)
        {
            var itemDef = DefsFacade.I.Items.Get(item.Id);
            var isAllRequirementsMet = tags.All(x => itemDef.HasTag(x));
            if (isAllRequirementsMet)
            {
                returnValue.Add(item);
            }
           
        }
        return returnValue.ToArray();
    }
        

    public int Count(string id)
    {
        var count = 0;   
        foreach ( var item in _inventory)
        {
            if (item.Id == id) count += item.Value;
        }
        return count;
  
    }

    private InventoryItemData GetItem(string id)
    {
        foreach (var itemData in _inventory) 
        {
            if (itemData.Id == id) return itemData;
        }
        return null;
    }
    public bool IsEnough(params ItemWithCount[] items)
    {
        var joined = new Dictionary<string, int>();

        foreach (var item in items)
        {
            if (joined.ContainsKey(item.ItemId))
                joined[item.ItemId] += item.Count;
            else
                joined.Add(item.ItemId, item.Count);
        }

        foreach (var kvp in joined)
        {
            var count = Count(kvp.Key);
            if (count < kvp.Value) return false;
        }

        return true;
    }
}
[Serializable]
public class InventoryItemData
{
    [InventoryId] public string Id;
    public int Value;
    
    public InventoryItemData(string id) 
    {
        Id = id;
  
    }
}
