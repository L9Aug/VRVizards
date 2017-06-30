using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageReciever : MonoBehaviour
{
    [Tooltip("The percentatge of damage to inflict from this collider.")]
    public float DamagePotency;

    [Tooltip("Only needs to be set if the health object is in an obscure place.")]
    public Health HealthObj;


}
