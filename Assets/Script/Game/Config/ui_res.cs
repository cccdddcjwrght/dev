// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct ui_resRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ui_resRowData GetRootAsui_resRowData(ByteBuffer _bb) { return GetRootAsui_resRowData(_bb, new ui_resRowData()); }
  public static ui_resRowData GetRootAsui_resRowData(ByteBuffer _bb, ui_resRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ui_resRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public string ComName { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetComNameBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetComNameBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetComNameArray() { return __p.__vector_as_array<byte>(8); }
  public string PackageName { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPackageNameBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetPackageNameBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetPackageNameArray() { return __p.__vector_as_array<byte>(10); }
  public int Type { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Order { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Group(int j) { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int GroupLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetGroupBytes() { return __p.__vector_as_span<int>(16, 4); }
#else
  public ArraySegment<byte>? GetGroupBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public int[] GetGroupArray() { return __p.__vector_as_array<int>(16); }
  public int AniShow { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int AniHide { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Mask { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int DisableAudio { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Priority { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Cache { get { int o = __p.__offset(28); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<GameConfigs.ui_resRowData> Createui_resRowData(FlatBufferBuilder builder,
      int id = 0,
      StringOffset nameOffset = default(StringOffset),
      StringOffset ComNameOffset = default(StringOffset),
      StringOffset PackageNameOffset = default(StringOffset),
      int type = 0,
      int order = 0,
      VectorOffset groupOffset = default(VectorOffset),
      int AniShow = 0,
      int AniHide = 0,
      int mask = 0,
      int DisableAudio = 0,
      int Priority = 0,
      int cache = 0) {
    builder.StartTable(13);
    ui_resRowData.AddCache(builder, cache);
    ui_resRowData.AddPriority(builder, Priority);
    ui_resRowData.AddDisableAudio(builder, DisableAudio);
    ui_resRowData.AddMask(builder, mask);
    ui_resRowData.AddAniHide(builder, AniHide);
    ui_resRowData.AddAniShow(builder, AniShow);
    ui_resRowData.AddGroup(builder, groupOffset);
    ui_resRowData.AddOrder(builder, order);
    ui_resRowData.AddType(builder, type);
    ui_resRowData.AddPackageName(builder, PackageNameOffset);
    ui_resRowData.AddComName(builder, ComNameOffset);
    ui_resRowData.AddName(builder, nameOffset);
    ui_resRowData.AddId(builder, id);
    return ui_resRowData.Endui_resRowData(builder);
  }

  public static void Startui_resRowData(FlatBufferBuilder builder) { builder.StartTable(13); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(1, nameOffset.Value, 0); }
  public static void AddComName(FlatBufferBuilder builder, StringOffset ComNameOffset) { builder.AddOffset(2, ComNameOffset.Value, 0); }
  public static void AddPackageName(FlatBufferBuilder builder, StringOffset PackageNameOffset) { builder.AddOffset(3, PackageNameOffset.Value, 0); }
  public static void AddType(FlatBufferBuilder builder, int type) { builder.AddInt(4, type, 0); }
  public static void AddOrder(FlatBufferBuilder builder, int order) { builder.AddInt(5, order, 0); }
  public static void AddGroup(FlatBufferBuilder builder, VectorOffset groupOffset) { builder.AddOffset(6, groupOffset.Value, 0); }
  public static VectorOffset CreateGroupVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateGroupVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartGroupVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddAniShow(FlatBufferBuilder builder, int AniShow) { builder.AddInt(7, AniShow, 0); }
  public static void AddAniHide(FlatBufferBuilder builder, int AniHide) { builder.AddInt(8, AniHide, 0); }
  public static void AddMask(FlatBufferBuilder builder, int mask) { builder.AddInt(9, mask, 0); }
  public static void AddDisableAudio(FlatBufferBuilder builder, int DisableAudio) { builder.AddInt(10, DisableAudio, 0); }
  public static void AddPriority(FlatBufferBuilder builder, int Priority) { builder.AddInt(11, Priority, 0); }
  public static void AddCache(FlatBufferBuilder builder, int cache) { builder.AddInt(12, cache, 0); }
  public static Offset<GameConfigs.ui_resRowData> Endui_resRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.ui_resRowData>(o);
  }
};

public struct ui_res : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ui_res GetRootAsui_res(ByteBuffer _bb) { return GetRootAsui_res(_bb, new ui_res()); }
  public static ui_res GetRootAsui_res(ByteBuffer _bb, ui_res obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ui_res __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.ui_resRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.ui_resRowData?)(new GameConfigs.ui_resRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.ui_res> Createui_res(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    ui_res.AddDatalist(builder, datalistOffset);
    return ui_res.Endui_res(builder);
  }

  public static void Startui_res(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.ui_resRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.ui_resRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.ui_res> Endui_res(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.ui_res>(o);
  }
  public static void Finishui_resBuffer(FlatBufferBuilder builder, Offset<GameConfigs.ui_res> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedui_resBuffer(FlatBufferBuilder builder, Offset<GameConfigs.ui_res> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
