// GENERATED AUTOMATICALLY FROM 'Assets/Other/Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace CJStudio.SSP.InputSystem
{
    public class @Player : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Player()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""5f2f28b0-2bc6-4052-b695-8964bedf2372"",
            ""actions"": [
                {
                    ""name"": ""Test"",
                    ""type"": ""Value"",
                    ""id"": ""d82eeb74-8dfa-470f-9477-132ab84963ef"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""Button"",
                    ""id"": ""0f72ae4a-8dd4-42bf-b6f1-6ec7985e0064"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""96880086-88a6-41c4-b845-199935f3f29e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""53e32656-112c-4810-b192-ac6b425b37d6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8c4c428d-8c34-41c5-a060-c6d73afb9ada"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""81ae7f94-ea7b-4f28-a92d-91585d7eaecf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""198a730d-d7b9-47b0-b89d-f55f4cf77401"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""52cd20f1-fa12-479f-a2fd-823a4c887364"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3dd67808-7c8e-4315-ae8a-ed7708b91d4d"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aec81c43-a666-4b37-9525-d2ff6e0d358d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // GamePlay
            m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
            m_GamePlay_Test = m_GamePlay.FindAction("Test", throwIfNotFound: true);
            m_GamePlay_Join = m_GamePlay.FindAction("Join", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // GamePlay
        private readonly InputActionMap m_GamePlay;
        private IGamePlayActions m_GamePlayActionsCallbackInterface;
        private readonly InputAction m_GamePlay_Test;
        private readonly InputAction m_GamePlay_Join;
        public struct GamePlayActions
        {
            private @Player m_Wrapper;
            public GamePlayActions(@Player wrapper) { m_Wrapper = wrapper; }
            public InputAction @Test => m_Wrapper.m_GamePlay_Test;
            public InputAction @Join => m_Wrapper.m_GamePlay_Join;
            public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
            public void SetCallbacks(IGamePlayActions instance)
            {
                if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
                {
                    @Test.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTest;
                    @Test.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTest;
                    @Test.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTest;
                    @Join.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJoin;
                    @Join.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJoin;
                    @Join.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJoin;
                }
                m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Test.started += instance.OnTest;
                    @Test.performed += instance.OnTest;
                    @Test.canceled += instance.OnTest;
                    @Join.started += instance.OnJoin;
                    @Join.performed += instance.OnJoin;
                    @Join.canceled += instance.OnJoin;
                }
            }
        }
        public GamePlayActions @GamePlay => new GamePlayActions(this);
        public interface IGamePlayActions
        {
            void OnTest(InputAction.CallbackContext context);
            void OnJoin(InputAction.CallbackContext context);
        }
    }
}
