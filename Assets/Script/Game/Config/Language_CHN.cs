// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Language_CHNRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Language_CHNRowData GetRootAsLanguage_CHNRowData(ByteBuffer _bb) { return GetRootAsLanguage_CHNRowData(_bb, new Language_CHNRowData()); }
  public static Language_CHNRowData GetRootAsLanguage_CHNRowData(ByteBuffer _bb, Language_CHNRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Language_CHNRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string StringId { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetStringIdBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetStringIdBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetStringIdArray() { return __p.__vector_as_array<byte>(4); }
  public string Value { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetValueBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetValueBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetValueArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<GameConfigs.Language_CHNRowData> CreateLanguage_CHNRowData(FlatBufferBuilder builder,
      StringOffset StringIdOffset = default(StringOffset),
      StringOffset ValueOffset = default(StringOffset)) {
    builder.StartTable(2);
    Language_CHNRowData.AddValue(builder, ValueOffset);
    Language_CHNRowData.AddStringId(builder, StringIdOffset);
    return Language_CHNRowData.EndLanguage_CHNRowData(builder);
  }

  public static void StartLanguage_CHNRowData(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddStringId(FlatBufferBuilder builder, StringOffset StringIdOffset) { builder.AddOffset(0, StringIdOffset.Value, 0); }
  public static void AddValue(FlatBufferBuilder builder, StringOffset ValueOffset) { builder.AddOffset(1, ValueOffset.Value, 0); }
  public static Offset<GameConfigs.Language_CHNRowData> EndLanguage_CHNRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Language_CHNRowData>(o);
  }
};

public struct Language_CHN : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Language_CHN GetRootAsLanguage_CHN(ByteBuffer _bb) { return GetRootAsLanguage_CHN(_bb, new Language_CHN()); }
  public static Language_CHN GetRootAsLanguage_CHN(ByteBuffer _bb, Language_CHN obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Language_CHN __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.Language_CHNRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.Language_CHNRowData?)(new GameConfigs.Language_CHNRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Language_CHN> CreateLanguage_CHN(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Language_CHN.AddDatalist(builder, datalistOffset);
    return Language_CHN.EndLanguage_CHN(builder);
  }

  public static void StartLanguage_CHN(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.Language_CHNRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.Language_CHNRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Language_CHN> EndLanguage_CHN(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Language_CHN>(o);
  }
  public static void FinishLanguage_CHNBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Language_CHN> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedLanguage_CHNBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Language_CHN> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
