using CJStudio.SSP.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using P=CJStudio.SSP.Player;
namespace CJStudio.SSP {
    class PlayerManager : MonoBehaviour {
        [SerializeField] P.Player player1 = null;
        [SerializeField] P.Player player2 = null;
        void OnEnable ( ) {
            InputUser.onUnpairedDeviceUsed += OnUnpairedDeviceUsed;
        }

        void OnDisable ( ) {
            InputUser.onUnpairedDeviceUsed -= OnUnpairedDeviceUsed;
        }

        void Awake ( ) {
            ++InputUser.listenForUnpairedDeviceActivity;
        }

        void OnUnpairedDeviceUsed (InputControl c, InputEventPtr e) {
            if (!(c.device.GetType ( ) == Keyboard.current.GetType ( ) || c.device.GetType ( ) == Gamepad.current.GetType ( )))
                return;
            if (!player1.InputUser.valid) {
                player1.InputUser = InputUser.PerformPairingWithDevice (c.device);
                player1.InputUser.AssociateActionsWithUser (player1.PlayerControl);
                Debug.Log ("Pairing player1 with " + c.device.name);
            }
            else if (!player2.InputUser.valid) {
                player2.InputUser = InputUser.PerformPairingWithDevice (c.device);
                player2.InputUser.AssociateActionsWithUser (player2.PlayerControl);
                Debug.Log ("Pairing player2 with " + c.device.name);
            }
        }
    }
}