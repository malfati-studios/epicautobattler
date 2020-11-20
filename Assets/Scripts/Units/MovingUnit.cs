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
        private static readonly int Moving = Animator.StringToHash("Moving");

        public abstract bool IsSupportClass();

        private void PlayMovingAnimation()
        {
            animator.SetBool(Moving, true);
        }

        private void StopMovingAnimation()
        {
            animator.SetBool(Moving, false);
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
            base.Update();
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
            FlipSprite();
            if (!move || !HasTarget() || InRange())
            {
                StopMovingAnimation();
                return;
            }

            PlayMovingAnimation();
            transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        private void FlipSprite()
        {
            if (!HasTarget()) return;
            if (target.transform.position.x > transform.position.x)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        private bool CheckLastTargetSearchTime()
        {
            return DateTime.Now > lastTargetSearch.AddMilliseconds(500);
        }
    }
}