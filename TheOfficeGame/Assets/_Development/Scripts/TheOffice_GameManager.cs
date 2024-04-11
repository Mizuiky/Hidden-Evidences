using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOffice_GameManager : Singleton<TheOffice_GameManager>
{
    public Transform PlayerPosition;
    public PlayerController playerController;

    public void Start()
    {
        Init();    
    }

    public void Init()
    {
        PlayerController player = Instantiate(playerController, PlayerPosition);
        player.Init();
    }
}
