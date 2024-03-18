using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using SGame;
using GameEvent = SGame.VS.GameEvent;

// This MonoBehaviour is responsible for controlling the CharacterGenerator,
// animating the character, and the user interface. When the user requests a 
// different character configuration the CharacterGenerator is asked to prepare
// the required assets. When all assets are downloaded and loaded a new
// character is created.
class TesetCharacterGame : MonoBehaviour
{
    CharacterGenerator  generator;
    GameObject          character;
    bool                usingLatestConfig;
    bool                newCharacterRequested       = true;
    bool                firstCharacter              = true;
    string              nonLoopingAnimationToPlay;

    const float         fadeLength = .6f;
    const int           typeWidth = 80;
    const int           buttonWidth = 20;
    private const int   toggleWith = 120;
    const string        prefName = "Character Generator Demo Pref";
    public string       characterName = "role";

    private bool        isWalking = false;

    public class EquipData
    {
        public List<int> equipId;
        public int       index;

        public int GetID()
        {
            return equipId[index];
        }
    }

    // 武器配置
    private Dictionary<SlotType, EquipData> m_slotIndex;

    private Equipments                      m_equipments;

    // Initializes the CharacterGenerator and load a saved config if any.
    IEnumerator Start()
    {
        SetupWeapons();

        while (!CharacterGenerator.ReadyToUse) yield return 0;
        //if (PlayerPrefs.HasKey(prefName))
        //    generator = CharacterGenerator.CreateWithConfig(PlayerPrefs.GetString(prefName));
        //else
        generator = CharacterGenerator.CreateWithRandomConfig(characterName);
    }

    void SetupWeapons()
    {
        m_slotIndex = new Dictionary<SlotType, EquipData>();
        List<int> weapons = new List<int>();
        weapons.Add(0);
        var configs = ConfigSystem.Instance.LoadConfig<GameConfigs.Equip>();
        for (int i = 0; i < configs.DatalistLength; i++)
        {
            var item = configs.Datalist(i);
            if (item.Value.Slot == (int)SlotType.WEAPON)
            {
                weapons.Add(item.Value.ItemId);
            }
        }
        
        m_slotIndex.Add(SlotType.WEAPON, new EquipData()
        {
            equipId = weapons
        });
    }

    // Requests a new character when the required asse
    // ts are loaded, starts
    // a non looping animation when changing certain pieces of clothing.
    void Update()
    {
        if (generator == null) return;
        if (usingLatestConfig) return;
        if (!generator.ConfigReady) return;

        usingLatestConfig = true;

        if (newCharacterRequested)
        {
            Destroy(character);
            character = generator.Generate();
            m_equipments = character.AddComponent<Equipments>();
            //character.GetComponent<Animation>().Play("idle1");
            //character.GetComponent<Animation>()["idle1"].wrapMode = WrapMode.Loop;
            newCharacterRequested = false;

            // Start the walkin animation for the first character.
            if (!firstCharacter) return;
            firstCharacter = false;
            //if (character.GetComponent<Animation>()["walkin"] == null) return;
            
            // Set the layer to 1 so this animation takes precedence
            // while it's blended in.
           // character.GetComponent<Animation>()["walkin"].layer = 1;
            
            // Use crossfade, because it will also fade the animation
            // nicely out again, using the same fade length.
            //character.GetComponent<Animation>().CrossFade("walkin", fadeLength);
            
            // We want the walkin animation to have full weight instantly,
            // so we overwrite the weight manually:
            //character.GetComponent<Animation>()["walkin"].weight = 1;
            
            // As the walkin animation starts outside the camera frustrum,
            // and moves the mesh outside its original bounding box,
            // updateWhenOffscreen has to be set to true for the
            // SkinnedMeshRenderer to update. This should be fixed
            // in a future version of Unity.
            //character.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
        }
        else
        {
            character = generator.Generate(character);
            //m_equipments = character.AddComponent<Equipments>();
            
            //if (nonLoopingAnimationToPlay == null) return;
            //character.GetComponent<Animation>()[nonLoopingAnimationToPlay].layer = 1;
            //character.GetComponent<Animation>().CrossFade(nonLoopingAnimationToPlay, fadeLength);
            //nonLoopingAnimationToPlay = null;
        }
    }

