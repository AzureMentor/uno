﻿using System;
using System.Diagnostics;

namespace Windows.UI.Xaml.Media
{
	public partial class FontFamily
	{
		private readonly int _hashCode;

		public FontFamily(string familyName)
		{
			Source = familyName;

#if __WASM__
			ParsedSource = ParseFontFamilySource(familyName);
#endif

			// This instance is immutable, we can cache the hash code.
			_hashCode = Source.GetHashCode();
		}

		public string Source { get; }

		// Makes introduction of FontFamily a non-breaking change (for now)
		public static implicit operator FontFamily(string familyName) => new FontFamily(familyName);

		public static FontFamily Default { get; } = new FontFamily("Segoe UI");

		public override bool Equals(object obj)
		{
			if (obj is FontFamily fontFamily)
			{
				return Source.Equals(fontFamily.Source, StringComparison.Ordinal);
			}

			return false;
		}

		public override int GetHashCode() => _hashCode;

		public static bool operator ==(FontFamily a, FontFamily b)
		{
			if (ReferenceEquals(a, b))
			{
				return true;
			}

			return !ReferenceEquals(a, null) && a.Equals(b);
		}

		public static bool operator !=(FontFamily a, FontFamily b)
		{
			if (ReferenceEquals(a, b))
			{
				return false;
			}

			return ReferenceEquals(a, null) || !a.Equals(b);
		}

#if __WASM__
		/// <summary>
		/// Contains the parsed font family for use in WASM
		/// (matches CSS @font-face's font-family)
		/// </summary>
		internal string ParsedSource { get; }

		private string ParseFontFamilySource(string familyName)
		{
			const string ForwardSlash = "/";
			const string Hash = "#";
			const string Dot = ".";
			if (string.IsNullOrEmpty(familyName))
			{
				throw new ArgumentException("Font family name must not be empty string nor null", nameof(familyName));
			}
			//check if family name is a pure name or a path
			if (familyName.Contains(ForwardSlash) || familyName.Contains(Hash))
			{
				//we have a path to font family name, parse just the name itself
				//there are two possible formats:
				//1) "some/path/to/font/MyNiceFont.ttf#My Nice Font" (actually works even with pure "MyNiceFont.ttf#My Font")
				//   -> we extract the part after #

				var hashFontNameStart = familyName.LastIndexOf(Hash);
				if (hashFontNameStart != -1)
				{
					return familyName.Substring(hashFontNameStart + 1);
				}

				//or 
				//2) "some/path/to/font/MyNiceFont.ttf"
				//   -> we fall back to the font file name

				var slashFontNameStart = familyName.LastIndexOf(ForwardSlash) + 1; //works even if slash is not present at all -> 0				
				var extensionStart = familyName.LastIndexOf(Dot);
				if (extensionStart < slashFontNameStart) //no dot after slash
				{
					extensionStart = familyName.Length;
				}
				return familyName.Substring(slashFontNameStart, extensionStart - slashFontNameStart);
			}
			return familyName;
		}
#endif
	}
}
