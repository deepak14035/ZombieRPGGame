using RPG.combat;
using RPG.core;
using RPG.movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.control
{
    public class AIController : MonoBehaviour
    {
        Fighter fighter;
        GameObject player;
        [SerializeField] private float chaseDistance;
        [SerializeField] private float suspicionTime=5f;
        [SerializeField] private Waypoints patrolPoints;
        [SerializeField] private float waypointTolerance = 0.5f;
        [SerializeField] private float waitPeriod = 3f;
        [SerializeField] private float patrolSpeedFraction = 0.3f;
        Health health;
        Vector3 guardLocation;
        float timeSinceLastSeenPlayer = 1f;
        int nextWaypointIndex = 0;
        float timeSinceWaypointReach = 0f;

        // Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindGameObjectWithTag("Player");
            guardLocation = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastSeenPlayer += Time.deltaTime;
            if (health.IsDead()) return;
            if(isInAttackRange() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSeenPlayer<suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                guardLocation = getWaypointPosition();
                GuardBehaviour();
                if(Vector3.Distance(guardLocation, transform.position) < waypointTolerance)
                {
                    timeSinceWaypointReach += Time.deltaTime;
                    if (timeSinceWaypointReach < waitPeriod)
                    {
                        GetComponent<Mover>().cancel();
                    }else
                        cycleWaypoint();
                }
                else
                {
                    timeSinceWaypointReach = 0f;
                }
            }
        }

        private void GuardBehaviour()
        {
            GetComponent<Mover>().startMoveAction(guardLocation, patrolSpeedFraction);
        }

        private void SuspicionBehaviour()
        {
            fighter.cancel();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSeenPlayer = 0f;
            fighter.Attack(player);
        }

        private void cycleWaypoint()
        {
            nextWaypointIndex = patrolPoints.getNextIndex(nextWaypointIndex);
        }

        private Vector3 getWaypointPosition()
        {
            return patrolPoints.getWaypointPosition(nextWaypointIndex);
        }

        bool isInAttackRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }
    }
}