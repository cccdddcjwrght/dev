using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGame
{
	partial class RequestExcuteSystem
	{
		[InitCall]
		static void InitEquip() {

			DataCenter.EquipUtil.Init();

		}
	}
}
