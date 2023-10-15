using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Model;

namespace Scripts.Components
{

    public class ReloadLevel : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            session.LoadLastSave();
            
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}


