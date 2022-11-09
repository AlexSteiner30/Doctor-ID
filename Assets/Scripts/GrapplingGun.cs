using UnityEngine;
 
public class GrapplingGun : MonoBehaviour
{
    [Header("Grappling Gun")]
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;

    [Header("References")] 
    public PlayerController controller;
    
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private SpringJoint joint;
 
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        controller = GetComponent<PlayerController>();
    }
 
    void Update()
    {
        if (controller.ShootPressed())
        {
            StartGrapple();
        }
        else if (!controller.ShootPressed())
        {
            StopGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
 
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
            
            joint.spring = 10f;
            joint.damper = 7f;
            joint.massScale = 6f;
 
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }
    
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
 
    private Vector3 currentGrapplePosition;
 
    void DrawRope()
    {
        if (!joint) return;
 
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
 
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }
    
    public bool IsGrappling()
    {
        return joint != null;
    }
 
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}