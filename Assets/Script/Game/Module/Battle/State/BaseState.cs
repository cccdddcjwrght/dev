
namespace SGame 
{
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

        protected BaseBattleCharacter _character;

        public void Init(BaseBattleCharacter character)
        {
            _character = character;
        }

        /// <summary>
        /// 状态触发
        /// </summary>
        public virtual void Exctue()
        {
            ChangeData();
            if (stateShow) ShowEffect();
        }

        //处理数据
        public virtual void ChangeData() { }

        //状态表现
        public virtual void ShowEffect() { }

        public virtual void RemoveEffect() { }

        public virtual void Dispose() 
        {
            RemoveEffect();
        }

        public virtual void Reduce() => round--;

        //检测状态结束
        public bool IsExpired()
        {
            return round <= 0;
        }
    }
}

