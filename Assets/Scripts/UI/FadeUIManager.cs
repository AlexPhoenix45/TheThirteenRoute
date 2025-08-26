using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeUIManager : MonoBehaviour
{
    public static FadeUIManager Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private Animator anim;

    private bool isAnimPlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static void FadeIn(Color color, float duration)
    {
        Instance._FadeIn(color, duration);
    }

    private void _FadeIn(Color _color, float _duration)
    {
        fadeImage.color    = _color;
        anim.speed         = 1.0f / _duration;
        anim.Play( "FadeUI_In");   
        
        isAnimPlaying = true;
    }
    
    public static void FadeOut(Color color, float duration)
    {
        Instance._FadeOut(color, duration);
    }

    private void _FadeOut(Color _color, float _duration)
    {
        fadeImage.color    = _color;
        anim.speed         = 1.0f / _duration;
        anim.Play( "FadeUI_Out");   
        
        isAnimPlaying = true;
    }

    private void AnimationCompleted()
    {
        isAnimPlaying = false;
    }

    public static bool IsAnimPlaying()
    {
        return Instance.isAnimPlaying;
    }
}
