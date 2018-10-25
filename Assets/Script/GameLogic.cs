using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public ulong MyID;
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

    public GameData Data = new GameData();

    public void Reset()
    {
        Data.Reset();
    }

    public GameData.PlayerData GetMyData()
    {
        return Data.Players[Data.MyID];
    }

    public void JoinRoom(ulong id)
    {
        Data.Players[id] = new GameData.PlayerData();
    }

    public void SetProgress(ulong id,int progress)
    {
        Data.Players[id].Progress = progress;
    }

    public void ProcessFrameData(pb.FrameData msg)
    {
        if (null == msg)
        {
            return;
        }
        if (null != msg.InputList)
        {
            foreach (var f in msg.InputList)
            {
                PlayerCmd(f);
            }
        }
        
    }

    public void PlayerCmd(pb.InputData cmd)
    {
        GameData.PlayerData data = null;
        if (!Data.Players.TryGetValue(cmd.Id, out data))
        {
            return;
        }
        var dir = cmd.Sid;

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
