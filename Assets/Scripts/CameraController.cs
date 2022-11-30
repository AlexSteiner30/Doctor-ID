using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float radius;

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, 10))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
