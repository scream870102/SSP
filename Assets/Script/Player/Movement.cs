using Eccentric.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
namespace CJStudio.SSP.Player
{
    class Movement : MonoBehaviour
    {
        [SerializeField] MovementAttr attr = null;
        [SerializeField] GameObject guardObj = null;
        [SerializeField] Slider guardSlider = null;
        ScaledTimer jumpTimer = null;
        CharacterController controller = null;
        Animator anim = null;
        GuardProps guardProps = null;
        float horiVel = 0f;
        float vertiVel = 0f;
        public Player Player { get; set; }
        public bool IsJumping { get; private set; }
        public bool IsGrounded { get; private set; }
        public bool IsGuard { get; private set; }
        public bool IsStunned { get; private set; }
        public bool IsHurting { get; private set; }
#if UNITY_EDITOR
        [Header("OBSERVE")]
        [ReadOnly] [SerializeField] bool bGround = false;
        [ReadOnly] [SerializeField] bool bGuard = false;
        [ReadOnly] [SerializeField] bool bStunned = false;
        [ReadOnly] [SerializeField] bool bHurting = false;
        [ReadOnly] [SerializeField] bool bJumping = false;
#endif
        public bool MinusGuardEnergy(float value)
        {
            this.guardProps.GuardEnergy -= value;
            if (this.guardProps.GuardEnergy <= 0f)
            {
                anim.SetTrigger("Stunned");
                IsStunned = true;
                CancelGuard();
                guardProps.GuardEnergy = 0f;
            }
            return this.guardProps.GuardEnergy <= 0f;
        }
        #region UI_UPDATE
        void UpdateGuardSlider()
        {
            guardProps.GuardEnergy = Mathf.Clamp(guardProps.GuardEnergy, 0f, attr.GuardInitEnergy);
            guardSlider.maxValue = attr.GuardInitEnergy;
            guardSlider.value = guardProps.GuardEnergy;
        }

        #endregion

        #region MONO_MESSAGE
        void OnDestroy()
        {
            Player.HurtStart -= OnHurtStart;
            Player.HurtEnd -= OnHurtEnd;
        }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            jumpTimer = new ScaledTimer(attr.JumpIgnoreGroundTime);
            guardProps = new GuardProps(attr.GuardInitEnergy);
            Player.HurtStart += OnHurtStart;
            Player.HurtEnd += OnHurtEnd;
        }

        void Update()
        {
            DetectGround();
            UpdateGuardSlider();
#if UNITY_EDITOR
            bGround = IsGrounded;
            bGuard = IsGuard;
            bStunned = IsStunned;
            bHurting = IsHurting;
            bJumping = IsJumping;
#endif
            Action();
        }
        #endregion

        void Action()
        {
            #region GRAVITY
            //if is not above the ground add gravity
            if (!IsGrounded && !IsJumping && !Player.IsTransition)
            {
                controller.Move(Vector3.down * Time.deltaTime * attr.Gravity);
            }
            #endregion

            if (Player.IsAttacking || IsStunned || IsHurting)
            {
                //if player is attacking force stop guard action
                if (IsGuard)
                    CancelGuard();
                return;
            }
            #region JUMP
            if (IsJumping)
            {
                controller.Move(Vector3.up * Time.deltaTime * vertiVel);
                if (vertiVel > 0f)
                    vertiVel -= attr.JumpGravity;
                else
                    vertiVel -= attr.JumpFallGravity;
                if (IsGrounded && jumpTimer.IsFinished)
                    IsJumping = false;
            }
            #endregion

            #region GUARD
            if (!IsGuard) guardProps.GuardEnergy += attr.GuardRecoverRate * Time.deltaTime;
            #endregion

            #region MOVE
            anim.SetFloat("Speed", Mathf.Abs(horiVel));
            if (horiVel != 0f)
            {
                transform.rotation = Quaternion.Euler(0f, horiVel > 0f ? 90f : -90f, 0f);
                if (!IsGuard)
                    controller.Move(Vector3.right * horiVel * (IsGrounded ? attr.Velocity : attr.AirVelocity) * Time.deltaTime);
            }
            #endregion

        }

