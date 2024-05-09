using GameConfigs;

public abstract class ConfigValue<T>
{
    protected T m_value;
    private bool m_isInit = false;
    protected string m_name;
    public ConfigValue(string name, T defaultValue)
    {
        m_isInit = false;
        m_value  = defaultValue;
        m_name = name;
    }

    protected abstract T GetValue();

    public T Value
    {
        get
        {
            if (m_isInit == false)
            {
                m_isInit = true;
                m_value = GetValue();
            }

            return m_value;
        }
    }
}

public class ConfigValueInt : ConfigValue<int>
{
    public ConfigValueInt(string name, int def = 0) : base(name, def)
    {
    }
    protected override int GetValue()
    {
        return GlobalDesginConfig.GetInt(m_name, m_value);
    }
}

public class ConfigValueFloat : ConfigValue<float>
{
    public ConfigValueFloat(string name, float def = 0) : base(name, def)
    {
    }
    protected override float GetValue()
    {
        return GlobalDesginConfig.GetFloat(m_name, m_value);
    }
}

public class ConfigValueStr : ConfigValue<string>
{
    public ConfigValueStr(string name) : base(name, "")
    {
    }
    
    protected override string GetValue()
    {
        return GlobalDesginConfig.GetStr(m_name);
    }
}