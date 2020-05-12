# Player 注意事項
---
## 名詞解釋
- HA = Hit area -- 在這個範圍內可以插入多個攻擊點 但只會計算一次傷害
- CC = Combo Continue -- 在這個時間點內可以持續接受玩家的輸入 來延續技能
- AA = Approach Area -- 在這個範圍內 透過Tween將玩家拉近與目標的距離

## 注意事項
### 有關拉近距離
在AA期間使用Tween拉近距離 但有最大距離的限制  
使用固定速率 固定時間 由此得出最大距離
拉近距離期間需要使用射線或是其他手段去判定是否接觸到玩家  
若已經接觸到記得設定停止  

---


# Unity new InputSystem Local Multiplayer
## Refference Data
[Input System - local multiplayer](https://forum.unity.com/threads/input-system-local-multiplayer.763829/)  
[Input User](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputDevice.html#constructors)
## How to pairing
Call this code below first at `constructor` or `Awake`
```csharp
++InputUser.listenForUnpairedDeviceActivity;
```
Then you can subscribe the event `onUnpairedDeviceUsed`  
parameters of this event type are
1. `InputControl`
2. `InputEventPtr` (namespace:UnityEngine.InputSystem.LowLevel)

Second you should find unpaired device.  
inputUser is a construct type,  
so if there is nothing set then property `valid` will return false.

Before pairing you should check if this device belongs to gamepad or keyboard.  
Withoout that player one will pair with mouse.

Call method to pair the device with player.  
`InputUser.PerformPairingWithDevice(InputDevice)`  
this will return type InputUser then assign that to `player.Inputuser`

After pairing the device you should bind action with user by call method `AssociateActionWithUser`  
The inputActionCollection should instantiate at Player constructor.

### Example Code
```csharp
using CJStudio.SSP.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
public class PlayerManager : MonoBehaviour {
    [SerializeField] Player player1 = null;
    [SerializeField] Player player2 = null;
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
```

---



