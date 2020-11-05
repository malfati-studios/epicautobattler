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

        private void Move()
        {
            if (!move) return;
            if (!target) return;
            if (InRange()) return;

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