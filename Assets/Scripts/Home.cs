using UnityEngine;

public class Home : GameMode
{
    public enum HomeStatus
    {
        INIT,
        IN,
    }   
    private HomeStatus status;
    
    public override void Start()
    {
        status = HomeStatus.INIT;
    }

    public override void Update()
    {
        switch (status)
        {
            case HomeStatus.INIT:
            {
                HomeUIManager.In();
                status = HomeStatus.IN;
                break;
            }
            case HomeStatus.IN:
            {
                break;
            }
        }
    }
}
