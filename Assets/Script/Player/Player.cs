using System;
using CJStudio.SSP.InputSystem;
using Eccentric.Utils;
using Lean.Transition;
using UnityEngine;
using UnityEngine.InputSystem.Users;
namespace CJStudio.SSP.Player {
    class Player : MonoBehaviour {
        [SerializeField] float health = 100f;
        [SerializeField] Player targetPlayer = null;
        Animator anim = null;
        Attack attack = null;
        Movement movement = null;
        ScaledTimer timer = new ScaledTimer ( );
        public InputUser InputUser { get; set; }
        public CharacterController CharacterController { get; private set; }
        public PlayerControl PlayerControl { get; private set; }
        public bool IsTransition { get; private set; }
        public bool IsAttacking => attack.IsAttacking;
        float transitionVel = 0f;
        Vector3 transitionDir = Vector3.zero;
#region EVENT
        public event Action HurtStart = null;
        public event Action HurtEnd = null;
#endregion

        public void TakeDamage (float damage, bool bLastHit = false, float knockBackDis = 0f, float knockBackDur = 0f, EKnockBackDirection knockBackDirection = EKnockBackDirection.BACKWARD) {
            if (movement.IsGuard) {
                bool bStunned = movement.MinusGuardEnergy (damage);
                Vector3 newPos = transform.position + (transform.position - targetPlayer.transform.position).normalized * knockBackDis / (bStunned? .5f : 2f);
                ControllerMove (newPos, knockBackDur);
                return;
            }
            health -= damage;
            if (anim != null) anim.SetTrigger (bLastHit? "Revive": "Hurt");
            if (knockBackDis != 0f) {
                anim.applyRootMotion = false;
                Vector3 direction = Vector3.zero;
                if (knockBackDirection == EKnockBackDirection.BACKWARD) direction = (transform.position - targetPlayer.transform.position).normalized;
                else if (knockBackDirection == EKnockBackDirection.UP) direction = Vector3.up;
                else if (knockBackDirection == EKnockBackDirection.OBLIQUE) direction = ((transform.position - targetPlayer.transform.position).normalized + Vector3.up).normalized;
                Vector3 newPos = transform.position + direction * knockBackDis;
                ControllerMove (newPos, knockBackDur);
            }
            if (bLastHit)
                Debug.Log ("最後一擊");
        }

        //Call this method then you can transition player and it will stop when it touch the wall
        public void ControllerMove (Vector3 targetPosition, float duration) {
            transitionVel = (targetPosition - transform.position).sqrMagnitude / duration;
            transitionDir = (targetPosition - transform.position).normalized;
            timer.Reset (duration);
            IsTransition = true;
        }

#region MONO_MESSAGE
        void Awake ( ) {
            PlayerControl = new PlayerControl ( );
            PlayerControl.Enable ( );
            CharacterController = GetComponent<CharacterController> ( );
            anim = GetComponent<Animator> ( );
            attack = GetComponent<Attack> ( );
            movement = GetComponent<Movement> ( );
            if (attack) {
                attack.Player = this;
                attack.TargetPlayer = targetPlayer;
            }
            if (movement) movement.Player = this;
        }

        void Update ( ) {
            ControllerMoving ( );
        }

        void OnControllerColliderHit (ControllerColliderHit hit) {
            if (hit.collider.tag == "Wall")
                IsTransition = false;
        }

#endregion
        //Call this method in Update to keep update moving
        void ControllerMoving ( ) {
            if (IsTransition) {
                if (timer.IsFinished) {
                    IsTransition = false;
                    return;
                }
                CharacterController.Move (transitionDir * transitionVel * Time.deltaTime);
            }
        }

#region CALLBACK
        void OnHurtStart ( ) {
            if (HurtStart != null)
                HurtStart ( );
        }

        void OnHurtEnd ( ) {
            if (HurtEnd != null)
                HurtEnd ( );
        }
#endregion
    }
}