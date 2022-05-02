using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneController : MonoBehaviour
{
    ARPlaneManager m_ARPlaneManager;
    void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
    }

    void OnEnable()
    {
        PlaceObjectOnPlane.onPlacedObject += DisablePlaneDetection;
    }

    // After invoke these are the subscribers that will execute their code.
    // It will search for onEnable after invoke or disable when destroy or setActive false
    void OnDisable()
    {
        PlaceObjectOnPlane.onPlacedObject -= DisablePlaneDetection;
    }

    void DisablePlaneDetection()
    {
        //planeDetectionMessage = "Disable Plane Detection and Hide Existing";
        SetAllPlanesActive(false);
        m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;
    }

    /// <summary>
    /// Iterates over all the existing planes and activates
    /// or deactivates their <c>GameObject</c>s'.
    /// </summary>
    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }


}
