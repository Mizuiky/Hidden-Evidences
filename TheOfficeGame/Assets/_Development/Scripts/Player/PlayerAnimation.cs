public class PlayerAnimation : AnimationBase
{
    private const string isMoving = "IsMoving";

    public void SetIsMoving(bool isPressing)
    {
        _animator.SetBool(isMoving, isPressing);
    }
}
