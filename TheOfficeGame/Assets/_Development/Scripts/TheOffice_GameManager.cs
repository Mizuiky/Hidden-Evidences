using UnityEngine;

public class TheOffice_GameManager : Singleton<TheOffice_GameManager>
{
    public Transform PlayerPosition;
    public PlayerController playerController;

    public void Start()
    {
        Init();    
    }

    private void Init()
    {
        InitPlayer();
    }

    private void InitPlayer()
    {
        PlayerController player = Instantiate(playerController, PlayerPosition);
        player.Init();
    }
}
