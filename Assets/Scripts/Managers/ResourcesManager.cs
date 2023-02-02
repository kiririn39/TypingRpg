using Assets.Scripts.SkillTree;
using Common;
using DefaultNamespace;
using DefaultNamespace.BattleActions;
using System;
using System.Collections.Generic;
using System.Linq;
using Map;
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

        [SerializeField] private List<SpriteForBattleAction> imgsBattleActionIcon =
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
            case DefenceAction defenceAction:
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
            case DefenceAction defenceAction:
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
}
