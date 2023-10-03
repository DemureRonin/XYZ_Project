using UnityEngine;

namespace Scripts.Components
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damageDelta;
        public int DamageDelta => _damageDelta;
        
        public void ApplyDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_damageDelta);
            }
           
        }
        public void Block(GameObject target)
        {
            var blockParticle = target.GetComponent<SpawnListComponent>();
            
                blockParticle.Spawn("Block");
            
        }
    }
}
