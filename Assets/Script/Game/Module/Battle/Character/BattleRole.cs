using FairyGUI;
using SGame;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// ս����ҽ�ɫ
    /// </summary>
    public class BattleRole : BaseBattleCharacter
    {
        public BattleRole(UIModel model, GProgressBar hpBar) : base(model, hpBar)
        {
            roleType = RoleType.ROLE;
            forward = 1;
        }

        public override void LoadAttribute(int cfgId)
        {
            base.LoadAttribute(cfgId);
            //���ؽ�ɫ����
            var attrList = DataCenter.Instance.exploreData.explorer.GetAllAttr();
            attributes.LoadAttribute(attrList);

            _hpBar.max = attributes.GetBaseAttributeUpperLimit(EnumAttribute.Hp);
            _hpBar.value = attributes.GetBaseAttribute(EnumAttribute.Hp);
        }

        public override void Dead()
        {

        }
    }

}
