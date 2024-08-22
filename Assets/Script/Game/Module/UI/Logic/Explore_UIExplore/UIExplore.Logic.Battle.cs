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

        private UIModel _monster;

        public void InitBattle() 
        {
            eventHandle += EventManager.Instance.Reg((int)GameEvent.BATTLE_OVER, ExitBattle);
            onClose += (UIContext context)=> BreakOffBattle();
        }

        partial void OnFightBtnClick(EventContext data)
        {
            EnterBattle().Start();
        }

        public IEnumerator EnterBattle()
        {
            if (BattleManager.Instance.isCombat) yield break;

            _battleRole = new BattleRole(_model);

            m_view.m_battlemonster.xy = m_view.m_mholder.xy;

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

            _battleMonster = new BattleMonster(_monster);
            _battleMonster.LoadAttribute(1);
            yield return _battleMonster.Move(1, 315, 2);

            BattleManager.Instance.BattleStart(_battleRole, _battleMonster);
        }

        public void ExitBattle()
        {
            _battleRole.Dispose();
            _battleMonster.Dispose();
            _monster.Reset();
        }

        public void BreakOffBattle()
        {
            BattleManager.Instance.DiscontinuePlayRound();
        }

        //
        public IEnumerator LoadMonsterModel(object data)
        {
            var modelName = "boss-2";
            var path = "Assets/BuildAsset/Prefabs/Monster/" + modelName;
#if UNITY_EDITOR
            if (!File.Exists(path + ".prefab"))
            {
                Debug.LogError($"模型资源不存在:{path},将使用临时资源");
                path = "Assets/BuildAsset/Prefabs/Monster/boss-2";
            }
#endif
            return SpawnSystem.Instance.SpawnAndWait(path);
        }
    }
}

