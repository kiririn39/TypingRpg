using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.SkillTree
{
    public class RuneTree
    {
        public event Action<List<RuneSentenceForBattleAction>> OnNewSkillsAdded = delegate{ };

        public ITree<RuneNodeData> tree = NodeTree<RuneNodeData>.NewTree();


        public void clear()
        {
            tree.Clear();
        }

        public void addSentences( IEnumerable<RuneSentenceForBattleAction> sentences )
        {
            if (!sentences.Any())
                return;

            sentences = sentences.ToList();
            foreach (RuneSentenceForBattleAction sentence in sentences)
                addSentenceWithoutNotify(sentence);

            OnNewSkillsAdded(sentences.ToList() );
        }

        public void addSentenceWithoutNotify(RuneSentenceForBattleAction sentence)
        {
            if (sentence.RuneKeys.Count == 0)
                throw new Exception("Chel...,there is no keys");
            
            if (sentence.RuneBattleActionInfo == null)
                throw new ArgumentNullException("Chel..., skill is null");

            INode<RuneNodeData> curNode = tree.Root;
            for (int i = 0; i < sentence.RuneKeys.Count; i++)
            {
                RuneKey runeKey = sentence.RuneKeys[i];
                INode<RuneNodeData> nextNode = curNode.DirectChildren.Nodes.FirstOrDefault(it => it.Data.runeKey == runeKey);
                if (nextNode == null)
                    curNode = curNode.AddChild(new RuneNodeData() {runeKey = runeKey});
                else
                    curNode = nextNode;

                if (i == sentence.RuneKeys.Count - 1)
                {
                    if (curNode.Data.RuneBattleActionInfo != null)
                        throw new Exception("Chel, wtf, tut vje e skill");

                    curNode.Data.RuneBattleActionInfo = sentence.RuneBattleActionInfo;
                }
            }
        }

        private void addSentence(RuneSentenceForBattleAction sentence)
        {
            addSentenceWithoutNotify(sentence);

            OnNewSkillsAdded(new List<RuneSentenceForBattleAction>() {sentence});
        }
    }
}
