using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;

        private void OnTriggerEnter(Collider other)
        {
            PlayerStats playerStats= other.GetComponentInParent<PlayerStats>();
            if(playerStats!=null)
            {
                playerStats.TakeDamage(damage);
                print("hit");
            }
        }
    }
}
