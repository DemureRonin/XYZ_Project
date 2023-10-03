using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseComponent : MainMenu
{
    public static bool IsGamePaused = false;
    private Animator _pauseMenuAnimator;
    private Scene _scene;
    protected override void Start() 
    {
        _scene = SceneManager.GetActiveScene();
        _pauseMenuAnimator = GetComponent<Animator>();  
    }
    public void Resume()
    {
        _pauseMenuAnimator.SetTrigger(hide);
     
        IsGamePaused = false;
    }
    public void Pause()
    {
        _pauseMenuAnimator.SetTrigger(show);

        IsGamePaused = true;
    }
    public void ToggleTime()
    {
        if (IsGamePaused)
        {
            Time.timeScale = 0f;  
        }
        else Time.timeScale = 1f;

    }
    public void OnRestart()
    {
        SceneManager.LoadScene(_scene.name);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }
    public override void OnExit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public override void OnCloseAnimationComplete()
    {
      
    }

}
