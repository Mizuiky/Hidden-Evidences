using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationBase : MonoBehaviour, IAnimation
{
    protected Animator _animator;

    #region Walk
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";
    #endregion

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalk(float horizontalValue, float verticalValue)
    {
        _animator.SetFloat(_horizontal, horizontalValue);
        _animator.SetFloat(_vertical, verticalValue);
    }

    public void SetLastWalk(float lastHorizontal, float lastVertical)
    {
        _animator.SetFloat(_lastHorizontal, lastHorizontal);
        _animator.SetFloat(_lastVertical, lastVertical);
    }
}
