// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct PetsRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static PetsRowData GetRootAsPetsRowData(ByteBuffer _bb) { return GetRootAsPetsRowData(_bb, new PetsRowData()); }
  public static PetsRowData GetRootAsPetsRowData(ByteBuffer _bb, PetsRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public PetsRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Quality { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(8); }
  public string Icon { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetIconBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetIconArray() { return __p.__vector_as_array<byte>(10); }
  public string Resource { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetResourceBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetResourceBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetResourceArray() { return __p.__vector_as_array<byte>(12); }
  public int Activity { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float Size { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public int Buffs(int j) { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int BuffsLength { get { int o = __p.__offset(18); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBuffsBytes() { return __p.__vector_as_span<int>(18, 4); }
#else
  public ArraySegment<byte>? GetBuffsBytes() { return __p.__vector_as_arraysegment(18); }
#endif
  public int[] GetBuffsArray() { return __p.__vector_as_array<int>(18); }
  public int Weights(int j) { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int WeightsLength { get { int o = __p.__offset(20); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetWeightsBytes() { return __p.__vector_as_span<int>(20, 4); }
#else
  public ArraySegment<byte>? GetWeightsBytes() { return __p.__vector_as_arraysegment(20); }
#endif
  public int[] GetWeightsArray() { return __p.__vector_as_array<int>(20); }
  public int RecycleReward(int j) { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int RecycleRewardLength { get { int o = __p.__offset(22); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetRecycleRewardBytes() { return __p.__vector_as_span<int>(22, 4); }
#else
  public ArraySegment<byte>? GetRecycleRewardBytes() { return __p.__vector_as_arraysegment(22); }
#endif
  public int[] GetRecycleRewardArray() { return __p.__vector_as_array<int>(22); }
  public int FootEffect { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ShowEffect { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float DelayMove { get { int o = __p.__offset(28); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }

  public static Offset<GameConfigs.PetsRowData> CreatePetsRowData(FlatBufferBuilder builder,
      int Id = 0,
      int Quality = 0,
      StringOffset NameOffset = default(StringOffset),
      StringOffset IconOffset = default(StringOffset),
      StringOffset ResourceOffset = default(StringOffset),
      int Activity = 0,
      float Size = 0.0f,
      VectorOffset BuffsOffset = default(VectorOffset),
      VectorOffset WeightsOffset = default(VectorOffset),
      VectorOffset RecycleRewardOffset = default(VectorOffset),
      int FootEffect = 0,
      int ShowEffect = 0,
      float DelayMove = 0.0f) {
    builder.StartTable(13);
    PetsRowData.AddDelayMove(builder, DelayMove);
    PetsRowData.AddShowEffect(builder, ShowEffect);
    PetsRowData.AddFootEffect(builder, FootEffect);
    PetsRowData.AddRecycleReward(builder, RecycleRewardOffset);
    PetsRowData.AddWeights(builder, WeightsOffset);
    PetsRowData.AddBuffs(builder, BuffsOffset);
    PetsRowData.AddSize(builder, Size);
    PetsRowData.AddActivity(builder, Activity);
    PetsRowData.AddResource(builder, ResourceOffset);
    PetsRowData.AddIcon(builder, IconOffset);
    PetsRowData.AddName(builder, NameOffset);
    PetsRowData.AddQuality(builder, Quality);
    PetsRowData.AddId(builder, Id);
    return PetsRowData.EndPetsRowData(builder);
  }

  public static void StartPetsRowData(FlatBufferBuilder builder) { builder.StartTable(13); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddQuality(FlatBufferBuilder builder, int Quality) { builder.AddInt(1, Quality, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset NameOffset) { builder.AddOffset(2, NameOffset.Value, 0); }
  public static void AddIcon(FlatBufferBuilder builder, StringOffset IconOffset) { builder.AddOffset(3, IconOffset.Value, 0); }
  public static void AddResource(FlatBufferBuilder builder, StringOffset ResourceOffset) { builder.AddOffset(4, ResourceOffset.Value, 0); }
  public static void AddActivity(FlatBufferBuilder builder, int Activity) { builder.AddInt(5, Activity, 0); }
  public static void AddSize(FlatBufferBuilder builder, float Size) { builder.AddFloat(6, Size, 0.0f); }
  public static void AddBuffs(FlatBufferBuilder builder, VectorOffset BuffsOffset) { builder.AddOffset(7, BuffsOffset.Value, 0); }
  public static VectorOffset CreateBuffsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBuffsVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBuffsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddWeights(FlatBufferBuilder builder, VectorOffset WeightsOffset) { builder.AddOffset(8, WeightsOffset.Value, 0); }
  public static VectorOffset CreateWeightsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateWeightsVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartWeightsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddRecycleReward(FlatBufferBuilder builder, VectorOffset RecycleRewardOffset) { builder.AddOffset(9, RecycleRewardOffset.Value, 0); }
  public static VectorOffset CreateRecycleRewardVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateRecycleRewardVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartRecycleRewardVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddFootEffect(FlatBufferBuilder builder, int FootEffect) { builder.AddInt(10, FootEffect, 0); }
  public static void AddShowEffect(FlatBufferBuilder builder, int ShowEffect) { builder.AddInt(11, ShowEffect, 0); }
  public static void AddDelayMove(FlatBufferBuilder builder, float DelayMove) { builder.AddFloat(12, DelayMove, 0.0f); }
  public static Offset<GameConfigs.PetsRowData> EndPetsRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.PetsRowData>(o);
  }
};

public struct Pets : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Pets GetRootAsPets(ByteBuffer _bb) { return GetRootAsPets(_bb, new Pets()); }
  public static Pets GetRootAsPets(ByteBuffer _bb, Pets obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Pets __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.PetsRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.PetsRowData?)(new GameConfigs.PetsRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Pets> CreatePets(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Pets.AddDatalist(builder, datalistOffset);
    return Pets.EndPets(builder);
  }

  public static void StartPets(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.PetsRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.PetsRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Pets> EndPets(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Pets>(o);
  }
  public static void FinishPetsBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Pets> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedPetsBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Pets> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
