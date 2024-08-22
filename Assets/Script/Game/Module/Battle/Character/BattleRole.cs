using FairyGUI;
using SGame;
using UnityEngine;

/// <summary>
/// 战斗玩家角色
/// </summary>
public class BattleRole : BaseBattleCharacter
{
    public BattleRole(UIModel model) :base(model)
    {
        roleType = RoleType.ROLE;
        forward = 1;
        //加载角色属性
        var attrList = DataCenter.Instance.exploreData.explorer.GetAllAttr();
        attributes.LoadAttribute(attrList);
    }
    
    public override void Dead()
    {
        
    }
}
