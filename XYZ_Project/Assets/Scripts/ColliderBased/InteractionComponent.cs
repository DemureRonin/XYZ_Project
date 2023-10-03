using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    
    public void DoInteraction(GameObject gameObject)
    {
       var interactable =  gameObject.GetComponent<InteractableComponent>();
        if (interactable != null)
            interactable.Interact();
    }
}
