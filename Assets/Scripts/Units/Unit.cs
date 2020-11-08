using Controllers;
using UnityEngine;

namespace Units
{
    public abstract class Unit : GameEntity
    {
        [SerializeField] public UnitType type;
        [SerializeField] public Faction faction;
        [SerializeField] public float speed;
        [SerializeField] public float stopDistance;
        [SerializeField] public Unit target;

        private bool move = true;
        private Animator animator;
        private static readonly int MovingAxis = Animator.StringToHash("MovingAxis");

        public abstract bool IsSupportClass();
    
        private void PlayMovingAnimation()
        {
            animator.SetInteger(MovingAxis, GetMovingAxis());
        }

        private void StopMovingAnimation()
        {
            animator.SetInteger(MovingAxis, 0);

        }

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            Move();
        }

        public void StartMoving()
        {
            move = true;
        }

        public void StopMoving()
        {
            move = false;
        }

        public void SetTarget(Unit target)
        {
            this.target = target;
        }

        public void ClearTarget()
        {
            target = null;
        }

        public bool HasTarget()
        {
            return target != null;
        }

        public bool InRange()
        {
            return (transform.position - target.transform.position).magnitude <= stopDistance;
        }

        public int GetMovingAxis()
        {
            if (InRange()) return 0;
            if (target.transform.position.x > transform.position.x)
            {
                return 1;
            }

            return -1;
        }
        
        public void SearchForTarget()
        {
            if (!target)
            {
                target = IsSupportClass()
                    ? BattleController.instance.GetNearestAlly(this)
                    : BattleController.instance.GetNearestEnemy(this);
            }
        }

        private void Move()
        {
            if (!move || !target || InRange())
            {
                StopMovingAnimation();
                return;
            }
           
            PlayMovingAnimation();
            transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

    }
}