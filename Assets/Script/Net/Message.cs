using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using cocosocket4unity;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public static class PacketWraper
{
    [System.Serializable]
    public class Packet
    {
        public int id;
        public byte[] data;
    }

    public static ByteBuf NewPacket(pb.ID id, Google.ProtocolBuffers.IMessage msg = null)
    {
        var bufLen = BitConverter.GetBytes((ushort)0);
        

        var t = new List<byte>();

        byte[] buffData = null;
        if (null != msg)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                //Save the person to a stream
                msg.WriteTo(stream);
                buffData = stream.ToArray();

                bufLen = BitConverter.GetBytes((ushort)buffData.Length);
            }
        }

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bufLen);
        }
        t.AddRange(bufLen);
        t.Add((byte)id);
        if (null != buffData)
        {
             t.AddRange(buffData);
        }
        
        return new ByteBuf(t.ToArray());
    }

    public static Packet ReadPacket(byte[] b)
    {
        Packet p = new Packet();
        List<byte> temp = new List<byte>(b);
        var bID = temp.GetRange(0, 2);
        bID.Reverse();
        var len = BitConverter.ToUInt16(bID.ToArray(), 0);
        p.id = (int) temp[2];
        p.data = temp.GetRange(3, len).ToArray();

        return p;
    }

}

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
/*
public static class message{

    public const int C2S_Connect = 9998;
    public const int S2C_Connect = C2S_Connect;

    public const int C2S_Heartbeat = 1;
    public const int S2C_Heartbeat = C2S_Heartbeat;

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

    static SnappyCompressor compressor = new SnappyCompressor();
    static SnappyDecompressor decompressor = new SnappyDecompressor();






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

[System.Serializable]
public class S2C_JoinRoomMsg
{
    public int roomseatid;
    public List<int> ID;
}
    
[System.Serializable]
public class C2S_ProgressMsg
{
    public int pro;
}
[System.Serializable]
public class S2C_ProgressMsg
{
    public int ID;
    public int pro;
}

    /*
[System.Serializable]
public class S2C_ReadyMsg  {
}

//empty frame [1]int{ frameid }
//skill frame [5]int{ frameid, seatid, skillid,sx,sy }


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
public class C2S_InputSkillMsg
{
    public int Sid;
    public int Sx;
    public int Sy;
}


}*/