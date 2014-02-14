// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectMetricsCalculator.cs" company="Reimers.dk">
//   Copyright � Reimers.dk 2013
//   This source is subject to the Microsoft Public License (Ms-PL).
//   Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
//   All other rights reserved.
// </copyright>
// <summary>
//   Defines the IProjectMetricsCalculator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ArchiMetrics.Common.Metrics
{
	using System.Threading.Tasks;
	using Roslyn.Services;

	public interface IProjectMetricsCalculator
	{
		Task<IProjectMetric> Calculate(IProject project, ISolution solution);
	}
}