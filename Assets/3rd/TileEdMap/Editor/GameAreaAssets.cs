using System;

namespace TileEdExt
{
	[TileEd.TileEDModule(label = "Area", id = 5)]
	[Serializable]
	public class GameAreaAssets : TileEd.TileEdSetAssets
	{
		public GameAreaAssets() { isDummy = true; }

	}
}
