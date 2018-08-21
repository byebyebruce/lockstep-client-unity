using System;
using System.Collections;
using System.Collections.Generic;
using cocosocket4unity;
using UnityEngine;

public class MyKcp : KcpClient
{
   

    protected override void HandleReceive(ByteBuf bb)
    {
        Loom.QueueOnMainThread(() =>
        {
            Network.Instance.HandleReceive(bb);

        });

    }

    /// <summary>
    /// 异常
    /// </summary>
    /// <param name="ex"></param>
    protected override void HandleException(Exception ex)
    {
        base.HandleException(ex);

        Loom.QueueOnMainThread(() =>
        {
            Network.Instance.HandleException(ex);
        });

        
    }

    /// <summary>
    /// 超时
    /// </summary>
    protected override void HandleTimeout()
    {
        base.HandleTimeout();

        Loom.QueueOnMainThread(() =>
        {
            Network.Instance.HandleTimeout();
        });

        
    }
}