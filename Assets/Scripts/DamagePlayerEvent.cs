using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerEvent : MonoBehaviour
{
    public delegate void DamagePlayerAction(GameObject player);
    public static event DamagePlayerAction OnDamagePlayer;

    void OnTriggerEnter(Collider collider)
    {
        if (!GetComponentInParent<Enemy>().isDead)
        {
            if(collider.gameObject.tag == "Player")
            {
                OnDamagePlayer?.Invoke(collider.gameObject);
            }
        }
    }
}
