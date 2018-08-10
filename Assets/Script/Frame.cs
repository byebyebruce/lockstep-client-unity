using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStepFrame  {
    public Dictionary<int,FrameMsg> FrameList = new Dictionary<int,FrameMsg>();
    public int FrameCount;




    public void Start()
    {

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
