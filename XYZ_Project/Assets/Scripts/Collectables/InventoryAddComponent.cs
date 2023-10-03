using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;
namespace Scripts.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {

        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _count;
        public string Id => _id;

        
        public void Add(GameObject gameObject)
        {
            var hero = gameObject.GetComponent<Hero>();
            if (hero != null)
            {
                hero.AddInInventory(_id, _count);
            }
        }
       
    }
}

