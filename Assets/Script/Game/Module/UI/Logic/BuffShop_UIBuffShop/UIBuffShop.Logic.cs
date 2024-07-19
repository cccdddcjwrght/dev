
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.BuffShop;
    using System.Collections.Generic;
    using GameConfigs;

    public partial class UIBuffShop
	{
		List<int> randomIds;
		List<int> fixedIds;

		int m_Height = -100;
		bool playing = false;
		float moveY = GlobalDesginConfig.GetFloat("buffshop_lottery_move_y", 2f);
		int whirlCount = GlobalDesginConfig.GetInt("buffshop_lottery_whirl_count", 2);
		float duration = GlobalDesginConfig.GetFloat("buffshop_lottery_duration", 1.5f);

		BuffShopData m_forceData;
		EventHandleContainer m_Event = new EventHandleContainer();

		partial void InitLogic(UIContext context){
			context.onUpdate += Update;

			Init();
			RefreshData();
			LocalScrollPosY();
		}

		void Init() 
		{
			m_view.m_lotteryList.SetVirtualAndLoop();
			m_view.m_lotteryList.itemRenderer = OnLotteryItemRenderer;

			m_view.m_shopBuffList.itemRenderer = OnShopBuffItemRenderer;
			m_view.m_tip.onClick.Add(OnTipBtnClick);

			m_Event += EventManager.Instance.Reg((int)GameEvent.BUFFSHOP_REFRESH, RefreshData);
		}

		public void RefreshData() 
		{
			m_view.m_total.SetTextByKey("ui_boostshop_tips1", ReputationModule.Instance.GetTotalValue());

			var good = DataCenter.Instance.shopData.goodDic[BuffShopModule.Instance.GetRandomShopId()];
			DoRefreshGoodsInfo(good, m_view.m_lotteryBtn);

			m_forceData = BuffShopModule.Instance.GetForceRandomBuffData();
			DoRefreshBuffInfo(m_forceData, m_view.m_time, m_view.m_time_bg, m_view.m_time_icon);

			randomIds = BuffShopModule.Instance.GetBuffList(1);
			fixedIds = BuffShopModule.Instance.GetBuffList(2);
			m_view.m_lotteryList.numItems = randomIds.Count;
			m_view.m_shopBuffList.numItems = fixedIds.Count;
		}

		void OnLotteryItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_BuffLotteryItem)gObject;
			var cfgId = randomIds[index];
			var buffShopConfig = BuffShopModule.Instance.GetConfig(cfgId);
			
			ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(buffShopConfig.BuffId(0), out var buffConfig);
			item.SetTextByKey(buffShopConfig.BuffDes, buffShopConfig.BuffId(1), buffShopConfig.BuffTime);
			item.SetIcon(buffConfig.Icon);

			if (m_forceData?.GetTime() > 0 && !playing) item.m_select.selectedIndex = m_forceData.cfgId == cfgId ? 1 : 0;
			else item.m_select.selectedIndex = 0;

		}

		void OnShopBuffItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_BuffShopItem)gObject;
			var buffShopCfgId = fixedIds[index];
			var buffShopConfig = BuffShopModule.Instance.GetConfig(buffShopCfgId);

			ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(buffShopConfig.BuffId(0), out var buffConfig);
			item.m_des.SetTextByKey(buffShopConfig.BuffDes, buffShopConfig.BuffId(1), buffShopConfig.BuffTime);
			item.m_value.SetText("x" + (1 + buffShopConfig.BuffId(1) * 0.01f));
			item.SetIcon(buffConfig.Icon);

			DataCenter.Instance.boostShopData.buffDict.TryGetValue(buffShopCfgId, out var buff);
			DoRefreshBuffInfo(buff, item.m_time, item.m_time_bg, null);

			var good = DataCenter.Instance.shopData.goodDic[buffShopConfig.ShopId];
			DoRefreshGoodsInfo(good, item.m_click);

			item.m_click.onClick.Set(()=>RequestExcuteSystem.BuyGoods(buffShopConfig.ShopId, (success)=> 
			{
				if (!success) return;
				BuffShopModule.Instance.AddBoostShopBuff(buffShopCfgId);
			}));
		}

		void DoRefreshBuffInfo(BuffShopData data, GTextField text, GImage border, GImage icon) 
		{
			var time = data !=null ? data.GetTime() : 0;
			text.SetText(Utils.FormatTime(time), false);
			if (icon != null) icon.visible = time > 0;
			if (border != null) border.visible = time > 0;
			text.visible = time > 0;

			if (time > 0) 
			{
				Utils.Timer(time, () =>
				{
					text.SetText(Utils.FormatTime(data.GetTime()), false);
				}, m_view, completed: () => DoRefreshBuffInfo(data, text, border, icon));
			}
		}

		void DoRefreshGoodsInfo(ShopGoods data, GObject gObject)
		{
			var g = data;
			var v = gObject as UI_ShopBuffBtn;
			var time = g.CDTime();

			v.m_saled.selectedIndex = 0;
			if (g.free > 0)
			{
				v.m_currency.selectedIndex = 0;
				v.m_price.SetTextByKey("ui_shop_free");
			}
			else
			{
				switch (g.cfg.PurchaseType)
				{
					case 1:
						v.enabled = NetworkUtils.IsNetworkReachability();
						v.m_currency.selectedIndex = 3;
						v.m_price.SetText((g.cfg.LimitNum - g.buy) + "/" + g.cfg.LimitNum);
						break;
					case 2:
						v.m_currency.selectedIndex = 2;
						v.m_price.SetText(g.cfg.Price.ToString());
						break;
					case 3:
						v.m_currency.selectedIndex = 4;
						v.m_price.SetText(g.cfg.Price.ToString());
						break;
				}
			}
			if (time > 0)
			{
				v.m_time.SetText(Utils.FormatTime(time), false);
				Utils.Timer(time, () =>
				{
					v.m_time.SetText(Utils.FormatTime(g.CDTime()), false);
				}, gObject, completed: () => DoRefreshGoodsInfo(data, gObject));
			}

			v.m_cd.selectedIndex = time > 0 ? 1 : 0;
			v.m_saled.selectedIndex = g.IsSaled() ? 1 : 0;
		}

		void Update(UIContext context) 
		{
            if (m_forceData?.GetTime() > 0) return;
            var posY = m_view.m_lotteryList.scrollPane.posY;
            m_view.m_lotteryList.scrollPane.SetPosY(posY - moveY, false);
        }

        partial void OnLotteryBtnClick(EventContext data)
        {
            RequestExcuteSystem.BuyGoods(BuffShopModule.Instance.GetRandomShopId(), (success) =>
            {
                if (!success) return;
                var cfgId = BuffShopModule.Instance.GetRandomBuff();
                PlayLotteryAnim(cfgId);

                BuffShopModule.Instance.AddBoostShopBuff(cfgId);
            });
        }

		void PlayLotteryAnim(int cfgId) 
		{
			playing = true;
			int index = cfgId;
			float pos_y = randomIds.Count * m_Height * 1 + (randomIds.Count - index + 1) * m_Height + m_Height * 0.5f; ;
			GTween.To(0, pos_y, duration).SetTarget(m_view).OnUpdate((t) =>
			{
				m_view.m_lotteryList.scrollPane.SetPosY((float)t.value.d, false);
			}).OnComplete(() => 
			{
				playing = false;
				RefreshData();
			});
		}

		void LocalScrollPosY() 
		{
			if (m_forceData?.GetTime() > 0) 
			{
				int index = m_forceData.cfgId;
				float pos_y = (randomIds.Count - index + 1) * m_Height + m_Height * 0.5f;
				m_view.m_lotteryList.scrollPane.SetPosY(pos_y, false);
			}
		}

		void OnTipBtnClick() 
		{
			SGame.UIUtils.OpenUI("totalBoost");
		}

        partial void UnInitLogic(UIContext context){
			context.onUpdate -= Update;

			m_Event.Close();
			m_Event = null;
		}
	}
}
