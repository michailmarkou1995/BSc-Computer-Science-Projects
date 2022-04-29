using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;

public class GPSController : MonoBehaviour
{
    string message = "Initialising GPS...";
    float thisLat, thisLong;
    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, (int)DateTimeKind.Utc); // Date since/from first epoch difference in seconds counter 1970
    [SerializeField] private int xPos, yPos, textSize;
    [SerializeField] private TMPro.TextMeshProUGUI GPSCordsText;
    private void OnGUI()
    {
        //GUI.skin.label.fontSize = textSize;
        //GUI.Label(new Rect(xPos, yPos, 1000, 1000), message);
        GPSCordsText.text = message;
    }

    /**
     * Input.location.Start(5,0) if bigger e.g., 100 then no power consumption high from queries
     * second parameters is how ofter should update itself e.g., if 5 then every 5 meters physically move will update
     */
    IEnumerator StartGPS()
    {
        message = "Starting";

#if UNITY_EDITOR
        // No permission handling needed in Editor
#endif

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // First, Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            message = "Location Services Not Enabled.";
            Debug.LogFormat("Android and Location not enabled");
            //yield break;
            yield return new WaitForSeconds(1);
        }

        // Start service before querying location
        Input.location.Start(5, 0); // 5 meters accuracy, displacement

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

#if UNITY_EDITOR
        int editorMaxWait = 5;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service did not initialize in 20 seconds
        if (maxWait < 1)
        {
            message = "Timed out";
            yield break;
        }

#if UNITY_EDITOR
        // Connection has failed
        if (Input.location.status != LocationServiceStatus.Running)
        {
            message = "Unable to determine device location";
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            yield break;
        }
#endif
        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            message = "Unable to determine device location";
            yield break;
        }
        else
        {
            Input.compass.enabled = true;

            message = "Lat: " + Input.location.lastData.latitude +
                      "\nLong: " + Input.location.lastData.longitude +
                      "\nAlt: " + Input.location.lastData.altitude +
                      "\nHoriz Acc: " + Input.location.lastData.horizontalAccuracy +
                      "\nVert Acc: " + Input.location.lastData.verticalAccuracy +
                      "\n=========" +
                      "\nHeading: " + Input.compass.trueHeading;
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGPS());
    }

    // Update is called once per frame
    void Update()
    {
        /*        DateTime lastUpdate = epoch.AddSeconds(Input.location.lastData.timestamp);
                DateTime rightNow = DateTime.Now;
                thisLat = Input.location.lastData.latitude;
                thisLong = Input.location.lastData.longitude;
                message = "Current Lat: " + thisLat +
                          "\nCurrent Long: " + thisLong +
                          "\nUpdate Time: " + lastUpdate.ToString("HH:mm:ss") +
                          "\nNow: " + rightNow.ToString("HH:mm:ss");*/
    }
}
