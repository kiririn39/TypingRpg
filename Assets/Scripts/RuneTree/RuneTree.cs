using Common;
using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.SkillTree
{
    public class RuneTree
    {
        public event Action<List<RuneSequenceForBattleAction>> OnNewSkillsAdded = delegate{ };

        public ITree<RuneNodeData> tree = NodeTree<RuneNodeData>.NewTree();


        public void clear()
        {
            tree.Clear();
        }

        public void addSequences( IEnumerable<RuneSequenceForBattleAction> sequences )
        {
            if (!sequences.Any())
                return;

            sequences = sequences.ToList();
            foreach (RuneSequenceForBattleAction sentence in sequences)
                addSequenceWithoutNotify(sentence);

            OnNewSkillsAdded(sequences.ToList() );
        }

        public void addSequence(RuneSequenceForBattleAction sequence)
        {
            addSequenceWithoutNotify(sequence);

            OnNewSkillsAdded(new List<RuneSequenceForBattleAction>() {sequence});
        }

        public void addSequenceWithoutNotify(RuneSequenceForBattleAction sequence)
        {
            if (sequence.RuneKeys.Count == 0)
                throw new Exception("Chel...,there is no keys");
            
            if (sequence.RuneBattleActionInfo == null)
                throw new ArgumentNullException("Chel..., skill is null");

            INode<RuneNodeData> curNode = tree.Root;
            for (int i = 0; i < sequence.RuneKeys.Count; i++)
            {
                RuneKey runeKey = sequence.RuneKeys[i];
                INode<RuneNodeData> nextNode = curNode.DirectChildren.Nodes.FirstOrDefault(it => it.Data.runeKey == runeKey);
                curNode = nextNode ?? curNode.AddChild(new RuneNodeData() {runeKey = runeKey});

                if (i < sequence.RuneKeys.Count - 1)
                    continue;

                if (curNode.Data.RuneBattleActionInfo != null)
                    throw new Exception("Chel, wtf, tut vje e skill");

                curNode.Data.RuneBattleActionInfo = sequence.RuneBattleActionInfo;
            }
        }

        public bool isSequenceValid(IEnumerable<RuneKey> runeKeys, out RuneBattleActionInfo runeBattleActionInfo)
        {
            runeBattleActionInfo = null;
            if (runeKeys == null || !runeKeys.Any())
                return false;

            List<RuneKey> runeKeysList = runeKeys.ToList();
            INode<RuneNodeData> curNode = tree.Root;
            for (int i = 0; i < runeKeysList.Count; i++)
            {
                curNode = curNode.DirectChildren.Nodes.FirstOrDefault(it => it.Data?.runeKey == runeKeysList[i]);
                if (curNode == null)
                    return false;
            }

            runeBattleActionInfo = curNode.Data.RuneBattleActionInfo;
            return true;
        }
        
        /// <returns>null if sequence invalid and empty array when it is end of branch</returns>
        public IEnumerable<RuneKey> getNextValidRuneKeys(IEnumerable<RuneKey> runeKeys)
        {
            List<RuneKey> runeKeysList = runeKeys.ToList();
            INode<RuneNodeData> curNode = tree.Root;
            for (int i = 0; i < runeKeysList.Count; i++)
            {
                curNode = curNode.DirectChildren.Nodes.FirstOrDefault(it => it.Data?.runeKey == runeKeysList[i]);
                if (curNode == null)
                    return null;
            }

            return curNode.DirectChildren.Values.Select(it => it.runeKey).ToList();
        }
    }
}
