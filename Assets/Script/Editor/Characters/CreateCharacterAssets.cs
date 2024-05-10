using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

class CreateCharacterAssets
{
    //public static string ASSET_PATH = "Assets/BuildAsset/CustomCharacters/";
    // This method creates an assetbundle of each SkinnedMeshRenderer
    // found in any selected character fbx, and adds any materials that
    // are intended to be used by the specific SkinnedMeshRenderer.
    [MenuItem("Tools/Character Generator/CreateCharacters")]
    static void Execute()
    {
        bool createdBundle = false;
        foreach (Object o in Selection.GetFiltered(typeof (Object), SelectionMode.DeepAssets))
        {
            if (!(o is GameObject)) continue;
            if (o.name.Contains("@")) continue;
            string assetPath = AssetDatabase.GetAssetPath(o);
            if (!AssetDatabase.GetAssetPath(o).Contains("/characters/")) continue;

            GameObject characterFBX = (GameObject)o;
            string name = characterFBX.name.ToLower();
           
            Debug.Log("******* Creating assetbundles for: " + name + " *******");
            
            // 将骨骼与动画单独保存在一个bundle中
            // Save bones and animations to a seperate assetbundle. Any 
            // possible combination of CharacterElements will use these
            // assets as a base. As we can not edit assets we instantiate 由于无法直接编辑资源, 我们实例化fbx并且删除不需要的部分
            // the fbx and remove what we dont need. As only assets can be 由于只有资源能被添加到bundle中, 所以我们修改的结果在保存在prefab中,
            // added to assetbundles we save the result as a prefab and delete
            // it as soon as the assetbundle is created.  并在assetbundle创建后 马上删除临时prefab
            GameObject characterClone = (GameObject)Object.Instantiate(characterFBX);

            // postprocess animations: we need them animating even offscreen
			foreach (Animation anim in characterClone.GetComponentsInChildren<Animation>())
                anim.animateOnlyIfVisible = false;

            // 删除多余的可见资源后, 剩余骨架
            foreach (SkinnedMeshRenderer smr in characterClone.GetComponentsInChildren<SkinnedMeshRenderer>())
                Object.DestroyImmediate(smr.gameObject);

            // 在根节点添加蒙皮动画组件
            characterClone.AddComponent<SkinnedMeshRenderer>();
            Animator animator = characterClone.GetComponent<Animator>();
            if (animator != null)
            {
                animator.applyRootMotion = false;
                string animatorPath = CharacterSetting.GetAnimatorPath(name);
                //if (File.Exists(animatorPath))
                var animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(animatorPath);
                if (animatorController != null)
                    animator.runtimeAnimatorController = animatorController;
            }
            Object characterBasePrefab = GetPrefabPath(characterClone, name + "/characterbase"); // 将处理后的对象生成为prefab
            

            // Collect materials. 收集材质球
            List<Material> materials = EditorHelpers.CollectAll<Material>(GenerateMaterials.MaterialsPath(characterFBX));
            
            characterClone = (GameObject)Object.Instantiate(characterFBX);

            // 创建每个部件的资源
            // Create assetbundles for each SkinnedMeshRenderer.
            foreach (SkinnedMeshRenderer smr in characterClone.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                List<Object> toinclude = new List<Object>();
                List<Material> all_materials = new List<Material>();

                //string renderAssetPath = "Assets/BuildAsset/Characters/" + name + "/" + smr.name + "/";
                //Mesh mesh = smr.sharedMesh;
                /*
                Mesh newMesh = new Mesh()
                {
                    vertices = mesh.vertices,
                    triangles = mesh.triangles,
                    normals = mesh.normals,
                    tangents = mesh.tangents,
                    bounds = mesh.bounds,
                    uv = mesh.uv,
                    uv2 = mesh.uv2
                };
                */
                // Save the current SkinnedMeshRenderer as a prefab so it can be included
                // in the assetbundle. As instantiating part of an fbx results in the
                // entire fbx being instantiated, we have to dispose of the entire instance
                // after we detach the SkinnedMeshRenderer in question.
                GameObject rendererClone = GameObject.Instantiate(smr.gameObject); //(GameObject)EditorUtility.InstantiatePrefab(smr.gameObject);
                CharacterElemInfo info = rendererClone.AddComponent<CharacterElemInfo>();
                //rendererClone.GetComponent<SkinnedMeshRenderer>().sharedMesh = newMesh;
                //Object rendererPrefab = GetPrefabPath(rendererClone, name + "/" + smr.name + "/rendererobject"); //GetPrefab(rendererClone, "rendererobject");
                //toinclude.Add(rendererPrefab);

                // Collect applicable materials.
                foreach (Material m in materials)
                {
                    string[] matchName = m.name.Split("_");
                    if (matchName.Length > 2)
                    {
                        if (matchName[1] == smr.name.ToLower())
                        {
                            toinclude.Add(m);
                            all_materials.Add(m);
                        }
                    }
                }

                // 将骨骼名称保存到一个字符串列表供 后续恢复骨骼绑定时使用
                // When assembling a character, we load SkinnedMeshRenderers from assetbundles,
                // and as such they have lost the references to their bones. To be able to
                // remap the SkinnedMeshRenderers to use the bones from the characterbase assetbundles,
                // we save the names of the bones used.
                List<string> boneNames = new List<string>();
                foreach (Transform t in smr.bones)
                    boneNames.Add(t.name);
                info.bones = boneNames.ToArray();
                info.materials = all_materials.ToArray();
                //string stringholderpath = renderAssetPath + "bonenames.asset";
				//StringHolder holder = ScriptableObject.CreateInstance<StringHolder> ();
				//holder.content = boneNames.ToArray();
                //AssetDatabase.CreateAsset(holder, stringholderpath);
                //toinclude.Add(AssetDatabase.LoadAssetAtPath(stringholderpath, typeof (StringHolder)));
                rendererClone.GetComponent<SkinnedMeshRenderer>().sharedMesh = CopyMesh(smr.sharedMesh, CharacterSetting.ASSET_PATH + name + "/" + smr.name);

                Object rendererPrefab = GetPrefabPath(rendererClone, name + "/" + smr.name); //GetPrefab(rendererClone, "rendererobject");

                // Save the assetbundle.
                string bundleName = name + "_" + smr.name.ToLower();
                Debug.Log("Saved " + bundleName + " with " + (toinclude.Count - 2) + " materials");

                // Delete temp assets.
                //AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(rendererPrefab));
                //AssetDatabase.DeleteAsset(stringholderpath);
                createdBundle = true;
            }
            
            GameObject.DestroyImmediate(characterClone);
        }

        if (createdBundle)
            UpdateCharacterElementDatabase.Execute();
        else
            EditorUtility.DisplayDialog("Character Generator", "No Asset Bundles created. Select the characters folder in the Project pane to process all characters. Select subfolders to process specific characters.", "Ok");
    }

