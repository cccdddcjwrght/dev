using UnityEngine;

/// <summary>
/// 角色元件信息
/// </summary>
public class CharacterElemInfo : MonoBehaviour
{
    /// <summary>
    /// 骨骼名称信息
    /// </summary>
    [SerializeField]
    public string[] bones;

    /// <summary>
    /// 该骨骼对应的所有材质信息
    /// </summary>
    public Material[] materials;
}