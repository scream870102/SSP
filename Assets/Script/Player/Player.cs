using System;
using CJStudio.SSP.InputSystem;
using Eccentric.Utils;
using Lean.Transition;
using UnityEngine;
using UnityEngine.InputSystem.Users;
namespace CJStudio.SSP.Player {
    class Player : MonoBehaviour {
        [SerializeField] float health = 100f;
        public string PlayerName = "";
        Animator anim = null;
        Attack attack = null;
        Movement movement = null;
        public CharacterController CharacterController { get; private set; }
        public bool IsAttacking => attack.IsAttacking;
        public InputUser InputUser { get; set; }
        public PlayerControl PlayerControl { get; private set; }
        public bool IsHurted { get; private set; }
#region HURT
        public event Action HurtStart;
        public event Action HurtEnd;
#endregion
#region TRANSITION
        bool bMoving = false;
        ScaledTimer timer = new ScaledTimer ( );
        float velocity = 0f;
        Vector3 direction = Vector3.zero;
#endregion
        // Start is called before the first frame update
        void Awake ( ) {
            PlayerControl = new PlayerControl ( );
            PlayerControl.Enable ( );
            CharacterController = GetComponent<CharacterController> ( );
            anim = GetComponent<Animator> ( );
            attack = GetComponent<Attack> ( );
            movement = GetComponent<Movement> ( );
            if (attack) attack.Player = this;
            if (movement) movement.Player = this;
        }

        void Update ( ) {
            ControllerMoving ( );
        }

        void ControllerMoving ( ) {
            if (bMoving) {
                if (timer.IsFinished) {
                    bMoving = false;
                    return;
                }
                CharacterController.Move (direction * velocity * Time.deltaTime);
            }
        }

        public void TakeDamage (float damage, bool bLastHit = false, float distanceForKnockBack = 0f, Vector3 knockBackDirection = new Vector3 ( )) {
            if (movement.IsGuard) {
                movement.MinusGuardEnergy (damage);
                return;
            }
            health -= damage;
            if (anim != null) anim.SetTrigger (bLastHit? "Revive": "Hurt");
            if (distanceForKnockBack != 0f) {
                anim.applyRootMotion = false;
                Vector3 newPos = transform.position + knockBackDirection * distanceForKnockBack;
                ControllerMove (newPos, .1f);
            }
            if (bLastHit)
                Debug.Log ("最後一擊");
        }

        void OnControllerColliderHit (ControllerColliderHit hit) {
            if (hit.collider.tag == "Wall")
                bMoving = false;
        }

        void ControllerMove (Vector3 targetPosition, float duration) {
            velocity = (targetPosition - transform.position).sqrMagnitude / duration;
            direction = (targetPosition - transform.position).normalized;
            timer.Reset (duration);
            bMoving = true;
        }

        void OnHurtStart ( ) {
            if (HurtStart != null)
                HurtStart ( );
        }

        void OnHurtEnd ( ) {
            if (HurtEnd != null)
                HurtEnd ( );
        }
    }
}