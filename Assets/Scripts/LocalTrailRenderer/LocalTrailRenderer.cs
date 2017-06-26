using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Based off of implmentation found here: https://forum.unity3d.com/threads/trail-renderer-local-space.97756/
*/

[RequireComponent(typeof(LineRenderer))]
public class LocalTrailRenderer : MonoBehaviour
{

    /// <summary>
    /// The object that we want to draw a trail for.
    /// </summary>
    [Tooltip("The object that we want to draw a trail for.")]
    public Transform Target;

    /// <summary>
    /// The smallest distance required before adding a new point.
    /// </summary>
    [Tooltip("The smallest distance required before adding a new point.")]
    public float Resolution = 0.1f;

    public Transform HMD;

    private LineRenderer myLine;
    private Vector3 LastPos;

    public Vector3 GetImageCenter()
    {
        Vector3 Smallest = myLine.GetPosition(0);
        Vector3 Largest = myLine.GetPosition(0);

        for(int i = 0; i < myLine.positionCount; ++i)
        {
            Vector3 current = myLine.GetPosition(i);
            if(current.x < Smallest.x)
            {
                Smallest.x = current.x;
            }
            else if(current.x > Largest.x)
            {
                Largest.x = current.x;
            }

            if (current.y < Smallest.y)
            {
                Smallest.y = current.y;
            }
            else if (current.y > Largest.y)
            {
                Largest.y = current.y;
            }

            if (current.z < Smallest.z)
            {
                Smallest.z = current.z;
            }
            else if (current.z > Largest.z)
            {
                Largest.z = current.z;
            }
        }

        return (Smallest + Largest) / 2f;
    }

    public Vector3 GetPointNormal()
    {
        Vector3 Center = GetImageCenter();
        Vector3 Normal = Vector3.zero;
        int NumAverages = 5;

        for(int i = 0; i < 5; ++i)
        {
            Vector3 TempNormal = Vector3.Cross(myLine.GetPosition(Random.Range(0, myLine.positionCount)) - Center, myLine.GetPosition(Random.Range(0, myLine.positionCount)) - Center);
            Normal += (Vector3.Angle(TempNormal, (HMD.position - Target.position)) > 90) ? -TempNormal : TempNormal;
        }

        return Normal / (float)NumAverages;
    }

    public float GetImageSize()
    {
        float size = 0;
        Vector3 Center = GetImageCenter();

        for(int i = 0; i < myLine.positionCount; ++i)
        {
            float Dist = Vector3.Distance(Center, myLine.GetPosition(i));
            if(Dist > size)
            {
                size = Dist;
            }
        }
        print(size);
        return size;
    }

    // Use this for initialization
    private void Awake()
    {
        myLine = GetComponent<LineRenderer>();
        myLine.useWorldSpace = false;
        Reset();
    }

    // put the line renderer back to it's starting state
    private void Reset()
    {
        myLine.positionCount = 0;
        AddPoint(Target.localPosition);
    }

    private void OnEnable()
    {
        // upon re-enabling this component reset the line renderer
        myLine.enabled = true;
        Reset();
    }

    private void OnDisable()
    {
        // when this object is disabled also turn off the line renderer
        myLine.enabled = false;
    }

    // Add a point to the line renderer
    private void AddPoint(Vector3 newPoint)
    {
        ++myLine.positionCount;
        myLine.SetPosition(myLine.positionCount - 1, newPoint);
        LastPos = newPoint;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 CurrentPos = Target.localPosition;
        if(Vector3.Distance(CurrentPos, LastPos) >= Resolution)
        {
            AddPoint(CurrentPos);
        }
    }
}
