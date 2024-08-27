using FairyGUI;
using SGame;
using SGame.UI.Explore;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// 战斗玩家角色
    /// </summary>
    public class BattleRole : BaseBattleCharacter
    {
        public BattleRole(UIModel model, UI_FightHp hpBar) : base(model, hpBar)
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

            _hpBar.max = attributes.GetBaseAttributeUpperLimit(EnumAttribute.Hp);
            _hpBar.value = attributes.GetBaseAttribute(EnumAttribute.Hp);
            UpdateHpUI();
        }

        public override void Dead()
        {

        }
    }

}
