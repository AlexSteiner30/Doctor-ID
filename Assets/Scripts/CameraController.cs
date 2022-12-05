using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float distance;

    [SerializeField] private GameObject camera;

    [SerializeField] private Vector3 offsetCamera;

    private void LateUpdate()
    {
        LoadChunk();
    }

    private void LoadChunk()
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
}