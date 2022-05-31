using System.Collections;
using System.Collections.Generic;
using FPS.Control;
using UnityEngine;

namespace FPS.Attributes
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] float hitPoints = 100f;

        [SerializeField] float expPoints = 25f;

        public void TakeDamage(GameObject player, float damage)
        {
            //BroadcastMessage("OnDamageTaken");
            hitPoints -= damage;
            if(hitPoints <= 0)
            { 
                //GetComponent<Animator>().SetTrigger("isDead");
                //GetComponent<EnemyAI>().enabled = false;
                AwardEXP(player);

                Destroy(gameObject);
            }
        }

        public void AwardEXP(GameObject player)
        {
            player = GameObject.FindWithTag("Player");
            Experience experience = player.GetComponent<Experience>();
            if(experience == null) return;

            experience.GainExperience(expPoints); 
        }
    }
}

