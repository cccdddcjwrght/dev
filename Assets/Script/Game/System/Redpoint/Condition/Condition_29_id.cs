using FlatBuffers;
namespace SGame
{
	/// <summary>
	/// 明日礼包是否可领取
	/// </summary>
	public class Condition_29_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args) => TomorrowGiftModule.Instance.CanTake;
	}
}