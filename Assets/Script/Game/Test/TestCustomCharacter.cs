using System.Collections;
using System.Collections.Generic;
using Fibers;
using UnityEngine;

public class TestCustomCharacter : MonoBehaviour
{
    private GameObject currentCharacter;
    private CharacterGenerator generator;
    private Fiber m_fiber;

    public string character_name = "role";
    
    // Start is called before the first frame update
    void Start()
    {
        m_fiber = new Fiber(CreateRandomCharacter(), FiberBucket.Manual);
    }

    IEnumerator CreateRandomCharacter()
    {
        if (currentCharacter != null)
            GameObject.Destroy(currentCharacter);

        while (CharacterGenerator.ReadyToUse == false)
            yield return null;

        var generator = CharacterGenerator.CreateWithRandomConfig(character_name);
        while (generator.ConfigReady == false)
            yield return null;
        
        currentCharacter = generator.Generate();
        currentCharacter.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        m_fiber.Step();
    }
    
}
