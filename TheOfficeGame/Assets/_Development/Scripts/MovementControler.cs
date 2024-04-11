using UnityEngine;

public enum speedType
{
    Normal,
    Fast,
    Slow
}

public class MovementControler : MonoBehaviour
{
    [SerializeField]
    private MovementSettings _settings;

    private float _speedMultiplier;
    private float _result;
    private float _angle;

    private void Start()
    {
        _speedMultiplier = _settings.normalSpeedMultiplier;    
    }

    public void Move(Vector3 movement)
    {
        Debug.Log(movement);

        if(movement != Vector3.zero)
        {
            transform.position +=  movement * _speedMultiplier * Time.deltaTime;
            Rotate(movement);
        }
    }

    private void Rotate(Vector3 movement)
    {
        _angle = Mathf.Atan2(movement.z, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(_angle, 0, 0);
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
