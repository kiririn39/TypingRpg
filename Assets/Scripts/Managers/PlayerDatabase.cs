using Assets.Scripts.SkillTree;
using BattleSystem.BattleActions;
using Common;
using DefaultNamespace.BattleActions;
using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Managers
{
    public class PlayerDatabase : MonoBehaviour
    {
        #region Skills
        public event Action<RuneSequenceForBattleAction> OnNewSkillAdded = delegate {};


        public static readonly RuneSequenceForBattleAction idleSkill       = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.Q, RuneKey.W, RuneKey.E},                                 RuneBattleActionInfo = new RuneBattleActionInfo(new IdleAction()) };
        public static readonly RuneSequenceForBattleAction attackSkill     = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.I, RuneKey.O, RuneKey.P},                                 RuneBattleActionInfo = new RuneBattleActionInfo(new AttackAction()) };
        public static readonly RuneSequenceForBattleAction defenceSkill    = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.Q, RuneKey.W, RuneKey.E,RuneKey.I, RuneKey.O, RuneKey.P}, RuneBattleActionInfo = new RuneBattleActionInfo(new DefencePrepareAction()) };
        public static readonly RuneSequenceForBattleAction magicFireSkill  = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.I, RuneKey.O, RuneKey.P,RuneKey.Q, RuneKey.W, RuneKey.E}, RuneBattleActionInfo = new RuneBattleActionInfo(new MagicFireAction()) };
        public static readonly RuneSequenceForBattleAction magicFrostSkill = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.E, RuneKey.W, RuneKey.Q,RuneKey.I, RuneKey.I},            RuneBattleActionInfo = new RuneBattleActionInfo(new MagicFrostAction()) };
        public static readonly RuneSequenceForBattleAction psyonicSkill    = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.Q, RuneKey.Q, RuneKey.I,RuneKey.P},                       RuneBattleActionInfo = new RuneBattleActionInfo(new PsyonicAction()) };
        public static readonly RuneSequenceForBattleAction poisonSkill     = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.Q, RuneKey.Q, RuneKey.W,RuneKey.O, RuneKey.Q, RuneKey.O}, RuneBattleActionInfo = new RuneBattleActionInfo(new PoisonPrepareAction()) };
        public static readonly RuneSequenceForBattleAction healSkill       = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.Q, RuneKey.Q, RuneKey.I,RuneKey.E, RuneKey.E, RuneKey.P, RuneKey.Q}, RuneBattleActionInfo = new RuneBattleActionInfo(new HealPrepareAction()) };
        public static readonly RuneSequenceForBattleAction evasionSkill    = new RuneSequenceForBattleAction() {RuneKeys = new List<RuneKey>() {RuneKey.I, RuneKey.O, RuneKey.E,RuneKey.E, RuneKey.O, RuneKey.Q}, RuneBattleActionInfo = new RuneBattleActionInfo(new EvasionPrepareAction()) };

        public IReadOnlyList<RuneSequenceForBattleAction> myCurrentSkillSentences
        {
            get
            {
                _myCurrentSkillSentences ??= defaultSkillSentences.ToList();
                return _myCurrentSkillSentences;
            }
        }

        private List<RuneSequenceForBattleAction> _myCurrentSkillSentences = null;

        public List<RuneSequenceForBattleAction> availableSkillsForNextLvl => newSkillsPerLvl.safeGet(playerLvl).ToList();


        private List<RuneSequenceForBattleAction> defaultSkillSentences = new List<RuneSequenceForBattleAction>()
        {
            attackSkill,
            defenceSkill,
            poisonSkill,
            healSkill,
            evasionSkill
        };

        private IReadOnlyList<IReadOnlyList<RuneSequenceForBattleAction>> newSkillsPerLvl = new List<List<RuneSequenceForBattleAction>>()
        {
            new []{magicFireSkill,magicFrostSkill}.ToList(),
            new []{psyonicSkill}.ToList(),
        };


        public void addNewSkill(RuneSequenceForBattleAction newSkill)
        {
            _myCurrentSkillSentences ??= defaultSkillSentences.ToList();
            _myCurrentSkillSentences.Add(newSkill);
            OnNewSkillAdded(newSkill);

            playerLvl++;
        }
        #endregion

        public int playerLvl { get; set; } = 0;

        #region Singleton
        public static PlayerDatabase Instance { get; private set; }

        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else
            {
                Instance = this; 
            } 
        }
        #endregion
    }
}
