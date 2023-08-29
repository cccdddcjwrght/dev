using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using UnityEngine;
using SGame;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;
using Time = UnityEngine.Time;

public class TestClickme : MonoBehaviour
{
    public int   num = 1;
    public float time = 2;
    private Random m_random;

    public int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //m_reandom = new Random(Time.)
        m_random = new Random((uint)Time.frameCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickMe1()
    {
        //num = m_random.NextInt(1,7);
        //DiceModule.Instance.Play(DiceModule.Instance.GetDice(), time, num);
        //MoveCharacter();
        EventManager.Instance.Trigger((int)GameEvent.PLAYER_ROTE_DICE);
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
