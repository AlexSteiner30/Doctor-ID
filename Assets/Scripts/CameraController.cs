using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float offset;
    [SerializeField] private Camera camera;

    private void LateUpdate()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Buildings"))
        {
            if (Mathf.Abs(Vector3.Distance(obj.transform.position, transform.position)) <= distance)
            {
                obj.SetActive(true);
            }

            else 
            {
                obj.SetActive(false);
            }
        }
    }

    private void Update()
    {
        camera.gameObject.transform.position = new Vector3(camera.transform.position.x, offset, camera.transform.position.z);
    }
}
