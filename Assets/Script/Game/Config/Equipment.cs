// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct EquipmentRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static EquipmentRowData GetRootAsEquipmentRowData(ByteBuffer _bb) { return GetRootAsEquipmentRowData(_bb, new EquipmentRowData()); }
  public static EquipmentRowData GetRootAsEquipmentRowData(ByteBuffer _bb, EquipmentRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public EquipmentRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public string Description { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDescriptionBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetDescriptionBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetDescriptionArray() { return __p.__vector_as_array<byte>(8); }
  public string Icon { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetIconBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetIconArray() { return __p.__vector_as_array<byte>(10); }
  public int Activity { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Group { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Type { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Quality { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Resource { get { int o = __p.__offset(20); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetResourceBytes() { return __p.__vector_as_span<byte>(20, 1); }
#else
  public ArraySegment<byte>? GetResourceBytes() { return __p.__vector_as_arraysegment(20); }
#endif
  public byte[] GetResourceArray() { return __p.__vector_as_array<byte>(20); }
  public int Level { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Part1(int j) { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Part1Length { get { int o = __p.__offset(24); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetPart1Bytes() { return __p.__vector_as_span<int>(24, 4); }
#else
  public ArraySegment<byte>? GetPart1Bytes() { return __p.__vector_as_arraysegment(24); }
#endif
  public int[] GetPart1Array() { return __p.__vector_as_array<int>(24); }
  public int Part2(int j) { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Part2Length { get { int o = __p.__offset(26); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetPart2Bytes() { return __p.__vector_as_span<int>(26, 4); }
#else
  public ArraySegment<byte>? GetPart2Bytes() { return __p.__vector_as_arraysegment(26); }
#endif
  public int[] GetPart2Array() { return __p.__vector_as_array<int>(26); }
  public int Part3(int j) { int o = __p.__offset(28); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Part3Length { get { int o = __p.__offset(28); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetPart3Bytes() { return __p.__vector_as_span<int>(28, 4); }
#else
  public ArraySegment<byte>? GetPart3Bytes() { return __p.__vector_as_arraysegment(28); }
#endif
  public int[] GetPart3Array() { return __p.__vector_as_array<int>(28); }
  public int Part4(int j) { int o = __p.__offset(30); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Part4Length { get { int o = __p.__offset(30); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetPart4Bytes() { return __p.__vector_as_span<int>(30, 4); }
#else
  public ArraySegment<byte>? GetPart4Bytes() { return __p.__vector_as_arraysegment(30); }
#endif
  public int[] GetPart4Array() { return __p.__vector_as_array<int>(30); }
  public int Part5(int j) { int o = __p.__offset(32); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Part5Length { get { int o = __p.__offset(32); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetPart5Bytes() { return __p.__vector_as_span<int>(32, 4); }
#else
  public ArraySegment<byte>? GetPart5Bytes() { return __p.__vector_as_arraysegment(32); }
#endif
  public int[] GetPart5Array() { return __p.__vector_as_array<int>(32); }
  public int Buff1(int j) { int o = __p.__offset(34); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Buff1Length { get { int o = __p.__offset(34); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBuff1Bytes() { return __p.__vector_as_span<int>(34, 4); }
#else
  public ArraySegment<byte>? GetBuff1Bytes() { return __p.__vector_as_arraysegment(34); }
#endif
  public int[] GetBuff1Array() { return __p.__vector_as_array<int>(34); }
  public int Buff2(int j) { int o = __p.__offset(36); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Buff2Length { get { int o = __p.__offset(36); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBuff2Bytes() { return __p.__vector_as_span<int>(36, 4); }
#else
  public ArraySegment<byte>? GetBuff2Bytes() { return __p.__vector_as_arraysegment(36); }
#endif
  public int[] GetBuff2Array() { return __p.__vector_as_array<int>(36); }
  public int Buff3(int j) { int o = __p.__offset(38); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Buff3Length { get { int o = __p.__offset(38); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBuff3Bytes() { return __p.__vector_as_span<int>(38, 4); }
#else
  public ArraySegment<byte>? GetBuff3Bytes() { return __p.__vector_as_arraysegment(38); }
#endif
  public int[] GetBuff3Array() { return __p.__vector_as_array<int>(38); }
  public int Buff4(int j) { int o = __p.__offset(40); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Buff4Length { get { int o = __p.__offset(40); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBuff4Bytes() { return __p.__vector_as_span<int>(40, 4); }
#else
  public ArraySegment<byte>? GetBuff4Bytes() { return __p.__vector_as_arraysegment(40); }
#endif
  public int[] GetBuff4Array() { return __p.__vector_as_array<int>(40); }
  public int Buff5(int j) { int o = __p.__offset(42); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int Buff5Length { get { int o = __p.__offset(42); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBuff5Bytes() { return __p.__vector_as_span<int>(42, 4); }
#else
  public ArraySegment<byte>? GetBuff5Bytes() { return __p.__vector_as_arraysegment(42); }
#endif
  public int[] GetBuff5Array() { return __p.__vector_as_array<int>(42); }

  public static Offset<GameConfigs.EquipmentRowData> CreateEquipmentRowData(FlatBufferBuilder builder,
      int Id = 0,
      StringOffset NameOffset = default(StringOffset),
      StringOffset DescriptionOffset = default(StringOffset),
      StringOffset IconOffset = default(StringOffset),
      int Activity = 0,
      int Group = 0,
      int Type = 0,
      int Quality = 0,
      StringOffset ResourceOffset = default(StringOffset),
      int Level = 0,
      VectorOffset Part1Offset = default(VectorOffset),
      VectorOffset Part2Offset = default(VectorOffset),
      VectorOffset Part3Offset = default(VectorOffset),
      VectorOffset Part4Offset = default(VectorOffset),
      VectorOffset Part5Offset = default(VectorOffset),
      VectorOffset Buff1Offset = default(VectorOffset),
      VectorOffset Buff2Offset = default(VectorOffset),
      VectorOffset Buff3Offset = default(VectorOffset),
      VectorOffset Buff4Offset = default(VectorOffset),
      VectorOffset Buff5Offset = default(VectorOffset)) {
    builder.StartTable(20);
    EquipmentRowData.AddBuff5(builder, Buff5Offset);
    EquipmentRowData.AddBuff4(builder, Buff4Offset);
    EquipmentRowData.AddBuff3(builder, Buff3Offset);
    EquipmentRowData.AddBuff2(builder, Buff2Offset);
    EquipmentRowData.AddBuff1(builder, Buff1Offset);
    EquipmentRowData.AddPart5(builder, Part5Offset);
    EquipmentRowData.AddPart4(builder, Part4Offset);
    EquipmentRowData.AddPart3(builder, Part3Offset);
    EquipmentRowData.AddPart2(builder, Part2Offset);
    EquipmentRowData.AddPart1(builder, Part1Offset);
    EquipmentRowData.AddLevel(builder, Level);
    EquipmentRowData.AddResource(builder, ResourceOffset);
    EquipmentRowData.AddQuality(builder, Quality);
    EquipmentRowData.AddType(builder, Type);
    EquipmentRowData.AddGroup(builder, Group);
    EquipmentRowData.AddActivity(builder, Activity);
    EquipmentRowData.AddIcon(builder, IconOffset);
    EquipmentRowData.AddDescription(builder, DescriptionOffset);
    EquipmentRowData.AddName(builder, NameOffset);
    EquipmentRowData.AddId(builder, Id);
    return EquipmentRowData.EndEquipmentRowData(builder);
  }

  public static void StartEquipmentRowData(FlatBufferBuilder builder) { builder.StartTable(20); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset NameOffset) { builder.AddOffset(1, NameOffset.Value, 0); }
  public static void AddDescription(FlatBufferBuilder builder, StringOffset DescriptionOffset) { builder.AddOffset(2, DescriptionOffset.Value, 0); }
  public static void AddIcon(FlatBufferBuilder builder, StringOffset IconOffset) { builder.AddOffset(3, IconOffset.Value, 0); }
  public static void AddActivity(FlatBufferBuilder builder, int Activity) { builder.AddInt(4, Activity, 0); }
  public static void AddGroup(FlatBufferBuilder builder, int Group) { builder.AddInt(5, Group, 0); }
  public static void AddType(FlatBufferBuilder builder, int Type) { builder.AddInt(6, Type, 0); }
  public static void AddQuality(FlatBufferBuilder builder, int Quality) { builder.AddInt(7, Quality, 0); }
  public static void AddResource(FlatBufferBuilder builder, StringOffset ResourceOffset) { builder.AddOffset(8, ResourceOffset.Value, 0); }
  public static void AddLevel(FlatBufferBuilder builder, int Level) { builder.AddInt(9, Level, 0); }
  public static void AddPart1(FlatBufferBuilder builder, VectorOffset Part1Offset) { builder.AddOffset(10, Part1Offset.Value, 0); }
  public static VectorOffset CreatePart1Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreatePart1VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartPart1Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddPart2(FlatBufferBuilder builder, VectorOffset Part2Offset) { builder.AddOffset(11, Part2Offset.Value, 0); }
  public static VectorOffset CreatePart2Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreatePart2VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartPart2Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddPart3(FlatBufferBuilder builder, VectorOffset Part3Offset) { builder.AddOffset(12, Part3Offset.Value, 0); }
  public static VectorOffset CreatePart3Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreatePart3VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartPart3Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddPart4(FlatBufferBuilder builder, VectorOffset Part4Offset) { builder.AddOffset(13, Part4Offset.Value, 0); }
  public static VectorOffset CreatePart4Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreatePart4VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartPart4Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddPart5(FlatBufferBuilder builder, VectorOffset Part5Offset) { builder.AddOffset(14, Part5Offset.Value, 0); }
  public static VectorOffset CreatePart5Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreatePart5VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartPart5Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBuff1(FlatBufferBuilder builder, VectorOffset Buff1Offset) { builder.AddOffset(15, Buff1Offset.Value, 0); }
  public static VectorOffset CreateBuff1Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBuff1VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBuff1Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBuff2(FlatBufferBuilder builder, VectorOffset Buff2Offset) { builder.AddOffset(16, Buff2Offset.Value, 0); }
  public static VectorOffset CreateBuff2Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBuff2VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBuff2Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBuff3(FlatBufferBuilder builder, VectorOffset Buff3Offset) { builder.AddOffset(17, Buff3Offset.Value, 0); }
  public static VectorOffset CreateBuff3Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBuff3VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBuff3Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBuff4(FlatBufferBuilder builder, VectorOffset Buff4Offset) { builder.AddOffset(18, Buff4Offset.Value, 0); }
  public static VectorOffset CreateBuff4Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBuff4VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBuff4Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBuff5(FlatBufferBuilder builder, VectorOffset Buff5Offset) { builder.AddOffset(19, Buff5Offset.Value, 0); }
  public static VectorOffset CreateBuff5Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBuff5VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBuff5Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.EquipmentRowData> EndEquipmentRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.EquipmentRowData>(o);
  }
};

public struct Equipment : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Equipment GetRootAsEquipment(ByteBuffer _bb) { return GetRootAsEquipment(_bb, new Equipment()); }
  public static Equipment GetRootAsEquipment(ByteBuffer _bb, Equipment obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Equipment __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.EquipmentRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.EquipmentRowData?)(new GameConfigs.EquipmentRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Equipment> CreateEquipment(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Equipment.AddDatalist(builder, datalistOffset);
    return Equipment.EndEquipment(builder);
  }

  public static void StartEquipment(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.EquipmentRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.EquipmentRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Equipment> EndEquipment(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Equipment>(o);
  }
  public static void FinishEquipmentBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Equipment> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedEquipmentBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Equipment> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
