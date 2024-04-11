using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer
{
    [SerializeField]
    private MovementControler _movementController;
    [SerializeField]
    private InputControler _inputController;

    private Vector3 _movement;
    private bool _isMoving = false;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _isMoving = false;
        _inputController.Init(this);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(_isMoving)
            _movementController.Move(_movement);
    }

    public void MovePlayer(bool isMoving, Vector3 movement)
    {
        _isMoving = isMoving;
        _movement = movement;
    }
}
