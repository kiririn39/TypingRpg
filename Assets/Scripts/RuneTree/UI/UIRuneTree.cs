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


        private void Awake()
        {
            btnSelectRandomBranch.onClick.AddListener( () => selectRandomBranch() );

            RuneTree.OnNewSkillsAdded += newSkills => StartCoroutine(updateRuneTreeUI());
            RuneTree.addSentences(mockSentences);
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

            maxDepth = Random.Range(1, 10);
            var curNode = RuneTree.tree.Root;
            int curDepth = 0;
            while (curNode != null && curDepth < maxDepth )
            {
                DataAndUIPairs.safeGet(curNode.Data)?.setRuneSelected(true);
                curNode = curNode.DirectChildren.Nodes.ToList().randomElement();
                curDepth++;
            }
        }

        private IEnumerator updateRuneTreeUI()
        {
            Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
            int nodeGlobalIndex = 0;
            DataAndUIPairs.Clear();;
            for (int i = 0; i < trRootForRows.childCount; i++)
                Destroy(trRootForRows.GetChild(i));

            
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

                yield return new WaitForSeconds(0.1f);
                curRowToDraw = nextRowToDraw;
            } while (curRowToDraw.Any());

            yield return new WaitForSeconds(1);
            foreach (UIRuneNode uiRuneNode in PoolRuneTreeNodes)
                uiRuneNode.reinit();

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
                curNodeUI.init(parent?.transform, data);
                    
                nodeGlobalIndex++;
                return curNodeUI;
            }
        }

        

        
        

        List<RuneSentenceForBattleAction> mockSentences = new List<RuneSentenceForBattleAction>()
        {
            new RuneSentenceForBattleAction()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.Q
                    , RuneKey.W
                    , RuneKey.E
                }
                , RuneBattleActionInfo = new RuneBattleActionInfo() {name = "first", battleActionBase = new AttackAction()}
            },
            new RuneSentenceForBattleAction()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.I
                    , RuneKey.O
                    , RuneKey.P
                }
                , RuneBattleActionInfo = new RuneBattleActionInfo() {name = "second", battleActionBase = new AttackAction()}
            },
            new RuneSentenceForBattleAction()
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
            new RuneSentenceForBattleAction()
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
