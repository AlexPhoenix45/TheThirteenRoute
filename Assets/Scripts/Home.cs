using System;
using UnityEngine;

public class Home : GameMode
{
    public enum HomeStatus
    {
        INITIALIZE,
        FADEIN,
        WAITANIMATION,
        MAIN,
        FADEOUT,
        WAITFADEOUT,
    }   
    private HomeStatus status;
    
    public override void Start()
    {
        status = HomeStatus.INITIALIZE;
    }

    public override void Update()
    {
#if GAME_DEBUG
        DebugUIManager.AddDebugText("currentStatus", status.ToString());
#endif
        
        switch (status)
        {
            case HomeStatus.INITIALIZE:
            {
                FadeUIManager.FadeIn(Color.black, 1);
                SoundManager.PlayBGM(SoundManager.BGM.MAINMENU);
                
                status = HomeStatus.FADEIN;
                
                break;
            }
            case HomeStatus.FADEIN:
            {
                if (!FadeUIManager.IsAnimPlaying())
                {
                    HomeUIManager.In();
                    status = HomeStatus.WAITANIMATION;
                }
                
                break;
            }
            case HomeStatus.WAITANIMATION:
            {
                if (!HomeUIManager.IsAnimPlaying())
                {
                    status = HomeStatus.MAIN;
                }
                
                break;
            }
            case HomeStatus.MAIN:
                break;
            case HomeStatus.FADEOUT:
                break;
            case HomeStatus.WAITFADEOUT:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
