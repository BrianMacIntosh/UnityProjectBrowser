﻿// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;

namespace ProjectBrowser
{
	/// <summary>
	/// Represents a relationship from one object to another.
	/// </summary>
	public struct ObjectRelationship : IEquatable<ObjectRelationship>
	{
		/// <summary>
		/// The other object in the relationship.
		/// </summary>
		public readonly string TargetObjectId;

		/// <summary>
		/// The type of relationship.
		/// </summary>
		public readonly string RelationshipType;

		public ObjectRelationship(string targetObjectId, string relationshipType)
		{
			if (targetObjectId == null)
			{
				throw new ArgumentNullException("targetObjectId");
			}
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			TargetObjectId = targetObjectId;
			RelationshipType = relationshipType;
		}

		public override bool Equals(object obj)
		{
			return obj is ObjectRelationship && Equals((ObjectRelationship)obj);
		}

		public override int GetHashCode()
		{
			return TargetObjectId.GetHashCode() + 13 * RelationshipType.GetHashCode();
		}

		public bool Equals(ObjectRelationship other)
		{
			return other.TargetObjectId.Equals(TargetObjectId) && other.RelationshipType.Equals(RelationshipType);
		}

		public static bool operator ==(ObjectRelationship a, ObjectRelationship b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(ObjectRelationship a, ObjectRelationship b)
		{
			return !a.Equals(b);
		}
	}
}
