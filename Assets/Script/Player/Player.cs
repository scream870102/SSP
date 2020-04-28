using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CJStudio.SSP.Player {
    class Player : MonoBehaviour {
        [SerializeField] float health = 100f;
        Attack attack = null;
        Movement movement = null;
        public bool IsAttacking => attack.IsAttacking;
        // Start is called before the first frame update
        void Start ( ) {
            attack = GetComponent<Attack> ( );
            movement = GetComponent<Movement> ( );
            if (attack) attack.Player = this;
            if (movement) movement.Player = this;
        }

        public void TakeDamage (float damage, bool bLastHit = false) {
            Debug.Log (this.name + "Get Hit");
            health -= damage;
            if (bLastHit)
                Debug.Log ("最後一擊");
        }
    }
}