﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
namespace CJStudio.SSP.Player {
    // HA = Hit area -- 在這個範圍內可以插入多個攻擊點 但只會計算一次傷害
    // CC = Combo Continue -- 在這個時間點內可以持續接受玩家的輸入 來延續技能
    // AA = Approach Area -- 在這個範圍內 透過Tween將玩家拉近與目標的距離
    // S=Start
    // E=End
    // 詳情參考Player.md
    class Attack : MonoBehaviour {
        public Player Player { get; set; }
        Animator anim = null;
        Dictionary<string, Transform> bones = new Dictionary<string, Transform> ( );
        bool bContinue = false;
        bool bDetect = false;
        bool bHit = false;
        bool bApproaching = false;
        float approachVel = 0f;
        float playerRadius = 0f;
        [SerializeField] float additionalRadius = 0f;
        [SerializeField] Transform target = null;
        [SerializeField] Player targetPlayer = null;
        public bool IsAttacking { get; private set; }
#if UNITY_EDITOR
        Vector3 pos = Vector3.zero;
        float radius = 0f;
        bool bDraw = false;
#endif
        void Awake ( ) {
            anim = GetComponent<Animator> ( );
            Transform[ ] obj = GetComponentsInChildren<Transform> ( );
            foreach (Transform o in obj) {
                bones.Add (o.name, o);
            }
        }
        void Start ( ) {
            IsAttacking = false;
            playerRadius = Player.CharacterController.radius;
        }

        void Update ( ) {
            if (Input.GetButtonDown ("Fire1")) {
                anim.SetInteger ("Combo", 3);
            }
            GetHit ( );
            Approach ( );
        }

        void OnHurt ( ) {
            bApproaching = false;
            bDetect = false;
        }

        void OnApproachAreaS (float velocity) {
            bApproaching = true;
            approachVel = velocity;
            anim.applyRootMotion = false;
        }

        void OnApproachAreaE ( ) {
            bApproaching = false;
            anim.applyRootMotion = true;
        }

        void OnComboContinueS ( ) {
            bContinue = false;
            anim.SetBool ("Continue", bContinue);
            bDetect = true;
        }

        void OnComboContinueE ( ) {
            bDetect = false;
            if (!bContinue) {
                OnEndCombo ( );
            }
        }
        void OnHitAreaS ( ) {
            bHit = false;
            IsAttacking = true;
        }
        void OnHitAreaE ( ) {
            bHit = false;
            IsAttacking = false;
        }

        void OnEndCombo ( ) {
            anim.SetInteger ("Combo", 0);
        }

        void GetHit ( ) {
            if (bDetect) {
                if (Input.GetButtonDown ("Fire1")) {
                    bContinue = true;
                    anim.SetBool ("Continue", bContinue);
                }
            }
        }

        void Approach ( ) {
            if (!bApproaching || target == null) return;
            //keep approaching target and also set rotation to face target
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation (direction, Vector3.up);
            transform.rotation = rotation;
            //if player bomb into something stop approaching
            RaycastHit[ ] hit;
            hit = Physics.RaycastAll (transform.position, transform.forward, playerRadius);
            Debug.DrawLine (transform.position, transform.position + transform.forward * playerRadius, Color.green, 1f);
            if (hit.Length != 0) {
                foreach (var o in hit) {
                    Debug.Log (o.collider.name);
                }
                return;
            }
            //Direct calculate distance between player and target if they are in small distance just stop approaching
            //This is to solve that if player is already in target hurtbox then it can't detect that
            Vector3 disBetween = transform.position - target.position;
            disBetween.y = 0f;
            if (disBetween.sqrMagnitude < Mathf.Pow (playerRadius + targetPlayer.Radius + additionalRadius, 2)) {
                Debug.Log ("太近了 變態~~~");
                return;
            }

            Vector3 newPos = transform.position + direction * approachVel * Time.deltaTime;
            transform.position = newPos;

        }

        void OnHit (string hitInfo) {
            if (bHit) {
                Debug.Log ("已經打過了 還想打啊!!");
                return;
            }
            CheckHit (hitInfo);
        }

        void OnLastHit (string hitInfo) {
            if (bHit) {
                Debug.Log ("已經打過了 還想打啊!!");
                return;
            }
            CheckHit (hitInfo, true);
        }

        void CheckHit (string hitInfo, bool bLastHit = false) {
            string pattern = @"(\w+)\\+(\d+\.?\d+)\\+(\d+\.?\d+)\\+(\d+\.?\d+)";
            MatchCollection matches = Regex.Matches (hitInfo, pattern);
            AttackInfo info = new AttackInfo (
                matches[0].Groups[1].Value,
                float.Parse (matches[0].Groups[2].Value),
                float.Parse (matches[0].Groups[3].Value),
                float.Parse (matches[0].Groups[4].Value)
            );
#if UNITY_EDITOR
            bDraw = true;
            pos = bones[info.boneName].position;
            radius = info.radius;
#endif
            Collider[ ] cols = Physics.OverlapSphere (bones[info.boneName].position, info.radius);
            if (cols.Length != 0) {
                foreach (Collider c in cols) {
                    if (c.name != this.name && c.tag == "Player") {
                        if (targetPlayer) {
                            bHit = true;
                            targetPlayer.TakeDamage (info.damage, bLastHit, info.knockBackDis, transform.forward);
                        }
                    }
                }
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmos ( ) {
            Gizmos.color = Color.red;
            if (bDraw)
                Gizmos.DrawSphere (pos, radius);
        }
#endif
        class AttackInfo {
            public AttackInfo (string boneName, float radius, float damage, float knockBackDis) {
                this.boneName = boneName;
                this.radius = radius;
                this.damage = damage;
                this.knockBackDis = knockBackDis;
            }
            public string boneName = "";
            public float radius = 0f;
            public float damage = 0f;
            public float knockBackDis = 0f;
        }
    }

}