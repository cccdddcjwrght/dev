// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct GuideRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static GuideRowData GetRootAsGuideRowData(ByteBuffer _bb) { return GetRootAsGuideRowData(_bb, new GuideRowData()); }
  public static GuideRowData GetRootAsGuideRowData(ByteBuffer _bb, GuideRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public GuideRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GuideId { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Step { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GuideType { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Cmd { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetCmdBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetCmdBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetCmdArray() { return __p.__vector_as_array<byte>(12); }
  public string StringParam { get { int o = __p.__offset(14); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetStringParamBytes() { return __p.__vector_as_span<byte>(14, 1); }
#else
  public ArraySegment<byte>? GetStringParamBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public byte[] GetStringParamArray() { return __p.__vector_as_array<byte>(14); }
  public string UIPath { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetUIPathBytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetUIPathBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetUIPathArray() { return __p.__vector_as_array<byte>(16); }
  public float FloatParam(int j) { int o = __p.__offset(18); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int FloatParamLength { get { int o = __p.__offset(18); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetFloatParamBytes() { return __p.__vector_as_span<float>(18, 4); }
#else
  public ArraySegment<byte>? GetFloatParamBytes() { return __p.__vector_as_arraysegment(18); }
#endif
  public float[] GetFloatParamArray() { return __p.__vector_as_array<float>(18); }
  public int UISize(int j) { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int UISizeLength { get { int o = __p.__offset(20); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetUISizeBytes() { return __p.__vector_as_span<int>(20, 4); }
#else
  public ArraySegment<byte>? GetUISizeBytes() { return __p.__vector_as_arraysegment(20); }
#endif
  public int[] GetUISizeArray() { return __p.__vector_as_array<int>(20); }
  public int OffsetXY(int j) { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int OffsetXYLength { get { int o = __p.__offset(22); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetOffsetXYBytes() { return __p.__vector_as_span<int>(22, 4); }
#else
  public ArraySegment<byte>? GetOffsetXYBytes() { return __p.__vector_as_arraysegment(22); }
#endif
  public int[] GetOffsetXYArray() { return __p.__vector_as_array<int>(22); }
  public int TimeOut { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Force { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<GameConfigs.GuideRowData> CreateGuideRowData(FlatBufferBuilder builder,
      int Id = 0,
      int GuideId = 0,
      int Step = 0,
      int GuideType = 0,
      StringOffset CmdOffset = default(StringOffset),
      StringOffset StringParamOffset = default(StringOffset),
      StringOffset UIPathOffset = default(StringOffset),
      VectorOffset floatParamOffset = default(VectorOffset),
      VectorOffset UISizeOffset = default(VectorOffset),
      VectorOffset OffsetXYOffset = default(VectorOffset),
      int TimeOut = 0,
      int Force = 0) {
    builder.StartTable(12);
    GuideRowData.AddForce(builder, Force);
    GuideRowData.AddTimeOut(builder, TimeOut);
    GuideRowData.AddOffsetXY(builder, OffsetXYOffset);
    GuideRowData.AddUISize(builder, UISizeOffset);
    GuideRowData.AddFloatParam(builder, floatParamOffset);
    GuideRowData.AddUIPath(builder, UIPathOffset);
    GuideRowData.AddStringParam(builder, StringParamOffset);
    GuideRowData.AddCmd(builder, CmdOffset);
    GuideRowData.AddGuideType(builder, GuideType);
    GuideRowData.AddStep(builder, Step);
    GuideRowData.AddGuideId(builder, GuideId);
    GuideRowData.AddId(builder, Id);
    return GuideRowData.EndGuideRowData(builder);
  }

  public static void StartGuideRowData(FlatBufferBuilder builder) { builder.StartTable(12); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddGuideId(FlatBufferBuilder builder, int GuideId) { builder.AddInt(1, GuideId, 0); }
  public static void AddStep(FlatBufferBuilder builder, int Step) { builder.AddInt(2, Step, 0); }
  public static void AddGuideType(FlatBufferBuilder builder, int GuideType) { builder.AddInt(3, GuideType, 0); }
  public static void AddCmd(FlatBufferBuilder builder, StringOffset CmdOffset) { builder.AddOffset(4, CmdOffset.Value, 0); }
  public static void AddStringParam(FlatBufferBuilder builder, StringOffset StringParamOffset) { builder.AddOffset(5, StringParamOffset.Value, 0); }
  public static void AddUIPath(FlatBufferBuilder builder, StringOffset UIPathOffset) { builder.AddOffset(6, UIPathOffset.Value, 0); }
  public static void AddFloatParam(FlatBufferBuilder builder, VectorOffset floatParamOffset) { builder.AddOffset(7, floatParamOffset.Value, 0); }
  public static VectorOffset CreateFloatParamVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateFloatParamVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartFloatParamVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddUISize(FlatBufferBuilder builder, VectorOffset UISizeOffset) { builder.AddOffset(8, UISizeOffset.Value, 0); }
  public static VectorOffset CreateUISizeVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateUISizeVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartUISizeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddOffsetXY(FlatBufferBuilder builder, VectorOffset OffsetXYOffset) { builder.AddOffset(9, OffsetXYOffset.Value, 0); }
  public static VectorOffset CreateOffsetXYVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateOffsetXYVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartOffsetXYVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddTimeOut(FlatBufferBuilder builder, int TimeOut) { builder.AddInt(10, TimeOut, 0); }
  public static void AddForce(FlatBufferBuilder builder, int Force) { builder.AddInt(11, Force, 0); }
  public static Offset<GameConfigs.GuideRowData> EndGuideRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.GuideRowData>(o);
  }
};

public struct Guide : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Guide GetRootAsGuide(ByteBuffer _bb) { return GetRootAsGuide(_bb, new Guide()); }
  public static Guide GetRootAsGuide(ByteBuffer _bb, Guide obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Guide __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.GuideRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.GuideRowData?)(new GameConfigs.GuideRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Guide> CreateGuide(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Guide.AddDatalist(builder, datalistOffset);
    return Guide.EndGuide(builder);
  }

  public static void StartGuide(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.GuideRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.GuideRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Guide> EndGuide(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Guide>(o);
  }
  public static void FinishGuideBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Guide> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedGuideBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Guide> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
