using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace CJStudio.SSP.Player {
    class Movement : MonoBehaviour {
        [SerializeField] MoveAttr attr = null;
        [SerializeField] Camera cam = null;
        bool bRollPressed = false;
        CharacterController controller = null;
        Animator anim = null;
        Vector2 moveDir = Vector2.zero;
        public Player Player { get; set; }

        void Awake ( ) {
            controller = GetComponent<CharacterController> ( );
            anim = GetComponent<Animator> ( );
        }

        void Update ( ) {
            GetInput ( );
        }

        void GetInput ( ) {
            //reset roll trigger to prevent the second time roll 
            anim.ResetTrigger ("Roll");
            if (Player.IsAttacking) return;
            float speed = moveDir.sqrMagnitude;
            if (speed > attr.AllowPlayerRotation) {
                anim.SetFloat ("Speed", speed, attr.DampForRotation, Time.deltaTime);
                MoveAndRotation ( );
            }
            else
                anim.SetFloat ("Speed", speed);
            if (bRollPressed) {
                anim.SetTrigger ("Roll");
                bRollPressed = false;
            }
        }
        void MoveAndRotation ( ) {
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize ( );
            right.Normalize ( );
            Vector3 move = forward * moveDir.y + right * moveDir.x;

            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (move), attr.RotationSpeed);
            controller.Move (move * Time.deltaTime * attr.Velocity);
        }

        void OnMovePerformed (InputAction.CallbackContext ctx) {
            moveDir = ctx.ReadValue<Vector2> ( );
        }

        void OnMoveCanceled (InputAction.CallbackContext ctx) {
            moveDir = Vector2.zero;
        }

        void OnRollStarted (InputAction.CallbackContext ctx) {
            bRollPressed = true;
        }

        void OnEnable ( ) {
            Player.PlayerControl.GamePlay.Move.performed += OnMovePerformed;
            Player.PlayerControl.GamePlay.Move.canceled += OnMoveCanceled;
            Player.PlayerControl.GamePlay.Roll.started += OnRollStarted;
        }

        void OnDisable ( ) {
            Player.PlayerControl.GamePlay.Move.performed -= OnMovePerformed;
            Player.PlayerControl.GamePlay.Move.canceled -= OnMoveCanceled;
            Player.PlayerControl.GamePlay.Roll.started -= OnRollStarted;
        }

        [System.Serializable]
        class MoveAttr {
            public float Velocity = 10f;
            public float AllowPlayerRotation = .1f;
            public float DampForRotation = .3f;
            public float RotationSpeed = .1f;
        }
    }
}