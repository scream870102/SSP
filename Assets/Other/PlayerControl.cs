// GENERATED AUTOMATICALLY FROM 'Assets/Other/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace CJStudio.SSP.InputSystem
{
    public class @PlayerControl : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""5f2f28b0-2bc6-4052-b695-8964bedf2372"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d82eeb74-8dfa-470f-9477-132ab84963ef"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""baf3dba4-6004-4853-97c4-18a50181c60a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveHori"",
                    ""type"": ""Value"",
                    ""id"": ""020e3ed4-6097-4209-82dd-2b16c787973e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e3691ebe-7625-4e48-a0fb-b6692cfb4600"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""Button"",
                    ""id"": ""1e253949-372e-4fb3-a2b0-cf7ac0dd7732"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
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
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6c851117-3100-4414-8cbe-168c8f764675"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""136c08fe-2c2f-4ebe-884d-f7109c9f9a2b"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b184d4a5-f229-4e67-bc5d-4c65dbb56bfa"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHori"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""29f8b126-4e27-4fef-b0c8-1160f8e047ad"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHori"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""97a4f9f9-28a5-4250-b052-bc94d0a90d1b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHori"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e481d66c-0f18-47b7-9a97-4678f393b6ab"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHori"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3c99b4ec-be73-492e-bd43-b754c3939c18"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b768f2da-5566-4651-b774-7b8856ed49ae"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13d0d1eb-0a82-42c2-8f12-03716740e209"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a896159b-2091-43f8-adbf-28e2c8ce2c31"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
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
            m_GamePlay_Move = m_GamePlay.FindAction("Move", throwIfNotFound: true);
            m_GamePlay_Attack = m_GamePlay.FindAction("Attack", throwIfNotFound: true);
            m_GamePlay_MoveHori = m_GamePlay.FindAction("MoveHori", throwIfNotFound: true);
            m_GamePlay_Jump = m_GamePlay.FindAction("Jump", throwIfNotFound: true);
            m_GamePlay_Guard = m_GamePlay.FindAction("Guard", throwIfNotFound: true);
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
        private readonly InputAction m_GamePlay_Move;
        private readonly InputAction m_GamePlay_Attack;
        private readonly InputAction m_GamePlay_MoveHori;
        private readonly InputAction m_GamePlay_Jump;
        private readonly InputAction m_GamePlay_Guard;
        public struct GamePlayActions
        {
            private @PlayerControl m_Wrapper;
            public GamePlayActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_GamePlay_Move;
            public InputAction @Attack => m_Wrapper.m_GamePlay_Attack;
            public InputAction @MoveHori => m_Wrapper.m_GamePlay_MoveHori;
            public InputAction @Jump => m_Wrapper.m_GamePlay_Jump;
            public InputAction @Guard => m_Wrapper.m_GamePlay_Guard;
            public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
            public void SetCallbacks(IGamePlayActions instance)
            {
                if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                    @Attack.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAttack;
                    @Attack.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAttack;
                    @Attack.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAttack;
                    @MoveHori.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveHori;
                    @MoveHori.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveHori;
                    @MoveHori.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveHori;
                    @Jump.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    @Guard.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGuard;
                    @Guard.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGuard;
                    @Guard.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGuard;
                }
                m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Attack.started += instance.OnAttack;
                    @Attack.performed += instance.OnAttack;
                    @Attack.canceled += instance.OnAttack;
                    @MoveHori.started += instance.OnMoveHori;
                    @MoveHori.performed += instance.OnMoveHori;
                    @MoveHori.canceled += instance.OnMoveHori;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Guard.started += instance.OnGuard;
                    @Guard.performed += instance.OnGuard;
                    @Guard.canceled += instance.OnGuard;
                }
            }
        }
        public GamePlayActions @GamePlay => new GamePlayActions(this);
        public interface IGamePlayActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnAttack(InputAction.CallbackContext context);
            void OnMoveHori(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnGuard(InputAction.CallbackContext context);
        }
    }
}
