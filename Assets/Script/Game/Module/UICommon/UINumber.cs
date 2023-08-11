using FairyGUI;

namespace SGame
{
    public class UINumber
    {
        private GTextField m_text;
        private GTextField m_shadow;
        private int m_number;

        public int Value
        {
            get { return m_number; }

            set
            {
                if (m_number == value)
                    return;

                m_number = value;
                m_text.text = m_number.ToString();
                m_shadow.text = m_number.ToString();
            }
        }

        public UINumber(GComponent component)
        {
            m_number = 0;
            m_text = component.GetChild("title").asTextField;
            m_shadow = component.GetChild("shadow").asTextField;
            m_text.text = "0";
            m_shadow.text = "0";
        }
    }
}