using UnityEngine;
using System.Collections;
using Fibers;

// 协程内部对象缓存池
public class FiberPools
{
    static BetterList<Fiber.State>          mStatePool = new BetterList<Fiber.State>();
    static BetterList<Fiber.OnTerminate>    mOnTerminatePool;
    static BetterList<Fiber.OnExit>         mOnExitPool;
    static BetterList<Fiber.OnLeave>        mOnLeavePool;
    static BetterList<Fiber.OnEnter>        mOnEnterPool;

    public static Fiber.State AllocState(IEnumerator e)
    {
        // 从缓存池中
        var val = mStatePool.Pop();
        if (val != null) {
            val.ActiveByPool(e);
            return val;
        }

        val = new Fiber.State(e);
        return val;
    }

    public static void FreeState(Fiber.State val)
    {
        val.Clear();
        mStatePool.Add(val);
    }

    //public static void 
}

