// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct PetBuffConfigRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static PetBuffConfigRowData GetRootAsPetBuffConfigRowData(ByteBuffer _bb) { return GetRootAsPetBuffConfigRowData(_bb, new PetBuffConfigRowData()); }
  public static PetBuffConfigRowData GetRootAsPetBuffConfigRowData(ByteBuffer _bb, PetBuffConfigRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public PetBuffConfigRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Buff { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Default { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Most { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Range(int j) { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int RangeLength { get { int o = __p.__offset(12); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetRangeBytes() { return __p.__vector_as_span<int>(12, 4); }
#else
  public ArraySegment<byte>? GetRangeBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public int[] GetRangeArray() { return __p.__vector_as_array<int>(12); }

  public static Offset<GameConfigs.PetBuffConfigRowData> CreatePetBuffConfigRowData(FlatBufferBuilder builder,
      int Id = 0,
      int Buff = 0,
      int Default = 0,
      int Most = 0,
      VectorOffset RangeOffset = default(VectorOffset)) {
    builder.StartTable(5);
    PetBuffConfigRowData.AddRange(builder, RangeOffset);
    PetBuffConfigRowData.AddMost(builder, Most);
    PetBuffConfigRowData.AddDefault(builder, Default);
    PetBuffConfigRowData.AddBuff(builder, Buff);
    PetBuffConfigRowData.AddId(builder, Id);
    return PetBuffConfigRowData.EndPetBuffConfigRowData(builder);
  }

  public static void StartPetBuffConfigRowData(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddBuff(FlatBufferBuilder builder, int Buff) { builder.AddInt(1, Buff, 0); }
  public static void AddDefault(FlatBufferBuilder builder, int Default) { builder.AddInt(2, Default, 0); }
  public static void AddMost(FlatBufferBuilder builder, int Most) { builder.AddInt(3, Most, 0); }
  public static void AddRange(FlatBufferBuilder builder, VectorOffset RangeOffset) { builder.AddOffset(4, RangeOffset.Value, 0); }
  public static VectorOffset CreateRangeVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateRangeVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartRangeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.PetBuffConfigRowData> EndPetBuffConfigRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.PetBuffConfigRowData>(o);
  }
};

public struct PetBuffConfig : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static PetBuffConfig GetRootAsPetBuffConfig(ByteBuffer _bb) { return GetRootAsPetBuffConfig(_bb, new PetBuffConfig()); }
  public static PetBuffConfig GetRootAsPetBuffConfig(ByteBuffer _bb, PetBuffConfig obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public PetBuffConfig __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.PetBuffConfigRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.PetBuffConfigRowData?)(new GameConfigs.PetBuffConfigRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.PetBuffConfig> CreatePetBuffConfig(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    PetBuffConfig.AddDatalist(builder, datalistOffset);
    return PetBuffConfig.EndPetBuffConfig(builder);
  }

  public static void StartPetBuffConfig(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.PetBuffConfigRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.PetBuffConfigRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.PetBuffConfig> EndPetBuffConfig(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.PetBuffConfig>(o);
  }
  public static void FinishPetBuffConfigBuffer(FlatBufferBuilder builder, Offset<GameConfigs.PetBuffConfig> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedPetBuffConfigBuffer(FlatBufferBuilder builder, Offset<GameConfigs.PetBuffConfig> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
