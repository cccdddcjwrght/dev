using System.Collections.Generic;

namespace SGame 
{
    /// <summary>
    /// 攻击效果
    /// </summary>
    public class AttackEffect
    {
        public int damage;          //伤害
        public int steal;           //吸血量

        public bool isCritical;     //是否暴击
        public bool isCombo;       //是否连击

        //攻击造成的状态（眩晕）
        public List<BaseState> stateList = new List<BaseState>();
    }
}

