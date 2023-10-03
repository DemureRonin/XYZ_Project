using UnityEngine;
using System;
using UnityEngine.Events;


namespace Scripts
{
    public class EnterCollision : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _action;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                if (_action != null )
                {
                                                         
                        _action.Invoke(other.gameObject);
                                       
                }

                 
            }
        }
       
    }
    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {

    }
}

