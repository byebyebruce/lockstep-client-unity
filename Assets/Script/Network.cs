using System;
using System.Collections;
using System.Collections.Generic;
using cocosocket4unity;
using UnityEngine;

public class Network : MonoBehaviour
{
    private MyKcp client;
    // Use this for initialization
    void Start () {
        client = new MyKcp();
        client.NoDelay(1, 10, 2, 1);//fast
        client.WndSize(64, 64);
        client.Timeout(10 * 1000);
        client.SetMtu(512);
        client.SetMinRto(10);
        client.SetConv(121106);
    }
	
	// Update is called once per frame
	void Update () {
	    if (0 == Time.frameCount % 100)
	    {
	        if (null != client && client.IsRunning())
	        {
                client.Send(new ByteBuf(1));
	        }
        }
	    
	    

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Connect") )
        {
            
            //client.Connect("119.29.153.92", 2222);
            client.Connect("127.0.0.1", 10086);
            client.Start();

            //StartCoroutine(Co());
        } else if (GUI.Button(new Rect(100, 0, 100, 100), "Disconnect"))
        {
            client.Stop();
        }
        else if (GUI.Button(new Rect(200, 0, 100, 100), "Join"))
        {
            CommandMsg msg = new CommandMsg();
            msg.Cmd = MsgType.CmdJoin;
            
            ByteBuf bb = new ByteBuf(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(msg)));

            client.Send(bb);
        }
        else if (GUI.Button(new Rect(300, 0, 100, 100), "Ready"))
        {
            CommandMsg msg = new CommandMsg();
            msg.Cmd = MsgType.CmdReady;
            ByteBuf bb = new ByteBuf(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(msg)));

            client.Send(bb);
        }
        else if (GUI.Button(new Rect(0, 100, 100, 100), "Foward"))
        {
            CommandMsg msg = new CommandMsg();
            msg.Cmd = MsgType.CmdInput;
            msg.InputMsg = new InputMsg();
            msg.InputMsg.Dir = 1;

            ByteBuf bb = new ByteBuf(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(msg)));

            client.Send(bb);
        }
        else if (GUI.Button(new Rect(100, 100, 100, 100), "Back"))
        {
            CommandMsg msg = new CommandMsg();
            msg.Cmd = MsgType.CmdInput;
            msg.InputMsg = new InputMsg();
            msg.InputMsg.Dir = 2;

            ByteBuf bb = new ByteBuf(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(msg)));

            client.Send(bb);
        }
        else if (GUI.Button(new Rect(200, 100, 100, 100), "Left"))
        {
            CommandMsg msg = new CommandMsg();
            msg.Cmd = MsgType.CmdInput;
            msg.InputMsg = new InputMsg();
            msg.InputMsg.Dir = 3;

            ByteBuf bb = new ByteBuf(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(msg)));

            client.Send(bb);
        }
        else if (GUI.Button(new Rect(300, 100, 100, 100), "Right"))
        {
            CommandMsg msg = new CommandMsg();
            msg.Cmd = MsgType.CmdInput;
            msg.InputMsg = new InputMsg();
            msg.InputMsg.Dir = 4;

            ByteBuf bb = new ByteBuf(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(msg)));

            client.Send(bb);
        }

    }

    void OnDestroy()
    {
        if (null != client)
        {
            client.Stop();
        }
        
    }
}
