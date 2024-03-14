using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(UIGroup))]
public class GameGroup : ComponentSystemGroup
{
    
}

[UpdateInGroup(typeof(GameGroup))]
public class GameLogicGroup : ComponentSystemGroup
{
    
}

[UpdateInGroup(typeof(GameGroup))]
[UpdateBefore(typeof(GameLogicGroup))]
public class GameLogicBefore : ComponentSystemGroup
{
    
}

[UpdateInGroup(typeof(GameGroup))]
[UpdateAfter(typeof(GameLogicGroup))]
public class GameLogicAfterGroup : ComponentSystemGroup
{
    
}
