using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;
using TMPro;

public class updateDB : MonoBehaviour
{
    public string dbName;
    public int newScore;
    public static updateDB Instance = null;

    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //updateDatabase(); // for testing uncomment this then input from params exposed in script component propertie inspector view
        readDatabase();
    }

    public void readDatabase()
    {
        string conn = SetDataBaseClass.SetDataBase(dbName + ".db");
        IDbConnection dbconn;
        IDbCommand dbcmd;
        IDataReader dreader;

        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT score " + "FROM user";
        dbcmd.CommandText = sqlQuery;
        dreader = dbcmd.ExecuteReader();
        while (dreader.Read())
        {
            int value = dreader.GetInt32(0);
            Debug.Log("score= " + value);
            this.GetComponent<Score>().oldScore.text = "Old Score: " + value.ToString();
            Score.Instance.OldScoreCount = value;
        }
        dreader.Close();
        dreader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void updateDatabase()
    {
        string conn = SetDataBaseClass.SetDataBase(dbName + ".db");
        IDbConnection dbconn;
        IDbCommand dbcmd;
        IDataReader dreader;

        dbconn = new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        dbcmd = dbconn.CreateCommand();
        string sqlQuery = "UPDATE user " + "set score = " + newScore.ToString(); // this is test will not work only from script params exposed input value given
        Debug.Log("Score Count is " + Score.Instance.ScoreCount);
        dbcmd.CommandText = sqlQuery;
        dreader = dbcmd.ExecuteReader();
        //Score.oldScore.text = "Old Score: " + newScore.ToString();

        dreader.Close();
        dreader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    // Overloading
    public void updateDatabase(string databasename)
    {
        Debug.Log("start saving...");
        string conn = SetDataBaseClass.SetDataBase(databasename + ".db");
        IDbConnection dbconn;
        IDbCommand dbcmd;
        IDataReader dreader;

        dbconn = new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        dbcmd = dbconn.CreateCommand();
        string sqlQuery = "UPDATE user " + "set score = " + Score.Instance.ScoreCount;
        dbcmd.CommandText = sqlQuery;
        dreader = dbcmd.ExecuteReader();
        Debug.Log("updated");

        dreader.Close();
        dreader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
