using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Model;

namespace Scripts.Components
{

    public class ReloadLevel : MonoBehaviour
    {
        
        
        public void Start()
        {
           
        }
        public void Reload()
        {

            
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);


            

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            


            



        }
        
       
    }
}


