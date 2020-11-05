using System;
using UnityEngine;

namespace Units
{
    public abstract class SupportUnit : Unit
    {
        [SerializeField] public int millisBetweenSupports;

        private DateTime lastSupportTime = DateTime.Now;

        public abstract void PlaySupportAnimation();

        public virtual void Support()
        {
            lastSupportTime = DateTime.Now;
            PlaySupportAnimation();
        }

        public new virtual void Update()
        {
            base.Update();
            if (HasTarget() && InRange() && CheckSupportTime())
            {
                Support();
            }
        }

        private bool CheckSupportTime()
        {
            return DateTime.Now > lastSupportTime.AddMilliseconds(millisBetweenSupports);
        }
    }
}