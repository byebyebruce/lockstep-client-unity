using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using MyType = System.Collections.Generic.List<int>;

public class Game : MonoBehaviour
{
    public  enum GameState
    {
        Waiting,
        Ready,
        Start,
        GameOver,
    }

    public static Game Instance;
    public int MyID = -1;
    
    public GameState State;

    public LockStepFrame Frame = new LockStepFrame();
    public GameLogic Logic = new GameLogic();

    public float TickTime = 0.03333333f;
    public float NextTime;

    public delegate void TickFrame(uint a, GameData b);

    public event TickFrame Callback;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Reset()
    {
        Frame.Reset();
        Logic.Reset();
    }

    public void JoinRoom(ulong id)
    {
        
        Logic.JoinRoom(id);
        UnityEngine.Debug.LogFormat("[{0}], JoinRoom", id);
    }

    public void LeaveRoom(int id)
    {
      
    }

    public void PushFrameData(List<pb.FrameData> msg)
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
        NextTime = Time.unscaledTime+ TickTime;

        //StartTime = Time.unscaledTime;
        //NextTime = StartTime + (FrameCount+1) * TickTime;
    }

    public void Update()
    {
        if (State < GameState.Start)
        {
            return;
        }

        if (NextTime > Time.unscaledTime)
        {
            return;
        }

        var remain = Frame.GetRemainFrameCount();
        if (remain <= 0)
        {
            return;
        }

        NextTime += TickTime;

        var n = 1;
        if (remain >= 5)
        {
            n = Math.Min(20, (int)((remain + 15.0f) / 10.0f));
        }


        for (int i = 0; i < n; i++)
        {
            var idx = Frame.CurrentFrameIdx;
            var data = Frame.TickFrame();
            
            Logic.ProcessFrameData(data);
            if (null != Callback)
            {
                Callback(idx, Logic.Data);
            }
        }
        

        
    }

    public string test;
    
    [ContextMenu("test")]
    public void Test()
    {
        /*
        var obj = new message.SCPacket();
        var str = JsonConvert.SerializeObject(obj);
        var data = System.Text.Encoding.UTF8.GetBytes(str);

        var target = new SnappyCompressor();

        int compressedSize = target.MaxCompressedLength(data.Length);
        var compressed = new byte[compressedSize];

        int result = target.Compress(data, 0, data.Length, compressed);

        //Assert.Equal(52, result);

        var decompressor = new SnappyDecompressor();
        var bytes = decompressor.Decompress(compressed, 0, result);
        //Assert.Equal(data, bytes);*/
    }

        
}
