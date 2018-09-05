using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                    GameLogic.Instance.SetProgress(msg.Id,msg.Pro);
                }
                break;
            case message.ID.S2C_Ready:
                SceneManager.LoadScene(2);
                Game.Instance.DoStart();
                break;
            case message.ID.S2C_Frame:
            {
                var msg = message.S2C_FrameMsg.ParseFrom(p.data);
                Game.Instance.PushFrameData(msg.FramesList.ToList());

            }
                break;
        }
       
    }

    
}
