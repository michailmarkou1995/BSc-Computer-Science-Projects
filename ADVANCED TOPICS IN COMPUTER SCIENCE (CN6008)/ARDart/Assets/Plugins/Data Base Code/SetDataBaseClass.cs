using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


    public class SetDataBaseClass
    {
        public static string SetDataBase(string DataBaseName)
        {
            string conn = "";
#if UNITY_EDITOR
            conn = "URI=file:" + Application.dataPath + "\\StreamingAssets" + "/"+ DataBaseName; //Path to database
            Debug.Log("Windows Mode");

#elif UNITY_ANDROID
            conn = "URI=file:" + Application.persistentDataPath + "/" + DataBaseName; //Path to database.
            Debug.Log("Android Mode");
#endif



        return conn;
        }

    


}
