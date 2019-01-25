using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public static GameplayManager thisClass;
    
    public bool IsGameReady = false;
    public bool IsGameReset = false;
    public GameObject GrassPosition;

    [Header("Level Index")]
    public int _DayCount;

    [Header("Assign Encouonter Spawners")]
    public GameObject[] EncounterSpawnerTransform;

    [Header("Assign People Spawners")]
    public GameObject[] PeopleSpawnerTransform;

    [Header("Assign Pers Spawners")]
    public GameObject[] PersSpawnerTransform;

    [Header("Assign some encounters")]
    public GameObject Encounters;

    [Header("Assign some People")]
    public GameObject People;

    [Header("Assign some Pers")]
    public GameObject Pers;

    [Header("Spawner Manager")]
    [Tooltip("Spawner manager is deferent, depend on game design by level.")]
    public List<Spawner> maxSpawnerByLevel;

    [SerializeField]
    bool breakingUp = false;

    [SerializeField]
    int curEncounter = 0;
    [SerializeField]
    int curPeople = 0;

    // Use this for initialization
    private void Awake()
    {
        thisClass = this;
    }

    public void InitGameplay()
    {
        InitEncounterSpawn();
    }

    public void InitEncounterSpawn()
    {
        StartCoroutine(Spawn(SpawnerType.PEOPLE, 3));
        StartCoroutine(Spawn(SpawnerType.ENCOUNTER, 5));
        StartCoroutine(Spawn(SpawnerType.PERS, 5));
    }

    //void DestroyAllEncounters()
    //{
    //    for (int i = 0; i <= (EncounterSpawnerTransform.Length - 1); i++)
    //    {
    //        if (EncounterSpawnerTransform[i].transform.childCount > 0)
    //        {
    //            for (int j = 0; j <= (EncounterSpawnerTransform[i].transform.childCount - 1); j++)
    //            {
    //                Color color = EncounterSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color;
    //                color.a -= Time.deltaTime * 5f;
    //                EncounterSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color = color;
    //                if (EncounterSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color.a <= 0.001)
    //                {
    //                    Destroy(EncounterSpawnerTransform[i].transform.GetChild(j).gameObject);
    //                }
    //            }
    //        }
    //    }

    //    for (int i = 0; i <= (PeopleSpawnerTransform.Length - 1); i++)
    //    {
    //        if (PeopleSpawnerTransform[i].transform.childCount > 0)
    //        {
    //            for (int j = 0; j <= (PeopleSpawnerTransform[i].transform.childCount - 1); j++)
    //            {
    //                Color color = PeopleSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color;
    //                color.a -= Time.deltaTime * 5f;
    //                PeopleSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color = color;
    //                if (PeopleSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color.a <= 0.001)
    //                {
    //                    Destroy(PeopleSpawnerTransform[i].transform.GetChild(j).gameObject);
    //                }
    //            }
    //        }
    //    }

    //    for (int i = 0; i <= (PersSpawnerTransform.Length - 1); i++)
    //    {
    //        if (PersSpawnerTransform[i].transform.childCount > 0)
    //        {
    //            for (int j = 0; j <= (PersSpawnerTransform[i].transform.childCount - 1); j++)
    //            {
    //                Color color = PersSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color;
    //                color.a -= Time.deltaTime * 5f;
    //                PersSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color = color;
    //                if (PersSpawnerTransform[i].transform.GetChild(j)
    //                    .gameObject.GetComponent<SpriteRenderer>().color.a <= 0.001)
    //                {
    //                    Destroy(PersSpawnerTransform[i].transform.GetChild(j).gameObject);
    //                }
    //            }
    //        }
    //    }
    //}

    void SpawnEncounter(GameObject parent, GameObject objectSpawn)
    {
        float[] yPosition = { 1f, -1f, 2.5f };
        GameObject newObject = Instantiate(objectSpawn, parent.transform);
        newObject.transform.SetParent(parent.transform);
        var name = newObject.GetComponent<EncounterBehaviour>().Type;
        if (parent.name.Equals("[RIGHT]"))
        {
            switch (name)
            {
                case EEncounterType.BEGAL:
                    newObject.GetComponent<BegalBehaviour>()._Direction =
                        (int)-(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
                case EEncounterType.PEOPLE:
                    newObject.GetComponent<PeopleBehaviour>()._Speed = 
                        -newObject.GetComponent<PeopleBehaviour>()._Speed;
                    break;
                case EEncounterType.PERS:
                    newObject.GetComponent<PersBehaviour>()._Speed =
                        -newObject.GetComponent<PersBehaviour>()._Speed;
                    break;
            }
        }
        else
        {
            switch (name)
            {
                case EEncounterType.BEGAL:
                    newObject.GetComponent<BegalBehaviour>()._Direction =
                        (int) (newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
                case EEncounterType.PEOPLE:
                    newObject.GetComponent<PeopleBehaviour>()._Speed =
                        newObject.GetComponent<PeopleBehaviour>()._Speed;
                    break;
                case EEncounterType.PERS:
                    newObject.GetComponent<PersBehaviour>()._Speed =
                       newObject.GetComponent<PersBehaviour>()._Speed;
                    break;
            }
        }
        newObject.transform.localPosition = new Vector2(0, yPosition[Random.Range(0, yPosition.Length)]);
    }

    IEnumerator Spawn(SpawnerType e, float timeToSpawn)
    {
        switch (e) {
            case SpawnerType.ENCOUNTER:
                while (curEncounter != maxSpawnerByLevel[_DayCount].EncounterMax)
                {
                    SpawnEncounter(EncounterSpawnerTransform[Random.Range(0, EncounterSpawnerTransform.Length)], Encounters);
                    yield return new WaitForSeconds(timeToSpawn);
                    curEncounter += 1;
                }
                break;
            case SpawnerType.PEOPLE:
                while (curPeople != maxSpawnerByLevel[_DayCount].PeopleMax)
                {
                    SpawnEncounter(PeopleSpawnerTransform[Random.Range(0, PeopleSpawnerTransform.Length)], People);
                    yield return new WaitForSeconds(timeToSpawn);
                    curPeople += 1;
                }
                break;
            case SpawnerType.PERS:
                SpawnEncounter(PersSpawnerTransform[Random.Range(0, PersSpawnerTransform.Length)], Pers);
                break;
        }

    }

    private bool IsStageClear()
    {
        bool isEncounterClear = false;
        bool isPeopleClear = false;
        bool isPersClear = false;
        foreach (GameObject encounterSpawner in EncounterSpawnerTransform)
        {
            if (encounterSpawner.transform.childCount == 0) isEncounterClear = true;
            else isEncounterClear = false;
        }
        foreach (GameObject peopleSpawner in PeopleSpawnerTransform)
        {
            if (peopleSpawner.transform.childCount == 0) isPeopleClear = true;
            else isPeopleClear = false;
        }
        foreach (GameObject persSpawner in PersSpawnerTransform)
        {
            if (persSpawner.transform.childCount == 0) isPersClear = true;
            else isPersClear = false;
        }

        if (!IsGameReset && isEncounterClear && isPersClear && isPeopleClear)
            return true;
        else
            return false;
    }

    private void Update()
    {
            if (curEncounter == maxSpawnerByLevel[_DayCount].EncounterMax &&
                curPeople == maxSpawnerByLevel[_DayCount].PeopleMax)
            {
                if (IsStageClear())
                    ResetGame();
                    //DestroyAllEncounters();
            }
    }
    
    void ResetGame()
    {
        PlayerBehaviour.thisClass.gameObject.transform.position
            = Vector2.MoveTowards(GrassPosition.transform.position,PlayerBehaviour.thisClass.gameObject.transform.position, Time.deltaTime * 5);
        if (PlayerBehaviour.thisClass.gameObject.transform.position == GrassPosition.transform.position)
        {            
            IsGameReset = true;
            curEncounter = 0;
            curPeople = 0;
        }
    }
}

[System.Serializable]
public class Spawner{
    public int EncounterMax;
    public int PeopleMax;
}

public enum SpawnerType
{
    ENCOUNTER,PEOPLE,PERS
}