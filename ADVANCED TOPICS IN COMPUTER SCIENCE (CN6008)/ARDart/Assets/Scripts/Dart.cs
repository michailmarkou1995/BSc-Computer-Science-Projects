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

        if (gameObject.tag == "dart")
        {
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
        else if(gameObject.tag == "dart_axe")
        {
            //Dart ready
            if (isDartReadyToShoot)
            {
                // because local is x and not z rotate on right which is forward (parent, top root component gameObj)
                transform.Rotate(Vector3.right * Time.deltaTime * 50f);
            }

            //Dart rotating
            if (isDartRotating)
            {
                isDartReadyToShoot = false;
                transform.Rotate(Vector3.right * Time.deltaTime * 400f);
            }
        }

    }

    IEnumerator InitDartDestroyVFX()
    {
        yield return new WaitForSeconds(5f); // if in 7 secs do not hit then destroy it
        if (!isDartHitOnBoard) {
        Destroy(gameObject);
        }
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
        if (other.CompareTag("score_50"))
        {
            Score.Instance.ScoreCount += 50;
        }
        else if (other.CompareTag("score_25"))
        {
            Score.Instance.ScoreCount += 25;
        }
        else if (other.CompareTag("score_20"))
        {
            Score.Instance.ScoreCount += 20;
        }
        else if (other.CompareTag("score_60"))
        {
            Score.Instance.ScoreCount += 60;
        }
        else if (other.CompareTag("score_40"))
        {
            Score.Instance.ScoreCount += 40;
        }
        else if (other.CompareTag("score_2"))
        {
            Score.Instance.ScoreCount += 2;
        }
        else if (other.CompareTag("score_1"))
        {
            Score.Instance.ScoreCount += 1;
        }
        else if (other.CompareTag("score_3"))
        {
            Score.Instance.ScoreCount += 3;
        }
        else if (other.CompareTag("score_36"))
        {
            Score.Instance.ScoreCount += 36;
        }
        else if (other.CompareTag("score_18"))
        {
            Score.Instance.ScoreCount += 18;
        }
        else if (other.CompareTag("score_54"))
        {
            Score.Instance.ScoreCount += 54;
        }
        else if (other.CompareTag("score_4"))
        {
            Score.Instance.ScoreCount += 4;
        }
        else if (other.CompareTag("score_8"))
        {
            Score.Instance.ScoreCount += 8;
        }
        else if (other.CompareTag("score_12"))
        {
            Score.Instance.ScoreCount += 12;
        }
        else if (other.CompareTag("score_13"))
        {
            Score.Instance.ScoreCount += 13;
        }
        else if (other.CompareTag("score_26"))
        {
            Score.Instance.ScoreCount += 26;
        }
        else if (other.CompareTag("score_39"))
        {
            Score.Instance.ScoreCount += 39;
        }
        else if (other.CompareTag("score_6"))
        {
            Score.Instance.ScoreCount += 6;
        }
        else if (other.CompareTag("score_10"))
        {
            Score.Instance.ScoreCount += 10;
        }
        else if (other.CompareTag("score_30"))
        {
            Score.Instance.ScoreCount += 30;
        }
        else if (other.CompareTag("score_15"))
        {
            Score.Instance.ScoreCount += 15;
        }
        else if (other.CompareTag("score_45"))
        {
            Score.Instance.ScoreCount += 45;
        }
        else if (other.CompareTag("score_17"))
        {
            Score.Instance.ScoreCount += 17;
        }
        else if (other.CompareTag("score_34"))
        {
            Score.Instance.ScoreCount += 34;
        }
        else if (other.CompareTag("score_51"))
        {
            Score.Instance.ScoreCount += 51;
        }
        else if (other.CompareTag("score_9"))
        {
            Score.Instance.ScoreCount += 9;
        }
        else if (other.CompareTag("score_19"))
        {
            Score.Instance.ScoreCount += 19;
        }
        else if (other.CompareTag("score_57"))
        {
            Score.Instance.ScoreCount += 57;
        }
        else if (other.CompareTag("score_38"))
        {
            Score.Instance.ScoreCount += 38;
        }
        else if (other.CompareTag("score_7"))
        {
            Score.Instance.ScoreCount += 7;
        }
        else if (other.CompareTag("score_14"))
        {
            Score.Instance.ScoreCount += 14;
        }
        else if (other.CompareTag("score_21"))
        {
            Score.Instance.ScoreCount += 21;
        }
        else if (other.CompareTag("score_16"))
        {
            Score.Instance.ScoreCount += 16;
        }
        else if (other.CompareTag("score_32"))
        {
            Score.Instance.ScoreCount += 32;
        }
        else if (other.CompareTag("score_48"))
        {
            Score.Instance.ScoreCount += 48;
        }
        else if (other.CompareTag("score_8"))
        {
            Score.Instance.ScoreCount += 8;
        }
        else if (other.CompareTag("score_24"))
        {
            Score.Instance.ScoreCount += 24;
        }
        else if (other.CompareTag("score_11"))
        {
            Score.Instance.ScoreCount += 11;
        }
        else if (other.CompareTag("score_22"))
        {
            Score.Instance.ScoreCount += 22;
        }
        else if (other.CompareTag("score_33"))
        {
            Score.Instance.ScoreCount += 33;
        }
        else if (other.CompareTag("score_28"))
        {
            Score.Instance.ScoreCount += 28;
        }
        else if (other.CompareTag("score_42"))
        {
            Score.Instance.ScoreCount += 42;
        }
        else if (other.CompareTag("score_27"))
        {
            Score.Instance.ScoreCount += 27;
        }
        else if (other.CompareTag("score_5"))
        {
            Score.Instance.ScoreCount += 5;
        }
        else if (other.CompareTag("score_15"))
        {
            Score.Instance.ScoreCount += 15;
        }
    }
}
