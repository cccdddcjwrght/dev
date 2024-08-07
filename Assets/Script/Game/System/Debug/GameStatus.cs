using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGame
{
    public class GameStatus : MonoBehaviour
    {
        private List<Character> m_characters = new List<Character>();
        
        public Vector2 overlayPos = Vector2.zero;
        public Color backgroundColor = Color.black;
        public Color playerColor = Color.red;
        public Color waiterColor = Color.green;
        public Color cookerColor = Color.blue;
        public Color customerColor = Color.white;

        // 是否显示订单信息
        private bool showOrder = false;
        
        // 是否显示座椅信息
        private bool showTable = false;

        void Start()
        {
            Game.console.AddCommand("debug", Cmd, "debug [order|table]");
        }

        void Cmd(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                this.enabled = !this.enabled;
                return;
            }

            switch (args[0])
            {
                case "order":
                    this.enabled = true;
                    showOrder = !showOrder;
                    break;
                
                case "table":
                    this.enabled = true;
                    showTable = !showTable;
                    break;
            }
    }

        Color GetColor(int roleType)
        {
            EnumRole r = (EnumRole)roleType;
            switch (r)
            {
                case EnumRole.Player:
                    return playerColor;
                case EnumRole.Waiter:
                    return waiterColor;
                case EnumRole.Cook:
                    return cookerColor;
                case EnumRole.Customer:
                    return customerColor;
            }

            return customerColor;
        }

        void ShowCharacterInfo(Character character)
        {
            var pos = character.transform.position;
            Vector2 ovpos = Utils.Cover3DToOverlayPos(pos);
            DebugOverlay.SetOrigin(ovpos.x, ovpos.y - 2);
            DebugOverlay.SetColor(GetColor(character.roleType));
            
            DebugOverlay.DrawRect(-3, 0, 7, 2, backgroundColor);
            DebugOverlay.Write(-3, 0, "ID:{0,-4}", character.CharacterID);
            DebugOverlay.Write(-3, 1, "T:{0,-4}", character.taskNum);
        }

        void ShowTableInfo(TableData t)
        {
            
        }
        
        // Update is called once per frame
        void Update()
        {
            if (showOrder)
            {
                if (CharacterModule.Instance.isInit && CharacterModule.Instance.FindCharacters(m_characters, (c) => true))
                {
                    Vector2 oldOrigin = DebugOverlay.GetOrigin();
                    foreach (var c in m_characters)
                        ShowCharacterInfo(c);
                    DebugOverlay.SetOrigin(oldOrigin.x, oldOrigin.y);
                }
            }

            if (showTable)
            {
                foreach (var t in TableManager.Instance.Datas)
                {
                    ShowTableInfo(t);
                }
            }
            
        }
    }
}