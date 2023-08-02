using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGame;

public class TestClickme : MonoBehaviour
{
    public int   num = 1;
    public float time = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickMe1()
    {
        DiceModule.Instance.Play(DiceModule.Instance.GetDice(), time, num);
    }
}
