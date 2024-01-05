using System;
using TileEd;

namespace TileEdExt
{
	[TileEd.TileEDModule(id = 4, label = "GroupLayer")]
	[Serializable]

	public class GroupLayerAssets : TileEdSetAssets
	{
		public GroupLayerAssets() { isDummy = true; }
	}
}
