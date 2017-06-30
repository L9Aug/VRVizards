using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Tooltip("The maximum amount of health the entitiy can have.")]
    public float MaxHealth;

    [Tooltip("The amount of health to regenerate per second.")]
    public float HealthRegenRate;

    [Tooltip("The time taken for health to begin regenerating after taking damage.")]
    public float HealthRegenDelay;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    private float currentHealth;    
    private float DamageTakenTime;
    private bool inDamageCooldown = false;
    private bool inRegen = false;

    /// <summary>
    /// Used to change the current health in either direction.
    /// </summary>
    /// <param name="Amount"></param>
    public void ChangeHealth(float Amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + Amount, 0, MaxHealth);
    }

    /// <summary>
    /// Amount as a positive float to be deducted from current health
    /// </summary>
    /// <param name="Amount"></param>
    public void InflictDamage(float Amount)
    {
        // don't allow the dealing of negative damage.
        Amount = (Amount >= 0) ? Amount : 0;
        currentHealth = Mathf.Clamp(currentHealth - Amount, 0, MaxHealth);
        if (Amount != 0) DamageDealt();
    }

    private void DamageDealt()
    {
        DamageTakenTime = Time.fixedTime;
        if (!inDamageCooldown) StartCoroutine(DamageDelay());
    }

    private void BeginRegen()
    {
        if (!inRegen) StartCoroutine(RegenHealth());
    }

    private IEnumerator DamageDelay()
    {
        inDamageCooldown = true;
        while (Time.fixedTime - DamageTakenTime <= HealthRegenDelay)
        {
            yield return null;
        }
        inDamageCooldown = false;
        BeginRegen();
    }

    private IEnumerator RegenHealth()
    {
        inRegen = true;
        while(!inDamageCooldown && (currentHealth < MaxHealth))
        {
            yield return null;
            currentHealth = Mathf.Clamp(currentHealth + (HealthRegenRate * Time.deltaTime), 0, MaxHealth);
        }
        inRegen = false;
    }

}
