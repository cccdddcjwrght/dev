using System.Collections.Generic;

namespace SGame 
{
    /// <summary>
    /// ����Ч��
    /// </summary>
    public class AttackEffect
    {
        public int damage;          //�˺�
        public int steal;           //��Ѫ��

        public bool isCritical;     //�Ƿ񱩻�
        public bool isCombo;       //�Ƿ�����

        //������ɵ�״̬��ѣ�Σ�
        public List<BaseState> stateList = new List<BaseState>();
    }
}

