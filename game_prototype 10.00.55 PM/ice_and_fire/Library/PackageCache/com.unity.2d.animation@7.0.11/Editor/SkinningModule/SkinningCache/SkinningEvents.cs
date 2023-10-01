using UnityEngine.Events;

namespace UnityEditor.U2D.Animation
{
    internal class SkinningEvents
    {
        // The re-implemented virtual methods in these classes are there so that 
        // they can be mocked in tests.
        public class SpriteEvent : UnityEvent<SpriteCache>
        {
            public new virtual void AddListener(UnityAction<SpriteCache> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<SpriteCache> listener) => base.RemoveListener(listener);
        }
        public class SkeletonEvent : UnityEvent<SkeletonCache>
        {
            public new virtual void AddListener(UnityAction<SkeletonCache> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<SkeletonCache> listener) => base.RemoveListener(listener);
        }
        public class MeshEvent : UnityEvent<MeshCache>
        {
            public new virtual void AddListener(UnityAction<MeshCache> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<MeshCache> listener) => base.RemoveListener(listener);
        }
        public class MeshPreviewEvent : UnityEvent<MeshPreviewCache>
        {
            public new virtual void AddListener(UnityAction<MeshPreviewCache> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<MeshPreviewCache> listener) => base.RemoveListener(listener);
        }
        public class SkinningModuleModeEvent : UnityEvent<SkinningMode>
        {
            public new virtual void AddListener(UnityAction<SkinningMode> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<SkinningMode> listener) => base.RemoveListener(listener);
        }
        public class BoneSelectionEvent : UnityEvent
        {
            public new virtual void AddListener(UnityAction listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction listener) => base.RemoveListener(listener);
        }
        public class BoneEvent : UnityEvent<BoneCache>
        {
            public new virtual void AddListener(UnityAction<BoneCache> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<BoneCache> listener) => base.RemoveListener(listener);
        }
        public class CharacterPartEvent : UnityEvent<CharacterPartCache>
        {
            public new virtual void AddListener(UnityAction<CharacterPartCache> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<CharacterPartCache> listener) => base.RemoveListener(listener);
        }
        public class ToolChangeEvent : UnityEvent<ITool>
        {
            public new virtual void AddListener(UnityAction<ITool> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<ITool> listener) => base.RemoveListener(listener);
        }
        public class RestoreBindPoseEvent : UnityEvent
        {
            public new virtual void AddListener(UnityAction listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction listener) => base.RemoveListener(listener);
        }
        public class CopyEvent : UnityEvent
        {
            public new virtual void AddListener(UnityAction listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction listener) => base.RemoveListener(listener);
        }
        public class PasteEvent : UnityEvent<bool, bool, bool, bool>
        {
            public new virtual void AddListener(UnityAction<bool, bool, bool, bool> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<bool, bool, bool, bool> listener) => base.RemoveListener(listener);
        }
        public class ShortcutEvent : UnityEvent<string>
        {
            public new virtual void AddListener(UnityAction<string> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<string> listener) => base.RemoveListener(listener);
        }
        public class BoneVisibilityEvent : UnityEvent<string>
        {
            public new virtual void AddListener(UnityAction<string> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<string> listener) => base.RemoveListener(listener);
        }
        public class MeshPreviewBehaviourChangeEvent : UnityEvent<IMeshPreviewBehaviour>
        {
            public new virtual void AddListener(UnityAction<IMeshPreviewBehaviour> listener) => base.AddListener(listener);
            public new virtual void RemoveListener(UnityAction<IMeshPreviewBehaviour> listener) => base.RemoveListener(listener);
        }

        SpriteEvent m_SelectedSpriteChanged = new SpriteEvent();
        SkeletonEvent m_SkeletonPreviewPoseChanged = new SkeletonEvent();
        SkeletonEvent m_SkeletonBindPoseChanged = new SkeletonEvent();
        SkeletonEvent m_SkeletonTopologyChanged = new SkeletonEvent();
        MeshEvent m_MeshChanged = new MeshEvent();
        MeshPreviewEvent m_MeshPreviewChanged = new MeshPreviewEvent();
        SkinningModuleModeEvent m_SkinningModuleModeChanged = new SkinningModuleModeEvent();
        BoneSelectionEvent m_BoneSelectionChangedEvent = new BoneSelectionEvent();
        BoneEvent m_BoneNameChangedEvent = new BoneEvent();
        BoneEvent m_BoneDepthChangedEvent = new BoneEvent();
        BoneEvent m_BoneColorChangedEvent = new BoneEvent();
        CharacterPartEvent m_CharacterPartChanged = new CharacterPartEvent();
        ToolChangeEvent m_ToolChanged = new ToolChangeEvent();
        RestoreBindPoseEvent m_RestoreBindPose = new RestoreBindPoseEvent();
        CopyEvent m_CopyEvent = new CopyEvent();
        PasteEvent m_PasteEvent = new PasteEvent();
        ShortcutEvent m_ShortcutEvent = new ShortcutEvent();
        BoneVisibilityEvent m_BoneVisibilityEvent = new BoneVisibilityEvent();
        MeshPreviewBehaviourChangeEvent m_MeshPreviewBehaviourChange = new MeshPreviewBehaviourChangeEvent();

        //Setting them as virtual so that we can create mock them
        public virtual SpriteEvent selectedSpriteChanged { get { return m_SelectedSpriteChanged; } }
        public virtual SkeletonEvent skeletonPreviewPoseChanged { get { return m_SkeletonPreviewPoseChanged; } }
        public virtual SkeletonEvent skeletonBindPoseChanged { get { return m_SkeletonBindPoseChanged; } }
        public virtual SkeletonEvent skeletonTopologyChanged { get { return m_SkeletonTopologyChanged; } }
        public virtual MeshEvent meshChanged { get { return m_MeshChanged; } }
        public virtual MeshPreviewEvent meshPreviewChanged { get { return m_MeshPreviewChanged; } }
        public virtual SkinningModuleModeEvent skinningModeChanged { get { return m_SkinningModuleModeChanged; } }
        public virtual BoneSelectionEvent boneSelectionChanged { get { return m_BoneSelectionChangedEvent; } }
        public virtual BoneEvent boneNameChanged { get { return m_BoneNameChangedEvent; } }
        public virtual BoneEvent boneDepthChanged { get { return m_BoneDepthChangedEvent; } }
        public virtual BoneEvent boneColorChanged { get { return m_BoneColorChangedEvent; } }
        public virtual CharacterPartEvent characterPartChanged { get { return m_CharacterPartChanged; } }
        public virtual ToolChangeEvent toolChanged { get { return m_ToolChanged; } }
        public virtual RestoreBindPoseEvent restoreBindPose { get { return m_RestoreBindPose; } }
        public virtual CopyEvent copy { get { return m_CopyEvent; } }
        public virtual PasteEvent paste { get { return m_PasteEvent; } }
        public virtual ShortcutEvent shortcut { get { return m_ShortcutEvent; } }
        public virtual BoneVisibilityEvent boneVisibility { get { return m_BoneVisibilityEvent; } }
        public virtual MeshPreviewBehaviourChangeEvent meshPreviewBehaviourChange { get { return m_MeshPreviewBehaviourChange; } }
    }
}
