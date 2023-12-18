using System;
using System.Diagnostics;
using UnityEngine;
using SGame;
using Random = Unity.Mathematics.Random;
using Time = UnityEngine.Time;
using Unity.Entities;
using Debug = UnityEngine.Debug;

public class TestClickme : MonoBehaviour
{
    public int   num = 1;
    public float time = 2;
    private Random m_random;

    [SerializeField]
    public Entity eee;

    public int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Reg<int, float, string>((int)GameEvent.TEST_EVENT2, OnEvent2);
   
        m_random = new Random((uint)Time.frameCount);
        
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEvent2(int a1, float a2, string a3)
    {
        Debug.Log("on evetn2 =" + a1.ToString() + " a2=" + a2.ToString() + " a3=" + a3.ToString());
    }
    
    
    [ContextMenu("Do Something")]
    public void ClickMe1()
    {  
        EventManager.Instance.Trigger((int)GameEvent.TEST_EVENT1, 100, "niubi", 33.6f);
    }

    void OnEventTriggerGen(params object[] p)
    {
        for (int i = 0; i < p.Length; i++)
        {
            Debug.Log("param " + i + " value=" + p[i]);
        }
    }

    void OnEventTrigger(object a, int b, object c)
    {
        Debug.Log("a=" + a.ToString() + " b=" + b.ToString() + " c=" + c.ToString());
    }
    
    void OnEventTrigger2(object a, int b, string c)
    {
        Debug.Log("aa=" + a.ToString() + " b=" + b.ToString() + " cc=" + c.ToString());
    }


    public void MoveCharacter()
    {
        /*
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery query = entityManager.CreateEntityQuery(typeof(CheckPointData));
        CheckPointData c = query.GetSingleton<CheckPointData>();
        
        EntityQuery query2 = entityManager.CreateEntityQuery(typeof(Character));
        Entity ce = query2.GetSingletonEntity();
        CharacterMover character = entityManager.GetComponentObject<CharacterMover>(ce);
        character.MoveTo(c.Value, 0, 0);
        */
    }
}
