// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct EquipQualityRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static EquipQualityRowData GetRootAsEquipQualityRowData(ByteBuffer _bb) { return GetRootAsEquipQualityRowData(_bb, new EquipQualityRowData()); }
  public static EquipQualityRowData GetRootAsEquipQualityRowData(ByteBuffer _bb, EquipQualityRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public EquipQualityRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MainBuff(int j) { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int MainBuffLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetMainBuffBytes() { return __p.__vector_as_span<int>(6, 4); }
#else
  public ArraySegment<byte>? GetMainBuffBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public int[] GetMainBuffArray() { return __p.__vector_as_array<int>(6); }
  public int LevelMax { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MainBuffAdd { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int AdvanceType { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int AdvanceValue { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int BreakReward(int j) { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int BreakRewardLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBreakRewardBytes() { return __p.__vector_as_span<int>(16, 4); }
#else
  public ArraySegment<byte>? GetBreakRewardBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public int[] GetBreakRewardArray() { return __p.__vector_as_array<int>(16); }
  public int BuffNum { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<GameConfigs.EquipQualityRowData> CreateEquipQualityRowData(FlatBufferBuilder builder,
      int Id = 0,
      VectorOffset MainBuffOffset = default(VectorOffset),
      int LevelMax = 0,
      int MainBuffAdd = 0,
      int AdvanceType = 0,
      int AdvanceValue = 0,
      VectorOffset BreakRewardOffset = default(VectorOffset),
      int BuffNum = 0) {
    builder.StartTable(8);
    EquipQualityRowData.AddBuffNum(builder, BuffNum);
    EquipQualityRowData.AddBreakReward(builder, BreakRewardOffset);
    EquipQualityRowData.AddAdvanceValue(builder, AdvanceValue);
    EquipQualityRowData.AddAdvanceType(builder, AdvanceType);
    EquipQualityRowData.AddMainBuffAdd(builder, MainBuffAdd);
    EquipQualityRowData.AddLevelMax(builder, LevelMax);
    EquipQualityRowData.AddMainBuff(builder, MainBuffOffset);
    EquipQualityRowData.AddId(builder, Id);
    return EquipQualityRowData.EndEquipQualityRowData(builder);
  }

  public static void StartEquipQualityRowData(FlatBufferBuilder builder) { builder.StartTable(8); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddMainBuff(FlatBufferBuilder builder, VectorOffset MainBuffOffset) { builder.AddOffset(1, MainBuffOffset.Value, 0); }
  public static VectorOffset CreateMainBuffVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateMainBuffVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartMainBuffVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddLevelMax(FlatBufferBuilder builder, int LevelMax) { builder.AddInt(2, LevelMax, 0); }
  public static void AddMainBuffAdd(FlatBufferBuilder builder, int MainBuffAdd) { builder.AddInt(3, MainBuffAdd, 0); }
  public static void AddAdvanceType(FlatBufferBuilder builder, int AdvanceType) { builder.AddInt(4, AdvanceType, 0); }
  public static void AddAdvanceValue(FlatBufferBuilder builder, int AdvanceValue) { builder.AddInt(5, AdvanceValue, 0); }
  public static void AddBreakReward(FlatBufferBuilder builder, VectorOffset BreakRewardOffset) { builder.AddOffset(6, BreakRewardOffset.Value, 0); }
  public static VectorOffset CreateBreakRewardVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBreakRewardVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBreakRewardVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBuffNum(FlatBufferBuilder builder, int BuffNum) { builder.AddInt(7, BuffNum, 0); }
  public static Offset<GameConfigs.EquipQualityRowData> EndEquipQualityRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.EquipQualityRowData>(o);
  }
};

public struct EquipQuality : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static EquipQuality GetRootAsEquipQuality(ByteBuffer _bb) { return GetRootAsEquipQuality(_bb, new EquipQuality()); }
  public static EquipQuality GetRootAsEquipQuality(ByteBuffer _bb, EquipQuality obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public EquipQuality __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.EquipQualityRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.EquipQualityRowData?)(new GameConfigs.EquipQualityRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.EquipQuality> CreateEquipQuality(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    EquipQuality.AddDatalist(builder, datalistOffset);
    return EquipQuality.EndEquipQuality(builder);
  }

  public static void StartEquipQuality(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.EquipQualityRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.EquipQualityRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.EquipQuality> EndEquipQuality(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.EquipQuality>(o);
  }
  public static void FinishEquipQualityBuffer(FlatBufferBuilder builder, Offset<GameConfigs.EquipQuality> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedEquipQualityBuffer(FlatBufferBuilder builder, Offset<GameConfigs.EquipQuality> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
