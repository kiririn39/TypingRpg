using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Map;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class StageManager : MonoBehaviour
    {
        public static StageManager Instance { get; private set; }


        private List<Stage> stages = new List<Stage>();

        private GameBattleSystem gameBattleSystem;

        private int monstersKilled;

        public int curStageIndex { get; private set; } = -1;

        public event Action stageChangeStarted;
        public event Action stageChangeFinished;

       public Stage curStage => stages.FirstOrDefault(x => x.id == curStageIndex) ?? throw new IndexOutOfRangeException($"No stage with {nameof(curStageIndex)} {curStageIndex}");


       public void prepareGame()
       {
           curStageIndex = -1;
           monstersKilled = 0;
           MapManager.Instance.spawnMap(stages);
       }

       public void startGame()
       {
           nextStage();
           gameBattleSystem = FindObjectOfType<GameBattleSystem>();
           gameBattleSystem.OnBattleEnded += onBattleEnded;
       }

        public void nextStage()
        {
            if (curStageIndex >=0 && curStage.type == StageType.FIGHT)
                ++monstersKilled;

            ++curStageIndex;
            stageChangeStarted?.Invoke();
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
                case PlayerVictoryResult: nextStage(); break;
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

            for (int i = 0; i < 7; i++)
            {
                Stage stage = new Stage(i, i == 3 ? StageType.TOWN : StageType.FIGHT);
                stages.Add(stage);
            }
        }

        private void Start()
        {
        }
    }
}
