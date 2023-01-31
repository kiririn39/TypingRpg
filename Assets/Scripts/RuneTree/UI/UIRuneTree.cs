using Common;
using DefaultNamespace.BattleActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random=UnityEngine.Random;

namespace Assets.Scripts.SkillTree
{
    public class UIRuneTree : MonoBehaviour
    {
        public RuneTree RuneTree = new RuneTree();

        [SerializeField] private UIRuneNode goUIRuneNode = null;
        [SerializeField] private GameObject goRowForNodes = null;
        [SerializeField] private Transform trRootForRows = null;
        [SerializeField] private List<UIRuneNode> PoolRuneTreeNodes = new List<UIRuneNode>();
        [SerializeField] private Button btnSelectRandomBranch = null;

        private Dictionary<RuneNodeData, UIRuneNode> DataAndUIPairs = new Dictionary<RuneNodeData, UIRuneNode>();


        public void init()
        {
            btnSelectRandomBranch.onClick.AddListener( () => selectRandomBranch() );
            
            RuneTree.OnNewSkillsAdded += newSkills =>
            {
                trySelectSequence(ArraySegment<RuneKey>.Empty);
                StartCoroutine(updateRuneTreeUI());
            };
            RuneTree.addSequences(mockSentences);
        }

        private void unselectAllRunes()
        {
            unselectRecursively(RuneTree.tree.Root);

            void unselectRecursively(INode<RuneNodeData> node)
            {
                DataAndUIPairs.safeGet(node.Data)?.setRuneSelected( false );
                node.DirectChildren.Nodes.forEach(unselectRecursively);
            }
        }

        public void selectRandomBranch(int maxDepth = 999)
        {
            unselectAllRunes();

            maxDepth = Random.Range(1, 6);
            var curNode = RuneTree.tree.Root.DirectChildren.Nodes.ToList().randomElement();
            int curDepth = 1;
            Debug.Log($"maxDepth is {maxDepth}");
            while (curNode != null && curDepth <= maxDepth)
            {
                DataAndUIPairs.safeGet(curNode.Data)?.setRuneSelected(true);
                Debug.Log($"CurData: {curNode.Data}, UI is null: {DataAndUIPairs.safeGet(curNode.Data) is null}");

                curNode = curNode.DirectChildren.Nodes.ToList().randomElement();
                curDepth++;
            }
        }

        public RuneBattleActionInfo trySelectSequence( IEnumerable<RuneKey> sequence )
        {
            unselectAllRunes();

            if (sequence == null || !sequence.Any())
                return null;

            List<RuneKey> sequenceList = sequence?.ToList();
            bool isValid = RuneTree.isSequenceValid(sequenceList, out RuneBattleActionInfo runeBattleActionInfo);
            if (!isValid)
            {
                Debug.LogError($"Trying to select invalid sequence in {nameof(UIRuneTree)}");
                return null;
            }

            INode<RuneNodeData> curNode = RuneTree.tree.Root;
            for (int i = 0; i < sequenceList.Count; i++)
            {
                curNode = curNode.DirectChildren.Nodes.FirstOrDefault(it => it.Data?.runeKey == sequenceList[i]);
                DataAndUIPairs.safeGet(curNode?.Data)?.setRuneSelected(true);
            }

            //StartCoroutine(updateRuneTreeUI());
            return runeBattleActionInfo;
        }

        private IEnumerator updateRuneTreeUI()
        {
            int nodeGlobalIndex = 0;
            DataAndUIPairs.Clear();;
            while (trRootForRows.childCount > 0)
            {
                Destroy(trRootForRows.GetChild(0));
                yield return null;
            }

            IEnumerable<(UIRuneNode parentUI, INode<RuneNodeData> node)> curRowToDraw = RuneTree.tree.Root.DirectChildren.Nodes.Select(x => (null as UIRuneNode, x) ).ToList();
            do
            {
                List<(UIRuneNode parentUI, INode<RuneNodeData> node)> nextRowToDraw = new List<(UIRuneNode parentUI, INode<RuneNodeData> node)>();
                Transform uiRow = Instantiate(goRowForNodes, trRootForRows).GetComponent<Transform>();
                foreach ((UIRuneNode parentUI, INode<RuneNodeData> node) in curRowToDraw)
                {
                    UIRuneNode nodeUI = drawNode(uiRow, parentUI, node.Data);
                    DataAndUIPairs[node.Data] = nodeUI;
                    nextRowToDraw.AddRange(node.DirectChildren.Nodes.Select(y => (nodeUI, y)));
                }

                curRowToDraw = nextRowToDraw;
            } while (curRowToDraw.Any());

            yield return new WaitForEndOfFrame();
            foreach (UIRuneNode uiRuneNode in PoolRuneTreeNodes)
                uiRuneNode.init();

            UIRuneNode getOrCreateRuneTreeNodeUI( Transform spawnRoot )
            {
                if (PoolRuneTreeNodes.Count > nodeGlobalIndex)
                    return PoolRuneTreeNodes[nodeGlobalIndex];

                var newNodeUI = Instantiate(goUIRuneNode, spawnRoot).GetComponent<UIRuneNode>();
                PoolRuneTreeNodes.Add(newNodeUI);
                return newNodeUI;
            }

            UIRuneNode drawNode(Transform spawnRoot, UIRuneNode parent, RuneNodeData data)
            {
                UIRuneNode curNodeUI = getOrCreateRuneTreeNodeUI(spawnRoot);
                curNodeUI.preinit(parent?.transform, data);
                    
                nodeGlobalIndex++;
                return curNodeUI;
            }
        }

        

        
        

        List<RuneSequenceForBattleAction> mockSentences = new List<RuneSequenceForBattleAction>()
        {
            new RuneSequenceForBattleAction()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.Q
                    , RuneKey.W
                    , RuneKey.E
                }
                , RuneBattleActionInfo = new RuneBattleActionInfo() {name = "first", battleActionBase = new IdleAction()}
            },
            new RuneSequenceForBattleAction()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.I
                    , RuneKey.O
                    , RuneKey.P
                }
                , RuneBattleActionInfo = new RuneBattleActionInfo() {name = "second", battleActionBase = new DefenceAction()}
            },
            new RuneSequenceForBattleAction()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.Q
                    , RuneKey.W
                    , RuneKey.E

                    , RuneKey.I
                    , RuneKey.O
                    , RuneKey.P
                }
                , RuneBattleActionInfo = new RuneBattleActionInfo() {name = "third", battleActionBase = new AttackAction()}
            },
            new RuneSequenceForBattleAction()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.Q
                    , RuneKey.Q
                    , RuneKey.Q
                }
                , RuneBattleActionInfo = new RuneBattleActionInfo() {name = "fifth", battleActionBase = new AttackAction()}
            }
        };
    }
}
