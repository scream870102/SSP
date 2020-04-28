using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;
public class Test : MonoBehaviour {
    [SerializeField] Transform target = null;
    [SerializeField] float time = 10f;
    LeanState state = null;
    // Start is called before the first frame update
    void Start ( ) {

    }

    // Update is called once per frame
    void Update ( ) {
        if (Input.GetKeyDown (KeyCode.Space)) {
            state = transform.positionTransition (target.position, 10f);
        }
        if (Input.GetKeyDown (KeyCode.F)) {
            state.Skip = true;
            Debug.Log ("Skip the state");
        }
    }
}