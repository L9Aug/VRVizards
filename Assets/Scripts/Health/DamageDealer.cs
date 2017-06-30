using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    public float DamageAmount;

    public Global.DamageTypes DamageType;

    private void TransmitDamage(DamageReciever opp, Collider other)
    {
        if(opp.HealthObj != null)
        {
            opp.HealthObj.InflictDamage(DamageAmount * opp.DamagePotency);
        }
        else
        {
            other.SendMessageUpwards("InflictDamage", DamageAmount * opp.DamagePotency, SendMessageOptions.RequireReceiver);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageReciever temp = other.GetComponent<DamageReciever>();
        if (temp != null) TransmitDamage(temp, other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DamageReciever temp = collision.gameObject.GetComponent<DamageReciever>();
        if(temp != null)
        {
            TransmitDamage(temp, collision.collider);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        DamageReciever temp = other.GetComponent<DamageReciever>();
        Collider col = other.GetComponent<Collider>();
        if(temp != null && col != null)
        {
            TransmitDamage(temp, col);
        }
    }

}
