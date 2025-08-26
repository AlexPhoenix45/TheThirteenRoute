using System;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public enum SE
    {
        BUTTONCLICK,
        
    }

    public enum BGM
    {
        MAINMENU,
        
    }
    
    [SerializeField] private AudioClip[] seClip;
    [SerializeField] private AudioClip[] bgmClip;
    [SerializeField] private AudioSource[] seSource;
    [SerializeField] private AudioSource bgmSource;
    
    private int index = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        UpdateVolume();
    }
    
    public static void UpdateVolume()
    {
        foreach (var se in Instance.seSource)
        {
            se.volume = DataManager.gameData.seVolume;
        }
        
        Instance.bgmSource.volume = DataManager.gameData.bgmVolume;
    }
    
    public static void PlaySE(SE se)
    {
        Instance._PlaySE(se);
    }

    private void _PlaySE(SE _se)
    {
        while (seSource[index].isPlaying)
        {
            if (index + 1 < seSource.Length)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
        
        seSource[index].clip = seClip[(int)_se];
        seSource[index].Play();
        seSource[index].loop = false;
        seSource[index].volume = DataManager.gameData.seVolume;
    }

    public static void StopSE(SE se)
    {  
        Instance._StopSE(se);
    }

    private void _StopSE(SE _se)
    {
        foreach (var se in seSource)
        {
            if (se.clip == seClip[(int)_se])
            {
                se.Stop();
                se.clip = null;
            }
        }
    }
    
    public static void PlayBGM(BGM bgm)
    {
        Instance._PlayBGM(bgm);
    }

    private void _PlayBGM(BGM _bgm)
    {
        if (bgmSource.isPlaying)
        {
            StartCoroutine(Execute());
            
            IEnumerator Execute()
            {
                float elapsedTime = 0f;
                float duration = 1f;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    bgmSource.volume = Mathf.Lerp(DataManager.gameData.bgmVolume, 0, elapsedTime / duration);
                    yield return new WaitForEndOfFrame();
                }
                
                elapsedTime = 0f;
                bgmSource.clip = bgmClip[(int)_bgm];
                bgmSource.Play();
                bgmSource.loop = true;
                
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    bgmSource.volume = Mathf.Lerp(0, DataManager.gameData.bgmVolume, elapsedTime / duration);
                    yield return new WaitForEndOfFrame();
                }
                
                elapsedTime = DataManager.gameData.bgmVolume;
            }
        }
        else
        {
            StartCoroutine(Execute());
            
            IEnumerator Execute()
            {
                float elapsedTime = 0f;
                float duration = 1f;

                bgmSource.clip = bgmClip[(int)_bgm];
                bgmSource.Play();
                bgmSource.loop = true;
                
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    bgmSource.volume = Mathf.Lerp(DataManager.gameData.bgmVolume, 0, elapsedTime / duration);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    public static void StopBGM()
    {
        Instance._StopBGM();
    }
    
    private void _StopBGM()
    {
        StartCoroutine(Execute());
        
        IEnumerator Execute()
        {
            float elapsedTime = 0f;
            float duration = 1f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                bgmSource.volume = Mathf.Lerp(DataManager.gameData.bgmVolume, 0, elapsedTime / duration);
                yield return new WaitForEndOfFrame();
            }
            
            bgmSource.volume = 0;
            bgmSource.Stop();
            bgmSource.clip = null;
        }
    }
}
