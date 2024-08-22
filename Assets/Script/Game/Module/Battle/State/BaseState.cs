
public enum BattleStateType 
{
    DIZZ,   //ѣ��
}

public abstract class BaseState
{
    public BattleStateType type;

    public int round;           //�����غ�
    public float value;

    public bool isImmediately;  //�Ƿ�������Ч
    public bool stateShow;      //״̬����

    /// <summary>
    /// ״̬����
    /// </summary>
    public virtual void Exctue() 
    {
        DataAmend();
        if (stateShow) ShowEffect();
    }

    //��������
    public virtual void DataAmend() { }

    //״̬����
    public virtual void ShowEffect() { }

    public abstract void Dispose();

    public void Reduce() => round--; 

    //�������غ�
    public bool IsExpired() 
    {
        return round <= 0;
    } 
}
