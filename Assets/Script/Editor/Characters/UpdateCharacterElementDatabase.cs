using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

class UpdateCharacterElementDatabase
{
    // This method collects information about all available
    // CharacterElements stores it in the CharacterElementDatabase
    // assetbundle. Which CharacterElements are available is 
    // determined by checking the generated materials.
    [MenuItem("Tools/Character Generator/Update Character Element Database")]
    public static void Execute()
    {
        List<CharacterElement> characterElements = new List<CharacterElement>();

        // 收集并保存部件信息
        //string[] assetbundles = Directory.GetFiles(CreateAssetbundles.AssetbundlePath);                                                  // 遍历所有生成的bundle 文件
        //string[] materials = Directory.GetFiles("Assets/BuildAsset/Art/Characters", "*.mat", SearchOption.AllDirectories); // 获取所有材质

        string[] assets = AssetDatabase.FindAssets("t:Prefab", new[] { CreateCharacterAssets.ASSET_PATH });
        
        foreach (string asset in assets)
        {
            var asset_path = AssetDatabase.GUIDToAssetPath(asset);
            var info = AssetDatabase.LoadAssetAtPath<CharacterElemInfo> (asset_path);
            if (info != null)
            {
                var meshName = Path.GetFileNameWithoutExtension(asset_path);
                var dirName = Path.GetDirectoryName(asset_path);
                var baseName = Path.GetFileNameWithoutExtension(dirName);
                foreach (var m in info.materials)
                {
                    var materialPath = AssetDatabase.GetAssetPath(m);
                    var name = Path.GetFileNameWithoutExtension(materialPath);
                    characterElements.Add(new CharacterElement(name, meshName, baseName));
                }
                //characterElements
            }
            //string name =  Path.GetFileNameWithoutExtension(material);
            //characterElements.Add(new CharacterElement(name, ""));
        }

        // 将信息保存到 脚本数据中去
        CharacterElementHolder t = ScriptableObject.CreateInstance<CharacterElementHolder> ();
		t.content = characterElements;
        
        // 保存成资源文件并打包
        // Save the ScriptableObject and load the resulting asset so it can 
        // be added to an assetbundle.
        string p = "Assets/BuildAsset/CustomCharacters/CharacterElementDatabase.asset";
        AssetDatabase.CreateAsset(t, p);
        

        Debug.Log("******* Updated Character Element Database, added " + characterElements.Count + " elements *******");
    }
}