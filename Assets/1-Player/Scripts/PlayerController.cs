using System;
using System.Collections;
using DirectionSystem;
using UnityEngine;
using UnityEngine.InputSystem;

// SORT OUT CODES IN ACTION MENU
// SWAP ACTION-MAPS WHEN ACTIVATING MENU

namespace Squeak
{
    public class PlayerController : MonoBehaviour
    {
        public Sound dash;
        public Sound damaged;

        // movement stuff
        public float movementDuration;
        public float movementBufferTime;
        public AnimationCurve transitionCurve;

        private Vector2 currentInputDirection = Vector2.zero;
        public Input inputManager;
		public ActionMenuController actionMenu;
		[SerializeField] private GameObject _actionMenu;
        private EnemySelector enemySelector;
        [SerializeField] ActionTargetSelector _actionTargetSelector;

        // lots of bools for state management
        private bool _moving;
        private bool _casting;
        private bool _damaged;
        private bool _canBuffer;
        private bool _dead;

        // components
        private Animator _animator;
        bool _winned;

        // other stuff
        private Vector2 Position => CombatGrid.Instance.PositionToCellCenter(transform.position);

        private void OnEnable()
        {
        	inputManager.Player.Enable();
        	inputManager.ActionMenu.Disable();
        }
		private void OnDisable()
		{
			inputManager.Player.Disable();
			inputManager.ActionMenu.Disable();
		}
        
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            // New input system logic
			inputManager = new Input();

			inputManager.Player.Move.performed += _0 =>
				ManageInput(_0.ReadValue<Vector2>());

			// Activates the ActionMenu when proper input is performed
			inputManager.Player.Menu.performed += _0 => ActivateActionMenu();
			inputManager.ActionMenu.Menu.performed += _0 => ActivateActionMenu();

            inputManager.Player.SelectLeftEnemy.performed += _0 => 
                enemySelector.SelectEnemy(Direction.left);
            inputManager.Player.SelectRightEnemy.performed += _0 => 
                enemySelector.SelectEnemy(Direction.right);
				
        }

        void Start()
        {
            PlayerStatusController.OnDamageEvent += () =>
            {
                CancelCasting();
                StopAllCoroutines();
                StartCoroutine(Damage());
            };
            PlayerStatusController.OnDeathEvent += Die;

            enemySelector = GetComponent<EnemySelector>();
        }

        private void Update()
        {
            if (!_moving && currentInputDirection != Vector2.zero)
            {
                StartCoroutine(Move(DirectionUtils.Vec3ToDir(currentInputDirection)));
                currentInputDirection = Vector2.zero;
            }
        }

        private void ActivateActionMenu()
        {
        	if (!_actionMenu.activeInHierarchy)
        	{
        		inputManager.ActionMenu.Enable();
            	inputManager.Player.Disable();
        	}
        	else
        	{
        		inputManager.Player.Enable();
            	inputManager.ActionMenu.Disable();
        	}

        	actionMenu.Activate();
        }

        public void ManageInput(Vector2 direction)
        {
            if (Time.time == 0f)
                return;

            if (_damaged)
                return;

            if (_casting)
            {
                CancelCasting();
                currentInputDirection = direction;
            }

            if (!_moving || _canBuffer)
            {
                currentInputDirection = direction;
            }
        }

        private IEnumerator Move(Direction direction)
        {
            AudioManager.Play(dash);
            
            Vector2 origin = Position;
            Vector2 destination =
                Position + DirectionUtils.DirToVec3(direction) * CombatGrid.Instance.DistanceBetweenTiles;

            if (!CombatGrid.Instance.IsPositionInGrid(destination))
            {
                _animator.Play("Idle");
                yield break;
            }

            // Animation
            switch (direction)
            {
                case Direction.up:
                    _animator.Play("MovingUp");
                    break;
                case Direction.down:
                    _animator.Play("MovingDown");
                    break;
                case Direction.left:
                    _animator.Play("MovingLeft");
                    break;
                case Direction.right:
                    _animator.Play("MovingRight");
                    break;
            }

            // Movement
            _moving = true;
            float time = movementDuration;

            while (time > 0f)
            {
                if (time <= movementBufferTime)
                    _canBuffer = true;

                float t = 1f - (time / movementDuration);
                t = transitionCurve.Evaluate(t);

                Vector3 currentPosition = Vector3.zero;
                currentPosition.x = Mathf.Lerp(origin.x, destination.x, t);
                currentPosition.y = Mathf.Lerp(origin.y, destination.y, t);

                transform.position = currentPosition;

                yield return null;
                time -= Time.deltaTime;
            }

            transform.position = destination;
            _canBuffer = false;

            if (currentInputDirection == Vector2.zero)
                _animator.Play("Idle");

            _moving = false;
        }

        private IEnumerator Damage()
        {
            AudioManager.Play(damaged);
            
            _damaged = true;
            _animator.Play("Damage");
            transform.position = Position;

            StartCoroutine(FreezeFrame(0.25f));

            yield return new WaitForSeconds(0.25f);
            currentInputDirection = Vector2.zero;
            _moving = false;
            _canBuffer = false;

            _animator.Play("Idle");
            _damaged = false;
        }

        public void ActivateWinComemoration()
        {
            CancelCasting();
            _winned = true;
            _animator.Play("WinDance");
        }
        
        public void DeactivateWinComemoration()
        {
            CancelCasting();
            _winned = false;
            _animator.Play("Idle");
        }
        private void Die()
        {
            //inputListener.enabled = false;

            CancelCasting();
            StopAllCoroutines();
            _animator.Play("Death");
            enabled = false;
        }

        public void Cast(PlayerAction action)
        {
            _casting = true;
            inputManager.Player.Enable();
            inputManager.ActionMenu.Disable();
            StartCoroutine(WaitCast((action)));
        }

        private void CancelCasting()
        {
            if (!_casting)
                return;
            _casting = false;
            StopAllCoroutines();
            ActionCaster.instance.CancelCasting();
        }

        private IEnumerator WaitCast(PlayerAction action)
        {
            _animator.Play("Charge");
            yield return new WaitForSeconds(action.castTime);
            _animator.Play("Attack");
            AudioManager.Play(action.activeSound);
            _casting = false;
        }

        private IEnumerator FreezeFrame(float time)
        {
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(time);
            Time.timeScale = 1f;
        }
    }
}