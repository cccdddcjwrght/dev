using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.VisualScripting;

[Inspectable]
[System.Serializable]
public struct TestData
{
    [Inspectable]
    public int abc;

    [Inspectable]
    public string name;
    //public Entity e;
    
    [Inspectable]
    public Vector2 pos;
}
