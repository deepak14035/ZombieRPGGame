using RPG.core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private float MaxSpeed;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Health>().IsDead())
                GetComponent<NavMeshAgent>().enabled = false;
            updateAnimation();
        }

        public void cancel()
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        public void MoveTo(Vector3 pos, float speedFraction)
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().speed = MaxSpeed * speedFraction;
            GetComponent<NavMeshAgent>().destination = pos;
        }

        public void startMoveAction(Vector3 pos, float speedFraction)
        {
            GetComponent<ActionScheduler>().startAction(this);
            //GetComponent<Fighter>().cancel();
            MoveTo(pos, speedFraction);
        }

        void updateAnimation()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            //Debug.Log(GetComponent<NavMeshAgent>().velocity + "local velocity - " + localVelocity);
            gameObject.GetComponent<Animator>().SetFloat("playerSpeed", localVelocity.z);
        }
    }
}