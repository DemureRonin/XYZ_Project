using UnityEngine;
namespace Scripts
{
    public class ResetHero : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;
        


        public void Teleport(GameObject target)
        {
            target.transform.position = _destTransform.position;
            

        }
    }
}

