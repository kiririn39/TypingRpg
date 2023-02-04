using Assets.Scripts.SkillTree;
using Common;
using DefaultNamespace;
using DefaultNamespace.BattleActions;
using System;
using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class ResourcesManager : MonoBehaviour
    {
        #region Sprites
        [Serializable]
        public class SpriteForBattleAction
        {
            public string className;
            public Sprite sprite;


            public SpriteForBattleAction(string className, Sprite sprite)
            {
                this.className = className;
                this.sprite = sprite;
            }
        }

        [Serializable]
        public class SpriteForMapNode
        {
            public StageType stageType;
            public Sprite    sprite;
        }

        [SerializeField] private Sprite undefinedBattleActionIcon = null;

        [SerializeField] public List<SpriteForBattleAction> imgsBattleActionIcon =
            CommonExtensions.GetAllInheritorsOf<BattleActionBase>()
                .Select(it => new SpriteForBattleAction(it.Name, null))
                .ToList();

        [SerializeField] private List<SpriteForMapNode> imgsMapNodeIcon = new List<SpriteForMapNode>();

        public Sprite getSpriteForBattleAction(Type type)
        {
            if (type == null)
                return undefinedBattleActionIcon;

            string typeName = type.Name;
            Sprite sprite = imgsBattleActionIcon.FirstOrDefault(it => it.className == typeName)?.sprite;
            if (sprite != null)
                return sprite;

            return undefinedBattleActionIcon;
        }

        public Sprite getSpriteForMapNode(StageType type)
        {
            Sprite sprite = imgsMapNodeIcon.FirstOrDefault(x => x.stageType == type)?.sprite;
            return sprite ? sprite : undefinedBattleActionIcon;

        }
        #endregion

        #region Localization

        public string getSkillTitle(BattleActionBase battleActionBase)
        {
            if (battleActionBase == null)
                return "NULL";

            return $"Skill title for {battleActionBase.GetType().Name}";
            //TODO
            switch (battleActionBase)
            {
            case AttackAction attackAction:
                break;
            case DefencePrepareAction defenceAction:
                break;
            case IdleAction idleAction:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(battleActionBase));

            }
        }

        public string getSkillDescription(BattleActionBase battleActionBase, List<RuneKey> runeKeys)
        {
            if (battleActionBase == null)
                return "NULL";

            return $"Skill description for {battleActionBase.GetType().Name}\n[{string.Join("", runeKeys)}]";
            //TODO
            switch (battleActionBase)
            {
            case AttackAction attackAction:
                break;
            case DefencePrepareAction defenceAction:
                break;
            case IdleAction idleAction:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(battleActionBase));

            }
        }
        #endregion


        #region Singleton
        public static ResourcesManager Instance { get; private set; }

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

    #if UNITY_EDITOR
    [CustomEditor(typeof(ResourcesManager))]
    public class ResourcesManagerEditor :Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update imgsBattleActionIcon"))
            {
                List<ResourcesManager.SpriteForBattleAction> curTypeNames = (target as ResourcesManager).imgsBattleActionIcon;
                List<string> allTypeNames = CommonExtensions.GetAllInheritorsOf<BattleActionBase>().Select(it => it.Name).ToList();
                foreach (string className in allTypeNames)
                {
                    if (curTypeNames.All(it => it.className != className))
                        curTypeNames.Add(new ResourcesManager.SpriteForBattleAction(className, null));
                }
            }
                
        }
    }
    #endif
}

