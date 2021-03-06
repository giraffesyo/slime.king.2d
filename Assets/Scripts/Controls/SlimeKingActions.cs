// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/SlimeKingActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @SlimeKingActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @SlimeKingActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""SlimeKingActions"",
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
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""2b446581-64cc-4835-a1c8-9bbcd3d23f82"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""dc73a732-0dcf-4d6e-adbf-c3d0c5b533e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99002a6a-1454-415b-8fde-b116c8685846"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Mouse"",
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
                    ""groups"": ""Mouse"",
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
                    ""groups"": ""Mouse"",
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
                    ""groups"": ""Mouse"",
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
                    ""groups"": ""Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""792c4e27-4682-4b59-bcbd-0a574f334b07"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Mouse"",
                    ""action"": ""Slap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7ce9902-1e35-47e1-8824-0418c699c9c5"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Mouse"",
                    ""action"": ""Engulf"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3971f5c-ce03-494e-9f6e-bf8c0152cdc5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Mouse"",
                    ""action"": ""Select Ability 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38f19965-96df-4592-bac7-e8124c8ed571"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Mouse"",
                    ""action"": ""Select Ability 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2602b85d-0957-4bcd-936a-ee8f38e4a7a4"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Mouse"",
                    ""action"": ""Select Ability 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f23a80f-3f1b-49a8-848a-38407ed4817e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Mouse"",
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
                    ""groups"": ""Mouse"",
                    ""action"": ""Use Ability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43b15f17-d031-408e-8116-4cb95939915f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4af5962-81df-471b-914c-670753fb6aef"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e07088e1-7964-4533-8fe3-f629ab1f67e0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
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
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private @SlimeKingActions m_Wrapper;
        public PlayerActions(@SlimeKingActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Slap => m_Wrapper.m_Player_Slap;
        public InputAction @Engulf => m_Wrapper.m_Player_Engulf;
        public InputAction @SelectAbility1 => m_Wrapper.m_Player_SelectAbility1;
        public InputAction @SelectAbility2 => m_Wrapper.m_Player_SelectAbility2;
        public InputAction @SelectAbility3 => m_Wrapper.m_Player_SelectAbility3;
        public InputAction @SelectAbility4 => m_Wrapper.m_Player_SelectAbility4;
        public InputAction @UseAbility => m_Wrapper.m_Player_UseAbility;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
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
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
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
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
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
        void OnAim(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
