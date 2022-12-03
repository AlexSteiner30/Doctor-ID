using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject camera;

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
        camera.transform.position = new Vector3(transform.position.x + offset.x, transform.position.y +  offset.y, transform.position.z + offset.z);
    }
}
