using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CJStudio.SSP.Player {
    class Movement : MonoBehaviour {
        [SerializeField] MoveAttr attr = null;
        [SerializeField] Camera cam = null;
        CharacterController controller = null;
        Animator anim = null;
        Vector2 moveDir = Vector2.zero;
        public Player Player { get; set; }

        void Awake ( ) {
            controller = GetComponent<CharacterController> ( );
            anim = GetComponent<Animator> ( );
        }

        void Start ( ) {

        }

        // Update is called once per frame
        void Update ( ) {
            GetInput ( );
        }

        void GetInput ( ) {
            if (Player.IsAttacking) return;
            moveDir = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
            float speed = moveDir.sqrMagnitude;
            if (speed > attr.AllowPlayerRotation) {
                anim.SetFloat ("Speed", speed, attr.DampForRotation, Time.deltaTime);
                MoveAndRotation ( );
            }
            else
                anim.SetFloat ("Speed", speed);
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
    }

    [System.Serializable]
    class MoveAttr {
        public float Velocity = 10f;
        public float AllowPlayerRotation = .1f;
        public float DampForRotation = .3f;
        public float RotationSpeed = .1f;
    }

}