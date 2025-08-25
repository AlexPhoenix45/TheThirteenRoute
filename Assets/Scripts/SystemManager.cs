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
            DontDestroyOnLoad(gameObject);
        }
        
        DataManager.LoadData(DataType.GAMEDATA);
        DataManager.LoadData(DataType.PLAYERDATA);
    }

    private void Start()
    {
        ChangeStatus(SystemStatus.HOME);
    }

    private void Update()
    {
        currentGameMode.Update();
    }

    public static void ChangeStatus(SystemStatus systemStatus)
    {
        switch (systemStatus)
        {
            case SystemStatus.HOME:
            {
                Instance.currentGameMode = new Home();
                Instance.currentGameMode.Start();
                break;
            }
            case SystemStatus.INGAME:
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
