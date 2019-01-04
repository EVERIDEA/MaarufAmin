using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public static GameplayManager thisClass;
    
    [SerializeField]
    public bool isGameReady = false;

    [Header("Level Index")]
    public int _DayCount;

    [Header("Assign Spawners")]
    public GameObject[] _SpawnerTransform;

    [Header("Assign some encounters")]
    public GameObject[] _Encounters;

    [Header("Spawner Manager")]
    [Tooltip("Spawner manager is deferent, depend on game design by level.")]
    public List<SpawnerManager> spawnerManager;

    float curTime = 0;
    float time = 0;

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
        if(time != spawnerManager[_DayCount].maxTimeEncounterSpawn)
        {
            StartCoroutine(SpawnEncounter(5));
        }
    }

    void SpawnEncounter(GameObject parent,GameObject encounter)
    {
        float[] yPosition = { 1f, -1f, 2.5f };
        GameObject newObject = Instantiate(encounter,parent.transform);
        newObject.transform.SetParent(parent.transform);
        var name = newObject.GetComponent<EncounterBehaviour>().Type;
        if (parent.name.Equals("[RIGHT]"))
        {
            switch (name)
            {
                case EEncounterType.BEGAL:
                    newObject.GetComponent<BegalBehaviour>()._Direction = (int) -(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
                case EEncounterType.PEOPLE:
                    break;
                case EEncounterType.PERS:
                    break;
            }
        }
        else
        {
            switch (name)
            {
                case EEncounterType.BEGAL:
                    newObject.GetComponent<BegalBehaviour>()._Direction = (int) (newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                    break;
                case EEncounterType.PEOPLE:
                    break;
                case EEncounterType.PERS:
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

    void Timer()
    {
        curTime += Time.deltaTime;
        if (curTime >= 1)
        {
            time += 1;
            curTime = 0;
        }
    }

    private void Update()
    {
        Timer();
        if (time >= spawnerManager[_DayCount].timerByIndex)
        {
            //FINISH
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