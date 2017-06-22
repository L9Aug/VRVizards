using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcController : MonoBehaviour
{

    public int NumSegments = 2;

    bool DrawLine = false;
    LineRenderer LR;
    public float lineRange = 100;

    private void Start()
    {
        LR = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (DrawLine)
        {            
            LR.enabled = true;
            LR.positionCount = 2;
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, GetHitPos());
        }
    }

    public Vector3 GetHitPos()
    {
        RaycastHit hit = new RaycastHit();
        if (!Physics.Raycast(transform.position, transform.forward, out hit, lineRange, ~0, QueryTriggerInteraction.Ignore))
        {
            HideLineFunc();
            hit.point = transform.position;
        }
        return hit.point;
    }

    public void HideLineFunc()
    {
        LR.enabled = false;
    }

    public void EndDraw()
    {
        HideLineFunc();
        DrawLine = false;
    }

    public void DrawLineFunc()
    {        
        DrawLine = true;
    }

}
