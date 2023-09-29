#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using EdNotes;
using UnityEditor;

[CustomEditor(typeof(ShowEditorNote))]
public class ShowEditorNoteInspector : Editor
{
	private void Awake()
	{
		// thanks to SisusCo...
		// https://forum.unity.com/threads/modifying-component-inspector-headers-for-component-folders.504247/
		
		// Change our ShowEditorNote inspector headers to just show "Editor Note"
		Type inspectorTitlesType = typeof(ObjectNames).GetNestedType("InspectorTitles", BindingFlags.Static | BindingFlags.NonPublic);
		var inspectorTitlesField = inspectorTitlesType.GetField("s_InspectorTitles", BindingFlags.Static | BindingFlags.NonPublic);
		var cache = (Dictionary<Type, string>)inspectorTitlesField.GetValue(null);
		cache[typeof(ShowEditorNote)] = "Editor Note";
	}

	public override void OnInspectorGUI()
	{
		ShowEditorNote targetScript = (ShowEditorNote)target;
		var instanceId = targetScript.gameObject.GetInstanceID();

		NotesContainer c = EditorNotes.GetContainer(targetScript.gameObject.scene);
		var inCache = c.Cache.ContainsKey(instanceId);
		if( inCache )
		{
			c.Cache.TryGetValue(instanceId, out Note note);
			EditorGUILayout.TextArea($"{note.text}", EditorStyles.whiteLargeLabel);
		}
		else
			EditorGUILayout.LabelField($"No Note has been saved for this GameObject, use Window > Notes", EditorStyles.boldLabel);
	}
}



#endif
