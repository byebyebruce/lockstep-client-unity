using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStepFrame  {
    public Dictionary<int,List<int>> FrameList = new Dictionary<int, List<int>>();
    public int FrameCount;




    public void Start()
    {

        FrameList.Clear();
    }

    public void PushFrameData(List<List<int>> msg)
    {
        foreach (var m in msg)
        {
            
            FrameList[m[0]] = m.GetRange(1,m.Count-1);
        }
    }

    public List<int> TickFrame()
    {

        if (FrameList.Count == 0)
        {
            return null;
        }

        List<int> msg = null;
        if (FrameList.TryGetValue(FrameCount, out msg))
        {
            FrameList.Remove(FrameCount);
            FrameCount++;
            return msg;
        }

        return null;
    }
}
