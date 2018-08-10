using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStepFrame  {
    public Dictionary<int,FrameMsg> FrameList = new Dictionary<int,FrameMsg>();
    public int FrameCount;

    public float TickTime = 0.03333333f;
    public float NextTime;
    public float StartTime;

    public void Start()
    {
        StartTime = Time.unscaledTime;
        FrameList.Clear();
    }

    public void PushFrameData(FrameSeqMsg msg)
    {
        foreach (var m in msg.Frames)
        {
            FrameList[m.FrameID] = m;
        }
    }

    public FrameMsg TickFrame()
    {
        //if ((Time.unscaledTime- StartTime)/ TickTime < FrameCount )
        {
        //    return null;
        }
        if (FrameList.Count == 0)
        {
            return null;
        }

        FrameMsg msg = null;
        if (FrameList.TryGetValue(FrameCount, out msg))
        {
            FrameList.Remove(FrameCount);
            FrameCount++;
            return msg;
        }

        return null;
    }
}
