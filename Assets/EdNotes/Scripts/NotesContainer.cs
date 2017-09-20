using System.Collections.Generic;
using UnityEngine;


namespace EdNotes
{
	public class NotesContainer : MonoBehaviour
	{
		[HideInInspector] public List<Note> notes = new List<Note>();

		private Dictionary<int, Note> _cache = null;
		public Dictionary<int, Note> Cache { get { return _cache ?? UpdateCache(); } }

		public Dictionary<int, Note> UpdateCache()
		{
			if (_cache == null) _cache = new Dictionary<int, Note>();
			else _cache.Clear();

			foreach (Note n in notes)
				if (n.targetObj != null) _cache.Add(n.targetObj.GetInstanceID(), n);

			return _cache;
		}
	}
}
