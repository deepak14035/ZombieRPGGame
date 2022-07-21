using RPG.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        public bool isAlive = true;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Health>().IsDead()) isAlive = false;
        }
    }
}