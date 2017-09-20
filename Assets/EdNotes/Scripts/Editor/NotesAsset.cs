using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace EdNotes
{
	public class NotesAsset : ScriptableObject
	{
		public enum IconPos { Left, Right, LeftName, RightName }

		[SerializeField, HideInInspector] private string iconName = "sv_icon_dot4_pix16_gizmo"; //"sv_label_4";
		[HideInInspector] public List<Note> notes = new List<Note>();
		[HideInInspector] public bool[] labelExpandWidth = { false, false };
		[HideInInspector] public IconPos[] iconPos = { IconPos.Left, IconPos.LeftName };
		[HideInInspector] public float[] iconOffs = { 0f, 0f };

		public GUIStyle Style { get; private set; }
		public GUIContent Icon { get; private set; }
		public bool IsLabelIcon { get; private set; }

		private Dictionary<string, Note> _cache = null;
		public Dictionary<string, Note> Cache { get { return _cache ?? UpdateCache(); } }

		public string IconName
		{
			get { return iconName; }
			set { iconName = value; UpdateIcon(); }
		}

		public Dictionary<string, Note> UpdateCache()
		{
			if (_cache == null) _cache = new Dictionary<string, Note>();
			else _cache.Clear();

			foreach (Note n in notes)
				if (n.targetObj != null) _cache.Add(n.targetGUID, n);

			return _cache;
		}

		public void UpdateIcon()
		{
			Icon = null;
			IsLabelIcon = false;
			if (!string.IsNullOrEmpty(iconName))
			{
				Icon = EditorGUIUtility.IconContent(iconName);
				if (Icon.image == null)
				{
					Icon = null;
				}
				else
				{
					if (IconName.Contains("sv_label")) IsLabelIcon = true;

					if (IsLabelIcon)
					{
						Style = new GUIStyle()
						{
							stretchWidth = false,
							border = new RectOffset(6, 6, 0, 0),
							padding = new RectOffset(5, 5, 0, 0),
							normal = { background = (Texture2D)Icon.image }
						};
					}
				}
			}
		}

		// ------------------------------------------------------------------------------------------------------------		
	}
}
