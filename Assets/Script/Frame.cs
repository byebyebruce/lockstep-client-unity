using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStepFrame  {
    public Dictionary<uint,pb.FrameData> FrameList = new Dictionary<uint, pb.FrameData>();
    public uint CurrentFrameIdx;
    public uint TotoalFrameCount;

    public void Start()
    {

        Reset();
    }

    public void Reset()
    {
        CurrentFrameIdx = 0;
        TotoalFrameCount = 0;
        FrameList.Clear();
    }

    public uint GetRemainFrameCount()
    {
        return TotoalFrameCount - CurrentFrameIdx;
    }

    public void PushFrameData(List<pb.FrameData> msg)
    {
        foreach (var m in msg)
        {
            FrameList[m.FrameID] = m;
            TotoalFrameCount = Math.Max(TotoalFrameCount, m.FrameID);
        }
    }

    public pb.FrameData TickFrame()
    {
        pb.FrameData data = null;
        if (FrameList.TryGetValue(CurrentFrameIdx, out data))
        {
            FrameList.Remove(CurrentFrameIdx);
        }
        
        CurrentFrameIdx++;

        return data;
    }
}
