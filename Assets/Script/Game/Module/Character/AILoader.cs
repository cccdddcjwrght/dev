using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using SGame;
using UnityEngine;

public class AILoader : MonoSingleton<AILoader>
{
    public class Request : IEnumerator
    {
        public bool isFinish = false;
        public bool MoveNext() => !isFinish;

        public object Current => null;
        public void Reset() { }
    }

    private List<Request> m_request = new List<Request>();
    //private float         m_waitTime = 0;
    //public float WAIT_TIME = .1f;

    public AILoader()
    {
        EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, OnNextLevel);
    }

    void OnNextLevel()
    {
        foreach (var item in m_request)
            item.isFinish = true;
        m_request.Clear();
    }

    /// <summary>
    /// 添加排队等待
    /// </summary>
    /// <returns></returns>
    public Request AddWait()
    {
        Request req = new Request();
        m_request.Add(req);
        return req;
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (m_waitTime > 0)
        //    m_waitTime -= Time.deltaTime;

        if (m_request.Count == 0)
            return;
        
        m_request[0].isFinish = true;
        m_request.RemoveAt(0);
    }
}
