using System.IO;

namespace ProjectBrowser
{
	public class UnityAsset : ProjectObject
	{
		private readonly string m_path;

		private readonly string m_filename;

		private readonly string m_extension;

		public UnityAsset(string path, string uniqueId)
			: base(uniqueId)
		{
			m_path = path;
			m_filename = Path.GetFileNameWithoutExtension(m_path);
			m_extension = Path.GetExtension(m_path);
		}

		public override string ToString()
		{
			return m_filename;
		}

		/// <summary>
		/// Gets the absolute path of the file this object belongs to.
		/// </summary>
		public override string GetFilePath()
		{
			return m_path;
		}

		public override string GetIconKey()
		{
			if (string.Compare(m_extension, ".unity", true) == 0)
			{
				return "SceneAsset_Icon";
			}
			else if (string.Compare(m_extension, ".prefab", true) == 0)
			{
				return "PrefabNormal_Icon";
			}
			else if (string.Compare(m_extension, ".asset", true) == 0)
			{
				return "ScriptableObject_Icon";
			}
			else if (string.Compare(m_extension, ".anim", true) == 0)
			{
				return "AnimationClip_Icon";
			}
			else if (string.Compare(m_extension, ".controller", true) == 0)
			{
				return "AnimatorController_Icon";
			}
			else if (string.Compare(m_extension, ".overridecontroller", true) == 0)
			{
				return "AnimatorOverrideController_Icon";
			}
			else
			{
				return "DefaultAsset_Icon";
			}
		}
	}
}
