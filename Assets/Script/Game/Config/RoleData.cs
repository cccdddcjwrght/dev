// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct RoleDataRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static RoleDataRowData GetRootAsRoleDataRowData(ByteBuffer _bb) { return GetRootAsRoleDataRowData(_bb, new RoleDataRowData()); }
  public static RoleDataRowData GetRootAsRoleDataRowData(ByteBuffer _bb, RoleDataRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RoleDataRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public int Type { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Model { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float MoveSpeed { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float OrderSpeed { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float Efficiency { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public int Instant { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Perfect { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int PerfectRatio { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Tip { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int TipRatio { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Price { get { int o = __p.__offset(28); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int LikeRatio { get { int o = __p.__offset(30); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int LikeNum { get { int o = __p.__offset(32); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<GameConfigs.RoleDataRowData> CreateRoleDataRowData(FlatBufferBuilder builder,
      int Id = 0,
      StringOffset NameOffset = default(StringOffset),
      int Type = 0,
      int Model = 0,
      float MoveSpeed = 0.0f,
      float OrderSpeed = 0.0f,
      float Efficiency = 0.0f,
      int Instant = 0,
      int Perfect = 0,
      int PerfectRatio = 0,
      int Tip = 0,
      int TipRatio = 0,
      int Price = 0,
      int LikeRatio = 0,
      int LikeNum = 0) {
    builder.StartTable(15);
    RoleDataRowData.AddLikeNum(builder, LikeNum);
    RoleDataRowData.AddLikeRatio(builder, LikeRatio);
    RoleDataRowData.AddPrice(builder, Price);
    RoleDataRowData.AddTipRatio(builder, TipRatio);
    RoleDataRowData.AddTip(builder, Tip);
    RoleDataRowData.AddPerfectRatio(builder, PerfectRatio);
    RoleDataRowData.AddPerfect(builder, Perfect);
    RoleDataRowData.AddInstant(builder, Instant);
    RoleDataRowData.AddEfficiency(builder, Efficiency);
    RoleDataRowData.AddOrderSpeed(builder, OrderSpeed);
    RoleDataRowData.AddMoveSpeed(builder, MoveSpeed);
    RoleDataRowData.AddModel(builder, Model);
    RoleDataRowData.AddType(builder, Type);
    RoleDataRowData.AddName(builder, NameOffset);
    RoleDataRowData.AddId(builder, Id);
    return RoleDataRowData.EndRoleDataRowData(builder);
  }

  public static void StartRoleDataRowData(FlatBufferBuilder builder) { builder.StartTable(15); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset NameOffset) { builder.AddOffset(1, NameOffset.Value, 0); }
  public static void AddType(FlatBufferBuilder builder, int Type) { builder.AddInt(2, Type, 0); }
  public static void AddModel(FlatBufferBuilder builder, int Model) { builder.AddInt(3, Model, 0); }
  public static void AddMoveSpeed(FlatBufferBuilder builder, float MoveSpeed) { builder.AddFloat(4, MoveSpeed, 0.0f); }
  public static void AddOrderSpeed(FlatBufferBuilder builder, float OrderSpeed) { builder.AddFloat(5, OrderSpeed, 0.0f); }
  public static void AddEfficiency(FlatBufferBuilder builder, float Efficiency) { builder.AddFloat(6, Efficiency, 0.0f); }
  public static void AddInstant(FlatBufferBuilder builder, int Instant) { builder.AddInt(7, Instant, 0); }
  public static void AddPerfect(FlatBufferBuilder builder, int Perfect) { builder.AddInt(8, Perfect, 0); }
  public static void AddPerfectRatio(FlatBufferBuilder builder, int PerfectRatio) { builder.AddInt(9, PerfectRatio, 0); }
  public static void AddTip(FlatBufferBuilder builder, int Tip) { builder.AddInt(10, Tip, 0); }
  public static void AddTipRatio(FlatBufferBuilder builder, int TipRatio) { builder.AddInt(11, TipRatio, 0); }
  public static void AddPrice(FlatBufferBuilder builder, int Price) { builder.AddInt(12, Price, 0); }
  public static void AddLikeRatio(FlatBufferBuilder builder, int LikeRatio) { builder.AddInt(13, LikeRatio, 0); }
  public static void AddLikeNum(FlatBufferBuilder builder, int LikeNum) { builder.AddInt(14, LikeNum, 0); }
  public static Offset<GameConfigs.RoleDataRowData> EndRoleDataRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.RoleDataRowData>(o);
  }
};

public struct RoleData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static RoleData GetRootAsRoleData(ByteBuffer _bb) { return GetRootAsRoleData(_bb, new RoleData()); }
  public static RoleData GetRootAsRoleData(ByteBuffer _bb, RoleData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RoleData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.RoleDataRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.RoleDataRowData?)(new GameConfigs.RoleDataRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.RoleData> CreateRoleData(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    RoleData.AddDatalist(builder, datalistOffset);
    return RoleData.EndRoleData(builder);
  }

  public static void StartRoleData(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.RoleDataRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.RoleDataRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.RoleData> EndRoleData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.RoleData>(o);
  }
  public static void FinishRoleDataBuffer(FlatBufferBuilder builder, Offset<GameConfigs.RoleData> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedRoleDataBuffer(FlatBufferBuilder builder, Offset<GameConfigs.RoleData> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
