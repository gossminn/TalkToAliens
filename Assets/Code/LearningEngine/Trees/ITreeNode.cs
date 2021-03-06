﻿using Code.LearningEngine.Semantics.Model;
using Code.LearningEngine.Semantics.Types;
using Code.LearningEngine.Syntax;

namespace Code.LearningEngine.Trees
{
    internal interface ITreeNode
    {
        // Syntax/semantics
        CategoryLabel Category { get; }
        ISemanticValue GetSemantics(LogicalModel model);

        // Get truth value (false if not of type <t>)
        bool GetTruthValue();

        // String methods
        string GetXmlString();
        string GetFlatString();
    }
}