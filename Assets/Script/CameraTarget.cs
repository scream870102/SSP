using UnityEngine;
public class CameraTarget : MonoBehaviour {
    [SerializeField] Transform target1 = null;
    [SerializeField] Transform target2 = null;
    //[SerializeField] float add = 15f;
    //[SerializeField] new CinemachineVirtualCamera camera = null;
    //[SerializeField] float offsetCam = 5f;
    //[SerializeField] Vector3 offset = new Vector3 (0f, 2f, 0f);
    //[SerializeField] Vector2 clamp = Vector2.zero;
    //CinemachineTransposer transposer = null;
    // Start is called before the first frame update
    void Start ( ) {
        //transposer = camera.GetCinemachineComponent<CinemachineTransposer> ( );
    }

    // Update is called once per frame
    void Update ( ) {
        Vector3 dir = (target2.position - target1.position).normalized;
        transform.forward = dir;
        //float a = offsetCam * ((target1.position - target2.position).magnitude);
        //Debug.Log ((target1.position - target2.position).magnitude);
        //offset.z = -Mathf.Clamp (a, clamp.x, clamp.y);
        //transposer.m_FollowOffset = offset;
        //Debug.Log (Math.GetDegree (new Vector2 (dir.x, dir.z)));
        //transform.rotation = Quaternion.Euler (0f, Math.GetDegree (new Vector2 (dir.x, dir.z)) + add, 0f);
    }
}