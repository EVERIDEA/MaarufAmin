using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DatabaseLocal
{

    [System.Serializable]
    public class SomeClass {
        public string stringA;
        public string stringB;
        public string stringC;
    }

    public class Backeend : MonoBehaviour {

        //    [SerializeField]
        //    List<SomeClass> DBLocalData = new List<SomeClass>();

        //    void Start()
        //    {
        //        //PlayerPrefs.SetString("DBLocalLevel.fyr", "");
        //        LoadAllData();
        //    }

        //    #region Save
        //    public void SaveHighScore(SaveDBLocalEvent e)
        //    {
        //        string fileName = "";
        //        string filePath = "";
        //        string json = "";

        //        fileName = "DBLocalLevel.fyr";

        //        if(e.LevelData != null)
        //            DBLocalData.Add(e.LevelData);
        //        json = JsonHelper.ToJson(DBLocalData.ToArray(), true);        //JsonUtility.ToJson(scoreData);

        //        PlayerPrefs.SetString(fileName, json);

        //        LoadAllData();

        //        CalculateScore();
        //        //File.WriteAllText(filePath, json);

        //    }

        //    void CalculateScore() {
        //        float score = 0;
        //        for(int i = 0; i < DBLocalData.Count; i++)
        //            score += DBLocalData[i].Score;

        //        float avg = score / DBLocalData.Count;
        //        Debug.Log(avg);
        //        EventManager.TriggerEvent(new LeaderboardAddEvent(avg, LeaderboardType.GENERAL));

        //    }
        //    #endregion

        //    #region LOAD
        //    public void LoadAllData()
        //    {
        //        string path = "";
        //        string fileName = "";

        //        fileName = "DBLocalLevel.fyr";

        //        if(PlayerPrefs.GetString(fileName) != "")
        //        {
        //            string data = PlayerPrefs.GetString(fileName);
        //            DBLocalData = new List<LevelData>();
        //            string json = data;
        //            LevelData[] dataLevel = JsonHelper.FromJson<LevelData>(json);
        //            DBLocalData = dataLevel.ToList();
        //        }

        //    }
        //    #endregion

        //    #region ARRAY_JSON
        //    public class JsonHelper
        //    {
        //        public static T[] FromJson<T>(string json)
        //        {
        //            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        //            return wrapper.data;
        //        }

        //        public static string ToJson<T>(T[] array)
        //        {
        //            Wrapper<T> wrapper = new Wrapper<T>();
        //            wrapper.data = array;
        //            return JsonUtility.ToJson(wrapper);
        //        }

        //        public static string ToJson<T>(T[] array, bool prettyPrint)
        //        {
        //            Wrapper<T> wrapper = new Wrapper<T>();
        //            wrapper.data = array;
        //            return JsonUtility.ToJson(wrapper, prettyPrint);
        //        }
        //    }
        //    [Serializable]
        //    private class Wrapper<T>
        //    {
        //        public T[] data;
        //    }
        //    #endregion
    }
}
