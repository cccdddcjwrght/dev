
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.MonsterHunter;
	using System;
	using SGame.UI.Common;
	using System.Collections.Generic;
	using SGame.UI.Pet;
	using GameConfigs;
	using System.Collections;
	using System.Linq;

	public struct AttackInfo
	{
		public int power;
		public int bullet;
		public int attack;
		public int step;
		public List<int> steps;
	}

	public partial class UIMonsterHunter
	{
		private bool _isInited;

		private int _actID;
		private Hunter _actData;
		private HunterMonster _monster;

		private Action _onRefreshMonsterList;
		private Action<bool> _timer;
		private List<GameConfigs.HunterWheelRowData> _wheels;
		private int[] weights;
		private int[] powers;

		private float wheel_start_time = 0;
		private float wheel_run_time = 0;
		private float wheel_loop_count = 0;

		private bool _showRewardPre;
		private bool _clickui;
		private bool _isRuning;
		private bool _attackFlag;
		private bool _attaclLoop;
		private bool _isFighting;
		private UnityEngine.Coroutine _logic;

		private GoWrapper goWrapper;
		private Animator _modelAnimator;

		const string c_dead_name = "dead";
		const string c_hit_name = "hit";
		const string c_born_name = "born";


		#region Init

		partial void InitLogic(UIContext context)
		{
			m_view.m_bg.z = 600;
			m_view.m_monster.z = 600;


			context.window.contentPane.fairyBatching = false;
			var args = context.GetParam()?.Value.To<object[]>();
			if (args != null)
			{
				_actID = args.Val<int>(0);
				_actData = DataCenter.HunterUtil.GetHunter(_actID);
			}

			if (_actData == null)
			{
				"@ui_hunter_not_find".Tips();
				DoCloseUIClick(null);
				return;
			}
			wheel_start_time = GlobalDesginConfig.GetFloat("hunter_wheel_start_time", 0.4f);
			wheel_run_time = GlobalDesginConfig.GetFloat("hunter_wheel_run_time", 1f);
			wheel_loop_count = GlobalDesginConfig.GetInt("hunter_wheel_loop_count", 5);
			powers = GlobalDesginConfig.GetIntArray("hunter_bet");
			_actData.power = Math.Clamp(_actData.power, 0, powers.Length - 1);
			_actData.Refresh();
			SetInfo();
			_logic = AttackLogic().Start();

			m_view.onClick.Add(OnUIClick);
			_clickui = true;
			_isInited = true;
			goWrapper = new GoWrapper();
			m_view.m_monster.SetNativeObject(goWrapper);

			m_view.m_panel.m_playbtn.SetIcon(_actData.item.Icon);

		}

		partial void UnInitLogic(UIContext context)
		{
			Timer(-1);
			_logic.Stop();
			PropertyManager.Instance.CombineCache2Items();
			RemoveModel(0);
			goWrapper?.Dispose();
			goWrapper = null;
		}

		void OnUIClick(EventContext e)
		{
			if (m_view == null) return;
			if (!_clickui)
			{
				e.StopPropagation();
				_clickui = true;
			}
			if (m_view.m_rewardsPreview != null)
			{
				if (!_showRewardPre && m_view.m_rewardsPreview.visible)
					m_view.m_rewardsPreview.visible = false;
				else
					_showRewardPre = false;
			}
		}

		#endregion

		#region SetInfo

		void SwitchMonster()
		{
			_monster = _actData.monster;
			RefreshMonster();
		}

		void SetInfo()
		{
			SetBaseInfo();
			SwitchMonster();
			SetSelectPanel();
			SetPower();
		}

		void RefreshMonster()
		{
			SetMonsterInfo();
			SetStepReward();
			SetMonsterList();
			ShowMonsterModel(true);
		}

		void SetBaseInfo()
		{
			Timer(1);
			m_view.m_panel.m_currency.SetCurrency(_actData.itemID, addCall: DoClickCurrencyClick);
		}

		void SetMonsterInfo()
		{
			var monster = _monster;

			if (monster != null)
			{
				SwitchTypePage(0);
				m_view.m_hp.max = monster.cfg.MonsterHP;
				m_view.m_hp.value = monster.cfg.MonsterHP - monster.progress;
			}
			else
			{
				SwitchTypePage(2);
			}
		}

		void SetStepReward()
		{
			var monster = _monster;
			var item1 = m_view.m_hp.m_item1;
			var item2 = m_view.m_hp.m_item2;
			item1.visible = false;
			item2.visible = false;

			if (monster != null)
			{
				var width = m_view.m_hp.width;
				item1.y = item2.y = 16;
				item1.scale = item2.scale = Vector3.one;
				item1.alpha = item2.alpha = 1;

				if (monster.cfg.MidHP1 > 0)
					MidRewardInfo(item1, width * monster.cfg.MidHP1 * 0.01f, monster.cfg.GetMidReward1Array(), monster.step > 0);
				if (monster.cfg.MidHP2 > 0)
					MidRewardInfo(item2, width * monster.cfg.MidHP2 * 0.01f, monster.cfg.GetMidReward2Array(), monster.step > 1);
			}
		}

		void SetMonsterList(bool ani = false)
		{
			if (!_isInited)
			{
				var ms = new List<HunterMonster>(_actData.monsters); ms.Reverse();
				m_view.m_list.RemoveChildrenToPool();
				_onRefreshMonsterList = null;
				SGame.UIUtils.AddListItems(m_view.m_list, ms, SetMonsterItem);
			}
			else
				_onRefreshMonsterList?.Invoke();
			if (_actData.index >= 0)
			{
				var index = m_view.m_list.numItems - 1 - _actData.index;
				if (index >= 0)
					m_view.m_list.ScrollToView(index, ani || _isInited);
			}
		}

		void SetSelectPanel()
		{
			var cfgs = _wheels = ConfigSystem.Instance.Finds<HunterWheelRowData>(c => true);
			var v = powers[_actData.power];
			weights = cfgs.Select(c => c.Weight).ToArray();
			foreach (var f in cfgs)
			{
				var item = m_view.m_panel.m_panel.m_panel.GetChild($"item{f.Id - 1}_x");
				item.SetText((v * f.WheelValue).ToString(), false);
				item.data = f.WheelValue;
				UIListener.SetControllerSelect(item, "type", f.Type - 1, false);
			}
		}

		void SetMonsterItem(int index, object data, GObject gObject)
		{
			var max = _actData.monsters.Count - 1;
			var val = data as HunterMonster;
			var view = gObject as UI_MonsterItem;
			view.SetIcon(val.cfg.MonsterIcon);
			view.m_isfirst.selectedIndex = index == _actData.monsters.Count - 1 ? 1 : 0;
			view.m_state.selectedIndex = (max - index).CompareTo(_actData.index) + 1;
			view.onClick?.Clear();
			view.onClick.Add(() => ShowRewardList(gObject, val));
			_onRefreshMonsterList += () =>
			{
				if (view != null && _actData != null)
				{
					view.m_isfirst.selectedIndex = index == _actData.monsters.Count - 1 ? 1 : 0;
					view.m_state.selectedIndex = (max - index).CompareTo(_actData.index) + 1;
				}
			};
		}


		void SetPower(bool refreshwheel = false)
		{
			var v = powers[_actData.power];
			var s = powers[_actData.power].ToString();
			m_view.m_panel.m_power.SetText("X" + s, false);
			m_view.m_panel.m_playbtn.SetText(s, false);
			m_view.m_panel.m_playbtn.grayed = _monster == null;
			if (refreshwheel)
			{
				var wheel = m_view.m_panel.m_panel.m_panel;
				foreach (var item in wheel.GetChildren())
				{
					if (item is GComponent com && com.data is int val)
						item.SetText((val * v).ToString());
				}
			}
		}


		void DoClickCurrencyClick(EventContext context)
		{
			SGame.UIUtils.OpenUI("tempshop", _actData.subID);
		}

		void MidRewardInfo(GObject gObject, float x, int[] reward, bool get = false)
		{
			gObject.SetCommonItem(null, reward);
			gObject.x = x;
			gObject.visible = true;
			gObject.grayed = get;
		}

		#endregion

		#region Timer
		void Timer(int flag = 0)
		{
			var time = ActiveTimeSystem.Instance.GetLeftTime(_actID, GameServerTime.Instance.serverTime);
			switch (flag)
			{
				case 1:
					_timer?.Invoke(false);
					_timer = Utils.Timer(time, () => Timer(0), m_view, completed: TimeOver);
					break;
				case -1:
					_timer?.Invoke(false);
					_timer = null;
					break;
				default:
					var t = (m_view.m_body as UI_HeadTitle);
					if (time > 0)
					{
						t.m_timestate.selectedIndex = 1;
						t.m_time.SetText(Utils.FormatTime(time), false);
					}
					else
					{
						t.m_timestate.selectedIndex = 2;
						t.m_time.SetTextByKey("ui_hunter_time_over");
						Timer(-1);
					}
					break;
			}
		}

		void TimeOver()
		{

		}
		#endregion

		#region Attack

		AttackInfo Attack()
		{
			var power = powers[_actData.power];
			if (Utils.CheckItemCount(_actData.itemID, power))
			{
				var select = SGame.Randoms.Random._R.NextWeight(weights);
				var attack = new AttackInfo()
				{
					bullet = select,
					power = power,
					attack = _wheels[select].WheelValue * power
				};

				PropertyManager.Instance.Update(1, _actData.itemID, power, true);
				_actData.Attack(attack.attack, out attack.steps);
				return attack;
			}
			else
			{
				DoClickCurrencyClick(null);
			}
			return default;
		}

		IEnumerator AttackLogic()
		{
			var wait = new WaitForSeconds(0.5f);
			_isRuning = true;
			while (_isRuning)
			{
				if (!_attackFlag)
					yield return null;
				else
				{
					_attackFlag = _attaclLoop;
					_isFighting = true;
					var attack = Attack();
					if (attack.attack > 0)
					{
						yield return RandomSelect(attack.bullet);
						yield return AttackView(attack);
						yield return SwitchNextMonster(attack);
						_isFighting = false;
						if (_attaclLoop) yield return wait;
						else SwitchAutoPage(0);
					}
					else
					{
						_isFighting = false;
						SwitchAutoPage(0);
					}
				}
			}
		}

		IEnumerator AttackView(AttackInfo attack)
		{
			yield return null;
			yield return BulletAnimation(attack);
			SetMonsterInfo();
			if (attack.steps?.Count > 0)
			{
				if (attack.steps.Contains(1))
					RewardGetAnimation(m_view.m_hp.m_item1);
				if (attack.steps.Contains(2))
					RewardGetAnimation(m_view.m_hp.m_item2);
				if (_monster.IsKilled())
				{
					_modelAnimator?.Play(c_dead_name);
					RemoveModel(0.5f);
					yield return new WaitForSeconds(0.3f);
					yield return ShowKillReward(attack);
				}
				PropertyManager.Instance.CombineCache2Items();
			}
		}

		IEnumerator BulletAnimation(AttackInfo attack)
		{

			yield return null;
			var flytime = 0.2f;
			var view = m_view.m_panel.m_panel;
			var hold = view.m___effect;
			hold.position = view.m_start.position;
			view.m_play.selectedIndex = 0;

			var e = EffectSystem.Instance.AddEffect(GetBulletEffectID(_wheels[attack.bullet].Type), view);
			yield return EffectSystem.Instance.WaitEffectLoaded(e);
			view.m_fight.Play();
			yield return new WaitForSeconds(0.3f);
			view.m_play.selectedIndex = 1;
			hold.TweenMove(m_view.m_monster.TransformPoint(m_view.m_monster.size * 0.5f, hold.parent) + new Vector2(0, -20), flytime);

			yield return new WaitForSeconds(flytime);
			_modelAnimator?.Play(c_hit_name);
			yield return EffectSystem.Instance.WaitEffectLoaded(EffectSystem.Instance.AddEffect(401100, m_view));
			var g = SGame.UIUtils.AddListItem(m_view, null, res: "ui://MonsterHunter/HpHud");
			g.SetPivot(0.5f, 0.5f);
			g.pivotAsAnchor = true;
			g.position = m_view.m___effect.position;
			g.SetText("-" + attack.attack, false)
				.TweenMove(g.xy + UnityEngine.Random.insideUnitCircle * 100, 0.5f)
				.SetEase(EaseType.BackOut);
			GTween.To(0, 1, 1f).OnComplete(() => g.Dispose());
			view.m_play.selectedIndex = 0;

		}

		IEnumerator ShowKillReward(AttackInfo attack)
		{
			var list = DataCenter.HunterUtil.GetRewards(_monster.cfg);
			list.RemoveAll(x => x == null || x.Length == 0);
			m_view.m_reward.m_list.RemoveChildrenToPool();
			SwitchHunterRewardTips_TypePage(list.Count - 1);
			SwitchTypePage(0);
			SwitchTypePage(1);
			yield return new WaitForSeconds(0.25f);
			SGame.UIUtils.AddListItems(m_view.m_reward.m_list, list, (i, d, g) =>
			{
				var v = g as UI_HunterRewardItem;
				v.m_type.selectedIndex = list.Count - 1 - i;
				g.SetCommonItem(" +{0}", d as int[]);

			}, ignoreNull: true);
			yield return new WaitForSeconds(0.5f);
		}

		IEnumerator SwitchNextMonster(AttackInfo attack)
		{

			if (_monster.IsKilled())
			{
				_clickui = false;
				var time = 1f;
				yield return new WaitUntil(() => _clickui || (time -= Time.deltaTime) <= 0);
				_clickui = true;
				SwitchAutoPage(0);
				SwitchMonster();
				SetPower();
			}
		}

		void RewardGetAnimation(GObject gObject)
		{
			if (gObject != null && gObject.visible)
			{
				gObject.TweenScale(Vector3.one * 1.2f, 0.5f).SetEase(EaseType.BackOut);
				gObject.TweenFade(0, 0.5f).SetDelay(0.3f);
				gObject.TweenFade(1, 0.1f).SetDelay(0.8f).OnStart(() =>
				{
					gObject.scale = Vector3.one;
					gObject.grayed = true;
				});

			}
		}

		int GetBulletEffectID(int type)
		{

			switch (type)
			{
				case 2: return 401102;
				case 3: return 401103;
			}

			return 401101;

		}

		#endregion

		#region Override

		private bool _clickFlag;

		partial void OnHunterWheel_PlaybtnClick(EventContext data)
		{
			if (_monster == null)
			{
				"@ui_hunter_completed".Tips();
				return;
			}

			if (_clickFlag)
			{
				_clickFlag = false;
				return;
			}
			if (_attaclLoop)
			{
				_attaclLoop = false;
				_attackFlag = false;
				if (_isFighting)
					SwitchAutoPage(0);
			}
			else if (!_isFighting)
				_attackFlag = true;
		}

		partial void OnAutoChanged(EventContext data)
		{
			if (m_view != null)
			{
				var select = m_view.m_panel.m_playbtn.m_auto.selectedIndex;
				_attackFlag = _attaclLoop = _clickFlag = select == 1;
			}
		}

		partial void OnHunterWheel_PowerClick(EventContext data)
		{
			_actData.power = (_actData.power + 1) % powers.Length;
			SetPower(true);
		}

		partial void OnShowrewardClick(EventContext data)
		{
			ShowRewardList(m_view.m_showreward, _monster);
		}

		#endregion

		#region Private


		IEnumerator RandomSelect(int index)
		{
			LockBtn();
			var panel = m_view.m_panel.m_panel.m_panel;
			var rotation = panel.rotation % 360;
			var st = wheel_start_time;
			var et = wheel_run_time;
			var rcount = wheel_loop_count;

			panel.rotation = rotation;
			var r = 360;
			if (st > 0)
			{
				r = 360;
				panel.TweenRotate(r, st).SetEase(EaseType.QuadIn);
				yield return new WaitForSeconds(st * 0.9f);
			}
			panel.TweenRotate(r + rcount * 360 + index * 30, et);
			yield return new WaitForSeconds(et + 0.1f);
			LockBtn(false);
		}

		void LockBtn(bool state = true)
		{
			if (m_view != null)
				m_view.m_panel.m_power.touchable = !state;
		}

		void Lock(bool state = true, float time = 0, Action call = null)
		{
			const string key = "hunter";

			if (state)
			{
				if (time > 0)
				{
					LockWait(time, call).Start();
					return;
				}
				UILockManager.Instance.Require(key);
			}
			else
				UILockManager.Instance.Release(key);
		}

		public IEnumerator LockWait(float time, Action call = null)
		{
			Lock();
			yield return new WaitForSeconds(time);
			Lock(false);
			call?.Invoke();
		}

		private void ShowMonsterModel(bool ani = false)
		{
			CreateModel(ani).Start();
		}

		private void RemoveModel(float ani = 0f)
		{
			if (goWrapper != null && goWrapper.wrapTarget)
			{
				var target = goWrapper.wrapTarget;
				var scale = target.transform.lossyScale;
				var pos = target.transform.position;
				var rot = target.transform.rotation;
				goWrapper.SetWrapTarget(null, false);
				if (ani > 0)
				{
					target.transform.localScale = scale;
					target.transform.position = pos;
					target.transform.rotation = rot;

					GTween.To(scale, Vector3.zero, ani).OnUpdate(v =>
					{
						target.transform.localScale = v.value.vec3;
					}).OnComplete(() =>
					{
						if (target) GameObject.Destroy(target);
					}).SetEase(EaseType.BackIn);
				}
				else if (target) GameObject.Destroy(target);
			}
		}

		private void ShowRewardList(GObject gObject, HunterMonster monster, Vector2 offset = default)
		{
			if (gObject != null && monster != null)
			{
				void SetRewardInfo(int i, object data, GObject gObject)
				{
					gObject.SetCommonItem(null, data as int[]);
					UIListener.SetControllerSelect(gObject, "hidebg", 1, false);
				}
				var rs = DataCenter.HunterUtil.GetRewards(monster.cfg);
				if (rs?.Count > 0)
				{
					_showRewardPre = true;
					m_view.m_rewardsPreview.m_list.RemoveChildrenToPool();
					SGame.UIUtils.AddListItems(m_view.m_rewardsPreview.m_list, rs, SetRewardInfo, ignoreNull: true);
					m_view.m_rewardsPreview.m_size.selectedIndex = rs.Count;
					m_view.m_rewardsPreview.position = gObject.TransformPoint(gObject.size * 0.5f, m_view.m_rewardsPreview.parent) + new Vector2(0, 20) + offset;
					m_view.m_rewardsPreview.z = -300;
					m_view.m_rewardsPreview.visible = true;
				}
			}
		}

		IEnumerator CreateModel(bool ani = false, float time = 0)
		{
			_modelAnimator = null;
			if (_monster == null || !_monster.cfg.IsValid()) yield break;
			if (time <= 0) yield return null;
			else yield return new WaitForSeconds(time);

			if (ConfigSystem.Instance.TryGet(int.Parse(_monster.cfg.MonsterRes), out effectsRowData cfg))
			{
				var path = cfg.Prefab;
#if UNITY_EDITOR
				if (!System.IO.File.Exists(path))
				{
					Debug.LogError($"模型资源不存在:{path},将使用临时资源");
					path = "Assets/BuildAsset/Prefabs/Pets/mouse";
				}
#endif
				var wait = SpawnSystem.Instance.SpawnAndWait(path);
				yield return wait;
				var go = wait.Current as GameObject;
				if (go)
				{
					if (goWrapper != null)
					{
						RemoveModel();
						goWrapper.SetWrapTarget(go, false);
						var animator = _modelAnimator = go.GetComponent<Animator>();
						go.transform.localPosition = cfg.GetPositionArray().GetVector3();
						go.transform.localRotation = Quaternion.Euler(cfg.GetEulerAngleArray().GetVector3());
						go.SetLayer("UILight");
						animator?.Play("idle");
						var scale = cfg.GetScaleArray().GetVector3();
						if (ani)
						{
							GTween.To(Vector3.zero, scale, 0.5f).OnUpdate(v =>
							{
								if (m_view != null && go)
									go.transform.localScale = v.value.vec3;
							}).SetEase(EaseType.QuadIn);
							animator?.Play(c_born_name);
						}
						else
							go.transform.localScale = scale;
						yield break;
					}
					GameObject.Destroy(go);
				}
			}
		}


		#endregion

	}
}
