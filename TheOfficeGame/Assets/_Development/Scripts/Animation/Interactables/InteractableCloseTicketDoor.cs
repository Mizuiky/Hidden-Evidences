using Test.Event;
using UnityEngine;

public class InteractableCloseTicketDoor : InteractableBase
{
    [SerializeField] private GameEventObject _onChangeTicketDoorState;
    private Animator _animator;

    private int _isOpen;
    private bool _canOpen = false;

    public bool CanOpen { get { return _canOpen; } }

    public override void Init()
    {
        base.Init();
        _isOpen = Animator.StringToHash("IsOpen");
        _onChangeTicketDoorState.Subscribe(SetCurrentDoor);
    }

    public void SetCurrentDoor(object[] param)
    {
        _animator = param[0] as Animator;            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (_animator != null && _animator.GetBool(_isOpen))
                _animator.SetBool(_isOpen, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _animator = null;
    }
}
