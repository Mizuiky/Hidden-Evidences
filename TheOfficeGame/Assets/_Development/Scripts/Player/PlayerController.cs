using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer
{
    [SerializeField]
    private MovementControler _movementController;
    [SerializeField]
    private InputControler _inputController;
    [SerializeField]
    private PlayerAnimation _animation;

    [SerializeField] private float _dectionRadius;
    [SerializeField] private LayerMask _detectionLayer;
    private Collider2D[] _results;

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
        _results = new Collider2D[1];
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

    public void ActivateInteraction()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, _dectionRadius, _results, _detectionLayer);

        if (count > 0)
        {
            Debug.Log("Try to interact");

            IDialogTrigger dialog = _results[0].gameObject.GetComponent<IDialogTrigger>();
            if (dialog != null)
            {
                Debug.Log("OnStartDialog");
                dialog.OnStartDialog();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _dectionRadius);
    }
}
