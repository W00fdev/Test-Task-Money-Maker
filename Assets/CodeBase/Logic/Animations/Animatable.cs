using UnityEngine;

[RequireComponent(typeof(Animator))]
public partial class Animatable : MonoBehaviour
{
    [SerializeField] private string _deadTriggerName;
    
    private Animator _animator;
    private int _deadTriggerHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _deadTriggerHash = Animator.StringToHash(_deadTriggerName);
    }

    public void PlayAnimationType(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.DEAD:
                PlayAnimation(_deadTriggerHash);
                break;
            default:
                throw new System.ArgumentException("No such type of animation");
        }
    }

    private void PlayAnimation(int animationTypeHash)
    {
        // Prevent multiple triggering
        if (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash != animationTypeHash)
            _animator.SetTrigger(animationTypeHash);
    }
}