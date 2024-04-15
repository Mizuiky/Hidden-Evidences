using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer
{
    [SerializeField]
    private MovementControler _movementController;
    [SerializeField]
    private InputControler _inputController;
    [SerializeField]
    private PlayerAnimation _animation;

    private Vector3 _movement;
    private bool _isMoving = false;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _isMoving = false;
        _movementController.Init(_animation);
        _inputController.Init(this);
    }

    private void FixedUpdate()
    {
        _animation.SetIsMoving(false);

        if (_isMoving)
        {
            _animation.SetIsMoving(true);
            _movementController.Move(_movement);           
        }         
    }

    public void MovePlayer(bool isMoving, Vector3 movement)
    {
        _isMoving = isMoving;
        _movement = movement;
    }
}
