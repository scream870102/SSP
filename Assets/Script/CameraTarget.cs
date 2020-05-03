using UnityEngine;
namespace CJStudio.SSP {
    class CameraTarget : MonoBehaviour {
        [SerializeField] Transform target1 = null;
        [SerializeField] Transform target2 = null;
        void Update ( ) {
            Vector3 dir = (target2.position - target1.position).normalized;
            transform.forward = dir;
        }
    }
}