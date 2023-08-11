using System.Collections;
using SGame.UI;
using FairyGUI;
using Fibers;
using log4net;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
   
    public class UIMain : IUIScript
    {
        private static ILog log = LogManager.GetLogger("xl.gameui");
        private UINumber         uiGold;
        private UserData         userData;
        private GTextField       m_showText;
        private Fiber            m_numberEffect;
        private UIContext m_context;
        
        public void OnInit(UIContext context)
        {
            m_context = context;
            m_numberEffect = new Fiber(FiberBucket.Manual);
            context.onUpdate += onUpdate;
            var clickBtn = context.content.GetChildByPath("battle.icon").asButton;
            clickBtn.onClick.Add(OnClick);
            
            uiGold = new UINumber(context.content.GetChild("gold").asCom);
            userData = DataCenter.Instance.GetUserData();
            uiGold.Value = userData.gold;

            m_showText = context.content.GetChild("goldText").asTextField;
        }

        IEnumerator ShowNumberEffect(int num)
        {
            EntityManager mgr = m_context.gameWorld.GetEntityManager();
            if (mgr.Exists(userData.player) == false)
                yield break;

            float3 pos = mgr.GetComponentData<Translation>(userData.player).Value;
            pos.y += 1.0f;
            Vector3 screenPoint = GameCamera.camera.WorldToScreenPoint(pos);
            screenPoint.y = Screen.height - screenPoint.y;

            Vector2 uiPos = m_context.content.GlobalToLocal(screenPoint);
            //GRoot.inst.LocalToGlobal(screenPoint)
            m_showText.alpha = 0;
            float wait = 1.0f;
            m_showText.text = num.ToString();
            for (float r = 0; r < wait;)
            {
                // pos = mgr.GetComponentData<Translation>(userData.player).Value;
                //m_context.content.scree
                float per = r / wait;
                Vector2 runPos = uiPos;
                runPos.y -= per * 100;

                m_showText.xy = runPos;
                m_showText.alpha = math.clamp(per + 0.2f, 0, 1);
                r += Time.deltaTime;
                yield return null;
            }
            m_showText.alpha = 0;
            yield return null;
        }

        public static IUIScript Create() { return new UIMain(); }

        void OnClick()
        {
            EventManager.Instance.Trigger((int)GameEvent.PLAYER_ROTE_DICE);
        }

        private void onUpdate(UIContext context)
        {
            UserData cur = DataCenter.Instance.GetUserData();
            if (cur.gold != userData.gold)
            {
                int addvalue = cur.gold - userData.gold;
                userData = cur;
                uiGold.Value = userData.gold;
                m_numberEffect.Start(ShowNumberEffect(addvalue));
            }

            m_numberEffect.Step();
        }
    }
}
