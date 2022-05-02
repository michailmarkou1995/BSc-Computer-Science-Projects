using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DartController : MonoBehaviour
{
    public GameObject DartPrefab;
    public Transform DartThrowPoint;
    ARSessionOrigin aRSession;
    GameObject ARCam;
    Transform DartboardObj;
    private GameObject DartTemp;
    private Rigidbody rb;
    private bool isDartBoardSearched = false;
    private float m_distanceFromDartBoard = 0f;
    public TMP_Text text_distance;

    void Start()
    {
        aRSession = GameObject.FindWithTag("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = aRSession.transform.Find("AR Camera").gameObject;
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
                if (raycastHit.collider.CompareTag("dart"))
                {
                    //Disable back touch Collider from dart 
                    raycastHit.collider.enabled = false;
                    DartTemp.transform.parent = aRSession.transform;

                    Dart currentDartScript = DartTemp.GetComponent<Dart>();
                    currentDartScript.isForceOK = true;

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
        DartboardObj = GameObject.FindWithTag("dart_board").transform;
        if (DartboardObj)
        {
            isDartBoardSearched = true;
        }
        StartCoroutine(WaitAndSpawnDart());
    }

    public IEnumerator WaitAndSpawnDart()
    {
        yield return new WaitForSeconds(1f);
        DartTemp = Instantiate(DartPrefab, DartThrowPoint.position, ARCam.transform.localRotation);
        DartTemp.transform.parent = ARCam.transform;

        if(DartTemp.TryGetComponent(out Rigidbody rigid))
        rb = rigid;

        rb.isKinematic = true;

        SoundManager.Instance.play_dartReloadSound();
    }
}