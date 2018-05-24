using System;
using System.Collections.Generic;
using System.Threading;

namespace ProjectBrowser
{
	public static class ObjectDatabase
	{
		private static ReaderWriterLockSlim s_objectsLock = new ReaderWriterLockSlim();

		/// <summary>
		/// Dictionary of all objects in the project by <see cref="Guid"/>.
		/// </summary>
		private static Dictionary<string, ProjectObject> s_objects = new Dictionary<string, ProjectObject>();

		private static ReaderWriterLockSlim s_relationshipsLock = new ReaderWriterLockSlim();

		/// <summary>
		/// Dictionary from object unique ID to that object's OUTGOING relationships.
		/// </summary>
		/// <remarks>These are any relationships are discovered from within that object's data.</remarks>
		private static Dictionary<string, List<ObjectRelationship>> s_relationships = new Dictionary<string, List<ObjectRelationship>>();

		private static ReaderWriterLockSlim s_inverseRelationshipsLock = new ReaderWriterLockSlim();

		/// <summary>
		/// Dictionary from object unique ID to that object's INCOMING relationships.
		/// </summary>
		/// <remarks>These are any relationships are inferred from another object's data.</remarks>
		private static Dictionary<string, List<ObjectRelationship>> s_inverseRelationships = new Dictionary<string, List<ObjectRelationship>>();

		private static ReaderWriterLockSlim s_folderMappingsLock = new ReaderWriterLockSlim();

		/// <summary>
		/// Dictionary mapping folder paths to the folder object's uniqueId.
		/// </summary>
		private static Dictionary<string, string> s_folderMappings = new Dictionary<string, string>();

		/// <summary>
		/// Event called when an object is added to the database.
		/// </summary>
		public static event ObjectDatabaseEventDelegate Added;

		/// <summary>
		/// Event called when an object is removed from the database.
		/// </summary>
		public static event ObjectDatabaseEventDelegate Removed;

		/// <summary>
		/// Event called when all objects are cleared from the database.
		/// </summary>
		public static event ObjectDatabaseEventDelegate Cleared;

		#region Objects

		/// <summary>
		/// Gets the object with the specified unique ID.
		/// </summary>
		public static ProjectObject GetObject(string key)
		{
			s_objectsLock.EnterReadLock();
			try
			{
				return s_objects[key];
			}
			catch (KeyNotFoundException)
			{
				throw;
			}
			finally
			{
				s_objectsLock.ExitReadLock();
			}
		}

		/// <summary>
		/// Gets the object with the specified unique ID.
		/// </summary>
		public static bool TryGetObject(string key, out ProjectObject obj)
		{
			s_objectsLock.EnterReadLock();
			try
			{
				return s_objects.TryGetValue(key, out obj);
			}
			finally
			{
				s_objectsLock.ExitReadLock();
			}
		}

