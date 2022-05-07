using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DartController : MonoBehaviour
{
    public GameObject DartPrefab, DartPrefab2;
    public Transform DartThrowPoint;
    ARSessionOrigin aRSession;
    GameObject ARCam;
    Transform DartboardObj;
    private GameObject DartTemp, AxeTemp;
    private Rigidbody rb;
    private bool isDartBoardSearched = false;
    private float m_distanceFromDartBoard = 0f;
    public TMP_Text text_distance, dartCounterLeft, endGameText;
    private const int DARTMAX = 8;
    private int dartCounterLimit = 0;

    void Start()
    {
        aRSession = GameObject.FindWithTag("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = aRSession.transform.Find("AR Camera").gameObject;
        dartCounterLeft.text = DARTMAX.ToString();


#if UNITY_EDITOR
        /**
         * Test code
         */

        // Test Debug on Run Unity Editor this also executes the Dart rotation script attached to DartTemp
        //DartTemp = Instantiate(DartPrefab, DartThrowPoint.position, Quaternion.Euler(new Vector3(ARCam.transform.eulerAngles.x, -90, ARCam.transform.eulerAngles.z))); //Quaternion.Euler(new Vector3(45, transform.eulerAngles.y, 0) //Quaternion.Euler(new Vector3(0, -90, 0)) // no tranform.rotation.xyz
        //DartTemp = Instantiate(DartPrefab, DartThrowPoint.position, Quaternion.Euler(ARCam.transform.localRotation.eulerAngles.x, ARCam.transform.localRotation.eulerAngles.y - 90, ARCam.transform.localRotation.eulerAngles.z));
        DartTemp = Instantiate(DartPrefab, DartThrowPoint.position, ARCam.transform.localRotation);
        DartTemp.transform.parent = ARCam.transform;
        Debug.Log(Quaternion.Euler(ARCam.transform.localRotation.eulerAngles.x, ARCam.transform.localRotation.eulerAngles.y, ARCam.transform.localRotation.eulerAngles.z));
        Debug.Log(ARCam.transform.localRotation);
        Debug.Log(DartTemp.transform.parent);
        endGameText.text = "Run Out of Darts Plz Save and Exit";
#endif
    }

    void OnEnable()
    {
        PlaceObjectOnPlane.onPlacedObject += DartsInit;
    }

    void OnDisable()
    {
        PlaceObjectOnPlane.onPlacedObject -= DartsInit;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("dart") || raycastHit.collider.CompareTag("dart_axe"))
                {
                    //Disable back touch Collider from dart 
                    raycastHit.collider.enabled = false;
                    DartTemp.transform.parent = aRSession.transform;
                    AxeTemp.transform.parent = aRSession.transform;

                    Dart currentDartScript = DartTemp.GetComponent<Dart>();
                    Dart currentDartScript2 = AxeTemp.GetComponent<Dart>();
                    currentDartScript.isForceOK = true;
                    currentDartScript2.isForceOK = true;

                    //Load next dart
                    DartsInit();
                }
            }
        }

        if (isDartBoardSearched)
        {
            m_distanceFromDartBoard = Vector3.Distance(DartboardObj.position, ARCam.transform.position);
            text_distance.text = m_distanceFromDartBoard.ToString().Substring(0, 3);
        }

    }

    void DartsInit()
    {
        endGameText.text = "";
        //dartCounterLimit++;
        DartboardObj = GameObject.FindWithTag("dart_board").transform;
        if (DartboardObj)
        {
            isDartBoardSearched = true;
        }
        if (dartCounterLimit != DARTMAX)
            StartCoroutine(WaitAndSpawnDart());
        else
        {
            // call Game Over
            endGameText.alpha = 1;
            endGameText.text = "Run Out of Darts Plz Save and Exit";
            Debug.Log("End Game");
        }
    }

    public IEnumerator WaitAndSpawnDart()
    {
        yield return new WaitForSeconds(1f);

        /*        if (DartPrefab.tag == "dart")
                {
                    DartTemp = Instantiate(DartPrefab, DartThrowPoint.position, ARCam.transform.localRotation);
                    DartTemp.transform.parent = ARCam.transform;
                }
                else if (DartPrefab.tag == "dart_axe")
                {
                    // because instead of depth z the model is imported from 3d model program as y the depth in local rotation
                    //DartTemp = Instantiate(DartPrefab, DartThrowPoint.position, Quaternion.Euler(ARCam.transform.localRotation.eulerAngles.x, ARCam.transform.localRotation.eulerAngles.y - 90, ARCam.transform.localRotation.eulerAngles.z));
                    DartTemp.transform.parent = ARCam.transform;
                }*/

        // Create Prefab Default Empty and not parent imported 3D model to avoid AR camera Angle + Spawn Rotation headache calculation
        DartTemp = Instantiate(DartPrefab, DartThrowPoint.position, ARCam.transform.localRotation);
        DartTemp.transform.parent = ARCam.transform;
        DartTemp.SetActive(false);

        AxeTemp = Instantiate(DartPrefab2, DartThrowPoint.position, ARCam.transform.localRotation);
        AxeTemp.transform.parent = ARCam.transform;
        AxeTemp.SetActive(false);

        if (Score.Instance.ScoreCount >= 30)
        {
            AxeTemp.SetActive(true);

            if (AxeTemp.TryGetComponent(out Rigidbody rigid))
                rb = rigid;
        }
        else
        {
            DartTemp.SetActive(true);

            if (DartTemp.TryGetComponent(out Rigidbody rigid))
                rb = rigid;
        }

        rb.isKinematic = true;

        SoundManager.Instance.play_dartReloadSound();

        dartCounterLimit++;
        dartCounterLeft.text = (DARTMAX - dartCounterLimit).ToString();
    }
}