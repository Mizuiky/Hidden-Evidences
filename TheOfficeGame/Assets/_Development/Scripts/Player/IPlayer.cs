using UnityEngine;

public interface IPlayer
{
    public void Init();
    public void MovePlayer(bool isMoving, Vector3 movement);
    public void ActivateInteraction();
}
