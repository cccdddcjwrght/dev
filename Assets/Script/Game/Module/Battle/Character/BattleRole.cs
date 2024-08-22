using FairyGUI;
using SGame;
using UnityEngine;

/// <summary>
/// ս����ҽ�ɫ
/// </summary>
public class BattleRole : BaseBattleCharacter
{
    public BattleRole(UIModel model) :base(model)
    {
        roleType = RoleType.ROLE;
        forward = 1;
        //���ؽ�ɫ����
        var attrList = DataCenter.Instance.exploreData.explorer.GetAllAttr();
        attributes.LoadAttribute(attrList);
    }
    
    public override void Dead()
    {
        
    }
}
