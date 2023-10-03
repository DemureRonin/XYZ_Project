using UnityEngine;

public class AnimatedWindow : MonoBehaviour
{
    private Animator _animator;
    protected static readonly int show = Animator.StringToHash("Show");
    protected static readonly int hide = Animator.StringToHash("Hide");
    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(show);
    }
    public virtual void Close()
    {
        _animator.SetTrigger(hide);

    }
    public virtual void OnCloseAnimationComplete()
    {
        Destroy(gameObject);
    }
}
