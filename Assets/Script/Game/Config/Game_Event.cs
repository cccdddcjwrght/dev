// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Game_EventRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Game_EventRowData GetRootAsGame_EventRowData(ByteBuffer _bb) { return GetRootAsGame_EventRowData(_bb, new Game_EventRowData()); }
  public static Game_EventRowData GetRootAsGame_EventRowData(ByteBuffer _bb, Game_EventRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Game_EventRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GroupId(int j) { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int GroupIdLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetGroupIdBytes() { return __p.__vector_as_span<int>(6, 4); }
#else
  public ArraySegment<byte>? GetGroupIdBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public int[] GetGroupIdArray() { return __p.__vector_as_array<int>(6); }
  public int GroupWeight(int j) { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int GroupWeightLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetGroupWeightBytes() { return __p.__vector_as_span<int>(8, 4); }
#else
  public ArraySegment<byte>? GetGroupWeightBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public int[] GetGroupWeightArray() { return __p.__vector_as_array<int>(8); }

  public static Offset<GameConfigs.Game_EventRowData> CreateGame_EventRowData(FlatBufferBuilder builder,
      int Id = 0,
      VectorOffset GroupIdOffset = default(VectorOffset),
      VectorOffset GroupWeightOffset = default(VectorOffset)) {
    builder.StartTable(3);
    Game_EventRowData.AddGroupWeight(builder, GroupWeightOffset);
    Game_EventRowData.AddGroupId(builder, GroupIdOffset);
    Game_EventRowData.AddId(builder, Id);
    return Game_EventRowData.EndGame_EventRowData(builder);
  }

  public static void StartGame_EventRowData(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddGroupId(FlatBufferBuilder builder, VectorOffset GroupIdOffset) { builder.AddOffset(1, GroupIdOffset.Value, 0); }
  public static VectorOffset CreateGroupIdVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateGroupIdVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartGroupIdVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddGroupWeight(FlatBufferBuilder builder, VectorOffset GroupWeightOffset) { builder.AddOffset(2, GroupWeightOffset.Value, 0); }
  public static VectorOffset CreateGroupWeightVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateGroupWeightVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartGroupWeightVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Game_EventRowData> EndGame_EventRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Game_EventRowData>(o);
  }
};

public struct Game_Event : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Game_Event GetRootAsGame_Event(ByteBuffer _bb) { return GetRootAsGame_Event(_bb, new Game_Event()); }
  public static Game_Event GetRootAsGame_Event(ByteBuffer _bb, Game_Event obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Game_Event __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.Game_EventRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.Game_EventRowData?)(new GameConfigs.Game_EventRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Game_Event> CreateGame_Event(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Game_Event.AddDatalist(builder, datalistOffset);
    return Game_Event.EndGame_Event(builder);
  }

  public static void StartGame_Event(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.Game_EventRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.Game_EventRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Game_Event> EndGame_Event(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Game_Event>(o);
  }
  public static void FinishGame_EventBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Game_Event> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedGame_EventBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Game_Event> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
