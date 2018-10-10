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

    private LockStepFrame Frame = new LockStepFrame();
    public GameLogic Logic = new GameLogic();

    public float TickTime = 0.03333333f;
    public float NextTime;

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

        NextTime += TickTime;

        var n = Frame.FrameList.Count;
        if (Frame.FrameList.Count >=5)
        {
            n = (int)((Frame.FrameList.Count + 15.0f) / 10.0f);
        }
        else
        {
            n = 1;
        }

        n = Math.Min(20, n);

        for (int i = 0; i < n; i++)
        {
            var data = Frame.TickFrame();
            if (null==data)
            {
                break;
            }
            if (i >= 2)
            {
                Debug.LogFormat("FrameCount={0} Remain{1}", Frame.FrameCount,Frame.FrameList.Count());
            }

            Logic.ProcessCmd(data);
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
