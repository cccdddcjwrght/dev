// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct RoomAreaRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static RoomAreaRowData GetRootAsRoomAreaRowData(ByteBuffer _bb) { return GetRootAsRoomAreaRowData(_bb, new RoomAreaRowData()); }
  public static RoomAreaRowData GetRootAsRoomAreaRowData(ByteBuffer _bb, RoomAreaRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RoomAreaRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Scene { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Type { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int TypeValue(int j) { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int TypeValueLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetTypeValueBytes() { return __p.__vector_as_span<int>(10, 4); }
#else
  public ArraySegment<byte>? GetTypeValueBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public int[] GetTypeValueArray() { return __p.__vector_as_array<int>(10); }
  public string TypeIcon { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTypeIconBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetTypeIconBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetTypeIconArray() { return __p.__vector_as_array<byte>(12); }
  public float Cost(int j) { int o = __p.__offset(14); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int CostLength { get { int o = __p.__offset(14); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetCostBytes() { return __p.__vector_as_span<float>(14, 4); }
#else
  public ArraySegment<byte>? GetCostBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public float[] GetCostArray() { return __p.__vector_as_array<float>(14); }
  public string LockRes { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetLockResBytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetLockResBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetLockResArray() { return __p.__vector_as_array<byte>(16); }
  public string UnlockRes { get { int o = __p.__offset(18); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetUnlockResBytes() { return __p.__vector_as_span<byte>(18, 1); }
#else
  public ArraySegment<byte>? GetUnlockResBytes() { return __p.__vector_as_arraysegment(18); }
#endif
  public byte[] GetUnlockResArray() { return __p.__vector_as_array<byte>(18); }
  public string Effects { get { int o = __p.__offset(20); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetEffectsBytes() { return __p.__vector_as_span<byte>(20, 1); }
#else
  public ArraySegment<byte>? GetEffectsBytes() { return __p.__vector_as_arraysegment(20); }
#endif
  public byte[] GetEffectsArray() { return __p.__vector_as_array<byte>(20); }
  public int ChefNum { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int WaiterNum { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int CustomerNum { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string CustomerBorn { get { int o = __p.__offset(28); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetCustomerBornBytes() { return __p.__vector_as_span<byte>(28, 1); }
#else
  public ArraySegment<byte>? GetCustomerBornBytes() { return __p.__vector_as_arraysegment(28); }
#endif
  public byte[] GetCustomerBornArray() { return __p.__vector_as_array<byte>(28); }
  public int AreaPos(int j) { int o = __p.__offset(30); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int AreaPosLength { get { int o = __p.__offset(30); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetAreaPosBytes() { return __p.__vector_as_span<int>(30, 4); }
#else
  public ArraySegment<byte>? GetAreaPosBytes() { return __p.__vector_as_arraysegment(30); }
#endif
  public int[] GetAreaPosArray() { return __p.__vector_as_array<int>(30); }
  public int NextArea { get { int o = __p.__offset(32); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int OrderMachineID(int j) { int o = __p.__offset(34); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int OrderMachineIDLength { get { int o = __p.__offset(34); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetOrderMachineIDBytes() { return __p.__vector_as_span<int>(34, 4); }
#else
  public ArraySegment<byte>? GetOrderMachineIDBytes() { return __p.__vector_as_arraysegment(34); }
#endif
  public int[] GetOrderMachineIDArray() { return __p.__vector_as_array<int>(34); }
  public int Reward1(int j) { int o = __p.__offset(36); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Reward1Length { get { int o = __p.__offset(36); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetReward1Bytes() { return __p.__vector_as_span<int>(36, 4); }
#else
  public ArraySegment<byte>? GetReward1Bytes() { return __p.__vector_as_arraysegment(36); }
#endif
  public int[] GetReward1Array() { return __p.__vector_as_array<int>(36); }
  public int Reward2(int j) { int o = __p.__offset(38); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Reward2Length { get { int o = __p.__offset(38); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetReward2Bytes() { return __p.__vector_as_span<int>(38, 4); }
#else
  public ArraySegment<byte>? GetReward2Bytes() { return __p.__vector_as_arraysegment(38); }
#endif
  public int[] GetReward2Array() { return __p.__vector_as_array<int>(38); }
  public int Reward3(int j) { int o = __p.__offset(40); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Reward3Length { get { int o = __p.__offset(40); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetReward3Bytes() { return __p.__vector_as_span<int>(40, 4); }
#else
  public ArraySegment<byte>? GetReward3Bytes() { return __p.__vector_as_arraysegment(40); }
#endif
  public int[] GetReward3Array() { return __p.__vector_as_array<int>(40); }

  public static Offset<GameConfigs.RoomAreaRowData> CreateRoomAreaRowData(FlatBufferBuilder builder,
      int ID = 0,
      int Scene = 0,
      int Type = 0,
      VectorOffset TypeValueOffset = default(VectorOffset),
      StringOffset TypeIconOffset = default(StringOffset),
      VectorOffset CostOffset = default(VectorOffset),
      StringOffset LockResOffset = default(StringOffset),
      StringOffset UnlockResOffset = default(StringOffset),
      StringOffset EffectsOffset = default(StringOffset),
      int ChefNum = 0,
      int WaiterNum = 0,
      int CustomerNum = 0,
      StringOffset CustomerBornOffset = default(StringOffset),
      VectorOffset AreaPosOffset = default(VectorOffset),
      int NextArea = 0,
      VectorOffset OrderMachineIDOffset = default(VectorOffset),
      VectorOffset Reward1Offset = default(VectorOffset),
      VectorOffset Reward2Offset = default(VectorOffset),
      VectorOffset Reward3Offset = default(VectorOffset)) {
    builder.StartTable(19);
    RoomAreaRowData.AddReward3(builder, Reward3Offset);
    RoomAreaRowData.AddReward2(builder, Reward2Offset);
    RoomAreaRowData.AddReward1(builder, Reward1Offset);
    RoomAreaRowData.AddOrderMachineID(builder, OrderMachineIDOffset);
    RoomAreaRowData.AddNextArea(builder, NextArea);
    RoomAreaRowData.AddAreaPos(builder, AreaPosOffset);
    RoomAreaRowData.AddCustomerBorn(builder, CustomerBornOffset);
    RoomAreaRowData.AddCustomerNum(builder, CustomerNum);
    RoomAreaRowData.AddWaiterNum(builder, WaiterNum);
    RoomAreaRowData.AddChefNum(builder, ChefNum);
    RoomAreaRowData.AddEffects(builder, EffectsOffset);
    RoomAreaRowData.AddUnlockRes(builder, UnlockResOffset);
    RoomAreaRowData.AddLockRes(builder, LockResOffset);
    RoomAreaRowData.AddCost(builder, CostOffset);
    RoomAreaRowData.AddTypeIcon(builder, TypeIconOffset);
    RoomAreaRowData.AddTypeValue(builder, TypeValueOffset);
    RoomAreaRowData.AddType(builder, Type);
    RoomAreaRowData.AddScene(builder, Scene);
    RoomAreaRowData.AddID(builder, ID);
    return RoomAreaRowData.EndRoomAreaRowData(builder);
  }

  public static void StartRoomAreaRowData(FlatBufferBuilder builder) { builder.StartTable(19); }
  public static void AddID(FlatBufferBuilder builder, int ID) { builder.AddInt(0, ID, 0); }
  public static void AddScene(FlatBufferBuilder builder, int Scene) { builder.AddInt(1, Scene, 0); }
  public static void AddType(FlatBufferBuilder builder, int Type) { builder.AddInt(2, Type, 0); }
  public static void AddTypeValue(FlatBufferBuilder builder, VectorOffset TypeValueOffset) { builder.AddOffset(3, TypeValueOffset.Value, 0); }
  public static VectorOffset CreateTypeValueVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateTypeValueVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartTypeValueVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddTypeIcon(FlatBufferBuilder builder, StringOffset TypeIconOffset) { builder.AddOffset(4, TypeIconOffset.Value, 0); }
  public static void AddCost(FlatBufferBuilder builder, VectorOffset CostOffset) { builder.AddOffset(5, CostOffset.Value, 0); }
  public static VectorOffset CreateCostVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateCostVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartCostVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddLockRes(FlatBufferBuilder builder, StringOffset LockResOffset) { builder.AddOffset(6, LockResOffset.Value, 0); }
  public static void AddUnlockRes(FlatBufferBuilder builder, StringOffset UnlockResOffset) { builder.AddOffset(7, UnlockResOffset.Value, 0); }
  public static void AddEffects(FlatBufferBuilder builder, StringOffset EffectsOffset) { builder.AddOffset(8, EffectsOffset.Value, 0); }
  public static void AddChefNum(FlatBufferBuilder builder, int ChefNum) { builder.AddInt(9, ChefNum, 0); }
  public static void AddWaiterNum(FlatBufferBuilder builder, int WaiterNum) { builder.AddInt(10, WaiterNum, 0); }
  public static void AddCustomerNum(FlatBufferBuilder builder, int CustomerNum) { builder.AddInt(11, CustomerNum, 0); }
  public static void AddCustomerBorn(FlatBufferBuilder builder, StringOffset CustomerBornOffset) { builder.AddOffset(12, CustomerBornOffset.Value, 0); }
  public static void AddAreaPos(FlatBufferBuilder builder, VectorOffset AreaPosOffset) { builder.AddOffset(13, AreaPosOffset.Value, 0); }
  public static VectorOffset CreateAreaPosVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateAreaPosVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartAreaPosVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddNextArea(FlatBufferBuilder builder, int NextArea) { builder.AddInt(14, NextArea, 0); }
  public static void AddOrderMachineID(FlatBufferBuilder builder, VectorOffset OrderMachineIDOffset) { builder.AddOffset(15, OrderMachineIDOffset.Value, 0); }
  public static VectorOffset CreateOrderMachineIDVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateOrderMachineIDVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartOrderMachineIDVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddReward1(FlatBufferBuilder builder, VectorOffset Reward1Offset) { builder.AddOffset(16, Reward1Offset.Value, 0); }
  public static VectorOffset CreateReward1Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateReward1VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartReward1Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddReward2(FlatBufferBuilder builder, VectorOffset Reward2Offset) { builder.AddOffset(17, Reward2Offset.Value, 0); }
  public static VectorOffset CreateReward2Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateReward2VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartReward2Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddReward3(FlatBufferBuilder builder, VectorOffset Reward3Offset) { builder.AddOffset(18, Reward3Offset.Value, 0); }
  public static VectorOffset CreateReward3Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateReward3VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartReward3Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.RoomAreaRowData> EndRoomAreaRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.RoomAreaRowData>(o);
  }
};

public struct RoomArea : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static RoomArea GetRootAsRoomArea(ByteBuffer _bb) { return GetRootAsRoomArea(_bb, new RoomArea()); }
  public static RoomArea GetRootAsRoomArea(ByteBuffer _bb, RoomArea obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RoomArea __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.RoomAreaRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.RoomAreaRowData?)(new GameConfigs.RoomAreaRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.RoomArea> CreateRoomArea(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    RoomArea.AddDatalist(builder, datalistOffset);
    return RoomArea.EndRoomArea(builder);
  }

  public static void StartRoomArea(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.RoomAreaRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.RoomAreaRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.RoomArea> EndRoomArea(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.RoomArea>(o);
  }
  public static void FinishRoomAreaBuffer(FlatBufferBuilder builder, Offset<GameConfigs.RoomArea> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedRoomAreaBuffer(FlatBufferBuilder builder, Offset<GameConfigs.RoomArea> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
