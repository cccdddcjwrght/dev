// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct ClubHeadRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ClubHeadRowData GetRootAsClubHeadRowData(ByteBuffer _bb) { return GetRootAsClubHeadRowData(_bb, new ClubHeadRowData()); }
  public static ClubHeadRowData GetRootAsClubHeadRowData(ByteBuffer _bb, ClubHeadRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ClubHeadRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Icon { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetIconBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetIconArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<GameConfigs.ClubHeadRowData> CreateClubHeadRowData(FlatBufferBuilder builder,
      int id = 0,
      StringOffset iconOffset = default(StringOffset)) {
    builder.StartTable(2);
    ClubHeadRowData.AddIcon(builder, iconOffset);
    ClubHeadRowData.AddId(builder, id);
    return ClubHeadRowData.EndClubHeadRowData(builder);
  }

  public static void StartClubHeadRowData(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddIcon(FlatBufferBuilder builder, StringOffset iconOffset) { builder.AddOffset(1, iconOffset.Value, 0); }
  public static Offset<GameConfigs.ClubHeadRowData> EndClubHeadRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.ClubHeadRowData>(o);
  }
};

public struct ClubHead : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ClubHead GetRootAsClubHead(ByteBuffer _bb) { return GetRootAsClubHead(_bb, new ClubHead()); }
  public static ClubHead GetRootAsClubHead(ByteBuffer _bb, ClubHead obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ClubHead __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.ClubHeadRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.ClubHeadRowData?)(new GameConfigs.ClubHeadRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.ClubHead> CreateClubHead(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    ClubHead.AddDatalist(builder, datalistOffset);
    return ClubHead.EndClubHead(builder);
  }

  public static void StartClubHead(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.ClubHeadRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.ClubHeadRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.ClubHead> EndClubHead(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.ClubHead>(o);
  }
  public static void FinishClubHeadBuffer(FlatBufferBuilder builder, Offset<GameConfigs.ClubHead> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedClubHeadBuffer(FlatBufferBuilder builder, Offset<GameConfigs.ClubHead> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
