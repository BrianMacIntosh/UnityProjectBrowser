// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjectBrowser
{
	/// <summary>
	/// Base class for any object that exists in the project.
	/// </summary>
	public abstract class ProjectObject
	{
		/// <summary>
		/// The unique identifier for this object.
		/// </summary>
		public readonly string UniqueId;

		/// <summary>
		/// Returns true if this object should not appear in the hierarchy view.
		/// </summary>
		public virtual bool HideFromHierarchy
		{
			get { return false; }
		}

		/// <summary>
		/// If there was an error while parsing the object, the error.
		/// </summary>
		public Exception ParseError;
		
		public ProjectObject(string uniqueId)
		{
			UniqueId = uniqueId;
		}

		/// <summary>
		/// If this object has a parent, returns its key.
		/// </summary>
		public virtual string GetParentId()
		{
			return "";
		}

		/// <summary>
		/// Gets the key of the icon to use for this object.
		/// </summary>
		public abstract string GetIconKey();

		/// <summary>
		/// Gets the absolute path of the file this object belongs to.
		/// </summary>
		public abstract string GetFilePath();

		/// <summary>
		/// Adds a new relationship to this object.
		/// </summary>
		/// <param name="uniqueId">The id of the other object in the relationship.</param>
		/// <param name="relationshipType">String key of the outgoing type of the relationship.</param>
		/// <param name="inverseRelationshipType">
		/// String key of the inverse type of the relationship.
		/// Only use if the other object will NOT read this inverse relationship from its own data.
		/// </param>
		public void AddRelationship(string uniqueId, string relationshipType, string inverseRelationshipType)
		{
			ObjectDatabase.AddRelationship(UniqueId, new ObjectRelationship(uniqueId, relationshipType));
			if (!string.IsNullOrEmpty(inverseRelationshipType))
			{
				ObjectDatabase.AddInverseRelationship(uniqueId, new ObjectRelationship(UniqueId, inverseRelationshipType));
			}
		}

		/// <summary>
		/// Adds a new relationship to this object.
		/// </summary>
		public void AddRelationship(ObjectRelationship relationship)
		{
			ObjectDatabase.AddRelationship(UniqueId, relationship);
		}
	}
}
