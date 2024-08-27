using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame 
{
    public class DizzState : BaseState
    {
        private Entity _effect;
        public DizzState()
        {
            round = BattleConst.dizziness_inning + 1;
            isImmediately = true;
            type = BattleStateType.DIZZ;
        }

        public override void ShowEffect()
        {
            //生成特效
            if (_effect == Entity.Null && _character != null) 
            {
                _effect = _character.ShowEffect(3005, _character.hpBar.m_effect.m__dizz, Vector2.zero);
            }
        }

        public override void Dispose()
        {
            if (stateShow && _effect != Entity.Null)
            {
                EffectSystem.Instance.CloseEffect(_effect);
                _effect = Entity.Null;
            }
        }
    }
}

