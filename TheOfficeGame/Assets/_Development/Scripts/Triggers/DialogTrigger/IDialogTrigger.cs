public interface IDialogTrigger
{
    public bool HasDialogFinished { get; }
    public void OnStartDialog();
}
