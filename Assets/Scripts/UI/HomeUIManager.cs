using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIManager : MonoBehaviour
{
    public static HomeUIManager Instance;

    public Button playButton;
    public Button settingButton; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static void In()
    {
        Instance._In();
    }

    private void _In()
    {
        Debug.Log("In");
        playButton.onClick.AddListener(OnClick_PlayButton);
        settingButton.onClick.AddListener(OnClick_SettingButton);
    }

    private void OnClick_PlayButton()
    {
        Debug.Log("OnClick_PlayButton");
    }

    private void OnClick_SettingButton()
    {
        Debug.Log("OnClick_SettingButton");
    }
}
