using System.Collections;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// 战斗角色基类
    /// </summary>
    public abstract class BaseBattleCharacter
    {
        //角色类型
        public RoleType roleType;
        //节点
        protected UIModel _model;
        protected int forward;

        public BattleAttritube attributes;
        //角色状态
        public CharacterState state;

        /// <summary>
        /// 是否存活
        /// </summary>
        public bool isAlive { get { return attributes.GetBaseAttribute(SGame.EnumAttribute.Hp) > 0; } }

        public BaseBattleCharacter(UIModel model)
        {
            _model = model;

            attributes = new BattleAttritube();
            state = new CharacterState();
        }

        public virtual void LoadAttribute(int cfgId)
        {
            attributes.ReadAttribute(cfgId);
        }

        public virtual IEnumerator DoAttack(BaseBattleCharacter target, AttackEffect attackEffect)
        {
            yield return Move(1, BattleConst.move_distance, BattleConst.move_time);

            //播放攻击动画
            _model.Play("Attack");
            yield return Move(1, 0, 1f);
     
            target.DoHit(attackEffect.damage).Start();

            yield return Move(1, 0, 1f);

            if (attackEffect.stateList != null && attackEffect.stateList.Count > 0)
                attackEffect.stateList.ForEach((v) => target.state.ApplyState(v));

            if (attackEffect.steal > 0)
            {
                attributes.ChangeAttribute(EnumAttribute.Hp, attackEffect.steal);
                //吸血效果
            }

            if (attackEffect.isCritical)
            {
                //暴击效果
            }

            if (attackEffect.isCombo)
            {
                //连击效果
            }

            yield return Move(-1, BattleConst.move_distance, BattleConst.move_time);
            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator Move(int dir, float distance, float time)
        {
            var root = _model.GetRoot();
            var cx = root.x;
            var tx = root.x + forward * distance * dir;

            float startTime = BattleTimer.Instance.CurTime();
            float forwardTime = startTime + time;

            float curTime = startTime;
            while (curTime < forwardTime)
            {
                curTime = BattleTimer.Instance.CurTime();
                root.x = Mathf.Lerp(cx, tx, (curTime - startTime) / time);
                yield return null;
            }
            root.x = tx;
        }


        public virtual IEnumerator DoHit(int damage)
        {
            if (damage <= 0)
            {
                yield return Move(-1, 50, 0.25f);
                yield return Move(1, 50, 0.25f);
            }
            else 
            {
                attributes.ChangeAttribute(EnumAttribute.Hp, -damage);
                yield return Move(-1, 10, 0.1f);
                yield return Move(1, 10, 0.1f);
                if (attributes.GetBaseAttribute(EnumAttribute.Hp) <= 0) Dead();
            }
            //Debug.Log(string.Format("<color=red>{0} hit:{1} curhp: {2} time: {3}</color>", roleType.ToString(), damage, attributes.GetBaseAttribute(EnumAttribute.Hp), Time.realtimeSinceStartupAsDouble));
        }

        public abstract void Dead();

        public virtual void Dispose() 
        {
            _model = null;
            attributes = null;
            state.Reset();
            state = null;
        }

        public virtual float GetAttackTime()
        {
            return 2f;
        }

    }

}
