using System;
using System.Collections;
using System.Collections.Generic;
using Model.Definitions.Repositories;
using UnityEngine;
[CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]

public class DefsFacade : ScriptableObject
{
    [SerializeField] private InventoryItemDef _items;
    [SerializeField] private ThrowableItemsDef _throwableItems;
    [SerializeField] private PlayerDef _player;
    [SerializeField] private PerkRepository _perks;


    public InventoryItemDef Items => _items;
    public PerkRepository Perks => _perks;

    public ThrowableItemsDef ThrowableItems => _throwableItems;
    public PlayerDef Player => _player;

    private static DefsFacade _instance;
    public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

    private static DefsFacade LoadDefs()
    {
        return _instance = Resources.Load<DefsFacade>("DefsFacade");
    }
}

   
