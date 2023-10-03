using UnityEngine.UI;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    public void SetProgress(float progress)
    {
        _bar.fillAmount = progress;
    }
}