    void OnGUI()
    {
        if (generator == null) return;
        //GUI.enabled = usingLatestConfig && !character.GetComponent<Animation>().IsPlaying("walkin");
        GUI.enabled = usingLatestConfig;
        GUILayout.BeginArea(new Rect(10, 10, typeWidth + 2 * buttonWidth + 8, 500));

        /*
        // Buttons for changing the active character.
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", GUILayout.Width(buttonWidth)))
            ChangeCharacter(false);

        GUILayout.Box("Character", GUILayout.Width(typeWidth));

        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
            ChangeCharacter(true);

        GUILayout.EndHorizontal();
        */

        // Buttons for changing character elements.
        AddCategory("hair", "Hair", null);
        AddCategory("body", "Body", null);
        AddCategory("clothes", "clothes", null);
        AddCategory("makeup", "Makeup", null);
        AddEquipCategory(SlotType.WEAPON, "weapon");
        DrawAnimation();

        // Buttons for saving and deleting configurations.
        // In a real world application you probably want store these
        // preferences on a server, but for this demo configurations 
        // are saved locally using PlayerPrefs.
        if (GUILayout.Button("Save Configuration"))
        {
            string character_config = generator.GetConfig();
            PlayerPrefs.SetString(prefName, character_config);
            Debug.Log("character config=" + character_config);
        }

        if (GUILayout.Button("Delete Configuration"))
            PlayerPrefs.DeleteKey(prefName);

        // Show download progress or indicate assets are being loaded.
        GUI.enabled = true;
        if (!usingLatestConfig)
        {
            float progress = generator.CurrentConfigProgress;
            string status = "Loading";
            if (progress != 1) status = "Downloading " + (int)(progress * 100) + "%";
            GUILayout.Box(status);
        }

        GUILayout.EndArea();
    }

    void DrawAnimation()
    {
        GUILayout.BeginHorizontal();

        var newValue = GUILayout.Toggle(isWalking, "IsWalking", GUILayout.Width(toggleWith));
        if (newValue != isWalking)
        {
            isWalking = newValue;
            Animator ani = character.GetComponent<Animator>();
            if (ani != null)
            {
                ani.SetBool("walking", isWalking);
            }
        }

        GUILayout.EndHorizontal();
    }

    // Draws buttons for configuring a specific category of items, like pants or shoes.
    void AddCategory(string category, string displayName, string anim)
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", GUILayout.Width(buttonWidth)))
            ChangeElement(category, false, anim);

        GUILayout.Box(displayName, GUILayout.Width(typeWidth));

        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
            ChangeElement(category, true, anim);

        GUILayout.EndHorizontal();
    }

    void ChangeEquip(SlotType slot, bool next)
    {
        if (m_slotIndex.TryGetValue(slot, out EquipData edata))
        {
            edata.index =  (edata.index + 1) % edata.equipId.Count;
            m_equipments.SetWeapon(edata.GetID());
        }
    }
    
    // Draws buttons for configuring a specific category of items, like pants or shoes.
    void AddEquipCategory(SlotType slot, string displayName)
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", GUILayout.Width(buttonWidth)))
            ChangeEquip(slot, false);

        GUILayout.Box(displayName, GUILayout.Width(typeWidth));

        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
            ChangeEquip(slot, true);

        GUILayout.EndHorizontal();
    }

    void ChangeCharacter(bool next)
    {
        generator.ChangeCharacter(next);
        usingLatestConfig = false;
        newCharacterRequested = true;
    }

    void ChangeElement(string catagory, bool next, string anim)
    {
        generator.ChangeElement(catagory, next);
        usingLatestConfig = false;
        
        //if (!character.GetComponent<Animation>().IsPlaying(anim))
        //    nonLoopingAnimationToPlay = anim;
    }
}