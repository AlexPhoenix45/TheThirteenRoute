using UnityEngine;

public class Ingame : GameMode
{
    public override void Start()
    {
        Debug.Log("Ingame Mode - Start" + DataManager.gameData.number);
        DataManager.gameData.number = 3;
        DataManager.SaveData(DataType.GAMEDATA);
    }

    public override void Update()
    {
        Debug.Log("Ingame Mode - Update" + DataManager.gameData.number);
    }
}
