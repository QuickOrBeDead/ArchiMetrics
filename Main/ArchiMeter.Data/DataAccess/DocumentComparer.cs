// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentComparer.cs" company="Roche">
//   Copyright � Roche 2012
//   This source is subject to the Microsoft Public License (Ms-PL).
//   Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
//   All other rights reserved.
// </copyright>
// <summary>
//   Defines the DocumentComparer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ArchiMeter.Data.DataAccess
{
	using System.Collections.Generic;
	using Roslyn.Services;

	internal class DocumentComparer : IEqualityComparer<IDocument>
	{
		public bool Equals(IDocument x, IDocument y)
		{
			return x == null
				       ? y == null
				       : y != null && x.FilePath == y.FilePath;
		}

		public int GetHashCode(IDocument obj)
		{
			return obj.FilePath.GetHashCode();
		}
	}
}