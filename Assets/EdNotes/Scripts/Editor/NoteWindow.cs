using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


namespace EdNotes
{
	public class NoteWindow : EditorWindow
	{
		private static readonly GUIContent GC_Save = new GUIContent("Save");
		private static readonly GUIContent GC_Close = new GUIContent("Close");
		private static readonly GUIContent GC_Delete = new GUIContent("X", "Remove this note");
		private static readonly GUIContent GC_IconHead = new GUIContent("Icon: ");
		private static GUIContent GC_Settings = null;

		[System.NonSerialized] private Object targetObj;
		[System.NonSerialized] private GameObject targetGO;
		[System.NonSerialized] private string targetGUID;
		[System.NonSerialized] private NotesContainer container;
		[System.NonSerialized] private Note note;
		[System.NonSerialized] private bool inScene;
		[System.NonSerialized] private bool isNew;
		[System.NonSerialized] private bool dirty;
		[System.NonSerialized] private string text;
		[System.NonSerialized] private EditorNotesSettings iconSelector = new EditorNotesSettings();

		// ------------------------------------------------------------------------------------------------------------

		[MenuItem("Assets/Notes", false, 0), MenuItem("GameObject/Notes", false, 11), MenuItem("Window/Notes &N", false)]
		public static void ShowNoteWindow()
		{
			if (Selection.activeObject != null)
			{
				GetWindow<NoteWindow>(true, "Note").Init();
			}
		}

		[MenuItem("Assets/Notes", true), MenuItem("GameObject/Notes", true), MenuItem("Window/Notes &N", true)]
		private static bool HierarchyMenuValidate()
		{
			return Selection.activeObject != null;
		}

		private void Init()
		{
			targetObj = Selection.activeObject;
			targetGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(targetObj));
			targetGO = Selection.activeGameObject;
			titleContent = new GUIContent("Note: " + targetObj.name);

			inScene = false;
			dirty = false;
			isNew = false;
			container = null;
			note = null;

			if (targetGO != null && targetGO.scene.IsValid())
			{
				inScene = true;
				container = EditorNotes.GetContainer(targetGO.scene);
				if (container != null)
				{
					foreach (Note n in container.notes)
						if (n.targetObj == targetGO) { note = n; break; }
				}

			}
			else
			{
				List<Note> notesList = EditorNotes.Settings.notes;
				if (notesList != null)
				{
					foreach (Note n in notesList)
						if (n.targetGUID == targetGUID) { note = n; break; }
				}
			}

			if (note == null)
			{
				isNew = true;
				note = new Note()
				{
					targetObj = targetObj,
					targetGUID = targetGUID,
					text = ""
				};
			}

			text = note.text;
		}

		private void Update()
		{
			if (targetObj == null || note == null) Close();
		}

		private void OnGUI()
		{
			if (targetObj == null || note == null)
			{
				GUIUtility.ExitGUI();
				return;
			}

			if (GC_Settings == null)
			{
				GC_Settings = EditorGUIUtility.IconContent("_Popup");
				GC_Settings.tooltip = "EdNotes Settings";
			}

			EditorGUI.BeginChangeCheck();
			text = EditorGUILayout.TextArea(text, GUILayout.ExpandHeight(true));
			if (EditorGUI.EndChangeCheck()) dirty = true;

			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.FlexibleSpace();
				Rect r = GUILayoutUtility.GetRect(GC_Settings, GUI.skin.button, GUILayout.Width(30));
				if (GUI.Button(r, GC_Settings)) PopupWindow.Show(r, iconSelector);
				GUI.enabled = !isNew;
				if (GUILayout.Button(GC_Delete, GUILayout.Width(25))) Delete();
				GUI.enabled = dirty;
				if (GUILayout.Button(GC_Save, GUILayout.Width(80))) Save();
				GUI.enabled = true;
				if (GUILayout.Button(GC_Close, GUILayout.Width(80))) { if (!dirty || EditorUtility.DisplayDialog("EdNotes", "Unsaved changes. Close?", "Yes", "Cancel")) Close(); }
				GUILayout.FlexibleSpace();
			}
			EditorGUILayout.EndHorizontal();
		}

		private void Save()
		{
			GUIUtility.keyboardControl = 0;
			dirty = false;
			note.text = text;

			if (text.Length == 0 && !isNew)
			{
				Delete();
				return;
			}

			if (!inScene)
			{
				if (isNew)
				{
					isNew = false;
					EditorNotes.Settings.notes.Add(note);
					EditorNotes.Settings.UpdateCache();
					EditorApplication.RepaintProjectWindow();
				}

				EditorUtility.SetDirty(EditorNotes.Settings);
			}
			else
			{
				if (container == null)
				{
					GameObject go = new GameObject(EditorNotes.NotesContainerObjName)
					{
						tag = "EditorOnly",
						hideFlags = HideFlags.HideInHierarchy
					};
					container = go.AddComponent<NotesContainer>();
					if (go.scene != targetGO.scene) EditorSceneManager.MoveGameObjectToScene(go, targetGO.scene);
					EditorNotes.UpdateSceneCache();
				}

				if (isNew)
				{
					isNew = false;
					container.notes.Add(note);
					container.UpdateCache();
					EditorApplication.RepaintHierarchyWindow();
				}

				EditorSceneManager.MarkSceneDirty(container.gameObject.scene);				
			}
		}

		private void Delete()
		{
			GUIUtility.keyboardControl = 0;
			isNew = true;
			note.text = text = "";

			if (!inScene)
			{
				EditorNotes.Settings.notes.Remove(note);

				if (EditorNotes.Settings.notes.Count > 0)
				{ 
					for (int i = EditorNotes.Settings.notes.Count - 1; i >= 0; i--)
					{
						if (EditorNotes.Settings.notes[i].targetObj == null || string.IsNullOrEmpty(EditorNotes.Settings.notes[i].text))
							EditorNotes.Settings.notes.RemoveAt(i);
					}
				}

				EditorNotes.Settings.UpdateCache();
				EditorUtility.SetDirty(EditorNotes.Settings);
				EditorApplication.RepaintProjectWindow();

			}
			else
			{
				container.notes.Remove(note);

				if (container.notes.Count > 0)
				{ 
					for (int i = container.notes.Count - 1; i >= 0; i--)
					{
						if (container.notes[i].targetObj == null || string.IsNullOrEmpty(container.notes[i].text))
							container.notes.RemoveAt(i);
					}
				}

				container.UpdateCache();
				EditorSceneManager.MarkSceneDirty(targetGO.scene);
				EditorApplication.RepaintHierarchyWindow();
			}

		}

		// ------------------------------------------------------------------------------------------------------------
	}
}
