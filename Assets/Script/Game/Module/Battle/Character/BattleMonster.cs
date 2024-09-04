using FairyGUI;
using SGame.UI.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class BattleMonster : BaseBattleCharacter
    {
        public BattleMonster(UIModel model, UI_FightHp hpBar) : base(model, hpBar)
        {
            roleType = RoleType.MONSTER;
            forward = -1;

            hpBar.m_effect.rotationY = 180;
        }

        public override void Dead()
        {
            base.Dead();
            _model.Play("die");
            //45.ToAudioID().PlayAudio();
            EventManager.Instance.Trigger((int)GameEvent.BATTLE_AUDIO, 45);
        }
    }
}

