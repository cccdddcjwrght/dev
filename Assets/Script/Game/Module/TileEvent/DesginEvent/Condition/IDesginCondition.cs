namespace SGame
{
    /// <summary>
    /// 判断条件
    /// </summary>
    public interface IDesginCondition
    {
        /// <summary>
        /// 检测地块是否符合条件
        /// </summary>
        /// <param name="playerId">角色ID</param>
        /// <param name="tileId">地块ID</param>
        /// <param name="state">地块事件类型</param>
        /// <returns></returns>
        bool CheckTile(int playerId, int tileId, TileEventTrigger.State state);
    }
}