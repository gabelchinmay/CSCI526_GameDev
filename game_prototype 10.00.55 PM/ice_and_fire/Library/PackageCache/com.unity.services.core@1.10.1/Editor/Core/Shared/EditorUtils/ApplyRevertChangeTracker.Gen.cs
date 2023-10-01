// WARNING: Auto generated code. Modifications will be lost!
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.Services.Core.Editor.Shared.EditorUtils
{
    class ApplyRevertChangeTracker<T> where T : ScriptableObject, ICopyable<T>
    {
        public SerializedObject SerializedObject { get; }

        readonly SerializedObject m_EditorTarget;

        public ApplyRevertChangeTracker(SerializedObject editorTarget)
        {
            m_EditorTarget = editorTarget;
            SerializedObject = DeepCopy(editorTarget);
        }

        public bool IsDirty()
        {
            var property = SerializedObject.GetIterator();
            while (property.NextVisible(true))
            {
                if (property.hasMultipleDifferentValues)
                {
                    continue;
                }

                for (var i = 0; i < SerializedObject.targetObjects.Length; i++)
                {
                    var stateObj = SerializedObject.targetObjects[i];
                    var editorObj = m_EditorTarget.targetObjects[i];

                    if (!Equals(FieldValue(property.propertyPath, (T)stateObj), FieldValue(property.propertyPath, (T)editorObj)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Apply()
        {
            CopyValues(SerializedObject, m_EditorTarget);
        }

        public void Reset()
        {
            CopyValues(m_EditorTarget, SerializedObject);
        }

        static void CopyValues(SerializedObject from, SerializedObject to)
        {
            for (var i = 0; i < from.targetObjects.Length; i++)
            {
                ((ICopyable<T>)from.targetObjects[i]).CopyTo((T)to.targetObjects[i]);
            }

            to.UpdateIfRequiredOrScript();
        }

        static object FieldValue(string path, T target)
        {
            return typeof(T)
                .GetField(path, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.GetValue(target);
        }

        static SerializedObject DeepCopy(SerializedObject source)
        {
            return new SerializedObject(source.targetObjects.Select(o => DeepCopy((T)o)).ToArray());
        }

        static Object DeepCopy(T source)
        {
            var inst = ScriptableObject.CreateInstance<T>();
            source.CopyTo(inst);
            return inst;
        }
    }
}
