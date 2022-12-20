using RPG.core;
using RPG.movement;
using UnityEngine;

namespace RPG.combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health target;
        [SerializeField] float weaponRange;
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] float weaponDamage;

        float timeSinceLastAttack = 10f;

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                Debug.Log("attacking");
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attackTrigger");
                timeSinceLastAttack = 0f;

            }
        }

        public void Attack(GameObject combatTarget) {
            //Debug.Log("hitting");

            target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().startAction(this);
        }
        //animation event
        void Hit()
        {
            if(target!=null)
                target.TakeDamage(weaponDamage);
        }

        public bool CanAttack(GameObject enemy)
        {
            return !enemy.GetComponent<Health>().IsDead();
        }

        public void cancel()
        {
            Debug.Log("[Figher] cancelling");
            GetComponent<Animator>().SetTrigger("stopAttack");
            GetComponent<Mover>().cancel();
            target = null;
        }

        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                timeSinceLastAttack += Time.deltaTime;

                if (target.IsDead())
                    return;

                //Debug.Log(Vector3.Distance(transform.position, target.position));
                if (Vector3.Distance(transform.position, target.transform.position) <= weaponRange)
                {
                    GetComponent<Mover>().cancel();
                    AttackBehaviour();
                }
                else
                {
                    GetComponent<Mover>().MoveTo(target.transform.position, 1.0f);

                }
            }
            else
                timeSinceLastAttack = 10f;
        }

        
    }
}