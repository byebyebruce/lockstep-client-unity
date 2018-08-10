using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public  enum GameState
    {
        Waiting,
        Ready,
        Start,
        GameOver,
    }
    public GameObject Prefab;
    public static Game Instance;
    public int MyID = -1;
    public Dictionary<int, GameObject> Players = new Dictionary<int,GameObject>();
    public GameState State;

    private LockStepFrame Frame = new LockStepFrame();
    private GameLogic Logic = new GameLogic();
    
    void Awake()
    {
        Instance = this;
    }

    public void JoinRoom(int id)
    {
        Players[id] = GameObject.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Logic.JoinRoom(id);
        UnityEngine.Debug.LogFormat("[{0}], JoinRoom", id);
    }

    public void LeaveRoom(int id)
    {
        GameObject o = null;
        if (Players.TryGetValue(id,out o))
        {
            GameObject.Destroy(o);
            Players.Remove(id);

            UnityEngine.Debug.LogFormat("[{0}], LeaveRoom", id);
        }
    }

    public void FrameData(FrameSeqMsg msg)
    {
        Frame.PushFrameData(msg);
        
    }

    public void DoReady()
    {
        State = GameState.Ready;
    }

    public void DoStart()
    {
        State = GameState.Start;
        Frame.Start();

        //StartTime = Time.unscaledTime;
        //NextTime = StartTime + (FrameCount+1) * TickTime;
    }

    public void Update()
    {
        if (State < GameState.Start)
        {
            return;
        }

        for (int i = 0; i < 10 ; i++)
        {
            var data = Frame.TickFrame();
            if (null==data)
            {
                break;
            }
            if (i > 2)
            {
                Debug.LogFormat("FrameCount={0} Remain{1}", Frame.FrameCount,Frame.FrameList.Count());
            }
            
            Logic.ProcessCmd(data.CommandMsg.ToList());
            if (Frame.FrameList.Count <= 6)
            {
                break;
            }
        }

        var d = Logic.Data;
        foreach (var playerData in d.Players)
        {
            GameObject o = null;
            if (Players.TryGetValue(playerData.Key, out o))
            {
                o.transform.localPosition = Vector3.Lerp(o.transform.localPosition, playerData.Value.Pos,
                    Time.deltaTime * 10);
            }
            
        }
        
    }

    public string test;

    [ContextMenu("test")]
    public void Test()
    {
        var a = JsonUtility.FromJson<ServerMsg>(test);
        var temp = a.Cmd;
    }

        
}
