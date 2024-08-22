using Unity.Entities;
using FairyGUI;
using System;
using log4net;
using UnityEngine;

namespace SGame.UI
{
	// UI组件, 使用UI组件好处在于 支持对象在没创建完成的时候进行销毁
	public class UIWindow : IComponentData
	{
		public string name
		{
			get { return BaseValue != null ? BaseValue.uiname : null; }
		}

		// 创建的Windoww
		public FairyWindow Value => BaseValue as FairyWindow;

		public IBaseWindow BaseValue;

		// 自己的Entity
		public Entity entity;

		// Fairygui 的包引用
		public UIPackageRequest uiPackage;

		// 创建的原始对象
		public GObject gObject;

		public void Dispose()
		{
			entity = Entity.Null;
			BaseValue = null;

			if (uiPackage != null)
			{
				uiPackage.Release();
				uiPackage = null;
			}
		}

		// 是否已经准备好显示
		public bool isReadlyShow
		{
			get
			{
				if (BaseValue == null)
					return false;

				return Value.isReadyShowed;
			}
		}
	}


}