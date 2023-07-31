
namespace MemUtils
{
    public enum ErrCode
    {
        SUCCESS = 0,
        FAIL_ADDRESS = 1,
        TWICE_FREE = 2,
        OUT_OF_MEMORY = 3,
        ITEMSIZE8 = 4,      // ITEM SIZE必须要大于8
        PARAM_FAIL = 5,     // 参数错误
        FAIL_TAG = 6,       // 标记错误
        UNINIT = 7, 		// 未初始化
        TWICE_INIT = 8,     // 多次初始化
        DATA_EMPTY = 9,     // 无数据了
        POOL_NOTFOUND = 10, // 找不到内存池
    }
}
