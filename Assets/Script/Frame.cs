using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStepFrame  {
    public Dictionary<uint,message.FrameData> FrameList = new Dictionary<uint, message.FrameData>();
    public uint FrameCount;




    public void Start()
    {

        FrameList.Clear();
    }

    public void PushFrameData(List<message.FrameData> msg)
    {
        foreach (var m in msg)
        {
            
            FrameList[m.FrameID] = m;
        }
    }

    public message.FrameData TickFrame()
    {

        if (FrameList.Count == 0)
        {
            return null;
        }

        message.FrameData msg = null;
        if (FrameList.TryGetValue(FrameCount, out msg))
        {
            FrameList.Remove(FrameCount);
            FrameCount++;
            return msg;
        }

        return null;
    }
}
