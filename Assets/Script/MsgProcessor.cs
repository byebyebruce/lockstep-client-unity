using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cocosocket4unity;

public static class MsgProcessor
{


    public static void ProcessMsg(ByteBuf bb)
    {
        string content = System.Text.Encoding.UTF8.GetString(bb.GetRaw());

        ServerMsg msg;
        try
        {
            msg = JsonUtility.FromJson<ServerMsg>(content);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("msg:" + content);
            return;
        }
        if (MsgType.CmdJoin == msg.Cmd)
        {
            Game.Instance.MyID = msg.Params[0];
            Game.Instance.JoinRoom(Game.Instance.MyID);
            UnityEngine.Debug.Log("msg:" + content);
        }
        else if (MsgType.CmdOtherJoin == msg.Cmd)
        {
            foreach (var id in msg.Params)
            {
                Game.Instance.JoinRoom(id);
            }
            UnityEngine.Debug.Log("msg:" + content);
        }
        else if (MsgType.CmdStart == msg.Cmd)
        {
             Game.Instance.DoStart();
            UnityEngine.Debug.Log("msg:" + content);
        }
        else if (MsgType.CmdInput == msg.Cmd)
        {
            Game.Instance.FrameData(msg.FrameSeqMsg);
            foreach (var f in msg.FrameSeqMsg.Frames)
            {
                if (f.CommandMsg.Count > 0)
                {
                    UnityEngine.Debug.Log("msg:" + content);
                    break;
                }
            }
            //UnityEngine.Debug.LogFormat("FrameSeqMsg:{0}", msg.FrameSeqMsg.Frames.Count);
        }
        else if (MsgType.CmdLeave == msg.Cmd)
        {
            Game.Instance.LeaveRoom(msg.Params[0]);
            UnityEngine.Debug.Log("msg:" + content);
        }
    }

    
}
