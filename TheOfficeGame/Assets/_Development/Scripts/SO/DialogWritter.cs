using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[CreateAssetMenu(menuName = "Data/Dialog/DialogWritter")]
public class DialogWritter : ScriptableObject
{
    #region Events

    [System.NonSerialized]
    public UnityEvent startDialogEvent;

    [System.NonSerialized]
    public UnityEvent endDialogEvent;

    [System.NonSerialized]
    public UnityEvent enableNextButtonEvent;

    [System.NonSerialized]
    public UnityEvent<List<Option>> updateOptionsEvent;

    [System.NonSerialized]
    public UnityEvent<Sprite> changePortraitEvent;

    #endregion

    public DialogLoader _dialogLoader;
    public LocalizationHandler _localizationHandler;

    public SOText _dialogText;
    public SOText _dialogName;

    public float _timeBetweenLetters = 0.03f;

    public Sprite _defaultPortrait;

    #region Private Fields

    private int _nextNode;
    private int _dialogIndex;

    private Node _currentNode;
    private Sprite _nodePortrait;
    private Coroutine _currentCoroutine;
    private List<Option> _currentOptions;

    private char[] _dialogLetters;

    private bool _isWriting;
    private bool _goToNextNode;

    #endregion

    public bool IsWriting { get { return _isWriting; } }

    private void OnEnable()
    {
        if (startDialogEvent == null)
            startDialogEvent = new UnityEvent();

        if (endDialogEvent == null)
            endDialogEvent = new UnityEvent();

        if (enableNextButtonEvent == null)
            enableNextButtonEvent = new UnityEvent();

        if (updateOptionsEvent == null)
            updateOptionsEvent = new UnityEvent<List<Option>>();

        if (changePortraitEvent == null)
            changePortraitEvent = new UnityEvent<Sprite>();
    }

    public void Init()
    {
        _currentOptions = new List<Option>()
    {
        new Option(),
        new Option(),
        new Option()
    };

        _dialogLoader.Load();
        _localizationHandler?.Init();

        ResetWritter();
    }

    public void ResetWritter()
    {
        _isWriting = false;
        _goToNextNode = false;
        _dialogIndex = 0;
        _dialogName.value = "";
        _dialogText.value = "";
        _currentNode = null;

        ResetOptions();
    }

    private void ResetOptions()
    {
        for (int i = 0; i < _currentOptions.Count; i++)
        {
            _currentOptions[i].text = "";
            _currentOptions[i].index = -1;
        }
    }

    public void StartDialog(int startNode)
    {
        _isWriting = true;
        _currentNode = _dialogLoader.DialogNodes[startNode];

        if (_currentCoroutine != null)
            CoreManager.Instance.StopCoroutine(_currentCoroutine);

        _currentCoroutine = CoreManager.Instance.StartCoroutine(WriteCoroutine());
    }

    private IEnumerator WriteCoroutine()
    {
        //Start dialog opening the box
        startDialogEvent.Invoke();

        while (_currentNode != null)
        {
            _goToNextNode = false;

            _dialogName.value = "";
            _dialogText.value = "";

            #region Set Dialog Components
            if (_currentNode.normalDialogs.Length > 0 && _dialogIndex < _currentNode.normalDialogs.Length)
            {
                UpdateDialog(_currentNode.normalDialogs[_dialogIndex].dialog, _currentNode.normalDialogs[_dialogIndex].nodePortrait);
                ChangePortrait();

                _dialogIndex++;

                //If node has answers, set them after all normal dialogs have been played, if not then just enable next button to go to next dialog
                if (_dialogIndex >= _currentNode.normalDialogs.Length && _currentNode.answers.Length > 0)
                {
                    ResetOptions();
                    OnUpdateOptions(_currentNode.answers);
                }
                else
                    enableNextButtonEvent?.Invoke();
            }
            #endregion

            #region Write Dialog
            _isWriting = true;

            foreach (char letter in _dialogLetters)
            {
                _dialogText.value = _dialogText.value + letter;
                yield return new WaitForSeconds(_timeBetweenLetters);
            }

            _isWriting = false;

            Debug.Log("waiting for answer or click next Button...");
            yield return new WaitUntil(() => _goToNextNode || _currentNode == null);
            Debug.Log("answered or clicked next Button");
            #endregion
        }
    }

    private void OnUpdateOptions(Answer[] answers)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            _currentOptions[i].text = "";
            _currentOptions[i].text = GetKeyTranslation(answers[i].answer);

            _currentOptions[i].index = i;
        }

        updateOptionsEvent?.Invoke(_currentOptions);
    }

    private void ChangeCurrentNode(int selectedAnswer)
    {
        _dialogIndex = 0;

        if (_currentNode?.answers.Length > 0)
        {
            _nextNode = _currentNode.answers[selectedAnswer].nodeLinkID;

            if (_nextNode != 0)
            {
                _currentNode = _dialogLoader.DialogNodes[_nextNode];
                _goToNextNode = true;
            }
            else
                OnEndDialog();
        }
    }

    //Called when selecting one answer
    public void ChangeToNextDialog(int index)
    {
        if (!_isWriting)
            ChangeCurrentNode(index);
    }

    public void OnContinueDialog()
    {
        if (!_isWriting)
        {
            if (_dialogIndex < _currentNode.normalDialogs.Length)
                _goToNextNode = true;

            else if (_currentNode.answers.Length > 0)
                return;

            else if (_currentNode.nextNode == 0)
                OnEndDialog();

            else
            {
                _nextNode = _currentNode.nextNode;
                _currentNode = _dialogLoader.DialogNodes[_nextNode];
                _dialogIndex = 0;
                _goToNextNode = true;
            }
        }
    }

    public void OnEndDialog()
    {
        _goToNextNode = false;
        _currentNode = null;
        _dialogIndex = 0;
        _isWriting = false;

        CoreManager.Instance.StopRoutine(WriteCoroutine());
        endDialogEvent.Invoke();
    }

    private void ChangePortrait()
    {
        _nodePortrait = _dialogLoader.GetPortrait(_dialogName.value.ToLower());

        changePortraitEvent?.Invoke(_nodePortrait);
    }

    private void UpdateDialog(string dialogKey, string portraitName)
    {
        var dialogTranslation = GetKeyTranslation(dialogKey);

        if (dialogTranslation != "")
            _dialogLetters = dialogTranslation.ToCharArray();

        _dialogName.value = _dialogLoader.CaptalizeFirstLeter(portraitName);
    }

    private string GetKeyTranslation(string dialogKey)
    {
        var text = _localizationHandler.GetTranslation(dialogKey);

        if (text != "")
            return text;
        return "";
    }
}

public class Option
{
    public string text;
    public int index;
}
