using System;
using Controllers;
using UnityEngine;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] public Faction faction;
        [SerializeField] public UnitType type;

        public Action<bool> deathListeners;
        
        [SerializeField] public int HP;
        [SerializeField] public int currentHP;

        private GameObject image;
        protected BattleLogicController battleLogicController;
        
        public abstract void PlayDeathAnimation();
        public abstract void PlayDamageAnimation();

        public bool IsHurt()
        {
            return currentHP < HP;
        }
        public void TakeDamage(int damage)
        {
            currentHP -= damage;
            PlayDamageAnimation();
            if (currentHP < 1)
            {
                Die();
            }
        }

        public void ReceiveHeal(int healAmount)
        {
            currentHP += healAmount;
            if (currentHP > HP)
            {
                currentHP = HP;
            }
        }

        public Sprite GetSprite()
        {
            return GetImage().GetComponent<SpriteRenderer>().sprite;
        }

        public GameObject GetImage()
        {
            return transform.GetChild(0).gameObject;
        }

        public void SetBattleController(BattleLogicController battleLogicController)
        {
            this.battleLogicController = battleLogicController;
        }
        

        private void Die()
        {
            battleLogicController.NotifyDeath(gameObject.GetComponent<MovingUnit>());
            deathListeners.Invoke(true);
            PlayDeathAnimation();
            Invoke("DestroyInstance", 1f);
        }

        private void DestroyInstance()
        {
            Destroy(gameObject);
        }
    }
}