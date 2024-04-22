// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct EggRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static EggRowData GetRootAsEggRowData(ByteBuffer _bb) { return GetRootAsEggRowData(_bb, new EggRowData()); }
  public static EggRowData GetRootAsEggRowData(ByteBuffer _bb, EggRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public EggRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Quality { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Weight(int j) { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int WeightLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetWeightBytes() { return __p.__vector_as_span<int>(8, 4); }
#else
  public ArraySegment<byte>? GetWeightBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public int[] GetWeightArray() { return __p.__vector_as_array<int>(8); }
  public int Activity(int j) { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int ActivityLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetActivityBytes() { return __p.__vector_as_span<int>(10, 4); }
#else
  public ArraySegment<byte>? GetActivityBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public int[] GetActivityArray() { return __p.__vector_as_array<int>(10); }
  public int Time { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<GameConfigs.EggRowData> CreateEggRowData(FlatBufferBuilder builder,
      int Id = 0,
      int Quality = 0,
      VectorOffset WeightOffset = default(VectorOffset),
      VectorOffset ActivityOffset = default(VectorOffset),
      int Time = 0) {
    builder.StartTable(5);
    EggRowData.AddTime(builder, Time);
    EggRowData.AddActivity(builder, ActivityOffset);
    EggRowData.AddWeight(builder, WeightOffset);
    EggRowData.AddQuality(builder, Quality);
    EggRowData.AddId(builder, Id);
    return EggRowData.EndEggRowData(builder);
  }

  public static void StartEggRowData(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddQuality(FlatBufferBuilder builder, int Quality) { builder.AddInt(1, Quality, 0); }
  public static void AddWeight(FlatBufferBuilder builder, VectorOffset WeightOffset) { builder.AddOffset(2, WeightOffset.Value, 0); }
  public static VectorOffset CreateWeightVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateWeightVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartWeightVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddActivity(FlatBufferBuilder builder, VectorOffset ActivityOffset) { builder.AddOffset(3, ActivityOffset.Value, 0); }
  public static VectorOffset CreateActivityVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateActivityVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartActivityVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddTime(FlatBufferBuilder builder, int Time) { builder.AddInt(4, Time, 0); }
  public static Offset<GameConfigs.EggRowData> EndEggRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.EggRowData>(o);
  }
};

public struct Egg : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Egg GetRootAsEgg(ByteBuffer _bb) { return GetRootAsEgg(_bb, new Egg()); }
  public static Egg GetRootAsEgg(ByteBuffer _bb, Egg obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Egg __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.EggRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.EggRowData?)(new GameConfigs.EggRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Egg> CreateEgg(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Egg.AddDatalist(builder, datalistOffset);
    return Egg.EndEgg(builder);
  }

  public static void StartEgg(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.EggRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.EggRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Egg> EndEgg(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Egg>(o);
  }
  public static void FinishEggBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Egg> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedEggBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Egg> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
