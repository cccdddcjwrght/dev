using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class BattleMonster : BaseBattleCharacter
    {
        public BattleMonster(UIModel model) : base(model)
        {
            roleType = RoleType.MONSTER;
            forward = -1;
        }

        public override void Dead()
        {

        }
    }
}

