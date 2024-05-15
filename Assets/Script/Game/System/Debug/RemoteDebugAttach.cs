using System;
using System.Collections;
using System.Collections.Generic;
using Hdg;
using UnityEngine;

public class RemoteDebugAttach : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RemoteDebugServer.AddDontDestroyOnLoadObject(gameObject);
    }

    private void OnDestroy()
    {
        RemoteDebugServer.RemoveDontDestroyOnLoadObject(gameObject);
    }
}
