﻿using System.Collections.Immutable;
using UnityEngine;

namespace LearningEngine
{
    class Tests
    {
        public static void EnglishTest()
        {
            // Categories
            var sentCat = CategoryLabel.Create(NodeType.NonTerminal);
            var npCat = CategoryLabel.Create(NodeType.NonTerminal);
            var vpCat = CategoryLabel.Create(NodeType.NonTerminal);
            var detCat = CategoryLabel.Create(NodeType.Terminal);
            var nCat = CategoryLabel.Create(NodeType.Terminal);
            var vCat = CategoryLabel.Create(NodeType.Terminal);

            // Terminals 
            var theNode = new TermNode(detCat, "the", "");
            var aNode = new TermNode(detCat, "a", "");
            var catNode = new TermNode(nCat, "cat", "");
            var dogNode = new TermNode(nCat, "dog", "");
            var girlNode = new TermNode(nCat, "girl", "");
            var seesNode = new TermNode(vCat, "sees", "");
            var killsNode = new TermNode(vCat, "kills", "");

            // Try parsing a sentence given a set of rules
            var rules = RuleSet.CreateEmpty()
                .AddRule(NonTermRule.CreateBinary(sentCat, npCat, vpCat, FunctorLoc.Right))
                .AddRule(NonTermRule.CreateBinary(npCat, detCat, nCat, FunctorLoc.Left))
                .AddRule(NonTermRule.CreateBinary(vpCat, vCat, npCat, FunctorLoc.Left))
                .AddRule(TermRule.CreateEmpty(detCat)
                    .AddToRight(theNode)
                    .AddToRight(aNode))
                .AddRule(TermRule.CreateEmpty(nCat)
                    .AddToRight(catNode)
                    .AddToRight(dogNode)
                    .AddToRight(girlNode))
                .AddRule(TermRule.CreateEmpty(vCat)
                    .AddToRight(seesNode)
                    .AddToRight(killsNode));

            var sentRule = rules.FindWithLeftSide(sentCat);
            var sampleInput2 = "the cat sees the cat".Split().ToImmutableList();
            var parsedInput2 = sentRule.Parse(sampleInput2, rules);

            if (parsedInput2.Success)
            {
                Debug.Log(parsedInput2.Tree.GetXMLString());
            }
            else
            {
                Debug.Log("Parsing failure!");
            }

            Debug.Log("");

            // Write all possible outputs
            foreach (var sent in sentRule.GenerateAll(rules))
            {
                Debug.Log(sent.GetXMLString());
            }
        }

        public static void AlienTest()
        {
            // Initialize agents
            var parent0 = AlienLanguage.MakeParentAgent();
            var child0 = ChildAgent.Initialize();

            // Parent says something
            var parent1 = parent0.SaySomething();

            // Child learns, says something back
            var child1 = child0.Learn(parent1.CurrentSentence);
            child1.SaySomething();
        }

        public static void LambdaTest()
        {
            var lambda = new SemValue("/X[/Y[PA(X,Y)]]");
            var applied = lambda.LambdaApply(new SemValue("BA(DU)"));
            Debug.Log(applied.Value);
        }
    }
}
