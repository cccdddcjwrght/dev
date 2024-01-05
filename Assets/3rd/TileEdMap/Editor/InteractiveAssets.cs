using System;
using TileEd;

namespace TileEdExt
{
	[TileEDModule(label = "Interactive", id = 6)]
	[Serializable]
	public class InteractiveAssets : TileEd.TileEdSetAssets
	{
		public InteractiveAssets()
		{
			fixBrushSize = 1;
		}
	}
}
