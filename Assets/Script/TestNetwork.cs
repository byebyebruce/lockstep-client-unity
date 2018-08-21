using System;
using System.Collections;
using System.Collections.Generic;
using cocosocket4unity;
using UnityEngine;

public class TestNetwork : MonoBehaviour
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
	    //client.Connect("119.29.153.92", 2222);
	    client.Connect("127.0.0.1", 10086);
	    client.Start();

        StartCoroutine(Co());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Co()
    {
        var i = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);

            String s = i+" hi,heoll world! 你好啊！！";
            //for (int i = 0; i < 2; i++)
            //{
            //    s = s + s;
            //}
            ByteBuf bb = new ByteBuf(System.Text.Encoding.UTF8.GetBytes(s));
            Console.WriteLine(bb.ReadableBytes());
            client.Send(bb);
            //Console.Read();

            i++;
        }
        
    }

    void OnDestroy()
    {
        if (null != client)
            client.Stop();
    }
}
