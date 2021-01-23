using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
namespace CJStudio.SSP.Player {
    // HA = Hit area -- 在這個範圍內可以插入多個攻擊點 但只會計算一次傷害
    // CC = Combo Continue -- 在這個時間點內可以持續接受玩家的輸入 來延續技能
    // AA = Approach Area -- 在這個範圍內 透過Tween將玩家拉近與目標的距離
    // S=Start
    // E=End
    // 詳情參考Player.md
    class Attack : MonoBehaviour {
        [SerializeField] ComboInfo comboInfo = null;
        Dictionary<string, Transform> bones = new Dictionary<string, Transform> ( );
        Animator anim = null;
        public Player Player { get; set; }
        public Player TargetPlayer { get; set; }
        public bool IsAttacking { get; private set; }
        bool bContinue = false;
        bool bDetect = false;
        bool bHit = false;
        bool bAttackPressed = false;
#region DEBUG
#if UNITY_EDITOR
        Vector3 pos = Vector3.zero;
        float radius = 0f;
        bool bDraw = false;
#endif
#endregion

#region MONO_MESSAGE
        void Awake ( ) {
            anim = GetComponent<Animator> ( );
            Transform[ ] obj = GetComponentsInChildren<Transform> ( );
            foreach (Transform o in obj) {
                if (!bones.ContainsKey (o.name))
                    bones.Add (o.name, o);
            }
            Player.HurtStart += OnHurtStarted;
        }

        void Start ( ) {
            IsAttacking = false;
        }

        void Update ( ) {
            if (bAttackPressed) {
                anim.SetInteger ("Combo", comboInfo.Normal);
            }
            DetectBtn4ComboExtend ( );
        }

        void OnDestroy ( ) {
            Player.HurtStart -= OnHurtStarted;
        }
#endregion

        void DetectBtn4ComboExtend ( ) {
            if (bDetect) {
                if (bAttackPressed) {
                    bContinue = true;
                    anim.SetBool ("Continue", bContinue);
                }
            }
        }

        void CheckIfHitTarget (string hitInfo, bool bLastHit = false) {
            string pattern = @"(\w+)\\+(\d+\.?\d+)\\+(\d+\.?\d+)\\+(\d+\.?\d+)\\+(\d+\.?\d+)\\+(\d+)";
            MatchCollection matches = Regex.Matches (hitInfo, pattern);
            AttackInfo info = new AttackInfo (
                matches[0].Groups[1].Value,
                float.Parse (matches[0].Groups[2].Value),
                float.Parse (matches[0].Groups[3].Value),
                float.Parse (matches[0].Groups[4].Value),
                float.Parse (matches[0].Groups[5].Value),
                (EKnockBackDirection) (int.Parse (matches[0].Groups[6].Value))
            );
#if UNITY_EDITOR
            bDraw = true;
            pos = bones[info.BoneName].position;
            radius = info.Radius;
#endif
            Collider[ ] cols = Physics.OverlapSphere (bones[info.BoneName].position, info.Radius);
            if (cols.Length != 0) {
                foreach (Collider c in cols) {
                    if (c.name != this.name && c.tag == "Player") {
                        if (TargetPlayer) {
                            bHit = true;
                            TargetPlayer.TakeDamage (info.Damage, bLastHit, info.KnockBackDis, info.KnockBackDur, info.Direction);
                        }
                    }
                }
            }
        }

#region BUTTON_CAllBACK
        void OnAttackStarted (InputAction.CallbackContext ctx) {
            bAttackPressed = true;
        }

        void OnAttackPerformed (InputAction.CallbackContext ctx) {
            bAttackPressed = true;
        }

        void OnAttackCanceled (InputAction.CallbackContext ctx) {
            bAttackPressed = false;
        }

        void OnEnable ( ) {
            Player.PlayerControl.GamePlay.Attack.started += OnAttackStarted;
            Player.PlayerControl.GamePlay.Attack.performed += OnAttackPerformed;
            Player.PlayerControl.GamePlay.Attack.canceled += OnAttackCanceled;
        }

        void OnDisable ( ) {
            Player.PlayerControl.GamePlay.Attack.started -= OnAttackStarted;
            Player.PlayerControl.GamePlay.Attack.performed -= OnAttackPerformed;
            Player.PlayerControl.GamePlay.Attack.canceled -= OnAttackCanceled;
        }
#endregion

#region ANIMATION_EVENT_CALLBACK

        //Call this method to stop accept player input for extend combo
        void OnHurtStarted ( ) {
            //bApproaching = false;
            bDetect = false;
        }

        //Call this method to start detection for extend combo
        void OnComboContinueS ( ) {
            bContinue = false;
            anim.SetBool ("Continue", bContinue);
            bDetect = true;
        }

        //Call this methdo to end detection for extend combo
        void OnComboContinueE ( ) {
            bDetect = false;
            if (!bContinue) {
                OnEndCombo ( );
            }
        }

        //Call this method to start attack detection
        void OnHitAreaS ( ) {
            bHit = false;
            IsAttacking = true;
        }

        //Call this method to end attack detection
        void OnHitAreaE ( ) {
            bHit = false;
            IsAttacking = false;
        }

        //Call for Normal Attack
        void OnHit (string hitInfo) {
            if (bHit) {
                //Debug.Log ("已經打過了 還想打啊!!");
                return;
            }
            CheckIfHitTarget (hitInfo);
        }

        //Call for Attack which is end of combo
        void OnLastHit (string hitInfo) {
            if (bHit) {
                //Debug.Log ("已經打過了 還想打啊!!");
                return;
            }
            CheckIfHitTarget (hitInfo, true);
        }

        //Call for Combo End then next attack btn press will be recognize as new combo
        void OnEndCombo ( ) {
            anim.SetInteger ("Combo", 0);
        }

#endregion

#region DEBUG
#if UNITY_EDITOR
        void OnDrawGizmos ( ) {
            Gizmos.color = Color.red;
            if (bDraw)
                Gizmos.DrawSphere (pos, radius);
        }
#endif   
#endregion

        //Info for an atack action
        class AttackInfo {
            public AttackInfo (string boneName, float radius, float damage, float knockBackDis, float knockBackDur, EKnockBackDirection direction) {
                this.BoneName = boneName;
                this.Radius = radius;
                this.Damage = damage;
                this.KnockBackDis = knockBackDis;
                this.KnockBackDur = knockBackDur;
                this.Direction = direction;
            }
            public string BoneName { get; set; }
            public float Radius { get; set; }
            public float Damage { get; set; }
            public float KnockBackDis { get; set; }
            public float KnockBackDur { get; set; }
            public EKnockBackDirection Direction { get; set; }
        }

        //Enum for Attack Type
        [System.Serializable]
        class ComboInfo {
            public int Normal;
            public int Special;
            public int Ultilimate;
        }

    }

    enum EKnockBackDirection {
        BACKWARD,
        UP,
        OBLIQUE,

    }

}