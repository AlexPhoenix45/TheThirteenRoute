using System;
using UnityEditor.Overlays;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager Instance;
    
    private GameMode currentGameMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        DataManager.LoadData(DataType.GAMEDATA);
        DataManager.LoadData(DataType.PLAYERDATA);
    }

    private void Start()
    {
        ChangeStatus(Status.HOME);
    }

    private void Update()
    {
        currentGameMode.Update();
    }

    public static void ChangeStatus(Status status)
    {
        switch (status)
        {
            case Status.HOME:
            {
                Instance.currentGameMode = new Home();
                Instance.currentGameMode.Start();
                break;
            }
            case Status.INGAME:
            {
                Instance.currentGameMode = new Ingame();
                Instance.currentGameMode.Start();
                break;
            }
            default:
                break;
        }
    }
}
