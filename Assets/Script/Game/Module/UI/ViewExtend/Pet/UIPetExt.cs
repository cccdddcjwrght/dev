using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using FairyGUI;
using GameConfigs;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame.UI.Pet
{

	static public partial class UIPetExt
	{

		static private string GetQName(int quality)
		{
			switch ((EnumQualityType)quality)
			{
				case EnumQualityType.Red:
					return $"ui_quality_name_{(int)EnumQuality.Red}".Local();
				case EnumQualityType.Orange:
					return $"ui_quality_name_{(int)EnumQuality.Orange}".Local();
				case EnumQualityType.Max:
					return $"ui_quality_name_{(int)EnumQuality.Max}".Local();
				default:
					return $"ui_quality_name_{quality}".Local();
			}
		}

		static public void SetPet(this GObject gObject, PetsRowData cfg, bool setq = true)
		{
			if (gObject != null && cfg.IsValid())
			{
				if (gObject is GComponent com)
				{
					com.SetTextByKey(cfg.Name);
					com.SetIcon(cfg.Icon);
					if (setq)
					{
						UIListener.SetControllerSelect(com, "quality", cfg.Quality);
						UIListener.SetTextWithName(com, "qname", GetQName(cfg.Quality));
					}
				}
			}
		}

		static public void SetPet(this GObject gObject, PetItem pet, bool hidered = false, bool showbuff = false, string nameappend = null)
		{
			if (gObject != null)
			{
				if (pet != null && pet.cfgID > 0)
				{
					nameappend = nameappend + pet.name.Local();
					gObject.SetIcon(pet.icon);
					gObject.SetText(nameappend, false);
					var count = pet.level > 0 ? pet.level : pet.count;
					UIListener.SetControllerSelect(gObject, "quality", pet.quality);
					UIListener.SetControllerSelect(gObject, "selected", pet.isselected ? 1 : 0, false);
					UIListener.SetTextWithName(gObject, "count", count == 0 ? "" : (pet.level > 0 ? "" : "x") + count.ToString(), false);
					UIListener.SetControllerSelect(gObject, "notempty", count > 0 ? 1 : 0, false);

					UIListener.SetTextWithName(gObject, "model", nameappend, false);
					UIListener.SetTextWithName(gObject, "qname", GetQName(pet.quality));

					if (!hidered) UIListener.SetControllerSelect(gObject, "__redpoint", pet.isnew, false);
					if (pet.type == 0)
					{
						if (showbuff)
						{
							var list = gObject.asCom?.GetChild("bufflist")?.asList;
							if (list != null)
							{
								list.RemoveChildrenToPool();
								var effects = pet.GetEffects(true, true,true);
								SGame.UIUtils.AddListItems(list, effects, (i, d, g) =>
								{
									g.SetBuffItem(effects[i], i != 0 ? pet.quality : 0, false, (1 << i).IsInState(pet.evo) ? 1 : 0);
								}, ignoreNull: true, useIdx: true);
							}
						}
					}

				}
				else
				{
					gObject.SetText(null, false);
					UIListener.SetControllerSelect(gObject, "quality", 1);
					gObject.asCom?.GetChild("bufflist")?.asList?.RemoveChildrenToPool();
				}
			}
		}

		static public GObject SetCurrency(this GObject gObject,
			int itemID, string name = null, string str = default,
			string iconCtr = default, bool listen = true,
			EventCallback1 addCall = null
		)
		{
			if (gObject != null)
			{
				if (ConfigSystem.Instance.TryGet<ItemRowData>(itemID, out var cfg))
				{
					var g = gObject;
					var s = !string.IsNullOrEmpty(str);
					if (!string.IsNullOrEmpty(name) && gObject is GComponent c)
						g = c.GetChildByPath(name) ?? g;

					if (!string.IsNullOrEmpty(iconCtr)) UIListener.SetControllerName(gObject, iconCtr, itemID.ToString(), false);
					else g.SetIcon(cfg.Icon);
					g.SetText(s ? str : Utils.ConvertNumberStr(PropertyManager.Instance.GetItem(itemID).num), false);
					if (addCall != null)
						UIListener.Listener(gObject, addCall);

					if (!s && listen)
					{
						var itid = itemID;
						void Refresh(int id, double num, double old)
						{
							if (itid == id)
								g.SetText(Utils.ConvertNumberStr(num), false);
						}

						void OnListen()
						{
							g.onRemovedFromStage.Clear();
							g.RemoveEventListener("__currency", OnListen);
							PropertyManager.Instance.GetGroup(1).onValueUpdate -= Refresh;
						}

						g.DispatchEvent("__currency");
						g.AddEventListener("__currency", OnListen);

						g.onRemovedFromStage.Clear();
						g.onRemovedFromStage.Add(OnListen);
						PropertyManager.Instance.GetGroup(1).onValueUpdate += Refresh;
					}

				}
			}
			return gObject;
		}
	}

	partial class UI_SimplePetModel
	{
		private PetItem pet;

		private GoWrapper goWrapper;
		private SwipeGesture swipe;
		private Animator animation;

		public Action onModelLoaded;
		public float scaleVal = 300;

		public UI_SimplePetModel SetPetInfo(PetItem pet, bool focusrefresh = false, float delay = 0, string animationName = null)
		{
			if (!focusrefresh && this.pet != null && this.pet.cfgID == pet.cfgID) return this;
			this.pet = pet;
			if (swipe == null)
			{
				swipe = new SwipeGesture(this);
				swipe.onMove.Add(OnTouchMove);
				goWrapper = new GoWrapper();
				m_holder.SetNativeObject(goWrapper);
			}
			RefreshModel(delay, animationName);

			return this;
		}

		public void Release()
		{
			goWrapper?.Dispose();
			swipe?.Dispose();
			goWrapper = null;
			swipe = null;
			pet = null;
			animation = null;
		}

		public override void Dispose()
		{
			base.Dispose();
			Release();
		}

		public UI_SimplePetModel RefreshModel(float delay = 0, string animationName = null)
		{
			CreateRole(delay, animationName).Start();
			return this;
		}

		public UI_SimplePetModel Play(string clip)
		{
			if (animation && !string.IsNullOrEmpty(clip))
			{
				animation.Play(clip);
			}
			return this;
		}

		IEnumerator CreateRole(float delay = 0, string animationName = null)
		{
			yield return null;

			var path = "Assets/BuildAsset/Prefabs/Pets/" + pet.cfg.Resource;
#if UNITY_EDITOR
			if (!File.Exists(path + ".prefab"))
			{
				Debug.LogError($"宠物模型资源不存在:{path},将使用临时资源");
				path = "Assets/BuildAsset/Prefabs/Pets/mouse";
			}
#endif
			if (delay > 0)
				yield return new WaitForSeconds(delay);
			var wait = SpawnSystem.Instance.SpawnAndWait(path);
			yield return wait;
			var go = wait.Current as GameObject;
			if (go)
			{
				if (goWrapper != null)
				{
					var old = goWrapper.wrapTarget;
					if (old) GameObject.Destroy(old);
					goWrapper.SetWrapTarget(go, false);
					go.SetActive(false);
					go.transform.localScale = Vector3.one * scaleVal;
					go.transform.localPosition = new Vector3(0, 0, -100);
					go.transform.localRotation = Quaternion.Euler(8, -145, 8);
					go.SetLayer("UILight");
					go.SetActive(true);
					onModelLoaded?.Invoke();
					animation = go.GetComponent<Animator>();
					animation?.Play(animationName ?? "idle");
					yield break;
				}
				GameObject.Destroy(go);
			}
		}


		void OnTouchMove(EventContext context)
		{
			if (goWrapper == null || goWrapper.isDisposed) return;
			goWrapper.wrapTarget.transform.Rotate(Vector3.up, -swipe.delta.x);
		}

	}

}
