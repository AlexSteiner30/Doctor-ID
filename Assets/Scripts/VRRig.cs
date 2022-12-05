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

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 playerOffset;

<<<<<<< HEAD
    [SerializeField] private Camera camera;
    [SerializeField] private Vector3 cameraOffset;

    private void FixedUpdate()
    {
        camera.gameObject.transform.position = new Vector3((camera.gameObject.transform.position.x + cameraOffset.x) , cameraOffset.y , (camera.gameObject.transform.position.z + cameraOffset.z));
        transform.rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);

=======
    [SerializeField] private Transform player;

    private void Update()
    {
>>>>>>> parent of 028f10b (VR Rig some bugs fix)
        transform.position = new Vector3((player.position.x + playerOffset.x), 0f, (player.position.z + playerOffset.z));
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(player.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);
      
        head.MapVRAvatar();
        leftHand.MapVRAvatar();
        rightHand.MapVRAvatar();
    }
}