        bool HitGround(float distance, out RaycastHit result)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * distance, UnityEngine.Color.red, Time.deltaTime);
            return Physics.Raycast(transform.position, Vector3.down, out result, distance);
        }

        void CancelGuard()
        {
            IsGuard = false;
            guardObj.SetActive(false);
            anim.SetBool("Guard", false);
        }

        void DetectGround()
        {
            RaycastHit result;
            IsGrounded = HitGround(attr.DistanceForDetectGround, out result);
            anim.SetBool("Ground", IsGrounded);
        }

        #region CALLBACK
        void OnHurtStart()
        {
            CancelGuard();
            IsStunned = false;
            IsHurting = true;
        }

        void OnHurtEnd()
        {
            IsHurting = false;
            IsStunned = false;
        }

        void OnStunnedEnd()
        {
            IsStunned = false;
            IsHurting = false;
        }

        void OnGuardActive()
        {
            if (IsGuard)
                guardObj.SetActive(true);
        }

        #endregion

        #region BUTTON_CALLBACK
        void OnJumpStarted(InputAction.CallbackContext ctx)
        {
            if (!IsJumping && IsGrounded && !IsGuard && !IsStunned && !IsHurting)
            {
                IsJumping = true;
                vertiVel = attr.JumpVelocity;
                jumpTimer.Reset();
                anim.SetTrigger("Jump");
            }
        }

        void OnGuardPerformed(InputAction.CallbackContext ctx)
        {
            if (IsJumping || IsHurting || IsStunned) return;
            var control = ctx.control as ButtonControl;
            if (control.wasPressedThisFrame)
            {
                IsGuard = true;
                anim.SetBool("Guard", true);
            }
            else if (control.wasReleasedThisFrame)
                CancelGuard();
        }

        void OnGuardCanceled(InputAction.CallbackContext ctx)
        {
            CancelGuard();
        }

        void OnMoveHoriPerformed(InputAction.CallbackContext ctx)
        {
            horiVel = ctx.ReadValue<float>();
        }

        void OnMoveHoriCanceled(InputAction.CallbackContext ctx)
        {
            horiVel = ctx.ReadValue<float>();
        }

        void OnEnable()
        {
            Player.PlayerControl.GamePlay.MoveHori.performed += OnMoveHoriPerformed;
            Player.PlayerControl.GamePlay.MoveHori.canceled += OnMoveHoriCanceled;
            Player.PlayerControl.GamePlay.Jump.started += OnJumpStarted;
            Player.PlayerControl.GamePlay.Guard.performed += OnGuardPerformed;
            Player.PlayerControl.GamePlay.Guard.canceled += OnGuardCanceled;
        }

        void OnDisable()
        {
            Player.PlayerControl.GamePlay.MoveHori.performed -= OnMoveHoriPerformed;
            Player.PlayerControl.GamePlay.MoveHori.canceled -= OnMoveHoriCanceled;
            Player.PlayerControl.GamePlay.Jump.started -= OnJumpStarted;
            Player.PlayerControl.GamePlay.Guard.performed -= OnGuardPerformed;
            Player.PlayerControl.GamePlay.Guard.canceled -= OnGuardCanceled;
        }

        #endregion

        [System.Serializable]
        class MovementAttr
        {
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
        public class GuardProps
        {
            public GuardProps(float initGuardEnergy)
            {
                this.guardEnergy = initGuardEnergy;
                this.initGuardEnergy = initGuardEnergy;
            }
            float guardEnergy = 0f;
            float initGuardEnergy = 0f;
            public float GuardEnergy
            {
                get => guardEnergy;
                set
                {
                    if (value > initGuardEnergy) guardEnergy = initGuardEnergy;
                    else if (value < 0f) guardEnergy = 0f;
                    else guardEnergy = value;
                }
            }
        }
    }
}