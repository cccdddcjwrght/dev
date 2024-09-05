
namespace SGame 
{
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

        protected BaseBattleCharacter _character;

        public void Init(BaseBattleCharacter character)
        {
            _character = character;
        }

        /// <summary>
        /// ״̬����
        /// </summary>
        public virtual void Exctue()
        {
            ChangeData();
            if (stateShow) ShowEffect();
        }

        //��������
        public virtual void ChangeData() { }

        //״̬����
        public virtual void ShowEffect() { }

        public virtual void RemoveEffect() { }

        public virtual void Dispose() 
        {
            RemoveEffect();
        }

        public virtual void Reduce() => round--;

        //���״̬����
        public bool IsExpired()
        {
            return round <= 0;
        }
    }
}

