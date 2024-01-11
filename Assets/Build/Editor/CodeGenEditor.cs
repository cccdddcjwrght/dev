using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class CodeGenEditor
{
	public const string C_PATH = "Assets/Scripts/CodeGen/";

	public static string C_CHARS = "abcdefghijklmnopqretuvwxyzABCDEFGHIJKLMNOPQRETUVWXYZ_";

	public const string CS_TEMP = @"
namespace CodeGens{
#if !UNITY_EDITOR
    public static class __AUTOTGEN__{
		[UnityEngine.RuntimeInitializeOnLoadMethod]
        static void Init(){
//body	
		}
	}
#endif
}
";

	public const string CS_CON = @"

namespace CodeGens
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct AutoGen___NAME__ : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static AutoGen___NAME__ GetRootAsAutoGen___NAME__(ByteBuffer _bb) { return GetRootAsAutoGen___NAME__(_bb, new AutoGen___NAME__()); }
  public static AutoGen___NAME__ GetRootAsAutoGen___NAME__(ByteBuffer _bb, AutoGen___NAME__ obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public AutoGen___NAME__ __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartAutoGen___NAME__(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<CodeGens.AutoGen___NAME__> EndAutoGen___NAME__(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<CodeGens.AutoGen___NAME__>(o);
  }
  public static void FinishAutoGen___NAME__Buffer(FlatBufferBuilder builder, Offset<CodeGens.AutoGen___NAME__> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedAutoGen___NAME__Buffer(FlatBufferBuilder builder, Offset<CodeGens.AutoGen___NAME__> offset) { builder.FinishSizePrefixed(offset.Value); }
};
}


";

	[MenuItem("[Tools]/CLS")]
	static public void Excute()
	{
		var body = new StringBuilder();
		if (Directory.Exists(C_PATH))
			Directory.Delete(C_PATH, true);
		Directory.CreateDirectory(C_PATH);
		Debug.Log("::Begin GenCode");
		var con = "";
		for (int i = 0; i < 100; i++)
		{
			var name = C_CHARS[UnityEngine.Random.Range(0, C_CHARS.Length)] + GetMD5(System.DateTime.Now.Ticks.ToString() + i);
			con = CS_CON.Replace("__NAME__", name);
			File.WriteAllText(Path.Combine(C_PATH, name + ".cs"), con, Encoding.UTF8);
			body.AppendLine($"\t\t\tnew AutoGen_{name}();");
		}

		con = CS_TEMP.Replace("//body", body.ToString());
		File.WriteAllText(Path.Combine(C_PATH, "AutoCodeGen.cs"), con, Encoding.UTF8);
		AssetDatabase.Refresh();
		Debug.Log("::End GenCode");
	}

	public static string GetMD5(string data)
	{
		return GetMD5(Encoding.UTF8.GetBytes(data));
	}

	public static string GetMD5(byte[] data)
	{
		string ret = null;
		using (MD5 md5Hash = MD5.Create())
		{
			data = md5Hash.ComputeHash(data);
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
				sBuilder.Append(data[i].ToString("x2"));
			ret = sBuilder.ToString();
		}
		return ret;
	}

}
