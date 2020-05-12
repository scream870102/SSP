using System.Collections;
using System.Collections.Generic;
using CJStudio.SSP.InputSystem;
using Lean.Transition;
using UnityEngine;
using UnityEngine.InputSystem;
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
        public float Radius => CharacterController.radius;
        public InputUser InputUser { get; set; }
        public PlayerControl PlayerControl { get; private set; }
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

        public void TakeDamage (float damage, bool bLastHit = false, float distanceForKnockBack = 0f, Vector3 knockBackDirection = new Vector3 ( )) {
            //Debug.Log (this.name + "Get Hit");
            health -= damage;
            if (anim != null) anim.SetTrigger (bLastHit? "Revive": "Hurt");
            if (distanceForKnockBack != 0f) {
                anim.applyRootMotion = false;
                Vector3 newPos = transform.position + knockBackDirection * distanceForKnockBack;
                transform.positionTransition (newPos, .1f);
            }
            if (bLastHit)
                Debug.Log ("最後一擊");
        }
        void OnHurted ( ) {
            anim.applyRootMotion = true;
            Debug.Log ("痛痛痛");

        }
    }
}