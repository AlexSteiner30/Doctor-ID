using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTransform
{
    public Transform vrTarget;
    public Transform IKTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapVRAvatar()
    {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    [SerializeField] private MapTransform head;
    [SerializeField] private MapTransform leftHand;
    [SerializeField] private MapTransform rightHand;

    [SerializeField] private float turnSmoothness;

    [SerializeField] private Vector3 playerOffset;

    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3((player.position.x + playerOffset.x), 0f, (player.position.z + playerOffset.z));
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(player.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);
        transform.rotation = Quaternion.Euler(player.rotation.x, player.transform.rotation.y, player.transform.rotation.z);

        head.MapVRAvatar();
        leftHand.MapVRAvatar();
        rightHand.MapVRAvatar();
    }
}