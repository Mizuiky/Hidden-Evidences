using UnityEngine;
using UnityEngine.Events;

public class InteractableElevator : InteractableDoor
{
    public UnityEvent _goToFirstFloor;

    public override void Init()
    {
        base.Init();
        _isOpen = Animator.StringToHash("IsOpen");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _animator.SetBool(_isOpen, true);       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _animator.SetBool(_isOpen, false);
    }
}
