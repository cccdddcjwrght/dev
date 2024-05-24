// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct ClubFrameRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ClubFrameRowData GetRootAsClubFrameRowData(ByteBuffer _bb) { return GetRootAsClubFrameRowData(_bb, new ClubFrameRowData()); }
  public static ClubFrameRowData GetRootAsClubFrameRowData(ByteBuffer _bb, ClubFrameRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ClubFrameRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Icon { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetIconBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetIconArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<GameConfigs.ClubFrameRowData> CreateClubFrameRowData(FlatBufferBuilder builder,
      int id = 0,
      StringOffset iconOffset = default(StringOffset)) {
    builder.StartTable(2);
    ClubFrameRowData.AddIcon(builder, iconOffset);
    ClubFrameRowData.AddId(builder, id);
    return ClubFrameRowData.EndClubFrameRowData(builder);
  }

  public static void StartClubFrameRowData(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddIcon(FlatBufferBuilder builder, StringOffset iconOffset) { builder.AddOffset(1, iconOffset.Value, 0); }
  public static Offset<GameConfigs.ClubFrameRowData> EndClubFrameRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.ClubFrameRowData>(o);
  }
};

public struct ClubFrame : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ClubFrame GetRootAsClubFrame(ByteBuffer _bb) { return GetRootAsClubFrame(_bb, new ClubFrame()); }
  public static ClubFrame GetRootAsClubFrame(ByteBuffer _bb, ClubFrame obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ClubFrame __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.ClubFrameRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.ClubFrameRowData?)(new GameConfigs.ClubFrameRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.ClubFrame> CreateClubFrame(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    ClubFrame.AddDatalist(builder, datalistOffset);
    return ClubFrame.EndClubFrame(builder);
  }

  public static void StartClubFrame(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.ClubFrameRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.ClubFrameRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.ClubFrame> EndClubFrame(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.ClubFrame>(o);
  }
  public static void FinishClubFrameBuffer(FlatBufferBuilder builder, Offset<GameConfigs.ClubFrame> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedClubFrameBuffer(FlatBufferBuilder builder, Offset<GameConfigs.ClubFrame> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
