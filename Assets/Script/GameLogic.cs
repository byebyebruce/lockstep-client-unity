using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public class PlayerData
    {
        public int Progress;
        public Vector3 Pos;
        public Quaternion Dir;

        public PlayerData()
        {
            Pos = Vector3.zero;
            Dir = Quaternion.identity;
        }
    }

    public Dictionary<ulong, PlayerData> Players = new Dictionary<ulong, PlayerData>();

    public void Reset()
    {
        Players.Clear();
    }
}

public class GameLogic
{
    static GameLogic()
    {
        Instance = new GameLogic();
    }

    public static GameLogic Instance;

    public GameData Data = new GameData();

    public void JoinRoom(ulong id)
    {
        Data.Players[id] = new GameData.PlayerData();
    }

    public void SetProgress(ulong id,int progress)
    {
        Data.Players[id].Progress = progress;
    }

    public void ProcessCmd(List<ulong> msg)
    {
        if (null == msg)
        {
            return;
        }
        for (var i = 0; i < msg.Count; i++)
        {
            var id = msg[i];
            //PlayerCmd(id, msg.GetRange(i+1 , 3));
            i += 4;
        }
    }

    public void PlayerCmd(ulong id, List<int> cmd)
    {
        GameData.PlayerData data = null;
        if (!Data.Players.TryGetValue(id, out data))
        {
            return;
        }
        var dir = cmd[0];

        if (1 == dir)
        {
            data.Pos = data.Pos + Vector3.forward;
        } else if (2 == dir)
        {
            data.Pos = data.Pos + Vector3.back;
        }
        else if (3 == dir)
        {
            data.Pos = data.Pos + Vector3.left;
        }
        else if (4 == dir)
        {
            data.Pos = data.Pos + Vector3.right;
        }
        else if (0 == dir)
        {
            data.Pos = Vector3.zero;
        }
    }
}
