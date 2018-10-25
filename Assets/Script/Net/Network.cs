using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cocosocket4unity;
using UnityEngine;

public class Network : MonoBehaviour
{
    public static Network Instance;
    public MyKcp client;

    public bool Connected = false;
    public float Heartbeat = 10.0f;
    public float Delta = 0;


    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        
    }

    public void Connect(string host, int port)
    {
        if (null != client)
        {
            client.Stop();
        }

        client = new MyKcp();
        client.NoDelay(1, 10, 2, 1);//fast
        client.WndSize(4096, 4096);
        client.Timeout(40 * 1000);
        client.SetMtu(512);
        client.SetMinRto(10);
        client.SetConv(121106);

        client.Connect(host, port);

        client.Start();
    }

	// Update is called once per frame
	void Update () {
	    if (null != client && client.IsRunning() && Connected)
	    {
	        Delta += Time.unscaledDeltaTime;
	        if (Delta > Heartbeat)
	        {
	            client.Send(PacketWraper.NewPacket(pb.ID.MSG_Heartbeat));
	            Delta = 0;
	        }

                
        }
	    
	    

    }

    public void Send(pb.ID id, Google.ProtocolBuffers.IMessage obj = null)
    {
        if (null != client && client.IsRunning() && Connected)
        {
            client.Send(PacketWraper.NewPacket(id,obj));
        }
        else
        {
            UnityEngine.Debug.LogError("client no connect");
        }
    }

    void OnGUI()
    {

        //StartCoroutine(Co());
        if (null != client && client.IsRunning() && Connected)
        {
            if (GUI.Button(new Rect(100, 0, 100, 100), "Disconnect"))
            {
                client.Stop();
            }
        }
    }

    public void HandleReceive(ByteBuf bb)
    {
        MsgProcessor.ProcessMsg(bb);
    }

    public void HandleException(Exception ex)
    {
        UnityEngine.Debug.LogWarning("MyKcp HandleException:"+ ex.Message);
        Application.LoadLevel(0);
    }

    public void HandleTimeout()
    {
        Connected = false;
        UnityEngine.Debug.LogWarning("MyKcp HandleTimeout");
        Application.LoadLevel(0);
    }

    void OnDestroy()
    {
        if (null != client)
        {
            client.Stop();
        }
        
    }


}
