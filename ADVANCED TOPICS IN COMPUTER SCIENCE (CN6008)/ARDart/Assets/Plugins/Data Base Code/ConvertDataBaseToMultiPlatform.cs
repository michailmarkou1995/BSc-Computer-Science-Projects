using UnityEngine;
using System.IO;

public class ConvertDataBaseToMultiPlatform : MonoBehaviour
{
    public string DataBaseName;

    public void Awake()
    {
        GenerateConnectionString(DataBaseName + ".db");
    }
    public void GenerateConnectionString(string DatabaseName)
    {
#if UNITY_EDITOR
        //if (Application.platform != RuntimePlatform.Android) 
        string dbPath = Application.dataPath + "/StreamingAssets/" + DatabaseName;
        //var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        //check if file exists in Application.persistentDataPath
        //var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
        string filepath = Application.persistentDataPath + "/" + DatabaseName;

        if (!File.Exists(filepath) || new System.IO.FileInfo(filepath).Length == 0)
        {
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
#if UNITY_ANDROID
                WWW loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
                while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath
                File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WINRT
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#endif
        }
        
        var dbPath = filepath;
#endif

    }
}