using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cocosocket4unity;
using Newtonsoft;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public static class MsgProcessor
{


    public static void ProcessMsg(ByteBuf bb)
    {
        var p = message.ReadPacket(bb.GetRaw());
        //UnityEngine.Debug.Log("msg:" + p.id);

        switch (p.id)
        {
            case message.S2C_Connect:
                {
                    string content = System.Text.Encoding.UTF8.GetString(p.data);
                    UnityEngine.Debug.Log("message.S2C_Connect");

                    Network.Instance.Send(message.C2S_JoinRoom);
                }
                break;
            case message.S2C_JoinRoom:
                {
                    string content = System.Text.Encoding.UTF8.GetString(p.data);
                    var msg = JsonUtility.FromJson<message.S2C_JoinRoomMsg>(content);

                    //GameLogic.Instance.JoinRoom(msg.MyID);

                    foreach (var id in msg.ID)
                    {
                        GameLogic.Instance.JoinRoom(id);
                    }
                    SceneManager.LoadScene(1);

                    UnityEngine.Debug.Log("msg:" + p.id);
                }
                break;
            case message.S2C_Progress:
                {
                    string content = System.Text.Encoding.UTF8.GetString(p.data);
                    var msg = JsonUtility.FromJson<message.S2C_ProgressMsg>(content);
                    GameLogic.Instance.SetProgress(msg.ID,msg.Pro);
                }
                break;
            case message.S2C_Ready:
                SceneManager.LoadScene(2);
                Game.Instance.DoStart();
                break;
            case message.S2C_Frame:
                {
                    string content = System.Text.Encoding.UTF8.GetString(p.data);
                    
                    var msg = JsonConvert.DeserializeObject<List<List<int>>>(content);
                    foreach (var VARIABLE in msg)
                    {
                        if (VARIABLE.Count > 1)
                        {
                            Debug.Log("1");
                        }
                    }
                    Game.Instance.FrameData(msg);
                    
                }
    
                break;
        }
        //string content = System.Text.Encoding.UTF8.GetString(bb.GetRaw());

        /*
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

    */
    }

    
}
