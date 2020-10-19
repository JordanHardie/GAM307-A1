using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathMessage : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            gameObject.SendMessageUpwards("OnDeath");
        }
    }
}
