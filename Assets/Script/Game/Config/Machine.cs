// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct MachineRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static MachineRowData GetRootAsMachineRowData(ByteBuffer _bb) { return GetRootAsMachineRowData(_bb, new MachineRowData()); }
  public static MachineRowData GetRootAsMachineRowData(ByteBuffer _bb, MachineRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public MachineRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string MachineName { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMachineNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetMachineNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetMachineNameArray() { return __p.__vector_as_array<byte>(6); }
  public int MachineLevelMax { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float UnlockPrice(int j) { int o = __p.__offset(10); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int UnlockPriceLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetUnlockPriceBytes() { return __p.__vector_as_span<float>(10, 4); }
#else
  public ArraySegment<byte>? GetUnlockPriceBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public float[] GetUnlockPriceArray() { return __p.__vector_as_array<float>(10); }
  public float UpgradeRatio { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float ShopPrice(int j) { int o = __p.__offset(14); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int ShopPriceLength { get { int o = __p.__offset(14); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetShopPriceBytes() { return __p.__vector_as_span<float>(14, 4); }
#else
  public ArraySegment<byte>? GetShopPriceBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public float[] GetShopPriceArray() { return __p.__vector_as_array<float>(14); }
  public int Time { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float Efficiency { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public int NumMax { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ItemId(int j) { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int ItemIdLength { get { int o = __p.__offset(22); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetItemIdBytes() { return __p.__vector_as_span<int>(22, 4); }
#else
  public ArraySegment<byte>? GetItemIdBytes() { return __p.__vector_as_arraysegment(22); }
#endif
  public int[] GetItemIdArray() { return __p.__vector_as_array<int>(22); }
  public string MachineModel { get { int o = __p.__offset(24); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMachineModelBytes() { return __p.__vector_as_span<byte>(24, 1); }
#else
  public ArraySegment<byte>? GetMachineModelBytes() { return __p.__vector_as_arraysegment(24); }
#endif
  public byte[] GetMachineModelArray() { return __p.__vector_as_array<byte>(24); }

  public static Offset<GameConfigs.MachineRowData> CreateMachineRowData(FlatBufferBuilder builder,
      int Id = 0,
      StringOffset MachineNameOffset = default(StringOffset),
      int MachineLevelMax = 0,
      VectorOffset UnlockPriceOffset = default(VectorOffset),
      float UpgradeRatio = 0.0f,
      VectorOffset ShopPriceOffset = default(VectorOffset),
      int Time = 0,
      float Efficiency = 0.0f,
      int NumMax = 0,
      VectorOffset ItemIdOffset = default(VectorOffset),
      StringOffset MachineModelOffset = default(StringOffset)) {
    builder.StartTable(11);
    MachineRowData.AddMachineModel(builder, MachineModelOffset);
    MachineRowData.AddItemId(builder, ItemIdOffset);
    MachineRowData.AddNumMax(builder, NumMax);
    MachineRowData.AddEfficiency(builder, Efficiency);
    MachineRowData.AddTime(builder, Time);
    MachineRowData.AddShopPrice(builder, ShopPriceOffset);
    MachineRowData.AddUpgradeRatio(builder, UpgradeRatio);
    MachineRowData.AddUnlockPrice(builder, UnlockPriceOffset);
    MachineRowData.AddMachineLevelMax(builder, MachineLevelMax);
    MachineRowData.AddMachineName(builder, MachineNameOffset);
    MachineRowData.AddId(builder, Id);
    return MachineRowData.EndMachineRowData(builder);
  }

  public static void StartMachineRowData(FlatBufferBuilder builder) { builder.StartTable(11); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddMachineName(FlatBufferBuilder builder, StringOffset MachineNameOffset) { builder.AddOffset(1, MachineNameOffset.Value, 0); }
  public static void AddMachineLevelMax(FlatBufferBuilder builder, int MachineLevelMax) { builder.AddInt(2, MachineLevelMax, 0); }
  public static void AddUnlockPrice(FlatBufferBuilder builder, VectorOffset UnlockPriceOffset) { builder.AddOffset(3, UnlockPriceOffset.Value, 0); }
  public static VectorOffset CreateUnlockPriceVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateUnlockPriceVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartUnlockPriceVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddUpgradeRatio(FlatBufferBuilder builder, float UpgradeRatio) { builder.AddFloat(4, UpgradeRatio, 0.0f); }
  public static void AddShopPrice(FlatBufferBuilder builder, VectorOffset ShopPriceOffset) { builder.AddOffset(5, ShopPriceOffset.Value, 0); }
  public static VectorOffset CreateShopPriceVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateShopPriceVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartShopPriceVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddTime(FlatBufferBuilder builder, int Time) { builder.AddInt(6, Time, 0); }
  public static void AddEfficiency(FlatBufferBuilder builder, float Efficiency) { builder.AddFloat(7, Efficiency, 0.0f); }
  public static void AddNumMax(FlatBufferBuilder builder, int NumMax) { builder.AddInt(8, NumMax, 0); }
  public static void AddItemId(FlatBufferBuilder builder, VectorOffset ItemIdOffset) { builder.AddOffset(9, ItemIdOffset.Value, 0); }
  public static VectorOffset CreateItemIdVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateItemIdVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartItemIdVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddMachineModel(FlatBufferBuilder builder, StringOffset MachineModelOffset) { builder.AddOffset(10, MachineModelOffset.Value, 0); }
  public static Offset<GameConfigs.MachineRowData> EndMachineRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.MachineRowData>(o);
  }
};

public struct Machine : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Machine GetRootAsMachine(ByteBuffer _bb) { return GetRootAsMachine(_bb, new Machine()); }
  public static Machine GetRootAsMachine(ByteBuffer _bb, Machine obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Machine __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.MachineRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.MachineRowData?)(new GameConfigs.MachineRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Machine> CreateMachine(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Machine.AddDatalist(builder, datalistOffset);
    return Machine.EndMachine(builder);
  }

  public static void StartMachine(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.MachineRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.MachineRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Machine> EndMachine(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Machine>(o);
  }
  public static void FinishMachineBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Machine> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedMachineBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Machine> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
