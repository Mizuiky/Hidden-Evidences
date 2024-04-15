using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MoveSettings")]
public class MovementSettings : ScriptableObject
{
    public float normalSpeedMultiplier;
    public float fastSpeedMultiplier;
    public float slowSpeedMultiplier;
}
