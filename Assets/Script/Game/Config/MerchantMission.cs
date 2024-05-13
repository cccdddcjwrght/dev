// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct MerchantMissionRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static MerchantMissionRowData GetRootAsMerchantMissionRowData(ByteBuffer _bb) { return GetRootAsMerchantMissionRowData(_bb, new MerchantMissionRowData()); }
  public static MerchantMissionRowData GetRootAsMerchantMissionRowData(ByteBuffer _bb, MerchantMissionRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public MerchantMissionRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int TypeValue { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string TaskDes { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTaskDesBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetTaskDesBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetTaskDesArray() { return __p.__vector_as_array<byte>(8); }
  public string TaskIcon { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTaskIconBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetTaskIconBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetTaskIconArray() { return __p.__vector_as_array<byte>(10); }
  public int TaskType { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int TaskValue { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int TaskReward(int j) { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int TaskRewardLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetTaskRewardBytes() { return __p.__vector_as_span<int>(16, 4); }
#else
  public ArraySegment<byte>? GetTaskRewardBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public int[] GetTaskRewardArray() { return __p.__vector_as_array<int>(16); }

  public static Offset<GameConfigs.MerchantMissionRowData> CreateMerchantMissionRowData(FlatBufferBuilder builder,
      int Id = 0,
      int TypeValue = 0,
      StringOffset TaskDesOffset = default(StringOffset),
      StringOffset TaskIconOffset = default(StringOffset),
      int TaskType = 0,
      int TaskValue = 0,
      VectorOffset TaskRewardOffset = default(VectorOffset)) {
    builder.StartTable(7);
    MerchantMissionRowData.AddTaskReward(builder, TaskRewardOffset);
    MerchantMissionRowData.AddTaskValue(builder, TaskValue);
    MerchantMissionRowData.AddTaskType(builder, TaskType);
    MerchantMissionRowData.AddTaskIcon(builder, TaskIconOffset);
    MerchantMissionRowData.AddTaskDes(builder, TaskDesOffset);
    MerchantMissionRowData.AddTypeValue(builder, TypeValue);
    MerchantMissionRowData.AddId(builder, Id);
    return MerchantMissionRowData.EndMerchantMissionRowData(builder);
  }

  public static void StartMerchantMissionRowData(FlatBufferBuilder builder) { builder.StartTable(7); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddTypeValue(FlatBufferBuilder builder, int TypeValue) { builder.AddInt(1, TypeValue, 0); }
  public static void AddTaskDes(FlatBufferBuilder builder, StringOffset TaskDesOffset) { builder.AddOffset(2, TaskDesOffset.Value, 0); }
  public static void AddTaskIcon(FlatBufferBuilder builder, StringOffset TaskIconOffset) { builder.AddOffset(3, TaskIconOffset.Value, 0); }
  public static void AddTaskType(FlatBufferBuilder builder, int TaskType) { builder.AddInt(4, TaskType, 0); }
  public static void AddTaskValue(FlatBufferBuilder builder, int TaskValue) { builder.AddInt(5, TaskValue, 0); }
  public static void AddTaskReward(FlatBufferBuilder builder, VectorOffset TaskRewardOffset) { builder.AddOffset(6, TaskRewardOffset.Value, 0); }
  public static VectorOffset CreateTaskRewardVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateTaskRewardVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartTaskRewardVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.MerchantMissionRowData> EndMerchantMissionRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.MerchantMissionRowData>(o);
  }
};

public struct MerchantMission : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static MerchantMission GetRootAsMerchantMission(ByteBuffer _bb) { return GetRootAsMerchantMission(_bb, new MerchantMission()); }
  public static MerchantMission GetRootAsMerchantMission(ByteBuffer _bb, MerchantMission obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public MerchantMission __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.MerchantMissionRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.MerchantMissionRowData?)(new GameConfigs.MerchantMissionRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.MerchantMission> CreateMerchantMission(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    MerchantMission.AddDatalist(builder, datalistOffset);
    return MerchantMission.EndMerchantMission(builder);
  }

  public static void StartMerchantMission(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.MerchantMissionRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.MerchantMissionRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.MerchantMission> EndMerchantMission(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.MerchantMission>(o);
  }
  public static void FinishMerchantMissionBuffer(FlatBufferBuilder builder, Offset<GameConfigs.MerchantMission> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedMerchantMissionBuffer(FlatBufferBuilder builder, Offset<GameConfigs.MerchantMission> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
