using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	internal class BuffSystem : Singleton<BuffSystem>
	{
		static EnumTarget _TypeFollowLevel = EnumTarget.Machine | EnumTarget.Player | EnumTarget.Cook | EnumTarget.Waiter | EnumTarget.Customer | EnumTarget.Investor;

		private AttributeSystem attrSys { get { return AttributeSystem.Instance; } }

		#region Init
		public void Initalize()
		{
			//监听房间进入
			EventManager.Instance.Reg<int>(((int)GameEvent.BEFORE_ENTER_ROOM), OnEnterRoom);
			//监听buff添加
			EventManager.Instance.Reg<BuffData>(((int)GameEvent.BUFF_TRIGGER), OnBuffAdd);
			//全局
			InitGlobalAttribute();
		}

		public void ReInitRoomAttribute(int room)
		{
			attrSys.TypeUnRegister(_TypeFollowLevel);
			//重置当前关卡所有角色属性
			ReInitAllAttribute(room);
			//关卡科技生效
			DataCenter.RoomUtil.InitTechBuffs();
		}
		#endregion

		#region Private
		private void InitGlobalAttribute()
		{
			var attr = attrSys.GetAttributeList(((int)EnumTarget.Game));
			attr[((int)EnumAttribute.DiamondRate)] = GlobalDesginConfig.GetInt("investor_gems_chance");
			attr[((int)EnumAttribute.Diamond)] = GlobalDesginConfig.GetInt("investor_gems");
			attr[((int)EnumAttribute.Gold)] = GlobalDesginConfig.GetInt("investor_coin_ratio");
			attr[((int)EnumAttribute.LevelGold)] = GlobalDesginConfig.GetInt("initial_coin");
			attr[((int)EnumAttribute.OfflineAddition)] = GlobalDesginConfig.GetInt("offline_ratio");
			attr[((int)EnumAttribute.OfflineTime)] = GlobalDesginConfig.GetInt("max_offline_time");
			attr[((int)EnumAttribute.AdAddition)] = GlobalDesginConfig.GetInt("ad_boost_ratio");
			attr[((int)EnumAttribute.AdTime)] = GlobalDesginConfig.GetInt("ad_boost_time");
		}

		/// <summary>
		/// 重置所有属性
		/// </summary>
		private void ReInitAllAttribute(int scene)
		{
			if (ConfigSystem.Instance.TryGet<LevelRowData>(scene, out var cfg))
			{
				//主角
				attrSys.Register(((int)EnumTarget.Player), 0, CreateAttribute(1, cfg.PlayerId));
				//厨师
				attrSys.Register(((int)EnumTarget.Cook), 0, CreateAttribute(1, cfg.ChefId));
				//服务员
				attrSys.Register(((int)EnumTarget.Waiter), 0, CreateAttribute(1, cfg.WaiterId));

				//工作台
				for (int i = 0; i < cfg.MachineIdLength; i++)
					attrSys.Register(((int)EnumTarget.Machine), cfg.MachineId(i), CreateAttribute(0, cfg.MachineId(i)));
				//顾客
				for (int i = 0; i < cfg.CustomerIdLength; i++)
					attrSys.Register(((int)EnumTarget.Customer), cfg.CustomerId(i), CreateAttribute(2, cfg.CustomerId(i)));
				//车
				for (int i = 0; i < cfg.CarIdLength; i++)
					attrSys.Register(((int)EnumTarget.Customer), cfg.CarId(i), CreateAttribute(2, cfg.CarId(i)));
			}
		}

		private AttributeList CreateAttribute(int type, int id)
		{

			var attr = new AttributeList();
			switch (type)
			{
				case 0://工作台
					if (ConfigSystem.Instance.TryGet<MachineRowData>(id, out var m))
						attr[((int)EnumAttribute.Price)] = m.ShopPrice(2);
					attr[((int)EnumAttribute.WorkSpeed)] = m.Efficiency;
					break;
				case 1://厨师，服务员
					if (ConfigSystem.Instance.TryGet<RoleDataRowData>(id, out var r))
					{
						attr[((int)EnumAttribute.Speed)] = r.MoveSpeed;
						attr[((int)EnumAttribute.OrderSpeed)] = r.OrderSpeed;
						attr[((int)EnumAttribute.WorkSpeed)] = r.Efficiency;
						attr[((int)EnumAttribute.ImmediatelyCompleteRate)] = r.Instant;
						attr[((int)EnumAttribute.PerfectCompleteRate)] = r.Perfect;
					}
					break;
				case 2://客人
					if (ConfigSystem.Instance.TryGet<RoleDataRowData>(id, out r))
					{
						attr[((int)EnumAttribute.Speed)] = r.MoveSpeed;
						attr[((int)EnumAttribute.Gratuity)] = r.Tip;
						attr[((int)EnumAttribute.GratuityRate)] = r.TipRatio;
					}
					break;
			}
			attr?.Break();
			return attr;
		}
		#endregion

		#region Events

		private void OnEnterRoom(int scene)
		{
			ReInitRoomAttribute(scene);
		}

		/// <summary>
		/// 监听buff添加
		/// </summary>
		/// <param name="data"></param>
		private void OnBuffAdd(BuffData data)
		{
			if (data.id > 0)
			{
				var from = data.from;
				if (from != 0)
					attrSys.RemoveBuff(data.id, from, data.targetid);
				attrSys.AddBuff(data.id, data.val, data.targetid, data.time, data.from);
			}
		}



		#endregion

	}
}
