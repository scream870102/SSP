using System.Collections;
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
        Animator anim = null;
        Dictionary<string, Transform> bones = new Dictionary<string, Transform> ( );
        bool bContinue = false;
        bool bDetect = false;
        bool bHit = false;
        bool bApproaching = false;
        float approachVel = 0f;
        [SerializeField] Transform target = null;
        public bool IsAttacking { get; private set; }
        public Player Player { get; set; }
#if UNITY_EDITOR
        Vector3 pos = Vector3.zero;
        float radius = 0f;
        bool bDraw = false;
#endif
        void Awake ( ) {
            IsAttacking = false;
            anim = GetComponent<Animator> ( );
            Transform[ ] obj = GetComponentsInChildren<Transform> ( );
            foreach (Transform o in obj) {
                bones.Add (o.name, o);
            }
        }
        // Start is called before the first frame update
        void Start ( ) {

        }

        // Update is called once per frame
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
            anim.applyRootMotion = true;
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
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            Vector3 newPos = transform.position + direction * approachVel * Time.deltaTime;
            Debug.Log (transform.position + "  " + newPos);
            transform.position = newPos;
            Quaternion rotation = Quaternion.LookRotation (direction, Vector3.up);
            transform.rotation = rotation;

        }

        void OnHit (string hitInfo) {
            if (bHit) {
                Debug.Log ("已經打過了 還想打啊!!");
                return;
            }
            bHit = true;

            string pattern = @"(\w+)\\+(\d+\.?\d+)\\+(\d+\.?\d+)";
            MatchCollection matches = Regex.Matches (hitInfo, pattern);
            AttackInfo info = new AttackInfo (matches[0].Groups[1].Value, float.Parse (matches[0].Groups[2].Value), float.Parse (matches[0].Groups[3].Value));
#if UNITY_EDITOR
            bDraw = true;
            pos = bones[info.boneName].position;
            radius = info.radius;
#endif
            Collider[ ] cols = Physics.OverlapSphere (bones[info.boneName].position, info.radius);
            if (cols.Length != 0) {
                foreach (Collider c in cols) {
                    if (c.name != this.name) {
                        Player other = c.GetComponent<Player> ( );
                        if (other) {
                            other.TakeDamage (info.damage);
                        }
                    }
                }
            }
        }

        void OnLastHit (string hitInfo) {
            if (bHit) {
                Debug.Log ("已經打過了 還想打啊!!");
                return;
            }

            string pattern = @"(\w+)\\+(\d+\.?\d+)\\+(\d+\.?\d+)";
            MatchCollection matches = Regex.Matches (hitInfo, pattern);
            AttackInfo info = new AttackInfo (matches[0].Groups[1].Value, float.Parse (matches[0].Groups[2].Value), float.Parse (matches[0].Groups[3].Value));
#if UNITY_EDITOR
            bDraw = true;
            pos = bones[info.boneName].position;
            radius = info.radius;
#endif
            Collider[ ] cols = Physics.OverlapSphere (bones[info.boneName].position, info.radius);
            if (cols.Length != 0) {
                foreach (Collider c in cols) {
                    if (c.name != this.name) {
                        Player other = c.GetComponent<Player> ( );
                        if (other) {
                            bHit = true;
                            other.TakeDamage (info.damage, true);
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
            public AttackInfo (string boneName, float radius, float damage) {
                this.boneName = boneName;
                this.radius = radius;
                this.damage = damage;
            }
            public string boneName = "";
            public float radius = 0f;
            public float damage = 0f;
        }
    }

}