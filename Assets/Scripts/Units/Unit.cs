﻿using Controllers;
using UnityEngine;

namespace Units
{
    public abstract class Unit : Damageable
    {
        [SerializeField] public UnitType type;
        [SerializeField] public GameController.Faction faction;
        [SerializeField] public float speed;
        [SerializeField] public float stopDistance;
        [SerializeField] public GameObject target;

        private bool move = true;

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

        public void SetTarget(GameObject target)
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
                Unit uTarget = BattleController.instance.GetNearestAlly(this);
                if (uTarget != null)
                {
                    target = BattleController.instance.GetNearestAlly(this).gameObject;
                }
            }
        }
    }
}