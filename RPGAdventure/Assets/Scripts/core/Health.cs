using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.core{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        public bool IsDead() {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(0, health-damage);
            print(health);
            if (health == 0&&!isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            GetComponent<ActionScheduler>().cancelAction();
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}