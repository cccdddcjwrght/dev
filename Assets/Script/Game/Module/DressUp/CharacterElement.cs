using System;
using System.Collections.Generic;
using UnityEngine;
using Object=UnityEngine.Object;

// When analyzing the available assets UpdateCharacterElementDatabase creates
// a CharacterElement for each possible element. For instance, one mesh with
// three possible textures results in three CharacterElements. All 
// CharacterElements are saved as part the CharacterGenerator ScriptableObject,
// and can be used on runtime to download and load the assets required for the
// element they represent.
[Serializable]
public class CharacterElement
{
    public string name;             // 元件名(材质名)
    public string meshName;         // 模型名
    public string baseName;         // 基础名称
   
    // The WWWs for retrieving the appropriate assetbundle are stored 
    // statically, so CharacterElements that share an assetbundle can
    // use the same WWW.
    // path to assetbundle -> WWW for retieving required assets
    static Dictionary<string, WWW> wwws = new Dictionary<string, WWW>();

    // The required assets are loaded asynchronously to avoid delays
    // when first using them. A LoadAsync results in an AssetBundleRequest
    // which are stored here so we can check their progress and use the
    // assets they contain once they are loaded.
    AssetBundleRequest gameObjectRequest;
    AssetBundleRequest materialRequest;
    AssetBundleRequest boneNameRequest;

    public CharacterElement(string name, string meshName, string baseName)
    {
        this.name = name;
        this.meshName = meshName;
        this.baseName = baseName;
    }
    
    
    public SkinnedMeshRenderer GetSkinnedMeshRenderer()
    {
        GameObject go = (GameObject)Object.Instantiate(gameObjectRequest.asset);
        go.GetComponent<Renderer>().material = (Material)materialRequest.asset;
        return (SkinnedMeshRenderer)go.GetComponent<Renderer>();
    }

    public string[] GetBoneNames()
    {
		var holder = (StringHolder)boneNameRequest.asset;
        return holder.content;
    }
}