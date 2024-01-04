using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameLogicGroup : ComponentSystemGroup
{
    
}

[UpdateAfter(typeof(GameLogicGroup))]
public class GameLogicAfterGroup : ComponentSystemGroup
{
    
}
