using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour, IDialogTrigger
{
    [SerializeField] private int _dialogRoot;
    [SerializeField] private DialogWritter _writter;
    [SerializeField] private bool _changeDialog = false;
    [SerializeField] private int [] nextDialogRoot;

    public UnityEvent onDialogFinished;

    private bool hasDialogFinished = true;
    public bool HasDialogFinished {  get { return hasDialogFinished; } }

    private int rootCount = 0;
    private int root;

    public void OnEnable()
    {
        root = _dialogRoot;
        _dialogRoot = root;
    }

    public void Start()
    {
        _writter.endDialogEvent.AddListener(OnEndDialog);
        root = _dialogRoot;
    }

    public virtual void OnStartDialog()
    {
        if (_dialogRoot != -1)
        {
            //Debug.Log("Dialog != -1");

            if (hasDialogFinished)
            {
                hasDialogFinished = false;
                _writter.StartDialog(_dialogRoot);                 
            }
        }
    }

    public void OnEndDialog()
    {
        if (onDialogFinished != null)
            onDialogFinished?.Invoke();

        hasDialogFinished = true;
        if(_changeDialog)
        {          
            if(rootCount < nextDialogRoot.Length)
            {
                _dialogRoot = nextDialogRoot[rootCount];
                rootCount++;
            }            
        }           
    }
}

