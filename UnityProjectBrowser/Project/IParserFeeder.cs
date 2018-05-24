// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;

namespace ProjectBrowser
{
	public interface IParserFeeder : IDisposable
	{
		/// <summary>
		/// Returns the type of parser this object feeds.
		/// </summary>
		Type GetParserType();

		/// <summary>
		/// Stops the feeder.
		/// </summary>
		void Stop();
	}
}
