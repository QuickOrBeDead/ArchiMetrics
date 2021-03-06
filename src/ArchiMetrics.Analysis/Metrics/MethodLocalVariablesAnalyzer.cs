// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodLocalVariablesAnalyzer.cs" company="Reimers.dk">
//   Copyright � Matthias Friedrich, Reimers.dk 2014
//   This source is subject to the MIT License.
//   Please see https://opensource.org/licenses/MIT for details.
//   All other rights reserved.
// </copyright>
// <summary>
//   Defines the MethodLocalVariablesAnalyzer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ArchiMetrics.Analysis.Metrics
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal sealed class MethodLocalVariablesAnalyzer : CSharpSyntaxWalker
    {
        private int _numLocalVariables;

        public MethodLocalVariablesAnalyzer()
            : base(SyntaxWalkerDepth.Node)
        {
        }

        public int Calculate(SyntaxNode node)
        {
            if (node != null)
            {
                Visit(node);
            }

            return _numLocalVariables;
        }

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            base.VisitVariableDeclaration(node);
            _numLocalVariables++;
        }
    }
}
