using UnityEngine;

// This class holds a string array and can be saved
// as an asset. This way MonoBehaviours can reference
// it, or it can be added to an assetbundle, making this
// a convenient way of storing procedurally generated
// data on editor time and accessing it at runtime.
public class CharacterSetting
{
    public const string ASSET_PATH    = "Assets/BuildAsset/CustomCharacters/";  // 角色资源地址
    public const string ELEMENT_NAME  = "CharacterElementDatabase";             // 换装角色元件名称
    public const string ROOT_NAME     = "characterbase";                   // 不同类型角色名称
}