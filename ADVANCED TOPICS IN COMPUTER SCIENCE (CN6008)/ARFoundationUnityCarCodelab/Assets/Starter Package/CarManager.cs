/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;

using UnityEngine;
using UnityEngine.XR.ARFoundation;

/**
 * Spawns a <see cref="CarBehaviour"/> when a plane is tapped.
 */
public class CarManager : MonoBehaviour
{
    public GameObject CarPrefab, CarPrefab2;
    GameObject CarPrefab1Obj, CarPrefab2Obj;
    bool doOnce;
    public ReticleBehaviour Reticle;
    public DrivingSurfaceManager DrivingSurfaceManager;

    public CarBehaviour Car, Car2;

    private void Update()
    {
#if UNITY_EDITOR
        //GameObject Obj;
        if (Input.GetKeyDown(KeyCode.W))
        {
            CarPrefab1Obj = GameObject.Instantiate(CarPrefab); 
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CarPrefab1Obj.SetActive(false);
            CarPrefab2Obj = GameObject.Instantiate(CarPrefab2);
        }
#endif

        if (Car == null && WasTapped() && Reticle.CurrentPlane != null)
        {
            // Spawn our car at the reticle location.
            //var obj = GameObject.Instantiate(CarPrefab);
            CarPrefab1Obj = GameObject.Instantiate(CarPrefab);
            Car = CarPrefab1Obj.GetComponent<CarBehaviour>();
            Car.Reticle = Reticle;
            Car.transform.position = Reticle.transform.position;

            CarPrefab2Obj = GameObject.Instantiate(CarPrefab2);
            CarPrefab2Obj.SetActive(false);

            DrivingSurfaceManager.LockPlane(Reticle.CurrentPlane);
        }

        if ((Score.scoreCount >= 5 || Input.location.lastData.longitude >= 180.0) && !doOnce)
        {
            CarPrefab1Obj.SetActive(false);
            Destroy(CarPrefab1Obj); // Double points?
            CarPrefab2Obj.SetActive(true);
            Car = CarPrefab2Obj.GetComponent<CarBehaviour>();
            Car.Reticle = Reticle;
            Car.transform.position = Reticle.transform.position;
            doOnce = true;
        }
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}
