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
            MsgProcessor.ProcessMsg(bb);
        });

    }

    /// <summary>
    /// 异常
    /// </summary>
    /// <param name="ex"></param>
    protected override void HandleException(Exception ex)
    {
        UnityEngine.Debug.LogWarning("MyKcp HandleException");
        base.HandleException(ex);
    }

    /// <summary>
    /// 超时
    /// </summary>
    protected override void HandleTimeout()
    {
        UnityEngine.Debug.LogWarning("MyKcp HandleTimeout");
        base.HandleTimeout();
    }
}