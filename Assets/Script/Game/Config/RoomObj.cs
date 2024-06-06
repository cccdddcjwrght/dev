// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct RoomObjRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static RoomObjRowData GetRootAsRoomObjRowData(ByteBuffer _bb) { return GetRootAsRoomObjRowData(_bb, new RoomObjRowData()); }
  public static RoomObjRowData GetRootAsRoomObjRowData(ByteBuffer _bb, RoomObjRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RoomObjRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public int MaxStar { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int RoomArea { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float Cost(int j) { int o = __p.__offset(12); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int CostLength { get { int o = __p.__offset(12); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetCostBytes() { return __p.__vector_as_span<float>(12, 4); }
#else
  public ArraySegment<byte>? GetCostBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public float[] GetCostArray() { return __p.__vector_as_array<float>(12); }
  public int LevelId(int j) { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int LevelIdLength { get { int o = __p.__offset(14); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetLevelIdBytes() { return __p.__vector_as_span<int>(14, 4); }
#else
  public ArraySegment<byte>? GetLevelIdBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public int[] GetLevelIdArray() { return __p.__vector_as_array<int>(14); }
  public string Icon { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconBytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetIconBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetIconArray() { return __p.__vector_as_array<byte>(16); }
  public string Des { get { int o = __p.__offset(18); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDesBytes() { return __p.__vector_as_span<byte>(18, 1); }
#else
  public ArraySegment<byte>? GetDesBytes() { return __p.__vector_as_arraysegment(18); }
#endif
  public byte[] GetDesArray() { return __p.__vector_as_array<byte>(18); }

  public static Offset<GameConfigs.RoomObjRowData> CreateRoomObjRowData(FlatBufferBuilder builder,
      int ID = 0,
      StringOffset NameOffset = default(StringOffset),
      int MaxStar = 0,
      int RoomArea = 0,
      VectorOffset CostOffset = default(VectorOffset),
      VectorOffset LevelIdOffset = default(VectorOffset),
      StringOffset IconOffset = default(StringOffset),
      StringOffset DesOffset = default(StringOffset)) {
    builder.StartTable(8);
    RoomObjRowData.AddDes(builder, DesOffset);
    RoomObjRowData.AddIcon(builder, IconOffset);
    RoomObjRowData.AddLevelId(builder, LevelIdOffset);
    RoomObjRowData.AddCost(builder, CostOffset);
    RoomObjRowData.AddRoomArea(builder, RoomArea);
    RoomObjRowData.AddMaxStar(builder, MaxStar);
    RoomObjRowData.AddName(builder, NameOffset);
    RoomObjRowData.AddID(builder, ID);
    return RoomObjRowData.EndRoomObjRowData(builder);
  }

  public static void StartRoomObjRowData(FlatBufferBuilder builder) { builder.StartTable(8); }
  public static void AddID(FlatBufferBuilder builder, int ID) { builder.AddInt(0, ID, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset NameOffset) { builder.AddOffset(1, NameOffset.Value, 0); }
  public static void AddMaxStar(FlatBufferBuilder builder, int MaxStar) { builder.AddInt(2, MaxStar, 0); }
  public static void AddRoomArea(FlatBufferBuilder builder, int RoomArea) { builder.AddInt(3, RoomArea, 0); }
  public static void AddCost(FlatBufferBuilder builder, VectorOffset CostOffset) { builder.AddOffset(4, CostOffset.Value, 0); }
  public static VectorOffset CreateCostVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateCostVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartCostVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddLevelId(FlatBufferBuilder builder, VectorOffset LevelIdOffset) { builder.AddOffset(5, LevelIdOffset.Value, 0); }
  public static VectorOffset CreateLevelIdVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateLevelIdVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartLevelIdVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddIcon(FlatBufferBuilder builder, StringOffset IconOffset) { builder.AddOffset(6, IconOffset.Value, 0); }
  public static void AddDes(FlatBufferBuilder builder, StringOffset DesOffset) { builder.AddOffset(7, DesOffset.Value, 0); }
  public static Offset<GameConfigs.RoomObjRowData> EndRoomObjRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.RoomObjRowData>(o);
  }
};

public struct RoomObj : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static RoomObj GetRootAsRoomObj(ByteBuffer _bb) { return GetRootAsRoomObj(_bb, new RoomObj()); }
  public static RoomObj GetRootAsRoomObj(ByteBuffer _bb, RoomObj obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RoomObj __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.RoomObjRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.RoomObjRowData?)(new GameConfigs.RoomObjRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.RoomObj> CreateRoomObj(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    RoomObj.AddDatalist(builder, datalistOffset);
    return RoomObj.EndRoomObj(builder);
  }

  public static void StartRoomObj(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.RoomObjRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.RoomObjRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.RoomObj> EndRoomObj(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.RoomObj>(o);
  }
  public static void FinishRoomObjBuffer(FlatBufferBuilder builder, Offset<GameConfigs.RoomObj> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedRoomObjBuffer(FlatBufferBuilder builder, Offset<GameConfigs.RoomObj> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
