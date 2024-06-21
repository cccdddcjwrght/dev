// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct LevelPathRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static LevelPathRowData GetRootAsLevelPathRowData(ByteBuffer _bb) { return GetRootAsLevelPathRowData(_bb, new LevelPathRowData()); }
  public static LevelPathRowData GetRootAsLevelPathRowData(ByteBuffer _bb, LevelPathRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LevelPathRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string PathTag { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPathTagBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetPathTagBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetPathTagArray() { return __p.__vector_as_array<byte>(6); }
  public float OrderPosition(int j) { int o = __p.__offset(8); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int OrderPositionLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetOrderPositionBytes() { return __p.__vector_as_span<float>(8, 4); }
#else
  public ArraySegment<byte>? GetOrderPositionBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public float[] GetOrderPositionArray() { return __p.__vector_as_array<float>(8); }
  public int OrderMapPos(int j) { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int OrderMapPosLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetOrderMapPosBytes() { return __p.__vector_as_span<int>(10, 4); }
#else
  public ArraySegment<byte>? GetOrderMapPosBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public int[] GetOrderMapPosArray() { return __p.__vector_as_array<int>(10); }
  public float Gap { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public int CarId(int j) { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int CarIdLength { get { int o = __p.__offset(14); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetCarIdBytes() { return __p.__vector_as_span<int>(14, 4); }
#else
  public ArraySegment<byte>? GetCarIdBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public int[] GetCarIdArray() { return __p.__vector_as_array<int>(14); }
  public int CarWeight(int j) { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int CarWeightLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetCarWeightBytes() { return __p.__vector_as_span<int>(16, 4); }
#else
  public ArraySegment<byte>? GetCarWeightBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public int[] GetCarWeightArray() { return __p.__vector_as_array<int>(16); }
  public int CarNum { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int BusStop1(int j) { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int BusStop1Length { get { int o = __p.__offset(20); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBusStop1Bytes() { return __p.__vector_as_span<int>(20, 4); }
#else
  public ArraySegment<byte>? GetBusStop1Bytes() { return __p.__vector_as_arraysegment(20); }
#endif
  public int[] GetBusStop1Array() { return __p.__vector_as_array<int>(20); }
  public int BusStop2(int j) { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int BusStop2Length { get { int o = __p.__offset(22); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBusStop2Bytes() { return __p.__vector_as_span<int>(22, 4); }
#else
  public ArraySegment<byte>? GetBusStop2Bytes() { return __p.__vector_as_arraysegment(22); }
#endif
  public int[] GetBusStop2Array() { return __p.__vector_as_array<int>(22); }
  public int BusStop3(int j) { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int BusStop3Length { get { int o = __p.__offset(24); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBusStop3Bytes() { return __p.__vector_as_span<int>(24, 4); }
#else
  public ArraySegment<byte>? GetBusStop3Bytes() { return __p.__vector_as_arraysegment(24); }
#endif
  public int[] GetBusStop3Array() { return __p.__vector_as_array<int>(24); }
  public int ShareQueueID { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<GameConfigs.LevelPathRowData> CreateLevelPathRowData(FlatBufferBuilder builder,
      int Id = 0,
      StringOffset PathTagOffset = default(StringOffset),
      VectorOffset OrderPositionOffset = default(VectorOffset),
      VectorOffset OrderMapPosOffset = default(VectorOffset),
      float gap = 0.0f,
      VectorOffset CarIdOffset = default(VectorOffset),
      VectorOffset CarWeightOffset = default(VectorOffset),
      int CarNum = 0,
      VectorOffset BusStop1Offset = default(VectorOffset),
      VectorOffset BusStop2Offset = default(VectorOffset),
      VectorOffset BusStop3Offset = default(VectorOffset),
      int shareQueueID = 0) {
    builder.StartTable(12);
    LevelPathRowData.AddShareQueueID(builder, shareQueueID);
    LevelPathRowData.AddBusStop3(builder, BusStop3Offset);
    LevelPathRowData.AddBusStop2(builder, BusStop2Offset);
    LevelPathRowData.AddBusStop1(builder, BusStop1Offset);
    LevelPathRowData.AddCarNum(builder, CarNum);
    LevelPathRowData.AddCarWeight(builder, CarWeightOffset);
    LevelPathRowData.AddCarId(builder, CarIdOffset);
    LevelPathRowData.AddGap(builder, gap);
    LevelPathRowData.AddOrderMapPos(builder, OrderMapPosOffset);
    LevelPathRowData.AddOrderPosition(builder, OrderPositionOffset);
    LevelPathRowData.AddPathTag(builder, PathTagOffset);
    LevelPathRowData.AddId(builder, Id);
    return LevelPathRowData.EndLevelPathRowData(builder);
  }

  public static void StartLevelPathRowData(FlatBufferBuilder builder) { builder.StartTable(12); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddPathTag(FlatBufferBuilder builder, StringOffset PathTagOffset) { builder.AddOffset(1, PathTagOffset.Value, 0); }
  public static void AddOrderPosition(FlatBufferBuilder builder, VectorOffset OrderPositionOffset) { builder.AddOffset(2, OrderPositionOffset.Value, 0); }
  public static VectorOffset CreateOrderPositionVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateOrderPositionVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartOrderPositionVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddOrderMapPos(FlatBufferBuilder builder, VectorOffset OrderMapPosOffset) { builder.AddOffset(3, OrderMapPosOffset.Value, 0); }
  public static VectorOffset CreateOrderMapPosVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateOrderMapPosVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartOrderMapPosVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddGap(FlatBufferBuilder builder, float gap) { builder.AddFloat(4, gap, 0.0f); }
  public static void AddCarId(FlatBufferBuilder builder, VectorOffset CarIdOffset) { builder.AddOffset(5, CarIdOffset.Value, 0); }
  public static VectorOffset CreateCarIdVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateCarIdVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartCarIdVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddCarWeight(FlatBufferBuilder builder, VectorOffset CarWeightOffset) { builder.AddOffset(6, CarWeightOffset.Value, 0); }
  public static VectorOffset CreateCarWeightVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateCarWeightVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartCarWeightVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddCarNum(FlatBufferBuilder builder, int CarNum) { builder.AddInt(7, CarNum, 0); }
  public static void AddBusStop1(FlatBufferBuilder builder, VectorOffset BusStop1Offset) { builder.AddOffset(8, BusStop1Offset.Value, 0); }
  public static VectorOffset CreateBusStop1Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBusStop1VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBusStop1Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBusStop2(FlatBufferBuilder builder, VectorOffset BusStop2Offset) { builder.AddOffset(9, BusStop2Offset.Value, 0); }
  public static VectorOffset CreateBusStop2Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBusStop2VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBusStop2Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBusStop3(FlatBufferBuilder builder, VectorOffset BusStop3Offset) { builder.AddOffset(10, BusStop3Offset.Value, 0); }
  public static VectorOffset CreateBusStop3Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBusStop3VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBusStop3Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddShareQueueID(FlatBufferBuilder builder, int shareQueueID) { builder.AddInt(11, shareQueueID, 0); }
  public static Offset<GameConfigs.LevelPathRowData> EndLevelPathRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.LevelPathRowData>(o);
  }
};

public struct LevelPath : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static LevelPath GetRootAsLevelPath(ByteBuffer _bb) { return GetRootAsLevelPath(_bb, new LevelPath()); }
  public static LevelPath GetRootAsLevelPath(ByteBuffer _bb, LevelPath obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LevelPath __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.LevelPathRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.LevelPathRowData?)(new GameConfigs.LevelPathRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.LevelPath> CreateLevelPath(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    LevelPath.AddDatalist(builder, datalistOffset);
    return LevelPath.EndLevelPath(builder);
  }

  public static void StartLevelPath(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.LevelPathRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.LevelPathRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.LevelPath> EndLevelPath(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.LevelPath>(o);
  }
  public static void FinishLevelPathBuffer(FlatBufferBuilder builder, Offset<GameConfigs.LevelPath> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedLevelPathBuffer(FlatBufferBuilder builder, Offset<GameConfigs.LevelPath> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
