using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.UI
{
	public partial class UIReg
	{
		public void RegAllUI(UIContext context) => Register(context);

		partial void Register(UIContext context);
	}

}