// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct roleRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static roleRowData GetRootAsroleRowData(ByteBuffer _bb) { return GetRootAsroleRowData(_bb, new roleRowData()); }
  public static roleRowData GetRootAsroleRowData(ByteBuffer _bb, roleRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public roleRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public string Part { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPartBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetPartBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetPartArray() { return __p.__vector_as_array<byte>(8); }
  public string Ai { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetAiBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetAiBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetAiArray() { return __p.__vector_as_array<byte>(10); }
  public string FriendAI { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetFriendAIBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetFriendAIBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetFriendAIArray() { return __p.__vector_as_array<byte>(12); }
  public float RoleScale(int j) { int o = __p.__offset(14); return o != 0 ? __p.bb.GetFloat(__p.__vector(o) + j * 4) : (float)0; }
  public int RoleScaleLength { get { int o = __p.__offset(14); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<float> GetRoleScaleBytes() { return __p.__vector_as_span<float>(14, 4); }
#else
  public ArraySegment<byte>? GetRoleScaleBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public float[] GetRoleScaleArray() { return __p.__vector_as_array<float>(14); }

  public static Offset<GameConfigs.roleRowData> CreateroleRowData(FlatBufferBuilder builder,
      int ID = 0,
      StringOffset NameOffset = default(StringOffset),
      StringOffset partOffset = default(StringOffset),
      StringOffset aiOffset = default(StringOffset),
      StringOffset friendAIOffset = default(StringOffset),
      VectorOffset RoleScaleOffset = default(VectorOffset)) {
    builder.StartTable(6);
    roleRowData.AddRoleScale(builder, RoleScaleOffset);
    roleRowData.AddFriendAI(builder, friendAIOffset);
    roleRowData.AddAi(builder, aiOffset);
    roleRowData.AddPart(builder, partOffset);
    roleRowData.AddName(builder, NameOffset);
    roleRowData.AddID(builder, ID);
    return roleRowData.EndroleRowData(builder);
  }

  public static void StartroleRowData(FlatBufferBuilder builder) { builder.StartTable(6); }
  public static void AddID(FlatBufferBuilder builder, int ID) { builder.AddInt(0, ID, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset NameOffset) { builder.AddOffset(1, NameOffset.Value, 0); }
  public static void AddPart(FlatBufferBuilder builder, StringOffset partOffset) { builder.AddOffset(2, partOffset.Value, 0); }
  public static void AddAi(FlatBufferBuilder builder, StringOffset aiOffset) { builder.AddOffset(3, aiOffset.Value, 0); }
  public static void AddFriendAI(FlatBufferBuilder builder, StringOffset friendAIOffset) { builder.AddOffset(4, friendAIOffset.Value, 0); }
  public static void AddRoleScale(FlatBufferBuilder builder, VectorOffset RoleScaleOffset) { builder.AddOffset(5, RoleScaleOffset.Value, 0); }
  public static VectorOffset CreateRoleScaleVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateRoleScaleVectorBlock(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartRoleScaleVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.roleRowData> EndroleRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.roleRowData>(o);
  }
};

public struct role : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static role GetRootAsrole(ByteBuffer _bb) { return GetRootAsrole(_bb, new role()); }
  public static role GetRootAsrole(ByteBuffer _bb, role obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public role __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.roleRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.roleRowData?)(new GameConfigs.roleRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.role> Createrole(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    role.AddDatalist(builder, datalistOffset);
    return role.Endrole(builder);
  }

  public static void Startrole(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.roleRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.roleRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.role> Endrole(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.role>(o);
  }
  public static void FinishroleBuffer(FlatBufferBuilder builder, Offset<GameConfigs.role> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedroleBuffer(FlatBufferBuilder builder, Offset<GameConfigs.role> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
