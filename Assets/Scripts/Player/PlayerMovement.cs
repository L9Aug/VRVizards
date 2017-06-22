using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public NavMeshAgent myAgent;
    public GameObject PlayerObj;

    public void SetCarpetDestination(Vector3 pos)
    {
        myAgent.SetDestination(pos);
    }

    private void FixedUpdate()
    {
        PlayerObj.transform.position = transform.position;
    }

}
