using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // Singleton
    public static AdsInitializer _instance;

    [SerializeField] string _androidGameId = "1234567";
    [SerializeField] string _iOSGameId = "2345678";
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisment is initialzed");
            //LoadAd();
        }
        //else
            //InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);

        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd(); // Interestial ads
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // throw new System.NotImplementedException();
        // Optionally execute code if the Ad Unit successfully loads content.
        // Advertisement.Show(adUnitId, this);
        Debug.Log("OnUnityAdsAdLoaded");
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { 
        // pause your game here disable controls and timer
        Time.timeScale = 0;
    }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) {
        //UnityAdsShowCompletionState.SKIPPED status code
        // un-pause your game here
        Debug.Log("OnUnityAdsShowComplete " + showCompletionState);

        // choose right ad to reward among types
        if (adUnitId.Equals("Rewarded_Android") && UnityAdsCompletionState.COMPLETED.Equals(showCompletionState))
        {
            // do stuff
            Debug.Log("Rewarded player from rewarded video only");
        }
        else if (adUnitId.Equals("Rewarded_Android2") && UnityAdsCompletionState.COMPLETED.Equals(showCompletionState))
        {
            // do stuff
            Debug.Log("Rewarded2 player from rewarded video2 only");
        }

        Time.timeScale = 1;
    }

}