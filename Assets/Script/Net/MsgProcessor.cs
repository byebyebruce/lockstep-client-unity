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
        UnityEngine.Debug.LogFormat("msg:{0}-[{1}]", p.id,p.data.Length);
        var id = (pb.ID)p.id;
        switch (id)
        {
            case pb.ID.MSG_Connect:
                {
                    UnityEngine.Debug.Log("pb.S2C_Connect");
                    var msg = pb.S2C_ConnectMsg.ParseFrom(p.data);
                    if (msg.ErrorCode == pb.ERRORCODE.ERR_Ok)
                    {
                        Network.Instance.Connected = true;
                        Network.Instance.Send(pb.ID.MSG_JoinRoom);
                    }
                    else
                    {
                        UnityEngine.Debug.LogErrorFormat("pb.S2C_Connect{0}",msg.ErrorCode);
                    }
                }
                break;
            case pb.ID.MSG_JoinRoom:
                {
                    
                    var msg = pb.S2C_JoinRoomMsg.ParseFrom(p.data);

                    UnityEngine.Random.seed = msg.RandomSeed;
                    Game.Instance.Logic.JoinRoom(Game.Instance.Logic.Data.MyID);

                    //foreach (var pid in msg.OthersList)
                    for(int i=0; i< msg.OthersList.Count; i++)
                    {
                        var pid = msg.GetOthers(i);
                        Game.Instance.Logic.JoinRoom(pid);
                        Game.Instance.Logic.Data.Players[pid].Progress = msg.GetPros(i);
                    }
                    SceneManager.LoadScene(1);

                }
                break;
            case pb.ID.MSG_Progress:
                {
                    var msg = pb.S2C_ProgressMsg.ParseFrom(p.data);
                    Game.Instance.Logic.SetProgress(msg.Id,msg.Pro);
                }
                break;
            case pb.ID.MSG_Ready:
                break;
            case pb.ID.MSG_Start:
                SceneManager.LoadScene(2);
                Game.Instance.DoStart();
                break;
            case pb.ID.MSG_Frame:
            {
                var msg = pb.S2C_FrameMsg.ParseFrom(p.data);
                Game.Instance.PushFrameData(msg.FramesList.ToList());

            }
                break;
            case pb.ID.MSG_Result:
            {
                    //UnityEngine.Debug.LogFormat("msg:{0}-[{1}]", p.id, p.data.Length);
                Network.Instance.client.Stop();
                    Application.LoadLevel(0);

            }
                break;
            case pb.ID.MSG_Close:
            {
                    //UnityEngine.Debug.LogFormat("msg:{0}-[{1}]", p.id, p.data.Length);
                Network.Instance.client.Stop();
                Application.LoadLevel(0);

            }
                break;
            case pb.ID.MSG_END:
            {
                  UnityEngine.Debug.LogFormat("msg:{0}-[{1}]", p.id,p.data.Length);

                }
                break;

        }
       
    }

    
}
