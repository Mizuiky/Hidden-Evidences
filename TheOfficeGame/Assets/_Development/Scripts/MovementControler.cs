using UnityEngine;

public enum speedType
{
    Normal,
    Fast,
    Slow
}

[RequireComponent(typeof(Rigidbody2D))]
public class MovementControler : MonoBehaviour
{
    [SerializeField]
    private MovementSettings _settings;
    private Rigidbody2D _rb;

    private AnimationBase _animation;
    private float _speedMultiplier;
    private float _result;
    private float _angle;

    private void Start()
    {
        _speedMultiplier = _settings.normalSpeedMultiplier;
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(AnimationBase animationBase)
    {
        _animation = animationBase;
    }

    public void Move(Vector2 movement)
    {
        if (movement != Vector2.zero)
        {
            _animation.SetWalk(movement.x, movement.y);
            _rb.MovePosition(_rb.position + movement * _speedMultiplier * Time.deltaTime);       
            //Rotate(movement);
        }

        _animation.SetLastWalk(movement.x, movement.y);
    }

    private void Rotate(Vector3 movement)
    {
        _angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
    }

    public void ChangeSpeed(speedType speedType)
    {
        _result = speedType switch
        {
            speedType.Normal => _settings.normalSpeedMultiplier,
            speedType.Fast => _settings.fastSpeedMultiplier,
            speedType.Slow => _settings.slowSpeedMultiplier,
            _ => _settings.normalSpeedMultiplier
        };

        _speedMultiplier = _result;
    }
}
