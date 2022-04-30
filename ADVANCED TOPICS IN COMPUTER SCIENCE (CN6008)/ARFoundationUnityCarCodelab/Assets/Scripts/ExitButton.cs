using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace UnityEngine.XR.ARFoundation.Samples
{
    public class ExitButton : MonoBehaviour
    {
        public GameObject appMgmt;

        public void QuitGame()
        {
            // if score bigger then save otherwise do not
            if (Application.isPlaying)
            {
                if(Score.scoreCount > Score.oldScoreCount)
                {
                    appMgmt.GetComponent<updateDB>().updateDatabase("playerData");
                    Debug.Log("You did new SCORE!!");
                }
                Debug.Log("saving");
                Application.Quit();
            }
        }
    }
}
