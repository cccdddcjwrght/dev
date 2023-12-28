using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.Rendering;

class GenerateMaterials
{
    // As each SkinnedMeshRenderer that make up a character can be
    // textured with several textures, we cant use the materials
    // Unity generates. This method creates a material for each texture
    // which name contains the name of any SkinnedMeshRenderer present
    // in a any selected character fbx's.
    [MenuItem("Tools/Character Generator/Generate Materials")]
    static void Execute()
    {
        bool validMaterial = false;
        foreach (Object o in Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets))
        {
            if (!(o is GameObject)) continue;
            if (o.name.Contains("@")) continue;
            if (!AssetDatabase.GetAssetPath(o).Contains("/characters/")) continue;

            GameObject characterFbx = (GameObject)o;

            // Create directory to store generated materials.
            if (!Directory.Exists(MaterialsPath(characterFbx)))
                Directory.CreateDirectory(MaterialsPath(characterFbx));

            // Collect all textures.
            List<Texture2D> textures = EditorHelpers.CollectAll<Texture2D>(CharacterRoot(characterFbx) + "textures");

            // Create materials for each SkinnedMeshRenderer.
            foreach (SkinnedMeshRenderer smr in characterFbx.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                // Check if this SkinnedMeshRenderer has a normalmap.
                Texture2D normalmap = null;
                foreach (Texture2D t in textures)
                {
                    if (!t.name.ToLower().Contains("normal")) continue;
                    if (!t.name.ToLower().Contains(smr.name.ToLower())) continue;
                    normalmap = t;
                    break;
                }

                // Create a Material for each texture which name contains
                // the SkinnedMeshRenderer name.
                bool findTexture = false;
                foreach (Texture2D t in textures)
                {
                    if (t.name.ToLower().Contains("normal")) continue;
                    if (!t.name.ToLower().Contains(smr.name.ToLower())) continue;
                
                    validMaterial = true;
                    string materialPath = MaterialsPath(characterFbx) + "/" + t.name.ToLower() + ".mat";

                    // Dont overwrite existing materials, as we would
                    // lose existing material settings.
                    if (File.Exists(materialPath))
                    {
                        findTexture = true;
                        continue;
                    }

                    // Use a default shader according to artist preferences.
                    //string shader = "Universal Render Pipeline/Unlit";
                    //if (normalmap != null) shader = "Universal Render Pipeline/Lit";
                    string shader = "Universal Render Pipeline/Lit";

                    // Create the Material
                    Material m = new Material(Shader.Find(shader));
                    m.SetTexture("_BaseMap", t);
                    if (normalmap != null) m.SetTexture("_BumpMap", normalmap);
                    AssetDatabase.CreateAsset(m, materialPath);
                    findTexture = true;
                }

                if (!findTexture)
                {
                    string materialPath = MaterialsPath(characterFbx) + "/" + characterFbx.name + "_" + smr.name.ToLower() + ".mat";
                    if (!File.Exists(materialPath))
                    {
                        // 没有贴图
                        var shaderObj = Shader.Find("Universal Render Pipeline/Unlit");
                        //var localkeyword = new LocalKeyword(shaderObj, "_SURFACE_TYPE_TRANSPARENT");
                        Material m = new Material(shaderObj);
                        SetBlendMode(m, UnityEngine.Rendering.BlendMode.SrcAlpha, UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                        m.SetColor("_Color", new Color(0,1,0,0));
                        m.SetColor("_BaseColor", new Color(0,1,0,0));
                        m.SetFloat("_Surface", 1);
                        m.renderQueue = (int)RenderQueue.Transparent;
                        m.SetOverrideTag("RenderType", "Transparent");
                        CoreUtils.SetKeyword(m, "_SURFACE_TYPE_TRANSPARENT", true);
                        AssetDatabase.CreateAsset(m, materialPath);
                    }
                }
            }
        }
        AssetDatabase.Refresh();
        
        if (!validMaterial) 
            EditorUtility.DisplayDialog("Character Generator", "No Materials created. Select the characters folder in the Project pane to process all characters. Select subfolders to process specific characters.", "Ok");
    }
    
    static void SetBlendMode(Material material, UnityEngine.Rendering.BlendMode srcBlendMode, UnityEngine.Rendering.BlendMode dstBlendMode)
    {
        var srcBlendProp = "_SrcBlend";
        if (material.HasProperty(srcBlendProp))
            material.SetFloat(srcBlendProp, (int)srcBlendMode);
        var dstBlendProp = "_DstBlend";
        if (material.HasProperty(dstBlendProp))
            material.SetFloat(dstBlendProp, (int)dstBlendMode);
    }

    // Returns the path to the directory that holds the specified FBX.
    static string CharacterRoot(GameObject character)
    {
        string root = AssetDatabase.GetAssetPath(character);
        return root.Substring(0, root.LastIndexOf('/') + 1);
    }

    // Returns the path to the directory that holds materials generated
    // for the specified FBX.
    public static string MaterialsPath(GameObject character)
    {
		// we will use it only for file enumeration, and separator will be appended for us
		// if we do append here, AssetDatabase will be confused
        return CharacterRoot(character) + "Per Texture Materials";
    }
}