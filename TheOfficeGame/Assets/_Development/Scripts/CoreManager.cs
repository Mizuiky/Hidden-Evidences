using UnityEngine;
using System.Collections;

public class CoreManager : Singleton<CoreManager>
{
    [SerializeField] private UIManager _uiManager;

    public Transform PlayerPosition;
    public PlayerController playerController;

    public void Start()
    {
        _uiManager?.Init();
        InitPlayer();
    }

    private void InitPlayer()
    {
        PlayerController player = Instantiate(playerController, PlayerPosition);
        player.Init();
    }

    public void StartRoutine(IEnumerator coroutine)
    {
        Instance.StartCoroutine(coroutine);
    }

    public void StopRoutine(IEnumerator coroutine)
    {
        Instance.StopCoroutine(coroutine);
    }
}
