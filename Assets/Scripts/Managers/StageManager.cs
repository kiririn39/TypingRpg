using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using Map;
using RuneStack;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class StageManager : MonoBehaviour
    {
        public static StageManager Instance { get; private set; }


        [SerializeField] private Transform   enemyTransform;
        [SerializeField] private List<Stage> stages = new List<Stage>();

        private GameBattleSystem gameBattleSystem;

        private int monstersKilled;

        private bool isSubscribed;

        public int curStageIndex { get; private set; } = -1;

        public event Action stageChangeStarted;
        public event Action stageMovementStarted;
        public event Action stageChangeFinished;
        public event Action gameReset;

        public Stage curStage => stages.FirstOrDefault(x => x.id == curStageIndex) ?? null;


        public void prepareGame()
        {
            curStageIndex = -1;
            monstersKilled = 0;
            MapManager.Instance.spawnMap(stages);
            gameBattleSystem = FindObjectOfType<GameBattleSystem>();
            gameBattleSystem.resetGame();
            if (!isSubscribed)
            {
                gameBattleSystem.OnBattleEnded += onBattleEnded;
                isSubscribed = true;
            }
            gameReset?.Invoke();
        }

        public void startGame()
        {
            nextStage();
        }

        public void nextStage()
        {
            if (curStageIndex >= 0 && curStage.type == StageType.FIGHT)
                ++monstersKilled;

            ++curStageIndex;

            if (curStageIndex >= stages.Count)
            {
                FindObjectOfType<WinScreen>().show(monstersKilled);
                return;
            }

            if (curStage.type == StageType.FIGHT)
                gameBattleSystem.PrepareBattle(curStage.enemyConfig);

            stageChangeStarted?.Invoke();
            FindObjectOfType<UIRuneStack>()?.clearSelected();
        }

        public void invokeStageMovementStarted()
        {
            stageMovementStarted?.Invoke();
        }

        public void invokeStageChangeFinished()
        {
            stageChangeFinished?.Invoke();

            if (curStage.type == StageType.TOWN)
            {
                UIPanelRunes ui_panel_runes = FindObjectOfType<UIPanelRunes>();
                ui_panel_runes.openNewSkillSelectorPanel();
                ui_panel_runes.uiSkillSelector.OnSkillSelected += _ => nextStage();
            }
            else
            {
                gameBattleSystem.StartBattle();
            }
        }
        private void onBattleEnded(BattleResult battle_result)
        {
            switch (battle_result)
            {
                case PlayerVictoryResult:
                    nextStage();
                    break;
                case PlayerLostResult:
                    FindObjectOfType<LoseScreen>().show(monstersKilled);
                    break;
            }
        }

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

        private void Start()
        {
        }
    }
}