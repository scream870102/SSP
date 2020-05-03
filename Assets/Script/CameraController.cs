// using System.Collections;
// using System.Collections.Generic;
// using Cinemachine;
// using Eccentric;
// using UnityEngine;
// public class CameraController : MonoBehaviour {
//     [SerializeField] List<Transform> cameras = new List<Transform> ( );
//     [SerializeField] List<Vector3> backward = new List<Vector3> ( );
//     [SerializeField] Transform target1 = null;
//     [SerializeField] Transform target2 = null;
//     // [SerializeField] float degree = 15f;
//     // [SerializeField] CinemachineVirtualCamera cam = null;
//     [SerializeField] Vector2 clamp = Vector2.zero;
//     [SerializeField] float yOffset = 2f;
//     [SerializeField] float lerp = .7f;
//     [SerializeField] float mul = 1.5f;
//     // Start is called before the first frame update
//     void Start ( ) {
//         for (int i = 0; i < cameras.Count; i++) {
//             backward.Add (-cameras[i].forward);
//         }
//     }

//     // Update is called once per frame
//     void Update ( ) {
//         Vector3 newPos = (target1.position + target2.position) / 2f;
//         newPos.y = newPos.y + yOffset;
//         Vector3 rotOffset = target1.position - target2.position;
//         transform.position = newPos;
//         transform.forward = rotOffset.normalized;
//         float dis = (target2.position - newPos).magnitude;
//         for (int i = 0; i < cameras.Count; i++) {
//             Vector3 newTPos = backward[i] * Mathf.Clamp (dis * mul, clamp.x, clamp.y);
//             newTPos.y = 0f;
//             cameras[i].localPosition = Vector3.Lerp (cameras[i].localPosition, newTPos, lerp);
//         }
//         // foreach (Transform t in cameras) {
//         //     Vector3 newTPos = -t.forward * Mathf.Clamp (dis, clamp.x, clamp.y);
//         //     newTPos.y = 0f;
//         //     t.position = Vector3.Lerp (t.position, newTPos, lerp);
//         // }
//         // Vector3 anotherBase = new Vector3 (dis * Mathf.Cos (degree * Mathf.Deg2Rad), 0f, dis * Mathf.Sin (degree * Mathf.Deg2Rad));
//         // Vector3 newC1Pos = new Vector3 (
//         //     transform.forward.x * anotherBase.x + transform.up.x * anotherBase.y + transform.right.x * anotherBase.z,
//         //     transform.forward.y * anotherBase.x + transform.up.y * anotherBase.y + transform.right.y * anotherBase.z,
//         //     transform.forward.z * anotherBase.x + transform.up.z * anotherBase.y + transform.right.z * anotherBase.z
//         // );
//         // cameras[0].position = newC1Pos;
//         // // Debug.Log (
//         // //     transform.right.x * Mathf.Cos (degree * Mathf.Deg2Rad) + " " +
//         // //     transform.forward.z * Mathf.Sin (degree * Mathf.Deg2Rad) 
//         // // );
//         // //cameras[0].position = (dis * Mathf.Cos (degree * Mathf.Deg2Rad) + dis * Mathf.Sin (degree * Mathf.Deg2Rad)) * dis;
//         // cam.transform.position = cameras[0].position;
//     }
// }