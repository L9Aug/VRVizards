using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisibleTrailController : MonoBehaviour
{

    public LineRenderer MyLinePoints;
    LineRenderer myLine;

    // Use this for initialization
    void Start()
    {
        myLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myLine.positionCount = MyLinePoints.positionCount;
        myLine.SetPositions(GetLinePoints());
    }

    Vector3[] GetLinePoints()
    {
        Vector3[] positions = new Vector3[MyLinePoints.positionCount];
        for(int i = 0; i < MyLinePoints.positionCount; ++i)
        {
            positions[i] = MyLinePoints.GetPosition(i);
        }
        return positions;
    }

    private void OnDisable()
    {
        myLine.positionCount = 0;
    }

}
