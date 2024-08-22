
public enum BattleStateType 
{
    DIZZ,   //眩晕
}

public abstract class BaseState
{
    public BattleStateType type;

    public int round;           //持续回合
    public float value;

    public bool isImmediately;  //是否立即生效
    public bool stateShow;      //状态表现

    /// <summary>
    /// 状态触发
    /// </summary>
    public virtual void Exctue() 
    {
        DataAmend();
        if (stateShow) ShowEffect();
    }

    //处理数据
    public virtual void DataAmend() { }

    //状态表现
    public virtual void ShowEffect() { }

    public abstract void Dispose();

    public void Reduce() => round--; 

    //检测持续回合
    public bool IsExpired() 
    {
        return round <= 0;
    } 
}
