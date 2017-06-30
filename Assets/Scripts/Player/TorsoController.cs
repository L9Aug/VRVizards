using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoController : MonoBehaviour
{

    public Transform HeadTarget;
    public float GroundCheckDistance = 3;

    private void FixedUpdate()
    {
        transform.position = GetFloorPoint();
        transform.rotation = Quaternion.Euler(0, HeadTarget.rotation.eulerAngles.y, 0);
    }

    Vector3 GetFloorPoint()
    {
        Ray ray = new Ray(HeadTarget.position, Vector3.down);
        RaycastHit hit = new RaycastHit();
        hit.point = HeadTarget.position + (GroundCheckDistance * Vector3.down);
        Physics.Raycast(ray, out hit, GroundCheckDistance, 1 << 11, QueryTriggerInteraction.Ignore);
        return hit.point;        
    }

}
