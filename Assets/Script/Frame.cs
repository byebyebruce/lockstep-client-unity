using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStepFrame  {
    public Dictionary<uint,pb.FrameData> FrameList = new Dictionary<uint, pb.FrameData>();
    public uint FrameCount;

    public void Start()
    {

        Reset();
    }

    public void Reset()
    {
        FrameCount = 0;
        FrameList.Clear();
    }
    public void PushFrameData(List<pb.FrameData> msg)
    {
        foreach (var m in msg)
        {
            
            FrameList[m.FrameID] = m;
        }
    }

    public pb.FrameData TickFrame()
    {

        if (FrameList.Count == 0)
        {
            return null;
        }

        pb.FrameData msg = null;
        if (FrameList.TryGetValue(FrameCount, out msg))
        {
            FrameList.Remove(FrameCount);
            FrameCount++;
            return msg;
        }

        return null;
    }
}
