using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;

public class Dart : MonoBehaviour
{
    private Rigidbody rg;
    private GameObject dirObj;
    public bool isForceOK = false;
    bool isDartRotating = false;
    bool isDartReadyToShoot = true;
    bool isDartHitOnBoard = false;


    ARSessionOrigin aRSession;
    GameObject ARCam;

    public Collider dartFrontCollider;

    // Start is called before the first frame update
    void Start()
    {
        aRSession = GameObject.FindGameObjectWithTag("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = aRSession.transform.Find("AR Camera").gameObject;

        if(TryGetComponent(out Rigidbody rigid))
        rg = rigid;

        dirObj = GameObject.FindGameObjectWithTag("DartThrowPoint");
    }

    private void FixedUpdate()
    {
        if (isForceOK)
        {
            dartFrontCollider.enabled = true;
            StartCoroutine(InitDartDestroyVFX());

            if (TryGetComponent(out Rigidbody rigid))
                rigid.isKinematic = false;

            isForceOK = false;
            isDartRotating = true;
        }

        //Add Force
        rg.AddForce(dirObj.transform.forward * (12f + 6f) * Time.deltaTime, ForceMode.VelocityChange);

        //Dart ready
        if (isDartReadyToShoot)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * 20f);
        }

        //Dart rotating
        if (isDartRotating)
        {
            isDartReadyToShoot = false;
            transform.Rotate(Vector3.forward * Time.deltaTime * 400f);
        }
    }

    IEnumerator InitDartDestroyVFX()
    {
        yield return new WaitForSeconds(7f);
        //if (!isDartHitOnBoard) { 
            Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dart_board"))
        {
            //Trigger viberaton
            Handheld.Vibrate();

            GetComponent<Rigidbody>().isKinematic = true;
            isDartRotating = false;

            //Dart hit the board
            isDartHitOnBoard = true;
        }
    }
}
