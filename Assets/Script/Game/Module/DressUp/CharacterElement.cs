﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object=UnityEngine.Object;
using libx;

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
    
    
    public AssetRequest asset_data;

    public string AssetPath
    {
        get
        {
            return string.Format("{0}{1}/{2}.prefab", CharacterSetting.ASSET_PATH, baseName, meshName);
        }
    }
    
    public bool IsLoaded
    {
        get
        {
            if (asset_data != null)
                return asset_data.isDone;

            asset_data = libx.Assets.LoadAssetAsync(AssetPath, typeof(GameObject));
            return asset_data.isDone;
        }
    }

    /// <summary>
    /// 资源下载进度
    /// </summary>
    public float progress
    {
        get
        {
            if (asset_data == null)
                return 0.0f;
            
            return asset_data.progress;
        }
    }

    public CharacterElement(string name, string meshName, string baseName)
    {
        this.name = name;
        this.meshName = meshName;
        this.baseName = baseName;
    }

    /// <summary>
    /// 获得材质球
    /// </summary>
    /// <returns></returns>
    public Material GetMaterial()
    {
        var info = asset_data.asset as GameObject;//CharacterElemInfo;
        var materials = info.GetComponent<CharacterElemInfo>().materials;
        foreach (var m in materials)
        {
            if (m.name.ToLower() == name)
                return m;
        }

        return null;
    }
    
    /// <summary>
    /// 获得Mesh信息
    /// </summary>
    /// <returns></returns>
    public SkinnedMeshRenderer GetSkinnedMeshRenderer()
    {
        var info = asset_data.asset as GameObject;//CharacterElemInfo;
        var render = info.GetComponent<Renderer>();
        //GameObject go = (GameObject)GameObject.Instantiate(info.gameObject);
        //go.GetComponent<Renderer>().sharedMaterial = GetMaterial();
        //return (SkinnedMeshRenderer)go.GetComponent<Renderer>();
        return render as SkinnedMeshRenderer;
    }

    /// <summary>
    /// 获得骨骼名称
    /// </summary>
    /// <returns></returns>
    public string[] GetBoneNames()
    {
        var info = asset_data.asset as GameObject;//CharacterElemInfo;
        return info.GetComponent<CharacterElemInfo>().bones;
        //return info.bones;
    }
}