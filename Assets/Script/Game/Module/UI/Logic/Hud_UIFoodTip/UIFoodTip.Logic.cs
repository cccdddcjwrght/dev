
using Unity.Entities;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UIFoodTip
	{
		private Entity _foodTipEntity;
		private double _value;
		partial void InitLogic(UIContext context)
		{
			var param = context.gameWorld.GetEntityManager().GetComponentData<UIParam>(context.entity);
			_foodTipEntity = (Entity)param.Value;
			//m_view.m_title.text = Utils.ConvertNumberStr(value);
			context.window.xy = new Vector2(999999f, 99999f);
			_value = 0;
			context.onUpdate += OnUpdate;
		}

		void OnUpdate(UIContext context)
		{
			var entityManager = context.gameWorld.GetEntityManager();
			if (entityManager.Exists(_foodTipEntity) && entityManager.HasComponent<FoodTips>(_foodTipEntity))
			{
				var foodTip = entityManager.GetComponentData<FoodTips>(_foodTipEntity);
				if (_value != foodTip.gold)
				{
					_value = foodTip.gold;
					m_view.m_title.text = Utils.ConvertNumberStrLimit3(_value);
				}
			}
		}
		
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
