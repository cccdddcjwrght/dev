using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SGame.UI 
{
    public partial class UIExplore
    {
        private BattleRole _battleRole;
        private BattleMonster _battleMonster;
        bool _show = false;

        private int _monsterCfgId;
        private UIModel _monster;

        private UnityEngine.Coroutine _battle;

        public void InitBattle() 
        {
            eventHandle += EventManager.Instance.Reg((int)GameEvent.BATTLE_START, TriggerBattle);
            eventHandle += EventManager.Instance.Reg((int)GameEvent.BATTLE_OVER, ()=> ExitExpression().Start());
            eventHandle += EventManager.Instance.Reg((int)GameEvent.BATTLE_ROUND, RefreshRound);
            onOpen += OnOpen_Battle;
            onHide += OnHide_Battle;
            onClose += OnClose_Battle;

            m_view.m_battlemonster.z = -200;
            m_view.m_fightHp1.z = -200;
            m_view.m_fightHp2.z = -200;
            m_view.m_fightBtn.visible = !DataCenter.BattleLevelUtil.IsMax;
            RefreshFightLevel();
        }

        partial void OnFightBtnClick(EventContext data)
        {
            SGame.UIUtils.OpenUI("fightlevel", DataCenter.Instance.battleLevelData.showLevel);
        }

        void TriggerBattle() 
        {
            _battle = EnterBattle().Start();
        }

        public IEnumerator EnterBattle()
        {
            if (BattleManager.Instance.isCombat) yield break;

            EnableExploreButton(false);
            m_view.m_fightBtn.visible = false;
            _battleRole = new BattleRole(_model,m_view.m_fightHp1);
            _battleRole.LoadAttribute(DataCenter.Instance.exploreData.explorer.roleID);

            ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(DataCenter.Instance.battleLevelData.showLevel, out var config);
            _monsterCfgId = config.Monster;

            if (_monster == null)
            {
                _monster = new UIModel(m_view.m_battlemonster)
                    .SetData(0)
                    .SetTRS(Vector3.zero, new Vector3(0, 180, 0), 50)
                    .SetLoad(LoadMonsterModel);
            }
            else
            {
                _monster.SetData(0).
                    SetTRS(Vector3.zero, new Vector3(0, 180, 0), 50).
                    SetLoad(LoadMonsterModel);
            }
            _monster.RefreshModel();
            yield return new WaitForSeconds(0.5f);
  
            _battleMonster = new BattleMonster(_monster, m_view.m_fightHp2);
            _battleMonster.LoadAttribute(_monsterCfgId);
            _battleMonster.attributes.SetAttribute((int)EnumAttribute.Stun, 5000);

            float moveTime = 2;
            BattleManager.Instance.BattleStart(_battleRole, _battleMonster, config.Inning, moveTime + 0.5f).Start();
            yield return _battleMonster.Move(1, 330, moveTime);
            _battleRole.model.Play("idle");
            MapLoop(true);
            yield return new WaitForSeconds(0.5f);
            m_view.m_roundGroup.visible = true;
        }

        public IEnumerator ExitExpression() 
        {
            if (BattleManager.Instance.isWin)
                _battleRole.model.Play("win");
            yield return new WaitForSeconds(0.8f);

            ExitBattle();
        }

        public void ExitBattle()
        {
            m_view.m_fightBtn.visible = !DataCenter.BattleLevelUtil.IsMax;
            m_view.m_roundGroup.visible = false;
            m_view.m_battlemonster.x = m_view.m_mholder.x;

            ShowBattleResult();
            EnableExploreButton(true);
            SetBaseInfo();
            RefreshFightLevel();

            MapLoop(false);

            _battleRole.Dispose();
            _battleMonster.Dispose();
            _monster.Reset();
        }

        //设置部分探索按钮状态
        public void EnableExploreButton(bool active) 
        {
            m_view.m_find.enabled = active;
            m_view.m_tool.enabled = active;
            m_view.m_auto.enabled = active;
        }

        public void RefreshFightLevel() 
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(DataCenter.Instance.battleLevelData.level, out var config))
            {
                m_view.m_fightBtn.SetText(UIListener.Local(config.Name));
            } 
        }

        public void RefreshRound() 
        {
            m_view.m_round.SetText($"{BattleManager.Instance.GetRoundIndex()}/{BattleConst.max_turncount}");
        }

        public void OnOpen_Battle(UIContext context) 
        {
            _show = true;
            DelaySetting().Start();
            ShowBattleResult();
        }

        public void OnHide_Battle(UIContext context) 
        {
            _show = false;
        }

        public void OnClose_Battle(UIContext context)
        {
            _battle.Stop();
            BattleManager.Instance.DiscontinuePlayRound();
        }

        public IEnumerator DelaySetting() 
        {
            yield return null;
            if (BattleManager.Instance.isCombat) MapLoop(true);
        }

        public void ShowBattleResult() 
        {
            if (_show && !BattleManager.Instance.isCombat) 
                DataCenter.BattleLevelUtil.ShowBattleResult();
        }

        public IEnumerator LoadMonsterModel(object data)
        {
            string modelName = string.Empty;
            if (ConfigSystem.Instance.TryGet<GameConfigs.BattleRoleRowData>(_monsterCfgId, out var config)) 
                modelName = config.Model;
            
            var path = "Assets/BuildAsset/Prefabs/Monster/" + modelName;
#if UNITY_EDITOR
            if (!File.Exists(path + ".prefab"))
            {
                Debug.LogError($"boss模型资源不存在:{path},将使用临时资源");
                path = "Assets/BuildAsset/Prefabs/Monster/boss-2";
            }
#endif
            return SpawnSystem.Instance.SpawnAndWait(path);
        }
    }
}

