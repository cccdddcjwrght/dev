// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct CarDataRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static CarDataRowData GetRootAsCarDataRowData(ByteBuffer _bb) { return GetRootAsCarDataRowData(_bb, new CarDataRowData()); }
  public static CarDataRowData GetRootAsCarDataRowData(ByteBuffer _bb, CarDataRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CarDataRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public int Type { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Model { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetModelBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetModelBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetModelArray() { return __p.__vector_as_array<byte>(10); }
  public float MoveSpeed { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public int WorkerArea { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string AI { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetAIBytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetAIBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetAIArray() { return __p.__vector_as_array<byte>(16); }
  public int ChairNum { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ShowCustomer { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float Position(int j) { int o = __p.__offset(22); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int PositionLength { get { int o = __p.__offset(22); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetPositionBytes() { return __p.__vector_as_span<float>(22, 4); }
#else
  public ArraySegment<byte>? GetPositionBytes() { return __p.__vector_as_arraysegment(22); }
#endif
  public float[] GetPositionArray() { return __p.__vector_as_array<float>(22); }
  public float Scale { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float Rotation(int j) { int o = __p.__offset(26); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int RotationLength { get { int o = __p.__offset(26); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetRotationBytes() { return __p.__vector_as_span<float>(26, 4); }
#else
  public ArraySegment<byte>? GetRotationBytes() { return __p.__vector_as_arraysegment(26); }
#endif
  public float[] GetRotationArray() { return __p.__vector_as_array<float>(26); }
  public float BodyLength { get { int o = __p.__offset(28); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public string PathTag { get { int o = __p.__offset(30); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPathTagBytes() { return __p.__vector_as_span<byte>(30, 1); }
#else
  public ArraySegment<byte>? GetPathTagBytes() { return __p.__vector_as_arraysegment(30); }
#endif
  public byte[] GetPathTagArray() { return __p.__vector_as_array<byte>(30); }

  public static Offset<GameConfigs.CarDataRowData> CreateCarDataRowData(FlatBufferBuilder builder,
      int Id = 0,
      StringOffset NameOffset = default(StringOffset),
      int Type = 0,
      StringOffset ModelOffset = default(StringOffset),
      float MoveSpeed = 0.0f,
      int WorkerArea = 0,
      StringOffset AIOffset = default(StringOffset),
      int ChairNum = 0,
      int ShowCustomer = 0,
      VectorOffset positionOffset = default(VectorOffset),
      float scale = 0.0f,
      VectorOffset rotationOffset = default(VectorOffset),
      float BodyLength = 0.0f,
      StringOffset PathTagOffset = default(StringOffset)) {
    builder.StartTable(14);
    CarDataRowData.AddPathTag(builder, PathTagOffset);
    CarDataRowData.AddBodyLength(builder, BodyLength);
    CarDataRowData.AddRotation(builder, rotationOffset);
    CarDataRowData.AddScale(builder, scale);
    CarDataRowData.AddPosition(builder, positionOffset);
    CarDataRowData.AddShowCustomer(builder, ShowCustomer);
    CarDataRowData.AddChairNum(builder, ChairNum);
    CarDataRowData.AddAI(builder, AIOffset);
    CarDataRowData.AddWorkerArea(builder, WorkerArea);
    CarDataRowData.AddMoveSpeed(builder, MoveSpeed);
    CarDataRowData.AddModel(builder, ModelOffset);
    CarDataRowData.AddType(builder, Type);
    CarDataRowData.AddName(builder, NameOffset);
    CarDataRowData.AddId(builder, Id);
    return CarDataRowData.EndCarDataRowData(builder);
  }

  public static void StartCarDataRowData(FlatBufferBuilder builder) { builder.StartTable(14); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset NameOffset) { builder.AddOffset(1, NameOffset.Value, 0); }
  public static void AddType(FlatBufferBuilder builder, int Type) { builder.AddInt(2, Type, 0); }
  public static void AddModel(FlatBufferBuilder builder, StringOffset ModelOffset) { builder.AddOffset(3, ModelOffset.Value, 0); }
  public static void AddMoveSpeed(FlatBufferBuilder builder, float MoveSpeed) { builder.AddFloat(4, MoveSpeed, 0.0f); }
  public static void AddWorkerArea(FlatBufferBuilder builder, int WorkerArea) { builder.AddInt(5, WorkerArea, 0); }
  public static void AddAI(FlatBufferBuilder builder, StringOffset AIOffset) { builder.AddOffset(6, AIOffset.Value, 0); }
  public static void AddChairNum(FlatBufferBuilder builder, int ChairNum) { builder.AddInt(7, ChairNum, 0); }
  public static void AddShowCustomer(FlatBufferBuilder builder, int ShowCustomer) { builder.AddInt(8, ShowCustomer, 0); }
  public static void AddPosition(FlatBufferBuilder builder, VectorOffset positionOffset) { builder.AddOffset(9, positionOffset.Value, 0); }
  public static VectorOffset CreatePositionVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreatePositionVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartPositionVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddScale(FlatBufferBuilder builder, float scale) { builder.AddFloat(10, scale, 0.0f); }
  public static void AddRotation(FlatBufferBuilder builder, VectorOffset rotationOffset) { builder.AddOffset(11, rotationOffset.Value, 0); }
  public static VectorOffset CreateRotationVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateRotationVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartRotationVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBodyLength(FlatBufferBuilder builder, float BodyLength) { builder.AddFloat(12, BodyLength, 0.0f); }
  public static void AddPathTag(FlatBufferBuilder builder, StringOffset PathTagOffset) { builder.AddOffset(13, PathTagOffset.Value, 0); }
  public static Offset<GameConfigs.CarDataRowData> EndCarDataRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.CarDataRowData>(o);
  }
};

public struct CarData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static CarData GetRootAsCarData(ByteBuffer _bb) { return GetRootAsCarData(_bb, new CarData()); }
  public static CarData GetRootAsCarData(ByteBuffer _bb, CarData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CarData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.CarDataRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.CarDataRowData?)(new GameConfigs.CarDataRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.CarData> CreateCarData(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    CarData.AddDatalist(builder, datalistOffset);
    return CarData.EndCarData(builder);
  }

  public static void StartCarData(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.CarDataRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.CarDataRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.CarData> EndCarData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.CarData>(o);
  }
  public static void FinishCarDataBuffer(FlatBufferBuilder builder, Offset<GameConfigs.CarData> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedCarDataBuffer(FlatBufferBuilder builder, Offset<GameConfigs.CarData> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
