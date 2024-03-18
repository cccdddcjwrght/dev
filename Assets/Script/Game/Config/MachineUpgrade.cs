// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct MachineUpgradeRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static MachineUpgradeRowData GetRootAsMachineUpgradeRowData(ByteBuffer _bb) { return GetRootAsMachineUpgradeRowData(_bb, new MachineUpgradeRowData()); }
  public static MachineUpgradeRowData GetRootAsMachineUpgradeRowData(ByteBuffer _bb, MachineUpgradeRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public MachineUpgradeRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int LevelId { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MachineStar { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float UpgradePrice(int j) { int o = __p.__offset(8); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int UpgradePriceLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetUpgradePriceBytes() { return __p.__vector_as_span<float>(8, 4); }
#else
  public ArraySegment<byte>? GetUpgradePriceBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public float[] GetUpgradePriceArray() { return __p.__vector_as_array<float>(8); }
  public int ShopPriceRatio { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ShopPriceStarRatio { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int TimeRatio { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Num { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int StarReward(int j) { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int StarRewardLength { get { int o = __p.__offset(18); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetStarRewardBytes() { return __p.__vector_as_span<int>(18, 4); }
#else
  public ArraySegment<byte>? GetStarRewardBytes() { return __p.__vector_as_arraysegment(18); }
#endif
  public int[] GetStarRewardArray() { return __p.__vector_as_array<int>(18); }

  public static Offset<GameConfigs.MachineUpgradeRowData> CreateMachineUpgradeRowData(FlatBufferBuilder builder,
      int LevelId = 0,
      int MachineStar = 0,
      VectorOffset UpgradePriceOffset = default(VectorOffset),
      int ShopPriceRatio = 0,
      int ShopPriceStarRatio = 0,
      int TimeRatio = 0,
      int Num = 0,
      VectorOffset StarRewardOffset = default(VectorOffset)) {
    builder.StartTable(8);
    MachineUpgradeRowData.AddStarReward(builder, StarRewardOffset);
    MachineUpgradeRowData.AddNum(builder, Num);
    MachineUpgradeRowData.AddTimeRatio(builder, TimeRatio);
    MachineUpgradeRowData.AddShopPriceStarRatio(builder, ShopPriceStarRatio);
    MachineUpgradeRowData.AddShopPriceRatio(builder, ShopPriceRatio);
    MachineUpgradeRowData.AddUpgradePrice(builder, UpgradePriceOffset);
    MachineUpgradeRowData.AddMachineStar(builder, MachineStar);
    MachineUpgradeRowData.AddLevelId(builder, LevelId);
    return MachineUpgradeRowData.EndMachineUpgradeRowData(builder);
  }

  public static void StartMachineUpgradeRowData(FlatBufferBuilder builder) { builder.StartTable(8); }
  public static void AddLevelId(FlatBufferBuilder builder, int LevelId) { builder.AddInt(0, LevelId, 0); }
  public static void AddMachineStar(FlatBufferBuilder builder, int MachineStar) { builder.AddInt(1, MachineStar, 0); }
  public static void AddUpgradePrice(FlatBufferBuilder builder, VectorOffset UpgradePriceOffset) { builder.AddOffset(2, UpgradePriceOffset.Value, 0); }
  public static VectorOffset CreateUpgradePriceVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateUpgradePriceVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartUpgradePriceVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddShopPriceRatio(FlatBufferBuilder builder, int ShopPriceRatio) { builder.AddInt(3, ShopPriceRatio, 0); }
  public static void AddShopPriceStarRatio(FlatBufferBuilder builder, int ShopPriceStarRatio) { builder.AddInt(4, ShopPriceStarRatio, 0); }
  public static void AddTimeRatio(FlatBufferBuilder builder, int TimeRatio) { builder.AddInt(5, TimeRatio, 0); }
  public static void AddNum(FlatBufferBuilder builder, int Num) { builder.AddInt(6, Num, 0); }
  public static void AddStarReward(FlatBufferBuilder builder, VectorOffset StarRewardOffset) { builder.AddOffset(7, StarRewardOffset.Value, 0); }
  public static VectorOffset CreateStarRewardVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateStarRewardVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartStarRewardVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.MachineUpgradeRowData> EndMachineUpgradeRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.MachineUpgradeRowData>(o);
  }
};

public struct MachineUpgrade : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static MachineUpgrade GetRootAsMachineUpgrade(ByteBuffer _bb) { return GetRootAsMachineUpgrade(_bb, new MachineUpgrade()); }
  public static MachineUpgrade GetRootAsMachineUpgrade(ByteBuffer _bb, MachineUpgrade obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public MachineUpgrade __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.MachineUpgradeRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.MachineUpgradeRowData?)(new GameConfigs.MachineUpgradeRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.MachineUpgrade> CreateMachineUpgrade(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    MachineUpgrade.AddDatalist(builder, datalistOffset);
    return MachineUpgrade.EndMachineUpgrade(builder);
  }

  public static void StartMachineUpgrade(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.MachineUpgradeRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.MachineUpgradeRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.MachineUpgrade> EndMachineUpgrade(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.MachineUpgrade>(o);
  }
  public static void FinishMachineUpgradeBuffer(FlatBufferBuilder builder, Offset<GameConfigs.MachineUpgrade> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedMachineUpgradeBuffer(FlatBufferBuilder builder, Offset<GameConfigs.MachineUpgrade> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
