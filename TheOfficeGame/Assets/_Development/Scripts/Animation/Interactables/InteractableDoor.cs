using Test.Event;
using UnityEngine;

public class InteractableDoor : InteractableBase
{
    [SerializeField] protected Animator _animator;
    [SerializeField] private GameEventObject _onChangeDoorState;

    protected int _isOpen;

    public override void Init()
    {
        base.Init();
        _isOpen = Animator.StringToHash("IsOpen");
    }

    public override void Interact()
    {
        base.Interact();
        _animator.SetBool(_isOpen, true);
        _onChangeDoorState?.Invoke(_animator);
    }
}
