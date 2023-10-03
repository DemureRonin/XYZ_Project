using Scripts;
using Scripts.Collectables;
using Scripts.Model;
using UnityEngine;

public class CollectablesComponent : MonoBehaviour
{
    [SerializeField] private EnterEvent _action;

    private GameSession _session;
    private InventoryAddComponent _inventoryItem;



    private void Awake()
    {
        _session = FindObjectOfType<GameSession>();
        _inventoryItem = GetComponent<InventoryAddComponent>();    
    }
    public void Check(GameObject other)
    {
        var itemDef = DefsFacade.I.Items.Get(_inventoryItem.Id);
       
        var itemCount = _session.Data.Inventory.Count(_inventoryItem.Id);
        
        
        if ((itemCount < itemDef.MaxValue && itemDef.SetMaxValue) || !itemDef.SetMaxValue)
        {
            _action?.Invoke(other.gameObject);
        }
    }
}
