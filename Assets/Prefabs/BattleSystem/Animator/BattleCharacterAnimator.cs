using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleCharacterAnimator : MonoBehaviour
{
    [Serializable]
    public class AnimatorsForCharacters
    {
        public Character character = Character.NONE;
        public AnimatorOverrideController overrideController = null;
    }

    public enum Character
    {
        NONE = 0,

        PLAYER  = 1,
        MAG     = 2,
        GROMILA = 3,
        KNIGHT  = 4,
    }

    public enum AnimationType
    {
        IDLE = 0,

        ATTACK         = 1,
        ATTACK_FORCE   = 2,
        DEFENCE        = 3,
        ATTACK_FIRE    = 4,
        ATTACK_FROST   = 5,
        ATTACK_PSYONIC = 6,
    }

    [SerializeField] private Animator animator = null;
    [SerializeField] private List<AnimatorsForCharacters> charactersOverrideControllers = null;

    private Character currentCharacter = Character.NONE;


    public void init(Character character)
    {
        this.currentCharacter = character;
        animator.enabled = false;
        var overrideController = charactersOverrideControllers.FirstOrDefault(it => it.character == character);
        if (overrideController != null)
            animator.runtimeAnimatorController = overrideController.overrideController;

        animator.enabled = true;
    }

    public void play( AnimationType animationType )
    {
        animator.Play(getAnimationStringKey(animationType));
    }
    
    // public bool isPlaying => animator.is

    private string getAnimationStringKey(AnimationType animationType)
    {
        switch (animationType)
        {
        case AnimationType.ATTACK:         return "Attack";
        case AnimationType.ATTACK_FORCE:   return "AttackForce";
        case AnimationType.DEFENCE:        return "Defence";
        case AnimationType.ATTACK_FIRE:    return "MagicFire";
        case AnimationType.ATTACK_FROST:   return "MagicFrost";
        case AnimationType.ATTACK_PSYONIC: return "Psyonic";
        }

        return "Idle";
    }
    
    
}
