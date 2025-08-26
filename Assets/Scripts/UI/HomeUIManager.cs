using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIManager : MonoBehaviour
{
    public static HomeUIManager Instance;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Animator anim;
    
    private bool isAnimPlaying = false;
    
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
        anim.Play("HomeUI_In");
        isAnimPlaying = true;
        SystemManager.excludeButton = true;
        
        playButton.onClick.AddListener(OnClick_PlayButton);
        settingButton.onClick.AddListener(OnClick_SettingButton);
    }

    public static void Out()
    {
        Instance._Out();
    }

    private void _Out()
    {
        anim.Play("HomeUI_Out");
        isAnimPlaying = true;
        SystemManager.excludeButton = true;
        
        playButton.onClick.RemoveListener(OnClick_PlayButton);
        settingButton.onClick.RemoveListener(OnClick_SettingButton);
    }

    private void OnClick_PlayButton()
    {
        if (SystemManager.excludeButton)
        {
            return;
        }
        SystemManager.excludeButton = true;
        
        Debug.Log("OnClick_PlayButton");

        SystemManager.excludeButton = false;
    }

    private void OnClick_SettingButton()
    {
        if (SystemManager.excludeButton)
        {
            return;
        }
        SystemManager.excludeButton = true;

        Debug.Log("OnClick_SettingButton");
        
        SystemManager.excludeButton = false;
    }

    public void AnimationCompleted()
    {
        isAnimPlaying = false;    
        SystemManager.excludeButton = false;
    }

    public static bool IsAnimPlaying()
    {
        return Instance.isAnimPlaying;
    }
}
