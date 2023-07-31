using System.Runtime.InteropServices;

// 数据包
public struct GamePackage
{
    // 简单的包头信息
    public int          msgId;
    public uint         seq;

    // 数据体
    public NetPackage   data;
}

// 包头
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PackageHead
{
    public const int SIZE = 16;

    // 包头为15个字节：版本号(3字节)+包总长度(4字节)+消息ID(4字节)+客户端发包序列号(4字节)
    public int          ver;

    public int          pkgLen;

    public int          msgId;

    public uint         pkgSeq;
}

public enum NetVer : byte
{ 
    VER0 = 0,
    VER1 = 0,
    VER2 = 1
}
