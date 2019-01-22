using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SysObj = System.Object;

public class GameController : Singleton<GameController>
{
    private Dictionary<System.String, SysObj> states;
    
    public Transform [] spawnerTransform;
    [SerializeField]
    private int[] enounterInLevel;
    private int curEncounterSpawned;

    private GameplayData dataGameplay;
    public GameplayData DataGameplay
    {
        get
        {
            if (dataGameplay == null)
                dataGameplay = RepositoryManager.Instance.GetState<GameplayData>("gameplay.data");

            return dataGameplay;
        }
    }

    private PlayerData dataPlayer;
    private BegalData dataBegalA;
    private BegalData dataBegalB;

    // Use this for initialization
    private void Awake()
    {
        this.states = new Dictionary<System.String, SysObj>();
    }
    private void Start()
    {
        InitializeData();
        InitGameplay();
    }

    void InitializeData()
    {
        //get data from scriptable
        dataGameplay = RepositoryManager.Instance.GetState<GameplayData>("gameplay.data");
        dataBegalA = RepositoryManager.Instance.GetState<BegalData>("begal.data.a");
        dataBegalB = RepositoryManager.Instance.GetState<BegalData>("begal.data.b");
        dataPlayer = RepositoryManager.Instance.GetState<PlayerData>("player.data");

        //add all save data to dictionary
        this.states.Add("gameplay.data", dataGameplay);
        this.states.Add("begal.data.a", dataBegalA);
        this.states.Add("begal.data.b", dataBegalB);
        this.states.Add("player.data", dataPlayer);

        //initialize some data to the object , based on object initialize
        EventManager.TriggerEvent(new InitializeDataEvent<GameplayData>(dataGameplay));
        EventManager.TriggerEvent(new InitializeDataEvent<PlayerData>(dataPlayer));
    }

    public void InitGameplay()
    {
        dataGameplay.Time = enounterInLevel[0];
        StartCoroutine(SpawnEncounter(5));
    }

    #region GetGameplayData
    public T GetGameplayData<T>(System.String key)
    {
        return (T)(!this.states.ContainsKey(key) ? default(T) : this.states[key]);
    }
    #endregion
    
    IEnumerator SpawnEncounter(float timeToSpawn)
    {
        while (curEncounterSpawned != dataGameplay.Time)
        {
            SpawnEncounter(spawnerTransform[Random.Range(0, spawnerTransform.Length)], dataGameplay.Encounters[Random.Range(0, dataGameplay.Encounters.Length)]);
            yield return new WaitForSeconds(timeToSpawn);
            curEncounterSpawned += 1;
        }
    }

    void SpawnEncounter(Transform parent, GameObject encounter)
    {
        float[] yPosition = { 1f, -1f, 2.5f };
        GameObject newObject = Instantiate(encounter, parent);
        newObject.transform.SetParent(parent);

        var name = newObject.GetComponent<EncounterBehaviour>().Type;
        if (parent.name.Equals("[RIGHT]"))
        {
            switch (name)
            {
                case EEncounterType.BEGAL:
                    EventManager.TriggerEvent(new InitializeDataEvent<BegalData>(dataBegalB));
                    newObject.GetComponent<BegalBehaviour>().Direction = (int) (newObject.GetComponent<BegalBehaviour>().curMoveSpeed);
                    break;
                //case EEncounterType.PEOPLE:
                //    newObject.GetComponent<PeopleBehaviour>()._Direction = (int)-(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                //    break;
                //case EEncounterType.PERS:
                //    newObject.GetComponent<PersBehaviour>()._Direction = (int)-(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                //    break;
            }
        }
        else
        {
            switch (name)
            {
                case EEncounterType.BEGAL:
                    EventManager.TriggerEvent(new InitializeDataEvent<BegalData>(dataBegalA));
                    newObject.GetComponent<BegalBehaviour>().Direction = (int) -(newObject.GetComponent<BegalBehaviour>().curMoveSpeed);
                    break;
                //case EEncounterType.PEOPLE:
                //    newObject.GetComponent<PeopleBehaviour>()._Direction = (int)(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                //    break;
                //case EEncounterType.PERS:
                //    newObject.GetComponent<PersBehaviour>()._Direction = (int)(newObject.GetComponent<BegalBehaviour>()._MovementSpeed);
                //    break;
            }
        }
        newObject.transform.localPosition = new Vector2(0, yPosition[Random.Range(0, yPosition.Length)]);
    }

    protected override void OnUpdateFrame(float deltaTime)
    {
        if (dataGameplay.IsGameReady)
        {
            dataGameplay.CurrentPlus += Time.deltaTime;
            if (dataGameplay.CurrentPlus > 1f)
            {
                dataGameplay.CurrentTime += 1;
                dataGameplay.CurrentPlus = 0;
            }

            if (dataGameplay.Time == curEncounterSpawned)
                DestroyAllEncounters();
        }
    }


    void DestroyAllEncounters()
    {
        for (int i = 0; i <= (spawnerTransform.Length - 1); i++)
        {
            if (spawnerTransform[i].childCount > 0)
            {
                for (int j = 0; j <= (spawnerTransform[i].childCount - 1); j++)
                {
                    Color color = spawnerTransform[i].GetChild(j).gameObject.GetComponent<SpriteRenderer>().color;
                    color.a -= Time.deltaTime * 5f;
                    spawnerTransform[i].GetChild(j).gameObject.GetComponent<SpriteRenderer>().color = color;
                    if (spawnerTransform[i].GetChild(j).gameObject.GetComponent<SpriteRenderer>().color.a <= 0.001)
                    {
                        Destroy(spawnerTransform[i].GetChild(j).gameObject);
                        ResetGame();
                    }
                }
            }
        }
    }
    void ResetGame()
    {
        dataGameplay.IsGameReady = false;
        dataGameplay.CurrentTime = 0;
        dataGameplay.CurrentPlus = 0;
        dataGameplay.Time = 0;
        dataGameplay.IsGameReset = true;
    }
}
