using System;
using UnityEngine;

namespace Units
{
    public abstract class MovingUnit : Unit
    {
        [SerializeField] public float speed;
        [SerializeField] public float stopDistance;
        [SerializeField] public Unit target;

        private DateTime lastTargetSearch = DateTime.Now;

        private bool move = true;
        protected Animator animator;
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
            if (BattleStarted() && !HasTarget() && CheckLastTargetSearchTime())
            {
                SearchForTarget();
            }

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

        public void SetTarget(MovingUnit target)
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

        public virtual void SearchForTarget()
        {
            if (!target)
            {
                target = IsSupportClass()
                    ? battleLogicController.GetNearestAlly(this)
                    : battleLogicController.GetNearestEnemy(this);

                if (target != null)
                {
                    target.deathListeners += OnTargetDeath;
                }
            }
        }

        protected void OnTargetDeath(bool obj)
        {
            ClearTarget();
            SearchForTarget();
        }

        private void Move()
        {
            if (!move || !HasTarget() || InRange())
            {
                StopMovingAnimation();
                return;
            }

            PlayMovingAnimation();
            transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        private bool CheckLastTargetSearchTime()
        {
            return DateTime.Now > lastTargetSearch.AddMilliseconds(500);
        }
    }
}