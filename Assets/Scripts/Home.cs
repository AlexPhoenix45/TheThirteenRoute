using UnityEngine;

public class Home : GameMode
{
    public override void Start()
    {
        Debug.Log("Home Mode - Start");
    }

    public override void Update()
    {
        Debug.Log("Home Mode - Update" + DataManager.gameData.number);
        DataManager.gameData.number = 1;
        DataManager.SaveData(DataType.GAMEDATA);
        SystemManager.ChangeStatus(Status.INGAME);
    }
}
