﻿using System;
using Controllers;
using UnityEngine;

namespace Units
{
    public abstract class GameEntity : MonoBehaviour
    {
        public Action<bool> deathListeners;
        
        [SerializeField] public int HP;

        private GameObject image;
        protected BattleLogicController battleLogicController;
        
        public abstract void PlayDeathAnimation();
        public abstract void PlayDamageAnimation();
        
        public bool TakeDamage(int damage)
        {
            HP -= damage;
            PlayDamageAnimation();
            if (HP < 1)
            {
                Die();
                return true;
            }
            return false;
        }

        public Sprite GetSprite()
        {
            return transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }

        public void SetBattleController(BattleLogicController battleLogicController)
        {
            this.battleLogicController = battleLogicController;
        }

        private void Die()
        {
            battleLogicController.NotifyDeath(gameObject.GetComponent<Unit>());
            PlayDeathAnimation();
            Invoke("DestroyInstance", 1f);
        }

        private void DestroyInstance()
        {
            Destroy(gameObject);
        }
    }
}