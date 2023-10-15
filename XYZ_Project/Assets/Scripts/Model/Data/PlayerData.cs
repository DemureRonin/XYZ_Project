using System;
using Model.Data;
using UnityEngine;
namespace Scripts.Model
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;

        public IntProperties Hp = new IntProperties();
        public PerksData Perks = new PerksData();
        public InventoryData Inventory => _inventory;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }

    }
    
    
}

