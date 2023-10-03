using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class HeroInputSystem : MonoBehaviour
    {
        [SerializeField] Hero _hero;
        [SerializeField] private PauseComponent _pauseComponent;
        
        public void OnMovement(InputAction.CallbackContext context)
        {
                var direction = context.ReadValue<Vector2>();
                _hero.SetDirection(direction);
        }
      

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled )
            {
                _hero.Interact();
            }
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {

            if (context.canceled)
            {
                _hero.Attack();
            }
        }
        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.UseItem();
                _hero.Throw();
            }
        }
        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.HextItem();

        }
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (PauseComponent.IsGamePaused)
                _pauseComponent.Resume();
                else _pauseComponent.Pause();
            }
        }
    }
}
