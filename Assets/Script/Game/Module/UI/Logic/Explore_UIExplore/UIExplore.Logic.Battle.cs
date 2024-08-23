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

        private int _monsterCfgId;
        private UIModel _monster;

        public void InitBattle() 
        {
            eventHandle += EventManager.Instance.Reg((int)GameEvent.BATTLE_START, TriggerBattle);
            eventHandle += EventManager.Instance.Reg((int)GameEvent.BATTLE_OVER, ExitBattle);
            onClose += (UIContext context)=> BreakOffBattle();

            m_view.m_fightBtn.visible = !DataCenter.BattleLevelUtil.IsMax;
        }

        partial void OnFightBtnClick(EventContext data)
        {
            SGame.UIUtils.OpenUI("fightlevel", DataCenter.Instance.battleLevelData.showLevel);
        }

        void TriggerBattle() 
        {
            EnterBattle().Start();
        }

        public IEnumerator EnterBattle()
        {
            if (BattleManager.Instance.isCombat) yield break;

            m_view.m_fightBtn.visible = false;
            _battleRole = new BattleRole(_model,m_view.m_fightHp1);
            _battleRole.LoadAttribute(DataCenter.Instance.exploreData.explorer.roleID);
            m_view.m_battlemonster.xy = m_view.m_mholder.xy;

            ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(DataCenter.Instance.battleLevelData.showLevel, out var config);
            _monsterCfgId = config.Monster;

            if (_monster == null)
            {
                _monster = new UIModel(m_view.m_battlemonster)
                    .SetData(0)
                    .SetTRS(Vector3.zero, new Vector3(0, -110, 45), 30)
                    .SetLoad(LoadMonsterModel);
            }
            else
            {
                _monster.SetData(0).
                    SetTRS(Vector3.zero, new Vector3(0, -110, 45), 30).
                    SetLoad(LoadMonsterModel);
            }
            _monster.RefreshModel();

            _battleMonster = new BattleMonster(_monster, m_view.m_fightHp2);
            _battleMonster.LoadAttribute(_monsterCfgId);
            yield return _battleMonster.Move(1, 330, 2);

            BattleManager.Instance.BattleStart(_battleRole, _battleMonster);
        }

        public void ExitBattle()
        {
            m_view.m_fightBtn.visible = !DataCenter.BattleLevelUtil.IsMax;

            _battleRole.Dispose();
            _battleMonster.Dispose();
            _monster.Reset();
        }

        public void BreakOffBattle()
        {
            BattleManager.Instance.DiscontinuePlayRound();
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

