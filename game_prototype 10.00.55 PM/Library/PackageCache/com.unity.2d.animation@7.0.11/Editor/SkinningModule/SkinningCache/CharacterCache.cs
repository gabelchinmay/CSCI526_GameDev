using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.U2D.Animation
{
    internal class CharacterCache : SkinningObject, IEnumerable<CharacterPartCache>
    {
        [SerializeField]
        SkeletonCache m_Skeleton;
        [SerializeField]
        List<CharacterPartCache> m_Parts = new List<CharacterPartCache>();
        [SerializeField]
        Vector2Int m_Dimension;
        [SerializeField]
        List<CharacterGroupCache> m_Groups = new List<CharacterGroupCache>();

        public SkeletonCache skeleton
        {
            get => m_Skeleton;
            set => m_Skeleton = value;
        }

        public virtual CharacterPartCache[] parts
        {
            get => m_Parts.ToArray();
            set => m_Parts = new List<CharacterPartCache>(value);
        }

        public virtual CharacterGroupCache[] groups
        {
            get => m_Groups.ToArray();
            set => m_Groups = new List<CharacterGroupCache>(value);
        }

        public Vector2Int dimension
        {
            get => m_Dimension;
            set => m_Dimension = value;
        }

        public IEnumerator<CharacterPartCache> GetEnumerator()
        {
            return ((IEnumerable<CharacterPartCache>)m_Parts).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CharacterPartCache>)m_Parts).GetEnumerator();
        }
    }
}
