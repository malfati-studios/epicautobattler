using System;
using Controllers;
using UI;
using UnityEngine;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] public Faction faction;
        [SerializeField] public UnitType type;
        
        [SerializeField] private UnitHealthBar healthBar;

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
            healthBar.UpdateBar(HP, currentHP);
            if (currentHP < 1)
            {
                Die();
            }
        }

        public void ReceiveHeal(int healAmount)
        {
            currentHP += healAmount;
            healthBar.UpdateBar(HP, currentHP);
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

        protected bool BattleStarted()
        {
            return battleLogicController != null && battleLogicController.BattleStarted();
        }
        
        private void Die()
        {
            battleLogicController.NotifyDeath(gameObject.GetComponent<MovingUnit>());
            deathListeners.Invoke(true);
            PlayDeathAnimation();
            Invoke("DeactivateInstance", 1f);
        }

        private void DeactivateInstance()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            FindHealthBar();
        }

        private void FindHealthBar()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).CompareTag("UnitHealthBar"))
                {
                    healthBar = transform.GetChild(i).GetComponent<UnitHealthBar>();
                }
            }
        }
    }
}