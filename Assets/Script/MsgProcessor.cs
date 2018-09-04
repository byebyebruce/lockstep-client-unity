using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cocosocket4unity;
using UnityEngine.SceneManagement;

public static class MsgProcessor
{


    public static void ProcessMsg(ByteBuf bb)
    {
        var p = PacketWraper.ReadPacket(bb.GetRaw());
        //UnityEngine.Debug.Log("msg:" + p.id);
        var id = (message.ID)p.id;
        switch (id)
        {
            case message.ID.S2C_Connect:
                {
                    string content = System.Text.Encoding.UTF8.GetString(p.data);
                    UnityEngine.Debug.Log("message.S2C_Connect");

                    Network.Instance.Send(message.ID.C2S_JoinRoom);
                }
                break;
            case message.ID.S2C_JoinRoom:
                {
                    
                    var msg = message.S2C_JoinRoomMsg.ParseFrom(p.data);
                    GameLogic.Instance.JoinRoom(msg.Id);

                    foreach (var pid in msg.OthersList)
                    {
                        GameLogic.Instance.JoinRoom(pid);
                    }
                    SceneManager.LoadScene(1);

                    UnityEngine.Debug.Log("msg:" + p.id);
                }
                break;
            case message.ID.S2C_Progress:
                {
                    var msg = message.S2C_ProgressMsg.ParseFrom(p.data);
                    GameLogic.Instance.JoinRoom(msg.Id);
                    GameLogic.Instance.SetProgress(msg.Id,msg.Pro);
                }
                break;
            case message.ID.S2C_Ready:
                SceneManager.LoadScene(2);
                Game.Instance.DoStart();
                break;
            case message.ID.S2C_Frame:
                {
                    string content = System.Text.Encoding.UTF8.GetString(p.data);

                    /*
                    var msg = JsonConvert.DeserializeObject<List<List<int>>>(content);
                    foreach (var VARIABLE in msg)
                    {
                        if (VARIABLE.Count > 1)
                        {
                            Debug.Log("1");
                        }
                    }
                    Game.Instance.FrameData(msg);
                    */

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
