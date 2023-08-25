// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfigs
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Game_FunctionRowData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Game_FunctionRowData GetRootAsGame_FunctionRowData(ByteBuffer _bb) { return GetRootAsGame_FunctionRowData(_bb, new Game_FunctionRowData()); }
  public static Game_FunctionRowData GetRootAsGame_FunctionRowData(ByteBuffer _bb, Game_FunctionRowData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Game_FunctionRowData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Unlock { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Exp { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<GameConfigs.Game_FunctionRowData> CreateGame_FunctionRowData(FlatBufferBuilder builder,
      int Id = 0,
      int Unlock = 0,
      int Exp = 0) {
    builder.StartTable(3);
    Game_FunctionRowData.AddExp(builder, Exp);
    Game_FunctionRowData.AddUnlock(builder, Unlock);
    Game_FunctionRowData.AddId(builder, Id);
    return Game_FunctionRowData.EndGame_FunctionRowData(builder);
  }

  public static void StartGame_FunctionRowData(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddUnlock(FlatBufferBuilder builder, int Unlock) { builder.AddInt(1, Unlock, 0); }
  public static void AddExp(FlatBufferBuilder builder, int Exp) { builder.AddInt(2, Exp, 0); }
  public static Offset<GameConfigs.Game_FunctionRowData> EndGame_FunctionRowData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Game_FunctionRowData>(o);
  }
};

public struct Game_Function : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Game_Function GetRootAsGame_Function(ByteBuffer _bb) { return GetRootAsGame_Function(_bb, new Game_Function()); }
  public static Game_Function GetRootAsGame_Function(ByteBuffer _bb, Game_Function obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Game_Function __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public GameConfigs.Game_FunctionRowData? Datalist(int j) { int o = __p.__offset(4); return o != 0 ? (GameConfigs.Game_FunctionRowData?)(new GameConfigs.Game_FunctionRowData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DatalistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<GameConfigs.Game_Function> CreateGame_Function(FlatBufferBuilder builder,
      VectorOffset datalistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Game_Function.AddDatalist(builder, datalistOffset);
    return Game_Function.EndGame_Function(builder);
  }

  public static void StartGame_Function(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDatalist(FlatBufferBuilder builder, VectorOffset datalistOffset) { builder.AddOffset(0, datalistOffset.Value, 0); }
  public static VectorOffset CreateDatalistVector(FlatBufferBuilder builder, Offset<GameConfigs.Game_FunctionRowData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDatalistVectorBlock(FlatBufferBuilder builder, Offset<GameConfigs.Game_FunctionRowData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDatalistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameConfigs.Game_Function> EndGame_Function(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<GameConfigs.Game_Function>(o);
  }
  public static void FinishGame_FunctionBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Game_Function> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedGame_FunctionBuffer(FlatBufferBuilder builder, Offset<GameConfigs.Game_Function> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
