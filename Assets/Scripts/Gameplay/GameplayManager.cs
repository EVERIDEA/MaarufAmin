using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public static GameplayManager thisClass;
    
    public bool isGameReady = false;
    public bool isGameReset = false;

    [Header("Level Index")]
    public int _DayCount;

    [Header("Assign Spawners")]
    public GameObject[] _SpawnerTransform;

    [Header("Assign some encounters")]
    public GameObject[] _Encounters;

    [Header("Spawner Manager")]
    [Tooltip("Spawner manager is deferent, depend on game design by level.")]
    public List<SpawnerManager> spawnerManager;

    [SerializeField]
    float curTime = 0;
    [SerializeField]
    float curPlus = 0;

    [SerializeField]
    float time = 0;

    [SerializeField]
    bool breakingUp = false;
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
        time = spawnerManager[_DayCount].timerByIndex;
        StartCoroutine(SpawnEncounter(5));
    }

    void DestroyAllEncounters()
    {
        for (int i = 0; i <= (_SpawnerTransform.Length - 1); i++)
        {
            if (_SpawnerTransform[i].transform.childCount > 0)
            {
                for (int j = 0; j <= (_SpawnerTransform[i].transform.childCount - 1); j++)
                {
                    Color color = _SpawnerTransform[i].transform.GetChild(j)
                        .gameObject.GetComponent<SpriteRenderer>().color;
                    color.a -= Time.deltaTime * 5f;
                    _SpawnerTransform[i].transform.GetChild(j)
                        .gameObject.GetComponent<SpriteRenderer>().color = color;
                    if (_SpawnerTransform[i].transform.GetChild(j)
                        .gameObject.GetComponent<SpriteRenderer>().color.a <= 0.001)
                    {
                        Destroy(_SpawnerTransform[i].transform.GetChild(j).gameObject);
                        ResetGame();
                    }
                }
            }
        }
    }

    void ResetGame()
    {
        isGameReady = false;
        curTime = 0;
        curPlus = 0;
        time = 0;
        isGameReset = true;
    }

    void SpawnEncounter(GameObject parent, GameObject encounter)
    {
        float[] yPosition = { 1f, -1f, 2.5f };
        GameObject newObject = Instantiate(encounter, parent.transform);
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
                    newObject.GetComponent<PeopleBehaviour>()._Direction =
                        (int)-(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
                case EEncounterType.PERS:
                    newObject.GetComponent<PersBehaviour>()._Direction =
                        (int)-(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
            }
        }
        else
        {
            switch (name)
            {
                case EEncounterType.BEGAL:
                    newObject.GetComponent<BegalBehaviour>()._Direction =
                        (int)(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
                case EEncounterType.PEOPLE:
                    newObject.GetComponent<PeopleBehaviour>()._Direction =
                        (int)(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
                case EEncounterType.PERS:
                    newObject.GetComponent<PersBehaviour>()._Direction =
                        (int)(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
            }
        }
        newObject.transform.localPosition = new Vector2(0, yPosition[Random.Range(0, yPosition.Length)]);
    }

    IEnumerator SpawnEncounter(float timeToSpawn)
    {
        while (isGameReady)
        {
            SpawnEncounter(_SpawnerTransform[Random.Range(0, _SpawnerTransform.Length)], _Encounters[Random.Range(0, _Encounters.Length)]);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    private void FixedUpdate()
    {
        if (isGameReady)
        {
            curPlus += Time.deltaTime;
            if (curPlus > 1f)
            {
                curTime += 1;
                curPlus = 0;
            }

            if(curTime == time)
                DestroyAllEncounters();
        }
    }
}

[System.Serializable]
public class SpawnerManager
{
    public int countOfSpawnerSpawnEncounter;
    public int maxTimeEncounterSpawn;
    public int timerByIndex;
}