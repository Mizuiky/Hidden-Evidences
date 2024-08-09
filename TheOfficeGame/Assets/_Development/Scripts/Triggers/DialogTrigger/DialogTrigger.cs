using UnityEngine;
using UnityEngine.Events;

namespace JAM.Dialog
{
    public class DialogTrigger : MonoBehaviour, IDialogTrigger
    {
        [SerializeField] private int _dialogRoot;
        [SerializeField] private DialogWritter _writter;

        public UnityEvent onDialogFinished;

        private bool hasDialogFinished = true;
        public bool HasDialogFinished {  get { return hasDialogFinished; } }

        public void Start()
        {
            _writter.endDialogEvent.AddListener(OnEndDialog);
        }

        public virtual void OnStartDialog()
        {
            Debug.Log("OnStartDialog");

            if (_dialogRoot != -1)
            {
                Debug.Log("Dialog != -1");

                if (hasDialogFinished)
                {
                    Debug.Log("Started dialog");
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
        }
    }
}
