﻿using UnityEngine;
using UnityEngine.UI;

namespace Squeak
{
    public class EnemyStatusController : MonoBehaviour
    {
        public EnemyStatusController Instance { get; private set; }
        public EnemyStatusPreset _preset;
        public Slider _healthBar;
        [SerializeField] Animator _animator = null;
        public delegate void OnEnemyDeath();
        public event OnEnemyDeath OnDeathEvent;

        public delegate void OnEnemyDamage();
        public event OnEnemyDamage OnDamageEvent;

        private StatBar _health;
        
        //TODO: Gambiarra (talvez colocar todas os triggers de animacao em uma classe separada, ou no state machine do inimigo)
        readonly int HitTriggerID = Animator.StringToHash("Hit");
        readonly int DieTriggerID = Animator.StringToHash("Die");


        public bool isDead = false;

        // +-------------------------+
        // | MonoBehaviour lifecycle |
        // +-------------------------+
        void Awake()
        {
            if (Instance == null)
                Instance = this;

            _health = new StatBar(_preset.maxHealth);

            OnDamageEvent += () => _animator.SetTrigger(HitTriggerID);
            OnDeathEvent += () => _animator.SetTrigger(DieTriggerID);
        }
        
        public void Damage(float damage)
        {
            _health.Decrease(damage);

            if (_health.Value > 0.0f)
            {
                Debug.Log($"{damage} de dano, vida atual {_health.Value}");
                OnDamageEvent?.Invoke();
            }
            else
            {
                isDead = true;
                Debug.Log("Morte");
                OnDeathEvent?.Invoke();
            }
            
            _healthBar.value = _health.GetHealthPercentage();
        }
    }
}