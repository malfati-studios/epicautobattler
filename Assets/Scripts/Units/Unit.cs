using Controllers;
using UnityEngine;

namespace Units
{
    public abstract class Unit : Damageable
    {
        [SerializeField] public UnitType type;
        [SerializeField] public GameController.Faction faction;
        [SerializeField] public float speed;
        [SerializeField] public float stopDistance;
        [SerializeField] public Unit target;

        private bool move = true;

        public abstract bool IsSupportClass();
        public abstract void PlayMovingAnimation();
        public abstract void StopMovingAnimation();

        protected virtual void Update()
        {
            SearchForTarget();
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

       
        private void SearchForTarget()
        {
            if (!target)
            {
                target = IsSupportClass()
                    ? BattleController.instance.GetNearestAlly(this)
                    : BattleController.instance.GetNearestEnemy(this);
            }
        }
    }
}