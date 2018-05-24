using System;

namespace ProjectBrowser
{
	/// <summary>
	/// Unique ID representing an object in a Unity scene.
	/// </summary>
	public struct UnityObjectKey : IEquatable<UnityObjectKey>
	{
		/// <summary>
		/// The <see cref="Guid"/> of the asset the object belongs to.
		/// </summary>
		public readonly Guid AssetGuid;

		/// <summary>
		/// The fileId of the object.
		/// </summary>
		public readonly long FileId;

		private static readonly char[] s_splitter = new char[] { '/' };

		/// <summary>
		/// Returns true if this reference is empty.
		/// </summary>
		public bool IsEmpty
		{
			get { return FileId == 0 && AssetGuid == Guid.Empty; }
		}

		public UnityObjectKey(Guid assetGuid, long fileId)
		{
			AssetGuid = assetGuid;
			FileId = fileId;
		}

		public UnityObjectKey(string key)
		{
			string[] split = key.Split(s_splitter, 2);
			try
			{
				AssetGuid = new Guid(split[0]);
			}
			catch (FormatException)
			{
				AssetGuid = Guid.Empty;
			}
			if (split.Length > 1)
			{
				FileId = long.Parse(split[1]);
			}
			else
			{
				FileId = 0;
			}
		}

		public override bool Equals(object obj)
		{
			return obj is UnityObjectKey && Equals((UnityObjectKey)obj);
		}

		public override int GetHashCode()
		{
			return AssetGuid.GetHashCode() + 13 * FileId.GetHashCode();
		}

		public bool Equals(UnityObjectKey other)
		{
			return other.AssetGuid == AssetGuid && other.FileId == FileId;
		}

		public static bool operator ==(UnityObjectKey a, UnityObjectKey b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(UnityObjectKey a, UnityObjectKey b)
		{
			return !a.Equals(b);
		}

		public override string ToString()
		{
			return AssetGuid.ToString() + "/" + FileId.ToString();
		}

		//HACK:
		public static implicit operator string(UnityObjectKey key)
		{
			return key.ToString();
		}
	}
}
