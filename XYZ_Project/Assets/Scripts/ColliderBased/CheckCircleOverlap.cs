using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Linq;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private OnOverlapEvent _onOverlap;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private string[] _tags;
    private  readonly Collider2D[] _interactionResult = new Collider2D[10];
   
 
    internal void Check()
    {
        var size = Physics2D.OverlapCircleNonAlloc(
                 transform.position,
                 _radius,
                 _interactionResult,
                 _layer);
        var overlaps = new List<GameObject>();
        for (var i = 0; i < size; i++)
        {
            var overlapResult = _interactionResult[i];
            var isInTags = _tags.Any(tag => _interactionResult[i].CompareTag(tag));
            if (isInTags)
            _onOverlap?.Invoke(_interactionResult[i].gameObject);
            
        }
        
    }

    
}
[Serializable]
public class OnOverlapEvent : UnityEvent<GameObject>
{

}
