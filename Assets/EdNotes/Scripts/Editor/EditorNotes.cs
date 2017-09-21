using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;


namespace EdNotes
{
	[InitializeOnLoad]
	public static class EditorNotes
	{
		public static readonly string NotesContainerObjName = "_EdNotes_K2JB5LWK4E3B_";

		private static NotesAsset _settings = null;
		public static NotesAsset Settings { get { return _settings ?? LoadNotesAsset(); } }

		public static Dictionary<Scene, NotesContainer> cache = null;
		
		// ------------------------------------------------------------------------------------------------------------

		static EditorNotes()
		{
			EditorApplication.hierarchyWindowItemOnGUI += HierarchyOnGUI;
			EditorApplication.projectWindowItemOnGUI += ProjectWindowItemCallback;
			EditorSceneManager.sceneOpened += SceneOpened;
			EditorSceneManager.sceneClosed += SceneClosed;
		}

		public static NotesContainer GetContainer(Scene scene)
		{
			if (cache == null)
			{
				cache = new Dictionary<Scene, NotesContainer>();
				UpdateSceneCache();
			}

			NotesContainer c;
			if (cache.TryGetValue(scene, out c)) return c;
			return null;
		}

		public static void UpdateSceneCache()
		{
			Object[] objs = Object.FindObjectsOfType<NotesContainer>();
			foreach (NotesContainer c in objs)
			{
				if (!cache.ContainsKey(c.gameObject.scene)) cache.Add(c.gameObject.scene, c);
			}
		}

		private static void SceneOpened(Scene scene, OpenSceneMode mode)
		{
			UpdateSceneCache();
		}

		private static void SceneClosed(Scene scene)
		{
			if (cache.ContainsKey(scene)) cache.Remove(scene);
		}

		private static void HierarchyOnGUI(int instanceID, Rect rect)
		{
			if (Settings.Icon == null || Event.current.type != EventType.Repaint) return;			
			GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
			if (go == null) return; // skip the "Scene" entry

			NotesContainer c = GetContainer(go.scene);
			if (c == null || !c.Cache.ContainsKey(instanceID)) return;

			if (Settings.IsLabelIcon) DrawLabel(0, rect, go.name);
			else DrawIcon(0, rect, go.name);
		}

		private static void ProjectWindowItemCallback(string guid, Rect rect)
		{
			if (Settings.Icon == null || Event.current.type != EventType.Repaint) return;
			if (!Settings.Cache.ContainsKey(guid)) return;

			string name = AssetDatabase.GUIDToAssetPath(guid);
			int i = name.LastIndexOf('/') + 1;
			if (i > 0) name = name.Substring(i);
			i = name.LastIndexOf('.');
			if (i > 0) name = name.Substring(0, i);

			if (Settings.IsLabelIcon)
			{
				rect.x += 16f;
				DrawLabel(1, rect, name);
			}
			else DrawIcon(1, rect, name);
		}

		private static void DrawLabel(int idx, Rect rect, string name)
		{
			GUIContent gc = TempGUIContent(name);

			if (!Settings.labelExpandWidth[idx])
				rect.width = Settings.Style.CalcSize(gc).x;

			GUI.Label(rect, gc, Settings.Style);
		}

		private static void DrawIcon(int idx, Rect rect, string name)
		{
			if (Settings.iconPos[idx] == NotesAsset.IconPos.Left)
			{
				rect.x = Settings.iconOffs[idx];
			}
			else if (Settings.iconPos[idx] == NotesAsset.IconPos.Right)
			{
				rect.x = rect.xMax - 18f - Settings.iconOffs[idx];
			}
			else if (Settings.iconPos[idx] == NotesAsset.IconPos.LeftName)
			{
				rect.y -= 3f;
				rect.x = rect.x - 7f + Settings.iconOffs[idx];
			}
			else if (Settings.iconPos[idx] == NotesAsset.IconPos.RightName)
			{
				GUIContent gc = TempGUIContent(name);
				rect.x = rect.x + GUI.skin.label.CalcSize(gc).x - 5f + Settings.iconOffs[idx];
				if (idx == 1) rect.x += 18f;
			}

			rect.width = 16;
			GUI.Label(rect, Settings.Icon);
		}

		private static NotesAsset LoadNotesAsset()
		{			
			string[] guids = AssetDatabase.FindAssets("t:NotesAsset");
			string fn = (guids.Length > 0 ? AssetDatabase.GUIDToAssetPath(guids[0]) : GetPackageFolder() + "NotesAsset.asset");
			_settings = AssetDatabase.LoadAssetAtPath<NotesAsset>(fn);
			if (_settings == null)
			{
				_settings = ScriptableObject.CreateInstance<NotesAsset>();
				AssetDatabase.CreateAsset(_settings, fn);
				AssetDatabase.SaveAssets();
			}

			_settings.UpdateIcon();
			return _settings;
		}

		private static string GetPackageFolder()
		{
			try
			{
				string[] res = System.IO.Directory.GetFiles(Application.dataPath, "EditorNotes.cs", System.IO.SearchOption.AllDirectories);
				if (res.Length > 0) return "Assets" + res[0].Replace(Application.dataPath, "").Replace("EditorNotes.cs", "").Replace("\\", "/");
			}
			catch (System.Exception ex)
			{
				Debug.LogException(ex);
			}

			return "Assets/";
		}

		private static GUIContent GC_Temp = new GUIContent();
		private static GUIContent TempGUIContent(string label)
		{
			GC_Temp.text = label;
			return GC_Temp;
		}

		// ------------------------------------------------------------------------------------------------------------		
	}
}