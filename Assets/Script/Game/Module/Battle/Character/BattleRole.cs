using FairyGUI;
using SGame;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// 战斗玩家角色
    /// </summary>
    public class BattleRole : BaseBattleCharacter
    {
        public BattleRole(UIModel model) : base(model)
        {
            roleType = RoleType.ROLE;
            forward = 1;
        }

        public override void LoadAttribute(int cfgId)
        {
            base.LoadAttribute(cfgId);
            //加载角色属性
            var attrList = DataCenter.Instance.exploreData.explorer.GetAllAttr();
            attributes.LoadAttribute(attrList);
        }

        public override void Dead()
        {

        }
    }

}
