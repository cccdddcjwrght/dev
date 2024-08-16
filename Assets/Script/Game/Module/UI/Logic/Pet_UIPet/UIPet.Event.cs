
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;

	public partial class UIPet
	{
		partial void InitEvent(UIContext context)
		{

			EventManager.Instance.Reg(((int)GameEvent.PET_LIST_REFRESH), OnPetListRefresh);
			EventManager.Instance.Reg<PetItem, bool>(((int)GameEvent.PET_FOLLOW_CHANGE), OnPetFollowChange);
			EventManager.Instance.Reg<PetItem, int>(((int)GameEvent.PET_REFRESH), OnPetRefresh);
			EventManager.Instance.Reg<string>(((int)GameEvent.UI_HIDE), OnUIClose);

			//context.window.needCache = true;
		}

		partial void UnInitEvent(UIContext context)
		{
			EventManager.Instance.UnReg(((int)GameEvent.PET_LIST_REFRESH), OnPetListRefresh);
			EventManager.Instance.UnReg<PetItem, bool>(((int)GameEvent.PET_FOLLOW_CHANGE), OnPetFollowChange);
			EventManager.Instance.UnReg<PetItem, int>(((int)GameEvent.PET_REFRESH), OnPetRefresh);
			EventManager.Instance.UnReg<string>(((int)GameEvent.UI_HIDE), OnUIClose);
		}

		void OnPetListRefresh()
		{
			RefreshList(true, type: 1);
		}

		void OnPetRefresh(PetItem pet, int type)
		{
			OnPetTab();
			RefreshList(type: 1);
		}

		void OnPetFollowChange(PetItem pet, bool state)
		{
			OnPetTab();
			RefreshList(type:1);
		}

		void OnUIClose(string name)
		{
			if(name == "shopui")
			{
				this.Delay(()=>OnTabChanged(null),1);
			}
		}

	}
}
