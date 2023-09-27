

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @RestartAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @RestartAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RestartAction"",
    ""maps"": [
        {
            ""name"": ""Restart"",
            ""id"": ""b8591026-ece3-429d-a4bc-6ff4ed415c03"",
            ""actions"": [
                {
                    ""name"": ""ReStart"",
                    ""type"": ""Button"",
                    ""id"": ""60b450eb-a0bd-4de6-8fb3-747981f283ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""97e8c915-8402-4a65-b14f-aa386dee9cc5"",
                    ""path"": ""<Keyboard>/f12"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Restart
        m_Restart = asset.FindActionMap("Restart", throwIfNotFound: true);
        m_Restart_ReStart = m_Restart.FindAction("ReStart", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Restart
    private readonly InputActionMap m_Restart;
    private List<IRestartActions> m_RestartActionsCallbackInterfaces = new List<IRestartActions>();
    private readonly InputAction m_Restart_ReStart;
    public struct RestartActions
    {
        private @RestartAction m_Wrapper;
        public RestartActions(@RestartAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @ReStart => m_Wrapper.m_Restart_ReStart;
        public InputActionMap Get() { return m_Wrapper.m_Restart; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RestartActions set) { return set.Get(); }
        public void AddCallbacks(IRestartActions instance)
        {
            if (instance == null || m_Wrapper.m_RestartActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RestartActionsCallbackInterfaces.Add(instance);
            @ReStart.started += instance.OnReStart;
            @ReStart.performed += instance.OnReStart;
            @ReStart.canceled += instance.OnReStart;
        }

        private void UnregisterCallbacks(IRestartActions instance)
        {
            @ReStart.started -= instance.OnReStart;
            @ReStart.performed -= instance.OnReStart;
            @ReStart.canceled -= instance.OnReStart;
        }

        public void RemoveCallbacks(IRestartActions instance)
        {
            if (m_Wrapper.m_RestartActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRestartActions instance)
        {
            foreach (var item in m_Wrapper.m_RestartActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RestartActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RestartActions @Restart => new RestartActions(this);
    public interface IRestartActions
    {
        void OnReStart(InputAction.CallbackContext context);
    }
}
