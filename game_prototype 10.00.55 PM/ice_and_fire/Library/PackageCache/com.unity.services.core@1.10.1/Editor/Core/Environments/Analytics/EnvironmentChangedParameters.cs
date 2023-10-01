using System;

namespace Unity.Services.Core.Editor.Environments.Analytics
{
    [Serializable]
    struct EnvironmentChangedParameters
    {
        public string action;
        public string component;
        public string package;
    }
}
