using System.Collections;
using System.Collections.Generic;
using Fibers;
using UnityEngine;

public class TestWebReq : MonoBehaviour
{
    private Fibers.Fiber m_fiber;
    public string TsetURL = "https://cn.bing.com/search?q=ss&FORM=HDRSC1";
    public string token = "1";
    // Start is called before the first frame update
    void Start()
    {
        m_fiber = new Fiber(Run(), FiberBucket.Manual);
    }

    IEnumerator Run()
    {
        string url = TsetURL;
        SGame.Http.HttpSystem.Instance.SetToken(token);
        var req = SGame.Http.HttpSystem.Instance.Get(url);
        yield return req;
        if (!string.IsNullOrEmpty(req.error))
        {
            Debug.LogError("req error=" + req.error);
        }
        else
        {
            Debug.Log(req.data);
        }
    }

    [ContextMenu("DoRequest")]
    public void DoRequest()
    {
        m_fiber.Start(Run());
    }

    // Update is called once per frame
    void Update()
    {
        m_fiber.Step();
    }
}
