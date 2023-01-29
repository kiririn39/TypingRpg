using DefaultNamespace.BattleActions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SkillTree
{
    public class RuneTreeController : MonoBehaviour
    {
        public RuneTree RuneTree = new RuneTree();

        private void Awake()
        {
            RuneTree.addSentences(mockSentences);
        }
        

        List<SkillRuneSentence> mockSentences = new List<SkillRuneSentence>()
        {
            new SkillRuneSentence()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.Q
                    , RuneKey.W
                    , RuneKey.E
                }
                , RuneSkillInfo = new RuneSkillInfo() {name = "first", battleActionBase = new AttackAction()}
            },
            new SkillRuneSentence()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.I
                    , RuneKey.O
                    , RuneKey.P
                }
                , RuneSkillInfo = new RuneSkillInfo() {name = "second", battleActionBase = new AttackAction()}
            },
            new SkillRuneSentence()
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
                , RuneSkillInfo = new RuneSkillInfo() {name = "third", battleActionBase = new AttackAction()}
            },
            new SkillRuneSentence()
            {
                RuneKeys = new List<RuneKey>()
                {
                    RuneKey.Q
                    , RuneKey.Q
                    , RuneKey.Q
                }
                , RuneSkillInfo = new RuneSkillInfo() {name = "fifth", battleActionBase = new AttackAction()}
            }
        };
    }
}
