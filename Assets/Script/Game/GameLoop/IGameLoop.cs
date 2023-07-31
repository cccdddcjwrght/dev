using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ÓÎÏ·Ñ­»·
public interface IGameLoop
{
    bool Init(string[] args);

    void Shutdown();

    void Update();

    void LateUpdate();
}