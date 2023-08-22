// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Grid_PathRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Grid_PathRowData GetRootAsGrid_PathRowData(ByteBuffer _bb) { return GetRootAsGrid_PathRowData(_bb, new Grid_PathRowData()); }
  public static Grid_PathRowData GetRootAsGrid_PathRowData(ByteBuffer _bb, Grid_PathRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Grid_PathRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int BoardArea { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Unlock { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Position(int j) { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int PositionLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetPositionBytes() { return __p.__vector_as_span<int>(10, 4); }
#else
  public ArraySegment<byte>? GetPositionBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public int[] GetPositionArray() { return __p.__vector_as_array<int>(10); }

  public static Offset<GameConfigs.Grid_PathRowData> CreateGrid_PathRowData(FlatBufferBuilder builder,
      int Id = 0,
      int BoardArea = 0,
      int Unlock = 0,
      VectorOffset PositionOffset = default(VectorOffset)) {
    builder.StartTable(4);
    Grid_PathRowData.AddPosition(builder, PositionOffset);
    Grid_PathRowData.AddUnlock(builder, Unlock);
    Grid_PathRowData.AddBoardArea(builder, BoardArea);
    Grid_PathRowData.AddId(builder, Id);
    return Grid_PathRowData.EndGrid_PathRowData(builder);
  }

  public static void StartGrid_PathRowData(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddBoardArea(FlatBufferBuilder builder, int BoardArea) { builder.AddInt(1, BoardArea, 0); }
  public static void AddUnlock(FlatBufferBuilder builder, int Unlock) { builder.AddInt(2, Unlock, 0); }
  public static void AddPosition(FlatBufferBuilder builder, VectorOffset PositionOffset) { builder.AddOffset(3, PositionOffset.Value, 0); }
  public static VectorOffset CreatePositionVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreatePositionVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartPositionVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Grid_PathRowData> EndGrid_PathRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Grid_PathRowData>(o);
  }
};

public struct Grid_Path : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Grid_Path GetRootAsGrid_Path(ByteBuffer _bb) { return GetRootAsGrid_Path(_bb, new Grid_Path()); }
  public static Grid_Path GetRootAsGrid_Path(ByteBuffer _bb, Grid_Path obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Grid_Path __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.Grid_PathRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.Grid_PathRowData?)(new GameConfigs.Grid_PathRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Grid_Path> CreateGrid_Path(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Grid_Path.AddDatalist(builder, datalistOffset);
    return Grid_Path.EndGrid_Path(builder);
  }

  public static void StartGrid_Path(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.Grid_PathRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.Grid_PathRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Grid_Path> EndGrid_Path(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Grid_Path>(o);
  }
  public static void FinishGrid_PathBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Grid_Path> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedGrid_PathBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Grid_Path> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
