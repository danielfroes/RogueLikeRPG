using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Squeak
{
    public class EnemyStatusController : MonoBehaviour
    {
        public static EnemyStatusController Instance { get; private set; }
        public EnemyStatusPreset _preset;
        public Slider _healthBar;
        [SerializeField] Animator _animator = null;
        public delegate void OnEnemyDeath();
        public static event OnEnemyDeath OnDeathEvent;

        public delegate void OnEnemyDamage();
        public static event OnEnemyDamage OnDamageEvent;

        private StatBar _health;
        
        //TODO: Gambiarra (talvez colocar todas os triggers de animacao em uma classe separada, ou no state machine do inimigo)
        static readonly int HitTriggerID = Animator.StringToHash("Hit");
        static readonly int DieTriggerID = Animator.StringToHash("Die");
        
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
                Debug.Log("Morte");
                OnDeathEvent?.Invoke();
            }
            
            _healthBar.value = _health.GetHealthPercentage();
        }
    }
}