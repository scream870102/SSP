using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
public class PlayerManager : MonoBehaviour {
    //[SerializeField] CJStudio.SSP.InputSystem.Player playerInput;
    // Start is called before the first frame update
    void Start ( ) {
        // Debug.Log (PlayerInputManager.instance.isActiveAndEnabled);
        // PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
        // PlayerInputManager.instance.onPlayerLeft += OnPlayerLeft;
        ++InputUser.listenForUnpairedDeviceActivity;
        InputUser.onUnpairedDeviceUsed += (c, e) => {
            //if(c.device.)
         };
        //playerInput = new CJStudio.SSP.InputSystem.Player ( );
        // foreach (var item in playerInput.devices) {
        //     Debug.Log (item.name);
        // }
    }

    void Update ( ) {
        // if (Input.GetKeyDown (KeyCode.K)) {
        //     Debug.Log (playerInput.devices.HasValue);
        // }
    }
    void OnPlayerJoined (PlayerInput i) {
        //Debug.Log (i.playerIndex);
        //Debug.Log (PlayerInputManager.instance.playerCount);
        //Debug.Log (i.devices[0].name);
    }

    void OnPlayerLeft (PlayerInput i) {
        Debug.Log ("Left");
        Debug.Log (PlayerInputManager.instance.playerCount);
    }
}