using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MsgType
{
    public const int CmdJoin = 0;
    public const int CmdOtherJoin = 1;
    public const int CmdReady = 10;
    public const int CmdStart = 20;
    public const int CmdInput = 30;
    public const int CmdLeave = 40;
    public const int CmdGameOver = 50;
}

[System.Serializable]
public class InputMsg
{
    public int Dir;
    public bool Fire;
}

[System.Serializable]
public class CommandMsg
{

    public int ID;
    public int Cmd;
    public InputMsg InputMsg;
}

[System.Serializable]
public class FrameMsg
{

    public int FrameID;
    public List<CommandMsg> CommandMsg;
}

[System.Serializable]
public class FrameSeqMsg
{

    public List<FrameMsg> Frames ;
}

[System.Serializable]
public class ServerMsg
{

    public int Cmd ;
    public List<int> Params;
    public FrameSeqMsg FrameSeqMsg;
}


