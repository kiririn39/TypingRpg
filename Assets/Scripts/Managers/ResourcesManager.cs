using Common;
using DefaultNamespace;
using DefaultNamespace.BattleActions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class ResourcesManager : MonoBehaviour
    {
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

        [SerializeField] private List<SpriteForBattleAction> imgsBattleActionIcon =
            CommonExtensions.GetAllInheritorsOf<BattleActionBase>()
                .Select(it => new SpriteForBattleAction(it.Name, null))
                .ToList();

        [SerializeField] private Sprite undefinedBattleActionIcon = null;

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

        #region Localization

        public string getSkillTitle(BattleActionBase battleActionBase)
        {
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

        public string getSkillDescription(BattleActionBase battleActionBase)
        {
            return $"Skill description for {battleActionBase.GetType().Name}";
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