    /// <summary>
    /// 将场景中的对象实例化, 然后删除场景中的对象, 并返回prefab对象
    /// </summary>
    /// <param name="go">场景中实例化的对象</param>
    /// <param name="name">要保存的预制名称</param>
    /// <returns>返回生成后的预制对象</returns>
    static Object GetPrefab(GameObject go, string name)
    {
        Object tempPrefab = EditorUtility.CreateEmptyPrefab("Assets/" + name + ".prefab");
        tempPrefab = EditorUtility.ReplacePrefab(go, tempPrefab);
        Object.DestroyImmediate(go);
        return tempPrefab;
    }
    
    /// <summary>
    /// 将场景中的对象实例化, 然后删除场景中的对象, 并返回prefab对象
    /// </summary>
    /// <param name="go">场景中实例化的对象</param>
    /// <param name="name">要保存的预制名称</param>
    /// <returns>返回生成后的预制对象</returns>
    static Object GetPrefabPath(GameObject go, string path)
    {
        var dir_path = CharacterSetting.ASSET_PATH + path; 
        dir_path = Path.GetDirectoryName(dir_path);
        if (!Directory.Exists(dir_path))
            Directory.CreateDirectory(dir_path);
        
        Object tempPrefab = EditorUtility.CreateEmptyPrefab( CharacterSetting.ASSET_PATH + path + ".prefab");
        tempPrefab = EditorUtility.ReplacePrefab(go, tempPrefab);
        Object.DestroyImmediate(go);
        return tempPrefab;
    }
    
    static Mesh CopyMesh(Mesh mesh, string path)
    {
        Mesh newmesh = new Mesh();
        newmesh.vertices = mesh.vertices;
        newmesh.triangles = mesh.triangles;
        newmesh.uv = mesh.uv;
        newmesh.uv2 = mesh.uv2;
        newmesh.normals = mesh.normals;
        newmesh.colors = mesh.colors;
        newmesh.tangents = mesh.tangents;
        newmesh.boneWeights = mesh.boneWeights;
        newmesh.bounds = mesh.bounds;
        newmesh.bindposes = mesh.bindposes;
        string asset_path = path + "_mesh.asset";
        AssetDatabase.CreateAsset(newmesh, asset_path);
        return AssetDatabase.LoadAssetAtPath(asset_path, typeof(Mesh)) as Mesh;
        //return AssetDatabase.LoadAssetAtPath<Mesh>(asset_path);
        //return newmesh;
    }


    /*
    public static string AssetbundlePath
    {
        get { return "Assets" + Path.DirectorySeparatorChar + "assetbundles" + Path.DirectorySeparatorChar; }
    }
    */
}