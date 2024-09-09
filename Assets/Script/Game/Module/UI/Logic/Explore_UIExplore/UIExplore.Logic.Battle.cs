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
            eventHandle += EventManager.Instance.Reg<int>((int)GameEvent.BATTLE_AUDIO, PlayBattleAudio);
            eventHandle += EventManager.Instance.Reg((int)GameEvent.EXPLORE_FIGHT_CHANGE, RefreshFightLevel);
            onOpen += OnOpen_Battle;
            onHide += OnHide_Battle;
            onClose += OnClose_Battle;

            m_view.m_find.onClick.Add(CloseFightLimitGuide);

            m_view.m_battlemonster.z = -200;
            m_view.m_fightHp1.z = -200;
            m_view.m_fightHp2.z = -200;
            m_view.m_fightBtn.visible = !DataCenter.BattleLevelUtil.IsMax;
            
        }

        partial void OnFightBtnClick(EventContext data)
        {
            if (m_view.m_fightBtn.m_state.selectedIndex == 2)
            {
                "@ui_fightbtn_cd_tip".Tips();
                PlayFightLimitGuide();
            }
            else if (m_view.m_fightBtn.m_state.selectedIndex == 0) 
            {
                "@ui_fightbtn_unlock_tip".Tips();
                PlayFightLimitGuide();
            }
            else
                SGame.UIUtils.OpenUI("fightlevel", DataCenter.Instance.battleLevelData.showLevel);
        }

        public void PlayFightLimitGuide() 
        {
            m_view.m_finger.visible = true;
            if (m_view.m_finger.m_play.playing)
                return;
            m_view.m_finger.m_play.Play(CloseFightLimitGuide);
        }

        public void CloseFightLimitGuide() 
        {
            m_view.m_finger.m_play.Stop();
            m_view.m_finger.visible = false;
        }

        void TriggerBattle() 
        {
            _battle = EnterBattle().Start();
        }

        public IEnumerator EnterBattle()
        {
            if (BattleManager.Instance.isCombat) yield break;
            43.ToAudioID().PlayAudio();
            BattleManager.Instance.isCombat = true;
            m_view.m_fightBtn.visible = false;

            SetBaseInfo();
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
            //yield return new WaitForSeconds(0.5f);
  
            _battleMonster = new BattleMonster(_monster, m_view.m_fightHp2);
            _battleMonster.LoadAttribute(_monsterCfgId);
            _battleMonster.model.Play("idle");

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
            if(_show) 42.ToAudioID().PlayAudio();
            m_view.m_fightBtn.visible = !DataCenter.BattleLevelUtil.IsMax;
            m_view.m_roundGroup.visible = false;
            m_view.m_battlemonster.x = m_view.m_mholder.x;

            ShowBattleResult();
            SetBaseInfo();
            RefreshFightLevel();

            MapLoop(false);

            _battleRole.Dispose();
            _battleMonster.Dispose();
            _monster.Reset();
        }

        void PlayBattleAudio(int audioId) 
        {
            if (!_show) return;
            audioId.ToAudioID().PlayAudio();
        }

        public void RefreshFightLevel() 
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(DataCenter.Instance.battleLevelData.level, out var config))
            {
                ConfigSystem.Instance.TryGet<GameConfigs.BattleRoleRowData>(config.Monster, out var roleConfig);
                m_view.m_fightBtn.SetText(UIListener.Local(config.Name));
                var battleLevelConfig = DataCenter.BattleLevelUtil.battleLevelConfig;
                DataCenter.BattleLevelUtil.UpdateUnlockFightLevel();
                if (!DataCenter.BattleLevelUtil.GetUnlock())
                {
                    m_view.m_fightBtn.m_state.selectedIndex = 0;
                    m_view.m_fightBtn.m_fight.SetText(Utils.ConvertNumberStrLimit3(battleLevelConfig.CombatNum));
                }
                else if (DataCenter.Instance.battleLevelData.GetCdTime() > 0)
                {
                    m_view.m_fightBtn.m_state.selectedIndex = 2;
                    Utils.Timer(DataCenter.Instance.battleLevelData.GetCdTime(), () =>
                    {
                        var totalTime = battleLevelConfig.ChallengeCd;
                        var cdTime = DataCenter.Instance.battleLevelData.GetCdTime();
                        m_view.m_fightBtn.m_bar.fillAmount = (float)(totalTime - cdTime)/ totalTime;
                        m_view.m_fightBtn.m_time.SetText(Utils.FormatTime(cdTime));
                    }, m_view, completed: RefreshFightLevel);
                }
                else 
                {
                    m_view.m_fightBtn.m_state.selectedIndex = 1;
                    m_view.m_fightBtn.m_bar.fillAmount = 1;
                }

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

            RefreshFightLevel();
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
            yield return new WaitForSeconds(0.1f);
            if (BattleManager.Instance.isCombat) 
            {
                43.ToAudioID().PlayAudio();
                MapLoop(true);
            };
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