		/// <summary>
		/// Returns a new list containing all loaded objects at the time of calling.
		/// </summary>
		public static List<ProjectObject> AllObjects
		{
			get
			{
				s_objectsLock.EnterReadLock();
				try
				{
					return new List<ProjectObject>(s_objects.Values);
				}
				finally
				{
					s_objectsLock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// Adds a new object to the database if it doesn't already exist.
		/// </summary>
		public static void AddPlaceholderObject(string uniqueId, ProjectObject obj)
		{
			s_objectsLock.EnterWriteLock();
			try
			{
				if (!s_objects.ContainsKey(uniqueId))
				{
					s_objects.Add(uniqueId, obj ?? throw new ArgumentNullException("obj"));
				}
			}
			finally
			{
				s_objectsLock.ExitWriteLock();
			}
			Added?.Invoke(null, new ObjectDatabaseEventArgs(obj));
		}

		/// <summary>
		/// Adds a new object to the database.
		/// </summary>
		public static void AddObject(string uniqueId, ProjectObject obj)
		{
			Thread.Sleep(0);

			s_objectsLock.EnterWriteLock();
			try
			{
				s_objects[uniqueId] = obj ?? throw new ArgumentNullException("obj");
			}
			finally
			{
				s_objectsLock.ExitWriteLock();
			}
			Added?.Invoke(null, new ObjectDatabaseEventArgs(obj));
		}

		/// <summary>
		/// Removes the object with the specified id from the database.
		/// </summary>
		public static void RemoveObject(string uniqueId)
		{
			ProjectObject obj;
			bool removed = false;
			s_objectsLock.EnterWriteLock();
			try
			{
				if (s_objects.TryGetValue(uniqueId, out obj))
				{
					removed = s_objects.Remove(uniqueId);
					//TODO: unmap folder
				}
				//TODO: clear relationships from this object and inverse relationships to it
			}
			finally
			{
				s_objectsLock.ExitWriteLock();
			}
			if (removed)
			{
				Removed?.Invoke(null, new ObjectDatabaseEventArgs(obj));
			}
		}

		#endregion

		#region Relationships

		/// <summary>
		/// Adds a new relationship to the database.
		/// </summary>
		public static void AddRelationship(string ownerUniqueId, ObjectRelationship relationship)
		{
			s_relationshipsLock.EnterWriteLock();
			try
			{
				if (!s_relationships.TryGetValue(ownerUniqueId, out List<ObjectRelationship> relationships))
				{
					relationships = new List<ObjectRelationship>();
					s_relationships.Add(ownerUniqueId, relationships);
				}
				relationships.Add(relationship);
			}
			finally
			{
				s_relationshipsLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Adds a new inverse relationship to the database.
		/// </summary>
		public static void AddInverseRelationship(string ownerUniqueId, ObjectRelationship relationship)
		{
			s_inverseRelationshipsLock.EnterWriteLock();
			try
			{
				if (!s_inverseRelationships.TryGetValue(ownerUniqueId, out List<ObjectRelationship> relationships))
				{
					relationships = new List<ObjectRelationship>();
					s_inverseRelationships.Add(ownerUniqueId, relationships);
				}
				relationships.Add(relationship);
			}
			finally
			{
				s_inverseRelationshipsLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Gets all relationships of the object with the specified id.
		/// </summary>
		public static List<ObjectRelationship> GetRelationships(string uniqueId)
		{
			List<ObjectRelationship> relationships = new List<ObjectRelationship>();

			bool foundBuffer;
			List<ObjectRelationship> relationshipsBuffer;

			s_relationshipsLock.EnterReadLock();
			try
			{
				foundBuffer = s_relationships.TryGetValue(uniqueId, out relationshipsBuffer);
			}
			finally
			{
				s_relationshipsLock.ExitReadLock();
			}
			if (foundBuffer)
			{
				relationships.AddRange(relationshipsBuffer);
			}

			s_inverseRelationshipsLock.EnterReadLock();
			try
			{
				foundBuffer = s_inverseRelationships.TryGetValue(uniqueId, out relationshipsBuffer);
			}
			finally
			{
				s_inverseRelationshipsLock.ExitReadLock();
			}
			if (foundBuffer)
			{
				relationships.AddRange(relationshipsBuffer);
			}

			return relationships;
		}

		#endregion

		#region Folder Mapping

		/// <summary>
		/// Gets the unique ID of the folder object with the specified path.
		/// </summary>
		public static string GetFolderUniqueId(string absolutePath)
		{
			s_folderMappingsLock.EnterReadLock();
			try
			{
				if (s_folderMappings.TryGetValue(absolutePath, out string uniqueId))
				{
					return uniqueId;
				}
				else
				{
					return absolutePath;
				}
			}
			finally
			{
				s_folderMappingsLock.ExitReadLock();
			}
		}

		/// <summary>
		/// Gets the unique ID of the folder object with the specified path.
		/// </summary>
		public static bool TryGetFolderUniqueId(string absolutePath, out string uniqueId)
		{
			s_folderMappingsLock.EnterReadLock();
			try
			{
				return s_folderMappings.TryGetValue(absolutePath, out uniqueId);
			}
			finally
			{
				s_folderMappingsLock.ExitReadLock();
			}
		}

		/// <summary>
		/// Maps the specified folder path to the object with the specified uniqueId.
		/// </summary>
		public static void MapFolderObject(string absolutePath, string uniqueId)
		{
			s_folderMappingsLock.EnterWriteLock();
			try
			{
				s_folderMappings[absolutePath] = uniqueId;
			}
			finally
			{
				s_folderMappingsLock.ExitWriteLock();
			}
		}

		#endregion

		/// <summary>
		/// Clears all objects from the database.
		/// </summary>
		public static void Clear()
		{
			s_objectsLock.EnterWriteLock();
			try
			{
				s_objects.Clear();
			}
			finally
			{
				s_objectsLock.ExitWriteLock();
			}

			s_folderMappingsLock.EnterWriteLock();
			try
			{
				s_folderMappings.Clear();
			}
			finally
			{
				s_folderMappingsLock.ExitWriteLock();
			}

			s_relationshipsLock.EnterWriteLock();
			try
			{
				s_relationships.Clear();
			}
			finally
			{
				s_relationshipsLock.ExitWriteLock();
			}

			s_inverseRelationshipsLock.EnterWriteLock();
			try
			{
				s_inverseRelationships.Clear();
			}
			finally
			{
				s_inverseRelationshipsLock.ExitWriteLock();
			}

			Cleared?.Invoke(null, null);
		}
	}
}
