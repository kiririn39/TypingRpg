using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.SkillTree
{
    public class RuneTree
    {
        public ITree<RuneNodeData> tree = NodeTree<RuneNodeData>.NewTree();


        public void addSentences( IEnumerable<SkillRuneSentence> sentences )
        {
            if (!sentences.Any())
                return;

            foreach (SkillRuneSentence sentence in sentences)
                addSentence(sentence);
        }

        public void addSentence(SkillRuneSentence sentence)
        {
            if (sentence.RuneKeys.Count == 0)
                throw new Exception("Chel...,there is no keys");
            
            if (sentence.RuneSkillInfo == null)
                throw new ArgumentNullException("Chel..., skill is null");


            INode<RuneNodeData> curNode = tree.Root;
            for (int i = 0; i < sentence.RuneKeys.Count; i++)
            {
                RuneKey runeKey = sentence.RuneKeys[i];
                INode<RuneNodeData> nextNode = curNode.DirectChildren.Nodes.FirstOrDefault(it => it.Data.runeKey == runeKey);
                if (nextNode == null)
                    curNode = curNode.AddChild(new RuneNodeData() {runeKey = runeKey, runeSkillInfo = sentence.RuneSkillInfo});

                if (i == sentence.RuneKeys.Count - 1)
                {
                    if (curNode.Data.runeSkillInfo != null)
                        throw new Exception("Chel, wtf, tut vje e skill");

                    curNode.Data.runeSkillInfo = sentence.RuneSkillInfo;
                }
            }
        }
    }
}
