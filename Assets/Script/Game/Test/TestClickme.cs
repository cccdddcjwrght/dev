using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using UnityEngine;
using SGame;
using Unity.Entities;

public class TestClickme : MonoBehaviour
{
    public int   num = 1;
    public float time = 2;

    public int currentIndex = 0;
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
        MoveCharacter();
    }

    public void MoveCharacter()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery query = entityManager.CreateEntityQuery(typeof(CheckPointData));
        CheckPointData c = query.GetSingleton<CheckPointData>();
        
        EntityQuery query2 = entityManager.CreateEntityQuery(typeof(Character));
        Entity ce = query2.GetSingletonEntity();
        CharacterMover character = entityManager.GetComponentObject<CharacterMover>(ce);
        character.MoveTo(c.Value);
    }
}
