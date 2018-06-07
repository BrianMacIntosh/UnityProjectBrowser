// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;

namespace UnityProjectBrowser
{
	public static class TimeSpanUtility
	{
		/// <summary>
		/// Returns the greater of the two <see cref="TimeSpan"/>s.
		/// </summary>
		public static TimeSpan Max(TimeSpan a, TimeSpan b)
		{
			return a > b ? a : b;
		}

		/// <summary>
		/// Returns the lesser of the two <see cref="TimeSpan"/>s.
		/// </summary>
		public static TimeSpan Min(TimeSpan a, TimeSpan b)
		{
			return a > b ? b : a;
		}
	}
}
