using Unity.Services.Core.Internal;
using UnityEditor;
using UnityEngine.Analytics;

namespace Unity.Services.Core.Editor.Environments.Analytics
{
    class EnvironmentAnalyticsSender : IAnalyticsSender
    {
        internal const string EditorGameServiceEventName = "editorgameserviceeditor";
        internal const int EditorGameServiceEventVersion = 1;

        public AnalyticsResult SendEvent(object parameters)
        {
            var result = EditorAnalytics.SendEventWithLimit(
                EditorGameServiceEventName,
                parameters,
                EditorGameServiceEventVersion);

            LogVerbose(EditorGameServiceEventName, EditorGameServiceEventVersion, result, parameters);
            return result;
        }

        static void LogVerbose(string eventName, int eventVersion, AnalyticsResult result, object parameters)
        {
            CoreLogger.LogVerbose($"Sent Analytics Event: {eventName}.v{eventVersion}. Result: {result}. Parameters {parameters.ToString()}");
        }
    }
}
