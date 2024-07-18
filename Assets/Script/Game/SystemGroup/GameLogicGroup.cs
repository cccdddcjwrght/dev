using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(UIGroup))]
public partial class GameGroup : ComponentSystemGroup
{
    
}

[UpdateInGroup(typeof(GameGroup))]
public partial class GameLogicGroup : ComponentSystemGroup
{
    
}

[UpdateInGroup(typeof(GameGroup))]
[UpdateBefore(typeof(GameLogicGroup))]
public partial class GameLogicBefore : ComponentSystemGroup
{
    
}

[UpdateInGroup(typeof(GameGroup))]
[UpdateAfter(typeof(GameLogicGroup))]
public partial class GameLogicAfterGroup : ComponentSystemGroup
{
    
}
