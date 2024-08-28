using FairyGUI;
using SGame.UI;
using SGame.UI.Explore;
using System.Collections;
using Unity.Entities;
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
        protected UI_FightHp _hpBar;

        private Equipments m_slot;

        public UI_FightHp hpBar 
        {
            get { return _hpBar; }
        }

        public UIModel model 
        {
            get { return _model; }
        }

        protected int forward;

        public BattleAttritube attributes;
        //角色状态
        public CharacterState state;

        /// <summary>
        /// 是否存活
        /// </summary>
        public bool isAlive { get { return attributes.GetBaseAttribute(SGame.EnumAttribute.Hp) > 0; } }

        public BaseBattleCharacter(UIModel model, UI_FightHp hpBar)
        {
            _model = model;
            _hpBar = hpBar;

            m_slot = model.GetModel().GetOrAddComponent<Equipments>();
            attributes = new BattleAttritube();
            state = new CharacterState();
        }

        public virtual void LoadAttribute(int cfgId)
        {
            attributes.ReadAttribute(cfgId);
            _hpBar.max = attributes.GetBaseAttributeUpperLimit(EnumAttribute.Hp);
            _hpBar.visible = true;
            UpdateHpUI();
        }

        public virtual IEnumerator DoAttack(BaseBattleCharacter target, AttackEffect attackEffect)
        {
            yield return Move(1, BattleConst.move_distance, BattleConst.move_time);

            //播放攻击动画
            _model.Play("attack");
            yield return new WaitForSeconds(0.55f);

            ShowEffect(3003, _hpBar.m_effect.m__attack, new Vector2(100, 150));
            target.DoHit(attackEffect.damage, attackEffect.isCritical).Start();
            if (attackEffect.stateList != null && attackEffect.stateList.Count > 0)
                attackEffect.stateList.ForEach((v) => target.state.ApplyState(v, target));

            if (attackEffect.steal > 0)
            {
                attributes.ChangeAttribute(EnumAttribute.Hp, attackEffect.steal);
                ShowBattleText($"+{attackEffect.steal}", Color.green, 52, 150);
                ShowEffect(3004, _hpBar.m_effect.m__steal, new Vector2(0, 170));
                UpdateHpUI();
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

            yield return new WaitForSeconds(0.55f);
            _model.Play("idle");
            yield return Move(-1, BattleConst.move_distance, BattleConst.move_time);
            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator Move(int dir, float distance, float time)
        {
            _model.Play("walk");
            var root = _model.GetRoot();
            var cx = root.x;
            var tx = root.x + forward * distance * dir;

            float startTime = BattleTimer.Instance.CurTime();
            float forwardTime = startTime + time;

            float curTime = startTime;
            yield return new WaitWhile(() =>
            {
                curTime = BattleTimer.Instance.CurTime();
                root.x = Mathf.Lerp(cx, tx, (curTime - startTime) / time);
                return curTime < forwardTime;
            });

            root.x = tx;
            _model.Play("idle");
        }


        public virtual IEnumerator DoHit(int damage, bool isCritical)
        {
            if (damage <= 0)
            {
                ShowBattleText("miss", Color.gray, 52, 150);
                yield return Move(-1, 50, 0.25f);
                yield return Move(1, 50, 0.25f);
            }
            else 
            {
                attributes.ChangeAttribute(EnumAttribute.Hp, -damage);
                var hitEffectId = isCritical ? 3002 : 3001;
                ShowEffect(hitEffectId, _hpBar.m_effect.m__hit, new Vector2(0, 150));
                ShowBattleText($"-{damage}", Color.red, 52, 150);
                _model.Play("hit");
                //yield return Move(-1, 10, 0.1f);
                //yield return Move(1, 10, 0.1f);
            }
            UpdateHpUI();
            yield return new WaitForSeconds(0.55f);
            if (attributes.GetBaseAttribute(EnumAttribute.Hp) <= 0) 
            {
                Dead();
                yield break;
            }
            _model.Play("idle");
        }

        public void ShowBattleText(string title, Color color,float offsetX, float offsetY) 
        {
            var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var e = SGame.UIUtils.Show2DUI("battletext", _hpBar);
            EntityManager.AddComponentObject(e, new HUDTips() { title = $"{title}", color = color, offsetX = offsetX, offsetY = offsetY });
            //EntityManager.SetComponentData(e, new LiveTime() { Value = 1.25f });
        }

        public void UpdateHpUI() 
        {
            if (_hpBar != null) 
            {
                _hpBar.value = attributes.GetBaseAttribute(EnumAttribute.Hp);
                _hpBar.GetChild("value").SetText($"{Utils.ConvertNumberStr(_hpBar.value)}/{Utils.ConvertNumberStr(_hpBar.max)}");
            }
        }

        public Entity ShowEffect(int effectId, GGraph holder, SlotType slotType) 
        {
            var t = m_slot.GetSlot(slotType);
            if (t == null) return default;

            var e = EffectSystem.Instance.SpawnUI(effectId, holder);
            var pos = _hpBar.m_effect.WorldToLocal(t.position, Camera.main);
            holder.xy = pos;
            return e;
        }

        public Entity ShowEffect(int effectId, GGraph holder, Vector2 pos) 
        {
            var e = EffectSystem.Instance.SpawnUI(effectId, holder);
            holder.SetPosition(pos.x, pos.y, -500 * forward);
            return e;
        }

        public abstract void Dead();

        public virtual void Dispose() 
        {
            _model = null;

            _hpBar.visible = false;
            _hpBar = null;
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
