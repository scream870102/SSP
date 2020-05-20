using System.Collections;
using System.Collections.Generic;
using Eccentric.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
namespace CJStudio.SSP.Player {
    class Movement : MonoBehaviour {
        [SerializeField] MoveAttr attr = null;
        MoveProps props = null;
        [SerializeField] GameObject guardObj = null;
        CharacterController controller = null;
        Animator anim = null;
        float moveHori = 0f;
        float vertiVel = 0f;
        ScaledTimer jumpTimer = null;
        public Player Player { get; set; }

        [ReadOnly][SerializeField] bool bJumping = false;
        public bool IsGrounded { get; private set; }
        public bool IsGuard { get; private set; }
#if UNITY_EDITOR
        [ReadOnly][SerializeField] bool bGround = false;
        [ReadOnly][SerializeField] bool bGuard = false;
        [ReadOnly][SerializeField] bool bHurting = false;
#endif
        void Awake ( ) {
            controller = GetComponent<CharacterController> ( );
            anim = GetComponent<Animator> ( );
            jumpTimer = new ScaledTimer (attr.JumpIgnoreGroundTime);
            props = new MoveProps (attr.GuardInitEnergy);
            Player.HurtStart += OnHurtStart;
            Player.HurtEnd += OnHurtEnd;
        }

        void Update ( ) {
            RaycastHit result;
            IsGrounded = HitGround (attr.DistanceForDetectGround, out result);
            anim.SetBool ("Ground", IsGrounded);
#if UNITY_EDITOR
            bGround = IsGrounded;
            bGuard = IsGuard;
            //if (result.collider != null)
            //Debug.Log (result.collider.name);
#endif
            GetInput ( );
        }

        void GetInput ( ) {
#region GRAVITY
            //if is not above the ground add gravity
            if (!IsGrounded && !bJumping)
                controller.Move (Vector3.down * Time.deltaTime * attr.Gravity);
#endregion
            if (Player.IsAttacking) {
                //if player is attacking force stop guard action
                if (IsGuard) {
                    IsGuard = false;
                    guardObj.SetActive (false);
                    anim.SetBool ("Guard", false);
                }
                return;
            }
#region JUMP
            if (bJumping) {
                controller.Move (Vector3.up * Time.deltaTime * vertiVel);
                if (vertiVel > 0f)
                    vertiVel -= attr.JumpGravity;
                else
                    vertiVel -= attr.JumpFallGravity;
                if (IsGrounded && jumpTimer.IsFinished)
                    bJumping = false;
            }
#endregion
#region GUARD
            if (!IsGuard) props.GuardEnergy += attr.GuardRecoverRate * Time.deltaTime;
            if (props.GuardEnergy > attr.GuardInitEnergy) props.GuardEnergy = attr.GuardInitEnergy;
#endregion
#region MOVE
            anim.SetFloat ("Speed", Mathf.Abs (moveHori));
            if (moveHori != 0f) {
                transform.rotation = Quaternion.Euler (0f, moveHori > 0f?90f: -90f, 0f);
                if (!IsGuard)
                    controller.Move (Vector3.right * moveHori * (IsGrounded?attr.Velocity : attr.AirVelocity) * Time.deltaTime);
            }
#endregion

        }
#region BUTTON_CALLBACK
        void OnJumpStarted (InputAction.CallbackContext ctx) {
            if (!bJumping && IsGrounded && !IsGuard) {
                bJumping = true;
                vertiVel = attr.JumpVelocity;
                jumpTimer.Reset ( );
                anim.SetTrigger ("Jump");
            }
        }

        void OnGuardStarted (InputAction.CallbackContext ctx) {
            if (bJumping || bHurting) return;
            IsGuard = true;
            guardObj.SetActive (true);
            anim.SetBool ("Guard", true);
        }

        void OnGuardCanceled (InputAction.CallbackContext ctx) {
            IsGuard = false;
            guardObj.SetActive (false);
            anim.SetBool ("Guard", false);
        }

        void OnMoveHoriPerformed (InputAction.CallbackContext ctx) {
            moveHori = ctx.ReadValue<float> ( );
        }

        void OnMoveHoriCanceled (InputAction.CallbackContext ctx) {
            moveHori = ctx.ReadValue<float> ( );
        }
#endregion

        void OnEnable ( ) {
            Player.PlayerControl.GamePlay.MoveHori.performed += OnMoveHoriPerformed;
            Player.PlayerControl.GamePlay.MoveHori.canceled += OnMoveHoriCanceled;
            Player.PlayerControl.GamePlay.Jump.started += OnJumpStarted;
            Player.PlayerControl.GamePlay.Guard.started += OnGuardStarted;
            Player.PlayerControl.GamePlay.Guard.canceled += OnGuardCanceled;
        }

        void OnDisable ( ) {
            Player.PlayerControl.GamePlay.MoveHori.performed -= OnMoveHoriPerformed;
            Player.PlayerControl.GamePlay.MoveHori.canceled -= OnMoveHoriCanceled;
            Player.PlayerControl.GamePlay.Jump.started -= OnJumpStarted;
            Player.PlayerControl.GamePlay.Guard.started -= OnGuardStarted;
            Player.PlayerControl.GamePlay.Guard.canceled -= OnGuardCanceled;
        }

        bool HitGround (float distance, out RaycastHit result) {
            Debug.DrawLine (transform.position, transform.position + Vector3.down * distance, UnityEngine.Color.red, Time.deltaTime);
            return Physics.Raycast (transform.position, Vector3.down, out result, distance);
        }
        public float MinusGuardEnergy (float value) {
            this.props.GuardEnergy -= value;
            return props.GuardEnergy;
        }

        void OnHurtStart ( ) {
            bHurting = true;
        }

        void OnHurtEnd ( ) {
            bHurting = false;
        }

        [System.Serializable]
        class MoveAttr {
            public float Velocity = 10f;
            public float AirVelocity = 5f;
            public float AllowPlayerRotation = .1f;
            public float DampForRotation = .3f;
            public float RotationSpeed = .1f;
            public float Gravity = 10f;
            public float JumpGravity = 1f;
            public float JumpFallGravity = 2f;
            public float JumpVelocity = 15f;
            public float DistanceForDetectGround = .15f;
            public float JumpIgnoreGroundTime = .05f;
            public float GuardInitEnergy = 10f;
            public float GuardRecoverRate = .5f;
        }

        [System.Serializable]
        public class MoveProps {
            public MoveProps (float initGuardEnergy) {
                this.guardEnergy = initGuardEnergy;
                this.initGuardEnergy = initGuardEnergy;
            }
            float guardEnergy = 0f;
            float initGuardEnergy = 0f;
            public float GuardEnergy {
                get => guardEnergy;
                set {
                    if (value > initGuardEnergy) guardEnergy = initGuardEnergy;
                    else if (value < 0f) guardEnergy = 0f;
                    else guardEnergy = value;
                }
            }
        }
    }
}