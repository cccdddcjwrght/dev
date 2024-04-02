// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct FunctionConfigRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static FunctionConfigRowData GetRootAsFunctionConfigRowData(ByteBuffer _bb) { return GetRootAsFunctionConfigRowData(_bb, new FunctionConfigRowData()); }
  public static FunctionConfigRowData GetRootAsFunctionConfigRowData(ByteBuffer _bb, FunctionConfigRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public FunctionConfigRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public int Order { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Parent { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Ui { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetUiBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetUiBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetUiArray() { return __p.__vector_as_array<byte>(12); }
  public int OpenType { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int OpenVal(int j) { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int OpenValLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetOpenValBytes() { return __p.__vector_as_span<int>(16, 4); }
#else
  public ArraySegment<byte>? GetOpenValBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public int[] GetOpenValArray() { return __p.__vector_as_array<int>(16); }
  public int FixConditionVal { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int NeedFinger { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int AutoShow { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Tips { get { int o = __p.__offset(24); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTipsBytes() { return __p.__vector_as_span<byte>(24, 1); }
#else
  public ArraySegment<byte>? GetTipsBytes() { return __p.__vector_as_arraysegment(24); }
#endif
  public byte[] GetTipsArray() { return __p.__vector_as_array<byte>(24); }
  public int LoginShow { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Entrance { get { int o = __p.__offset(28); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Uniqid { get { int o = __p.__offset(30); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetUniqidBytes() { return __p.__vector_as_span<byte>(30, 1); }
#else
  public ArraySegment<byte>? GetUniqidBytes() { return __p.__vector_as_arraysegment(30); }
#endif
  public byte[] GetUniqidArray() { return __p.__vector_as_array<byte>(30); }
  public string Icon { get { int o = __p.__offset(32); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconBytes() { return __p.__vector_as_span<byte>(32, 1); }
#else
  public ArraySegment<byte>? GetIconBytes() { return __p.__vector_as_arraysegment(32); }
#endif
  public byte[] GetIconArray() { return __p.__vector_as_array<byte>(32); }
  public string Alias { get { int o = __p.__offset(34); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetAliasBytes() { return __p.__vector_as_span<byte>(34, 1); }
#else
  public ArraySegment<byte>? GetAliasBytes() { return __p.__vector_as_arraysegment(34); }
#endif
  public byte[] GetAliasArray() { return __p.__vector_as_array<byte>(34); }
  public string Res { get { int o = __p.__offset(36); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetResBytes() { return __p.__vector_as_span<byte>(36, 1); }
#else
  public ArraySegment<byte>? GetResBytes() { return __p.__vector_as_arraysegment(36); }
#endif
  public byte[] GetResArray() { return __p.__vector_as_array<byte>(36); }
  public int Net { get { int o = __p.__offset(38); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Nettips { get { int o = __p.__offset(40); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNettipsBytes() { return __p.__vector_as_span<byte>(40, 1); }
#else
  public ArraySegment<byte>? GetNettipsBytes() { return __p.__vector_as_arraysegment(40); }
#endif
  public byte[] GetNettipsArray() { return __p.__vector_as_array<byte>(40); }

  public static Offset<GameConfigs.FunctionConfigRowData> CreateFunctionConfigRowData(FlatBufferBuilder builder,
      int id = 0,
      StringOffset nameOffset = default(StringOffset),
      int order = 0,
      int parent = 0,
      StringOffset uiOffset = default(StringOffset),
      int openType = 0,
      VectorOffset openValOffset = default(VectorOffset),
      int fixConditionVal = 0,
      int needFinger = 0,
      int autoShow = 0,
      StringOffset tipsOffset = default(StringOffset),
      int loginShow = 0,
      int entrance = 0,
      StringOffset uniqidOffset = default(StringOffset),
      StringOffset iconOffset = default(StringOffset),
      StringOffset aliasOffset = default(StringOffset),
      StringOffset resOffset = default(StringOffset),
      int net = 0,
      StringOffset nettipsOffset = default(StringOffset)) {
    builder.StartTable(19);
    FunctionConfigRowData.AddNettips(builder, nettipsOffset);
    FunctionConfigRowData.AddNet(builder, net);
    FunctionConfigRowData.AddRes(builder, resOffset);
    FunctionConfigRowData.AddAlias(builder, aliasOffset);
    FunctionConfigRowData.AddIcon(builder, iconOffset);
    FunctionConfigRowData.AddUniqid(builder, uniqidOffset);
    FunctionConfigRowData.AddEntrance(builder, entrance);
    FunctionConfigRowData.AddLoginShow(builder, loginShow);
    FunctionConfigRowData.AddTips(builder, tipsOffset);
    FunctionConfigRowData.AddAutoShow(builder, autoShow);
    FunctionConfigRowData.AddNeedFinger(builder, needFinger);
    FunctionConfigRowData.AddFixConditionVal(builder, fixConditionVal);
    FunctionConfigRowData.AddOpenVal(builder, openValOffset);
    FunctionConfigRowData.AddOpenType(builder, openType);
    FunctionConfigRowData.AddUi(builder, uiOffset);
    FunctionConfigRowData.AddParent(builder, parent);
    FunctionConfigRowData.AddOrder(builder, order);
    FunctionConfigRowData.AddName(builder, nameOffset);
    FunctionConfigRowData.AddId(builder, id);
    return FunctionConfigRowData.EndFunctionConfigRowData(builder);
  }

  public static void StartFunctionConfigRowData(FlatBufferBuilder builder) { builder.StartTable(19); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(1, nameOffset.Value, 0); }
  public static void AddOrder(FlatBufferBuilder builder, int order) { builder.AddInt(2, order, 0); }
  public static void AddParent(FlatBufferBuilder builder, int parent) { builder.AddInt(3, parent, 0); }
  public static void AddUi(FlatBufferBuilder builder, StringOffset uiOffset) { builder.AddOffset(4, uiOffset.Value, 0); }
  public static void AddOpenType(FlatBufferBuilder builder, int openType) { builder.AddInt(5, openType, 0); }
  public static void AddOpenVal(FlatBufferBuilder builder, VectorOffset openValOffset) { builder.AddOffset(6, openValOffset.Value, 0); }
  public static VectorOffset CreateOpenValVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateOpenValVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartOpenValVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddFixConditionVal(FlatBufferBuilder builder, int fixConditionVal) { builder.AddInt(7, fixConditionVal, 0); }
  public static void AddNeedFinger(FlatBufferBuilder builder, int needFinger) { builder.AddInt(8, needFinger, 0); }
  public static void AddAutoShow(FlatBufferBuilder builder, int autoShow) { builder.AddInt(9, autoShow, 0); }
  public static void AddTips(FlatBufferBuilder builder, StringOffset tipsOffset) { builder.AddOffset(10, tipsOffset.Value, 0); }
  public static void AddLoginShow(FlatBufferBuilder builder, int loginShow) { builder.AddInt(11, loginShow, 0); }
  public static void AddEntrance(FlatBufferBuilder builder, int entrance) { builder.AddInt(12, entrance, 0); }
  public static void AddUniqid(FlatBufferBuilder builder, StringOffset uniqidOffset) { builder.AddOffset(13, uniqidOffset.Value, 0); }
  public static void AddIcon(FlatBufferBuilder builder, StringOffset iconOffset) { builder.AddOffset(14, iconOffset.Value, 0); }
  public static void AddAlias(FlatBufferBuilder builder, StringOffset aliasOffset) { builder.AddOffset(15, aliasOffset.Value, 0); }
  public static void AddRes(FlatBufferBuilder builder, StringOffset resOffset) { builder.AddOffset(16, resOffset.Value, 0); }
  public static void AddNet(FlatBufferBuilder builder, int net) { builder.AddInt(17, net, 0); }
  public static void AddNettips(FlatBufferBuilder builder, StringOffset nettipsOffset) { builder.AddOffset(18, nettipsOffset.Value, 0); }
  public static Offset<GameConfigs.FunctionConfigRowData> EndFunctionConfigRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.FunctionConfigRowData>(o);
  }
};

public struct FunctionConfig : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static FunctionConfig GetRootAsFunctionConfig(ByteBuffer _bb) { return GetRootAsFunctionConfig(_bb, new FunctionConfig()); }
  public static FunctionConfig GetRootAsFunctionConfig(ByteBuffer _bb, FunctionConfig obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public FunctionConfig __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.FunctionConfigRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.FunctionConfigRowData?)(new GameConfigs.FunctionConfigRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.FunctionConfig> CreateFunctionConfig(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    FunctionConfig.AddDatalist(builder, datalistOffset);
    return FunctionConfig.EndFunctionConfig(builder);
  }

  public static void StartFunctionConfig(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.FunctionConfigRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.FunctionConfigRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.FunctionConfig> EndFunctionConfig(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.FunctionConfig>(o);
  }
  public static void FinishFunctionConfigBuffer(FlatBufferBuilder builder, Offset<GameConfigs.FunctionConfig> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedFunctionConfigBuffer(FlatBufferBuilder builder, Offset<GameConfigs.FunctionConfig> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
