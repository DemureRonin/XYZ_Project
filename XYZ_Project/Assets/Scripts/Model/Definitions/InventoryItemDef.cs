﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
public class InventoryItemDef : ScriptableObject
{
    [SerializeField] private ItemDef[] _items;
    public ItemDef Get(string id)
    {
        foreach (var itemDef in _items)
        {
            if(itemDef.Id == id) return itemDef;
        }
        return default;
    }
#if UNITY_EDITOR
    public ItemDef[] ItemsForEditor => _items;
#endif
}

[Serializable]
public struct ItemDef
{
    [SerializeField] private string _id;
    [SerializeField] private bool _setMaxValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private ItemTag[] _tags;
    [SerializeField] private Sprite _icon;
    public bool SetMaxValue => _setMaxValue;
    public int MaxValue => _maxValue;   
    public string Id => _id;
    
    public bool IsVoid => string.IsNullOrEmpty(_id);
    public Sprite Icon => _icon;
    public bool HasTag(ItemTag tag)
    {
        return _tags.Contains(tag); 
    }
}