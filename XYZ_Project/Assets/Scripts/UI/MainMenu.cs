using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class MainMenu : AnimatedWindow
{
    protected Action _closeAction;
    public void OnShowSettings()
    {
        var window = Resources.Load<GameObject>("UI/Options");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
    }
    public void OnStartGame()
    {
        _closeAction = () => { SceneManager.LoadScene("Level 1"); };

     
        Close();
    }
    public void OnLanguages()
    {
        WindowUtils.CreateWindow("UI/LocalizationWindow");
    }
    public virtual void OnExit()
    {

        _closeAction = () =>
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        };
        Close();

    }
    public override void OnCloseAnimationComplete()
    {
        base.OnCloseAnimationComplete();
        SceneManager.LoadScene("Level 1");
        _closeAction?.Invoke(); 
    }

}
