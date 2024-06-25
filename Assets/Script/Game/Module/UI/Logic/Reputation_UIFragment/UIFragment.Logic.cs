
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
    using System.Collections.Generic;

    public partial class UIFragment
	{
		private List<ItemData.Value> _args;
		
		partial void InitLogic(UIContext context){
			var a = context.GetParam()?.Value as object[];
			_args = a.Val<List<ItemData.Value>>(0);

			m_view.m_list.itemRenderer = OnItemRenderer;
			m_view.m_list.numItems = _args.Count;

			Utils.Timer(0.75f, null, m_view, completed: () =>
			{
				 EffectSystem.Instance.AddEffect(32, m_view.m_effect);
			});
			
		}

		void OnItemRenderer(int index, GObject gObject) 
		{
			gObject.SetIcon(Utils.GetItemIcon((int)_args[index].type, _args[index].id));
			gObject.SetText("x" + _args[index].num);
		}

        partial void OnBtnClick(EventContext data)
        {
			SGame.UIUtils.CloseUIByID(__id);
		}

        partial void UnInitLogic(UIContext context){

		}
	}
}
