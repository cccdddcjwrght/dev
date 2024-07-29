// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct LevelGridPropertyRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static LevelGridPropertyRowData GetRootAsLevelGridPropertyRowData(ByteBuffer _bb) { return GetRootAsLevelGridPropertyRowData(_bb, new LevelGridPropertyRowData()); }
  public static LevelGridPropertyRowData GetRootAsLevelGridPropertyRowData(ByteBuffer _bb, LevelGridPropertyRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LevelGridPropertyRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MinX { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MinY { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MaxX { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MaxY { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Area { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<GameConfigs.LevelGridPropertyRowData> CreateLevelGridPropertyRowData(FlatBufferBuilder builder,
      int Id = 0,
      int minX = 0,
      int minY = 0,
      int maxX = 0,
      int maxY = 0,
      int Area = 0) {
    builder.StartTable(6);
    LevelGridPropertyRowData.AddArea(builder, Area);
    LevelGridPropertyRowData.AddMaxY(builder, maxY);
    LevelGridPropertyRowData.AddMaxX(builder, maxX);
    LevelGridPropertyRowData.AddMinY(builder, minY);
    LevelGridPropertyRowData.AddMinX(builder, minX);
    LevelGridPropertyRowData.AddId(builder, Id);
    return LevelGridPropertyRowData.EndLevelGridPropertyRowData(builder);
  }

  public static void StartLevelGridPropertyRowData(FlatBufferBuilder builder) { builder.StartTable(6); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddMinX(FlatBufferBuilder builder, int minX) { builder.AddInt(1, minX, 0); }
  public static void AddMinY(FlatBufferBuilder builder, int minY) { builder.AddInt(2, minY, 0); }
  public static void AddMaxX(FlatBufferBuilder builder, int maxX) { builder.AddInt(3, maxX, 0); }
  public static void AddMaxY(FlatBufferBuilder builder, int maxY) { builder.AddInt(4, maxY, 0); }
  public static void AddArea(FlatBufferBuilder builder, int Area) { builder.AddInt(5, Area, 0); }
  public static Offset<GameConfigs.LevelGridPropertyRowData> EndLevelGridPropertyRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.LevelGridPropertyRowData>(o);
  }
};

public struct LevelGridProperty : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static LevelGridProperty GetRootAsLevelGridProperty(ByteBuffer _bb) { return GetRootAsLevelGridProperty(_bb, new LevelGridProperty()); }
  public static LevelGridProperty GetRootAsLevelGridProperty(ByteBuffer _bb, LevelGridProperty obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LevelGridProperty __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.LevelGridPropertyRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.LevelGridPropertyRowData?)(new GameConfigs.LevelGridPropertyRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.LevelGridProperty> CreateLevelGridProperty(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    LevelGridProperty.AddDatalist(builder, datalistOffset);
    return LevelGridProperty.EndLevelGridProperty(builder);
  }

  public static void StartLevelGridProperty(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.LevelGridPropertyRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.LevelGridPropertyRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.LevelGridProperty> EndLevelGridProperty(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.LevelGridProperty>(o);
  }
  public static void FinishLevelGridPropertyBuffer(FlatBufferBuilder builder, Offset<GameConfigs.LevelGridProperty> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedLevelGridPropertyBuffer(FlatBufferBuilder builder, Offset<GameConfigs.LevelGridProperty> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
