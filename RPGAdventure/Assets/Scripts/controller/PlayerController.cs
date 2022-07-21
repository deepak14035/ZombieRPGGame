using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.movement;
using RPG.combat;
using RPG.core;

namespace RPG.control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
        }

        private bool moveToCursor()
        {
            Ray ray = getCursorRay();
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Debug.Log("raycast-" + hit.transform.gameObject.name);
                GetComponent<Mover>().startMoveAction(hit.point, 1f);
                return true;
            }
            else
                return false;
        }

        private static Ray getCursorRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;
            if (!Input.GetMouseButton(0)) return;
            if (interactWithCombat()) return;
            else interactWithMovement();
        }
        private bool interactWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(getCursorRay());
            for(int i = 0; i < hits.Length; i++)
            {
                CombatTarget target = hits[i].collider.gameObject.GetComponent<CombatTarget>();
                if (target != null && target.isAlive)
                {
                    Debug.Log("found enemy");
                    GetComponent<Fighter>().Attack(target.gameObject);
                    return true;
                }
            }
            return false;
        }


        private void interactWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                moveToCursor();
            }
        }
    }
}
