using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace EdNotes
{
	public class EditorNotesSettings : PopupWindowContent
	{
		private class StyleDefs
		{
			public GUIStyle Background = "sv_iconselector_back";
			public GUIStyle Seperator = "sv_iconselector_sep";
			public GUIStyle Selection = "sv_iconselector_selection";
			public GUIStyle SelectionLabel = "sv_iconselector_labelselection";
			public GUIStyle NoneButton = "sv_iconselector_button";
		}

		private static readonly GUIContent GC_Hierarchy = new GUIContent("Hierarchy");
		private static readonly GUIContent GC_Project = new GUIContent("Project Panel");
		private static readonly GUIContent GC_LabelExpand = new GUIContent("Label Expand");
		private static readonly GUIContent GC_IconPos = new GUIContent("Icon Position");
		private static readonly GUIContent GC_IconOffs = new GUIContent("Icon Offset");

		private static readonly Vector2 sz = new Vector2(140f, 260f);
		private static int HashIconSelector = "IconSelectorPopup".GetHashCode();

		private static GUIContent[] GC_LabelIconsL = null;
		private static GUIContent[] GC_LabelIcons = null;
		private static GUIContent[] GC_SmallIcons = null;
		private static GUIContent[] GC_LargeIcons = null;
		private static GUIContent GC_NoneButton;

		private StyleDefs Styles = null;
		private Texture2D selectedTexture = null;

		// ------------------------------------------------------------------------------------------------------------		

		public override Vector2 GetWindowSize()
		{
			return sz;
		}

		public override void OnOpen()
		{
			base.OnOpen();
			Init();
		}

		public override void OnClose()
		{
			base.OnClose();
		}

		public override void OnGUI(Rect rect)
		{
			if (Styles == null) Styles = new StyleDefs();
			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape) CloseWindow();

			GUI.BeginGroup(new Rect(0f, 0f, editorWindow.position.width, editorWindow.position.height), Styles.Background);
			
			// ---
			GUI.Label(new Rect(6f, 4f, 110f, 20f), "Select Icon");
			if (GUI.Button(new Rect(93f, 6f, 43f, 12f), GC_NoneButton, Styles.NoneButton))
			{
				selectedTexture = null;
				EditorNotes.Settings.IconName = "";
				EditorUtility.SetDirty(EditorNotes.Settings);
				EditorApplication.RepaintHierarchyWindow();
				EditorApplication.RepaintProjectWindow();
			}

			// ---
			GUILayout.Space(22f);
			GUILayout.Label(GUIContent.none, Styles.Seperator);
			GUILayout.Space(3f);

			// ---
			GUILayout.BeginHorizontal();
			GUILayout.Space(6f);
			for (int i = 0; i < GC_LabelIcons.Length / 2; i++) DoButton(GC_LabelIcons[i], true);
			GUILayout.EndHorizontal();
			GUILayout.Space(5f);
			GUILayout.BeginHorizontal();
			GUILayout.Space(6f);
			for (int j = GC_LabelIcons.Length / 2; j < GC_LabelIcons.Length; j++) DoButton(GC_LabelIcons[j], true);
			GUILayout.EndHorizontal();

			// ---
			GUILayout.Space(3f);
			GUILayout.Label(GUIContent.none, Styles.Seperator);
			GUILayout.Space(3f);

			// ---
			GUILayout.BeginHorizontal();
			GUILayout.Space(9f);
			for (int k = 0; k < GC_SmallIcons.Length / 2; k++) DoButton(GC_SmallIcons[k], false);
			GUILayout.Space(3f);
			GUILayout.EndHorizontal();
			GUILayout.Space(6f);
			GUILayout.BeginHorizontal();
			GUILayout.Space(9f);
			for (int l = GC_SmallIcons.Length / 2; l < GC_SmallIcons.Length; l++) DoButton(GC_SmallIcons[l], false);
			GUILayout.Space(3f);
			GUILayout.EndHorizontal();

			// ---
			GUILayout.Space(3f);
			GUILayout.Label(GUIContent.none, Styles.Seperator);
			GUILayout.Space(3f);

			// ---
			EditorGUIUtility.labelWidth = 80f;
			EditorGUI.BeginChangeCheck();
			GUILayout.Label(GC_Hierarchy);
			EditorNotes.Settings.labelExpandWidth[0] = EditorGUILayout.Toggle(GC_LabelExpand, EditorNotes.Settings.labelExpandWidth[0]);
			EditorNotes.Settings.iconPos[0] = (NotesAsset.IconPos)EditorGUILayout.EnumPopup(GC_IconPos, EditorNotes.Settings.iconPos[0]);
			EditorNotes.Settings.iconOffs[0] = EditorGUILayout.FloatField(GC_IconOffs, EditorNotes.Settings.iconOffs[0]);
			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(EditorNotes.Settings);
				EditorApplication.RepaintHierarchyWindow();
			}

			// ---
			GUILayout.Space(3f);
			GUILayout.Label(GUIContent.none, Styles.Seperator);
			GUILayout.Space(3f);

			// ---
			EditorGUIUtility.labelWidth = 80f;
			EditorGUI.BeginChangeCheck();
			GUILayout.Label(GC_Project);
			EditorNotes.Settings.labelExpandWidth[1] = EditorGUILayout.Toggle(GC_LabelExpand, EditorNotes.Settings.labelExpandWidth[1]);
			EditorNotes.Settings.iconPos[1] = (NotesAsset.IconPos)EditorGUILayout.EnumPopup(GC_IconPos, EditorNotes.Settings.iconPos[1]);
			EditorNotes.Settings.iconOffs[1] = EditorGUILayout.FloatField(GC_IconOffs, EditorNotes.Settings.iconOffs[1]);
			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(EditorNotes.Settings);
				EditorApplication.RepaintProjectWindow();
			}

			// ---
			GUI.EndGroup();
		}

		private void CloseWindow()
		{
			editorWindow.Close();
			GUI.changed = true;
			GUIUtility.ExitGUI();
		}

		// ------------------------------------------------------------------------------------------------------------		

		private void Init()
		{
			if (GC_LabelIcons == null)
			{
				GC_NoneButton = EditorGUIUtility.IconContent("sv_icon_none");
				GC_NoneButton.text = "None";

				GC_LabelIcons = GetTextures("sv_icon_name", "", 0, 8);
				GC_LabelIconsL = GetTextures("sv_label_", "", 0, 8);
				GC_SmallIcons = GetTextures("sv_icon_dot", "_sml", 0, 16);
				GC_LargeIcons = GetTextures("sv_icon_dot", "_pix16_gizmo", 0, 16);
			}

			SetSelectedTexture(EditorNotes.Settings.IconName);
		}

		private GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
		{
			GUIContent[] array = new GUIContent[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = EditorGUIUtility.IconContent(baseName + (startIndex + i) + postFix);
			}
			return array;
		}

		private void DoButton(GUIContent content, bool labelIcon)
		{
			Rect rect = GUILayoutUtility.GetRect(content, GUIStyle.none);

			if (content.image == selectedTexture)
			{
				Rect r = rect;
				r.x -= 2f;
				r.y -= 2f;
				r.width = selectedTexture.width + 4f;
				r.height = selectedTexture.height + 4f;
				GUI.Label(r, GUIContent.none, (!labelIcon) ? Styles.Selection : Styles.SelectionLabel);
			}

			if (GUI.Button(rect, content, GUIStyle.none))
			{
				selectedTexture = (Texture2D)content.image;
				EditorNotes.Settings.IconName = GetSelectedTextureName();
				EditorUtility.SetDirty(EditorNotes.Settings);
				EditorApplication.RepaintHierarchyWindow();
				EditorApplication.RepaintProjectWindow();
			}
		}

		private Texture2D ConvertLargeIconToSmallIcon(Texture2D largeIcon)
		{
			Texture2D result;
			if (largeIcon == null)
			{
				result = null;
			}
			else
			{
				for (int i = 0; i < GC_LabelIconsL.Length; i++)
				{
					if (GC_LabelIconsL[i].image == largeIcon)
					{
						result = (Texture2D)GC_LabelIcons[i].image;
						return result;
					}
				}
				for (int j = 0; j < GC_LargeIcons.Length; j++)
				{
					if (GC_LargeIcons[j].image == largeIcon)
					{
						result = (Texture2D)GC_SmallIcons[j].image;
						return result;
					}
				}
				result = largeIcon;
			}
			return result;
		}

		private Texture2D ConvertSmallIconToLargeIcon(Texture2D smallIcon)
		{
			bool labelIcon = smallIcon.name.Contains("icon_name");

			Texture2D result;
			if (labelIcon)
			{
				for (int i = 0; i < GC_LabelIcons.Length; i++)
				{
					if (GC_LabelIcons[i].image == smallIcon)
					{
						result = (Texture2D)GC_LabelIconsL[i].image;
						return result;
					}
				}
			}
			else
			{
				for (int j = 0; j < GC_SmallIcons.Length; j++)
				{
					if (GC_SmallIcons[j].image == smallIcon)
					{
						result = (Texture2D)GC_LargeIcons[j].image;
						return result;
					}
				}
			}
			result = smallIcon;
			return result;
		}

		private void SetSelectedTexture(string name)
		{
			selectedTexture = null;
			foreach (GUIContent g in GC_LabelIconsL)
			{
				if (g.image.name == name)
				{
					selectedTexture = ConvertLargeIconToSmallIcon((Texture2D)g.image);
					return;
				}
			}

			foreach (GUIContent g in GC_LargeIcons)
			{
				if (g.image.name == name)
				{
					selectedTexture = ConvertLargeIconToSmallIcon((Texture2D)g.image);
					return;
				}
			}

		}

		private string GetSelectedTextureName()
		{
			if (selectedTexture != null)
			{
				Texture2D t = ConvertSmallIconToLargeIcon(selectedTexture);
				return (t == null ? "" : t.name);
			}

			return "";
		}

		// ------------------------------------------------------------------------------------------------------------		
	}
}
