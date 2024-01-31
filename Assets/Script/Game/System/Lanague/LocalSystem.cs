using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using Unity.Entities;

namespace SGame
{

	public partial class LocalSystem : SystemBase
	{
		protected override void OnCreate()
		{
			base.OnCreate();
			//��ui���󴴽�ʱ�����б��ػ�����
			UIPackage.onObjectCreate += OnUICreate;
		}

		void OnUICreate(GObject obj)
		{
			UIListener.LocalAllChild(obj, true);
		}


		protected override void OnUpdate()
		{
		}


	}
}
