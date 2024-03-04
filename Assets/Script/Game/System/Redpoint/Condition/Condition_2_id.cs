using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id��2
	/// �Ƿ��йؿ��Ƽ����Թ���
	/// </summary>
	public class Condition_2_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, string args)
		{
			var techs = DataCenter.Instance.roomData?.current?.roomTechs;
			if (techs?.Count > 0)
				return techs.Values.Any(t => PropertyManager.Instance.CheckCountByArgs(t.GetCostArray()));
			return false;
		}
	}

}