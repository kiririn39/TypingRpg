using Assets.Scripts.SkillTree;
using BattleSystem.BattleActions;
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

            string result = "";
            switch (battleActionBase)
            {
            case DefenceEffect           : result = "Defence Effect"; break;
            case EvasionEffect           : result = "Evasion Effect"; break;
            case EvasionPrepareAction    : result = "Evasion Effect"; break;
            case HealEffect              : result = "Heal Effect";    break;
            case HealPrepareAction       : result = "Heal Effect";    break;
            case MagicFireAction         : result = "Fire Attack";    break;
            case MagicFrostAction        : result = "Frost Attack";   break;
            case PenetratingAttackAction : result = "Force Attack";   break;
            case PoisonEffect            : result = "Poison Effect";  break;
            case AttackAction            : result = "Attack";         break;
            case DefencePrepareAction    : result = "Defence Effect"; break;
            case PoisonPrepareAction     : result = "Poison Effect";  break;

            }

            return result;
        }

        public string getSkillDescription(BattleActionBase battleActionBase, List<RuneKey> runeKeys)
        {
            if (battleActionBase == null)
                return "NULL";

            string result = "";
            switch (battleActionBase)
            {
            case DefenceEffect           : result = "Protects you from incoming physical damage";      break;
            case EvasionEffect           : result = "Gives you a temporary physical evasion bonus";    break;
            case EvasionPrepareAction    : result = "Gives you a temporary physical evasion bonus";    break;
            case HealEffect              : result = "Heals you over a period of time";                 break;
            case HealPrepareAction       : result = "Heals you over a period of time";                 break;
            case MagicFireAction         : result = "Throws a ball of fire on to the enemy";           break;
            case MagicFrostAction        : result = "Freezes an enemy to death";                       break;
            case PenetratingAttackAction : result = "Deals less damage but does not count protection"; break;
            case PoisonEffect            : result = "Deals damage to the target over period of time";  break;
            case AttackAction            : result = "Deals physical damage";                           break;
            case DefencePrepareAction    : result = "Protects you from incoming physical damage";      break;
            case PoisonPrepareAction     : result = "Deals damage to the target over period of time";  break;

            }

            result += $"\n{string.Join("", runeKeys)}";
            return result;
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

