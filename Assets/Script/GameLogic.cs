using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public class PlayerData
    {
        public Vector3 Pos;
        public Quaternion Dir;

        public PlayerData()
        {
            Pos = Vector3.zero;
            Dir = Quaternion.identity;
        }
    }

    public Dictionary<int,PlayerData> Players = new Dictionary<int, PlayerData>();

    public void Reset()
    {
        Players.Clear();
    }
}

public class GameLogic  {

    public GameData Data = new GameData();

    public void JoinRoom(int id)
    {
        Data.Players[id] = new GameData.PlayerData();
    }

    public void ProcessCmd(List<CommandMsg> msg)
    {
        if (null == msg)
        {
            return;
        }
        foreach (var c in msg)
        {
            PlayerCmd(c.ID,c.InputMsg);
        }
        
    }

    public void PlayerCmd(int id, InputMsg cmd)
    {
        GameData.PlayerData data = null;
        if (!Data.Players.TryGetValue(id, out data))
        {
            return;
        }

        if (1 == cmd.Dir)
        {
            data.Pos = data.Pos + Vector3.forward;
        } else if (2 == cmd.Dir)
        {
            data.Pos = data.Pos + Vector3.back;
        }
        else if (3 == cmd.Dir)
        {
            data.Pos = data.Pos + Vector3.left;
        }
        else if (4 == cmd.Dir)
        {
            data.Pos = data.Pos + Vector3.right;
        }
        else if (0 == cmd.Dir)
        {
            data.Pos = Vector3.zero;
        }
    }
}
