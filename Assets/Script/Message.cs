using System;
using System.Collections;
using System.Collections.Generic;
using cocosocket4unity;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

/*
--[[
    c2s 5001 进入房间
    s2c 5001 收到

    c2s 5002 读条 pro
    s2c 5002 对方进度 pro

    c2s 5003 准备开始
    s2c 5003 可以开始

    s2c 5004 刷帧 帧ID 技能序列

    c2s 5005 技能 skillID x y
    s2c 5005 释放成功

    c2s 5006 战斗结束 输赢
    s2c 5006 回应收到

	PROTOCOL_ID_OPEN = 9998
	PROTOCOL_ID_HEARTBEAT = 1

]]
*/

public static class message{

    public const int C2S_Connect = 9998;
    public const int S2C_Connect = 9999;

    public const int C2S_Heartbeat = 1;

    public const int C2S_JoinRoom = 5001;
    public const int S2C_JoinRoom = C2S_JoinRoom;
    public const int C2S_Progress = 5002;
    public const int S2C_Progress = C2S_Progress;
    public const int C2S_Ready = 5003;
    public const int S2C_Ready = C2S_Ready;
    public const int S2C_Frame = 5004;
    public const int C2S_InputSkill = 5005;
    public const int S2C_InputSkill = C2S_InputSkill;
    public const int C2S_Result = 5006;
    public const int S2C_Result = C2S_Result;

    public static ByteBuf NewCSPacket(int type, object obj=null)
    {
        byte rLen = 0;
        var bID = BitConverter.GetBytes(type);
        var bLen = BitConverter.GetBytes(0);
        byte[] temp = null;
        if (null != obj)
        {
            var str = JsonConvert.SerializeObject(obj);
            temp = System.Text.Encoding.UTF8.GetBytes(str);
            bLen = BitConverter.GetBytes(temp.Length);
        }
        List<byte> t = new List<byte>();

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bID);
            Array.Reverse(bLen);
        }
        t.Add(rLen);
        t.AddRange(bID);
        t.AddRange(bLen);
        if (null != temp)
        {
            t.AddRange(temp);
        }

        return new ByteBuf(t.ToArray());
    }

    public static SCPacket ReadPacket(byte[] b)
    {
        SCPacket p = new SCPacket();
        List<byte> temp = new List<byte>(b);
        var bID = temp.GetRange(0, 4);
        bID.Reverse();
        p.id = BitConverter.ToInt32(bID.ToArray(), 0);

        var bDatLen = temp.GetRange(4, 4);
        bDatLen.Reverse();
        var len = BitConverter.ToInt32(bDatLen.ToArray(), 0);
        p.data = temp.GetRange(8, len).ToArray();

        return p;
    }





[System.Serializable]
public class SCPacket
{
    public int id;
    public byte[] data;
}

[System.Serializable]
public class CSPacket
{

    public int id;
    public string rid;
    public byte[] data;

}

/*[System.Serializable]
public class S2C_JoinRoomMsg  {
	RoomSeatid int32    `json:"roomseatid"`
}
[System.Serializable]
public class C2S_ProgressMsg  {
	Pro int32 `json:"pro"` //0~100
}
[System.Serializable]
public class S2C_ReadyMsg  {
}

//empty frame [1]int{ frameid }
//skill frame [5]int{ frameid, seatid, skillid,sx,sy }

type S2C_FrameMsg [][]int
[System.Serializable]
public class C2S_InputSkillMsg  {
	Sid int32 `json:"sid"`
	X   int32 `json:"sx"`
	Y   int32 `json:"sy"`
}


type S2C_InputSkillMsg []int
*/
//----------[System.Serializable]-----------------------------------------------------
public class S2C_ConnectMsg
{
    
}
[System.Serializable]
public class C2S_JoinRoomMsg
{
    public int RoomID;
}
[System.Serializable]
public class S2C_JoinRoomMsg
{
    public int MyID;
    public List<int> ID;
}
[System.Serializable]
public class C2S_ProgressMsg
{
    public int ID;
    public int Pro;
}
[System.Serializable]
public class S2C_ProgressMsg
{
    public int ID;
    public int Pro;
}


[System.Serializable]
public class C2S_InputSkillMsg
{
    public int Sid;
    public int Sx;
    public int Sy;
}


}