using System.Collections;
using System.Collections.Generic;
using Fibers;
using UnityEngine;

public class TestWebReq : MonoBehaviour
{
    private Fibers.Fiber m_fiber;
    // Start is called before the first frame update
    void Start()
    {
        m_fiber = new Fiber(Run(), FiberBucket.Manual);
    }

    IEnumerator Run()
    {
        string url = "https://cn.bing.com/search?q=ss&FORM=HDRSC1";
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

    // Update is called once per frame
    void Update()
    {
        m_fiber.Step();
    }
}
