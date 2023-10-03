using System.Collections;
using System.Collections.Generic;
using Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Scripts.Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        public void ExitLevel()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}

