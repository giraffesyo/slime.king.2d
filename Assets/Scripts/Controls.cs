// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace slimeking
{
    public class @GameControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""ecb366d3-9b50-496d-a4e4-06750624710f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""0dafdd47-4c9f-4b9c-93db-cd6fe9bbc4e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""3ab79015-cd7f-4a77-87ec-bd78f6e75e85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slap"",
                    ""type"": ""Button"",
                    ""id"": ""68761a93-8f9a-435a-86a1-6d14a094f3a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Engulf"",
                    ""type"": ""Button"",
                    ""id"": ""e609ce30-5682-465a-9527-1461025773db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select Ability 1"",
                    ""type"": ""Button"",
                    ""id"": ""9b9786c2-507d-4a42-bb4f-a89aa84a21cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select Ability 2"",
                    ""type"": ""Button"",
                    ""id"": ""82175a3a-f8d1-4132-9ac2-86ed4fc1703d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select Ability 3"",
                    ""type"": ""Button"",
                    ""id"": ""00181cfb-6512-4d89-8520-bc138687d2cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select Ability 4"",
                    ""type"": ""Button"",
                    ""id"": ""2f846506-d0eb-4b5f-8386-2969b0fd6e3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use Ability"",
                    ""type"": ""Button"",
                    ""id"": ""a79cc012-3474-4de8-aaf1-f21b2cf46611"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99002a6a-1454-415b-8fde-b116c8685846"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""ebc0e3a6-cec6-4091-84c1-5c777df3cdcf"",
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
                    ""id"": ""055bb292-3949-429a-9c19-6318aba2d43c"",
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
                    ""id"": ""b64a4d40-4e2e-4369-951d-6ee8c598356e"",
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
                    ""id"": ""fe477e65-de21-4990-a687-ffa0013af9c8"",
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
                    ""id"": ""f70024e8-9bbb-4922-9ae8-0fcb99f93c8b"",
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
                    ""id"": ""e5c0835f-e9ec-4e6d-b9e5-a5c8b20f6e43"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1689db7c-c3e0-4299-b3f1-1aae94e2ebc5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""410dbcd2-9af2-4638-ab76-c8162f6f4cb6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Engulf"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b773d33-5c82-40cb-9474-461d40076f78"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Ability 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""515a3dce-bbf5-42e0-9bc9-4d7ac3015d80"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Ability 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""686351d1-f254-4237-86d1-48eea8ecdc30"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Ability 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""193f7a71-2694-4183-8f38-e752c40723f3"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Ability 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec0cd772-12e7-41d5-acb5-a120b814b9e7"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use Ability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
            m_Player_Slap = m_Player.FindAction("Slap", throwIfNotFound: true);
            m_Player_Engulf = m_Player.FindAction("Engulf", throwIfNotFound: true);
            m_Player_SelectAbility1 = m_Player.FindAction("Select Ability 1", throwIfNotFound: true);
            m_Player_SelectAbility2 = m_Player.FindAction("Select Ability 2", throwIfNotFound: true);
            m_Player_SelectAbility3 = m_Player.FindAction("Select Ability 3", throwIfNotFound: true);
            m_Player_SelectAbility4 = m_Player.FindAction("Select Ability 4", throwIfNotFound: true);
            m_Player_UseAbility = m_Player.FindAction("Use Ability", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Move;
        private readonly InputAction m_Player_Shoot;
        private readonly InputAction m_Player_Slap;
        private readonly InputAction m_Player_Engulf;
        private readonly InputAction m_Player_SelectAbility1;
        private readonly InputAction m_Player_SelectAbility2;
        private readonly InputAction m_Player_SelectAbility3;
        private readonly InputAction m_Player_SelectAbility4;
        private readonly InputAction m_Player_UseAbility;
        public struct PlayerActions
        {
            private @GameControls m_Wrapper;
            public PlayerActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
            public InputAction @Slap => m_Wrapper.m_Player_Slap;
            public InputAction @Engulf => m_Wrapper.m_Player_Engulf;
            public InputAction @SelectAbility1 => m_Wrapper.m_Player_SelectAbility1;
            public InputAction @SelectAbility2 => m_Wrapper.m_Player_SelectAbility2;
            public InputAction @SelectAbility3 => m_Wrapper.m_Player_SelectAbility3;
            public InputAction @SelectAbility4 => m_Wrapper.m_Player_SelectAbility4;
            public InputAction @UseAbility => m_Wrapper.m_Player_UseAbility;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                    @Slap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlap;
                    @Slap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlap;
                    @Slap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlap;
                    @Engulf.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEngulf;
                    @Engulf.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEngulf;
                    @Engulf.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEngulf;
                    @SelectAbility1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility1;
                    @SelectAbility1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility1;
                    @SelectAbility1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility1;
                    @SelectAbility2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility2;
                    @SelectAbility2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility2;
                    @SelectAbility2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility2;
                    @SelectAbility3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility3;
                    @SelectAbility3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility3;
                    @SelectAbility3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility3;
                    @SelectAbility4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility4;
                    @SelectAbility4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility4;
                    @SelectAbility4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectAbility4;
                    @UseAbility.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseAbility;
                    @UseAbility.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseAbility;
                    @UseAbility.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseAbility;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                    @Slap.started += instance.OnSlap;
                    @Slap.performed += instance.OnSlap;
                    @Slap.canceled += instance.OnSlap;
                    @Engulf.started += instance.OnEngulf;
                    @Engulf.performed += instance.OnEngulf;
                    @Engulf.canceled += instance.OnEngulf;
                    @SelectAbility1.started += instance.OnSelectAbility1;
                    @SelectAbility1.performed += instance.OnSelectAbility1;
                    @SelectAbility1.canceled += instance.OnSelectAbility1;
                    @SelectAbility2.started += instance.OnSelectAbility2;
                    @SelectAbility2.performed += instance.OnSelectAbility2;
                    @SelectAbility2.canceled += instance.OnSelectAbility2;
                    @SelectAbility3.started += instance.OnSelectAbility3;
                    @SelectAbility3.performed += instance.OnSelectAbility3;
                    @SelectAbility3.canceled += instance.OnSelectAbility3;
                    @SelectAbility4.started += instance.OnSelectAbility4;
                    @SelectAbility4.performed += instance.OnSelectAbility4;
                    @SelectAbility4.canceled += instance.OnSelectAbility4;
                    @UseAbility.started += instance.OnUseAbility;
                    @UseAbility.performed += instance.OnUseAbility;
                    @UseAbility.canceled += instance.OnUseAbility;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
            void OnSlap(InputAction.CallbackContext context);
            void OnEngulf(InputAction.CallbackContext context);
            void OnSelectAbility1(InputAction.CallbackContext context);
            void OnSelectAbility2(InputAction.CallbackContext context);
            void OnSelectAbility3(InputAction.CallbackContext context);
            void OnSelectAbility4(InputAction.CallbackContext context);
            void OnUseAbility(InputAction.CallbackContext context);
        }
    }
}
