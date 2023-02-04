using Common;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Sequence=DG.Tweening.Sequence;

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

        ATTACK             = 1,
        ATTACK_FORCE       = 2,
        DEFENCE            = 3,
        ATTACK_FIRE        = 4,
        ATTACK_FROST       = 5,
        ATTACK_PSYONIC     = 6,
        ATTACK_POISON      = 7,
        TAKE_DAMAGE        = 8,
        TAKE_DAMAGE_POISON = 9,
        HEAL               = 10,
        DEATH              = 11,
    }

    [SerializeField] private Animator animator = null;
    [SerializeField] private List<AnimatorsForCharacters> charactersOverrideControllers = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [Header("Flash on damage effect")]
    [SerializeField] private Material flashOnDamageMaterial = null;
    [SerializeField] private float flashOnDamageDuration = 0.125f;
    [Header("Other effects color")]
    [SerializeField] private Color poisonColor = new Color(0.03f, 0.73f, 0f);
    [SerializeField] private Color damageColor = new Color(0.73f, 0f, 0.11f);

    private Character currentCharacter = Character.NONE;
    private Sequence flashCoroutine  = null;
    private Sequence colorSpriteCoroutine  = null;


    public void init(Character character)
    {
        this.currentCharacter = character;
        animator.enabled = false;
        var overrideController = charactersOverrideControllers.FirstOrDefault(it => it.character == character);
        if (overrideController != null)
            animator.runtimeAnimatorController = overrideController.overrideController;

        animator.enabled = true;

        bool isPlayer = character == Character.PLAYER;
        spriteRenderer.flipX = isPlayer;
        float absScaleX = Mathf.Abs(transform.localScale.x);
        transform.localScale = transform.localScale.setX(isPlayer ? -absScaleX : absScaleX);
    }

    public void play( AnimationType animationType )
    {
        if (tryPlayTweenAnimation(animationType))
            return;

        animator.Play(getAnimationStringKey(animationType));
    }

    private bool tryPlayTweenAnimation(AnimationType animationType)
    {
        if (animationType != AnimationType.TAKE_DAMAGE && animationType != AnimationType.TAKE_DAMAGE_POISON)
            return false;

        switch (animationType)
        {
        case AnimationType.TAKE_DAMAGE:
            // colorSpriteCoroutine.Complete();
            // spriteRenderer.color = Color.white;
            //
            // colorSpriteCoroutine = DOTween.Sequence();
            // colorSpriteCoroutine.Append(spriteRenderer.DOColor(damageColor, flashOnDamageDuration/2));
            // colorSpriteCoroutine.Append(spriteRenderer.DOColor(Color.white, flashOnDamageDuration/2));
            // colorSpriteCoroutine.onComplete += () => spriteRenderer.color = Color.white;
            //
            // colorSpriteCoroutine.Play();

            flashCoroutine?.Complete();

            FindObjectOfType<CameraScript>().tweenDamageTaken();

            Material oldMaterial = spriteRenderer.material;
            spriteRenderer.material = flashOnDamageMaterial;
            flashCoroutine = DOTween.Sequence();
            flashCoroutine.AppendInterval(flashOnDamageDuration);
            flashCoroutine.onComplete += () => spriteRenderer.material = oldMaterial;
            flashCoroutine.Play();
            break;

        case AnimationType.TAKE_DAMAGE_POISON:
            colorSpriteCoroutine.Complete();
            spriteRenderer.color = Color.white;

            colorSpriteCoroutine = DOTween.Sequence();
            colorSpriteCoroutine.Append(spriteRenderer.DOColor(poisonColor, flashOnDamageDuration/2));
            colorSpriteCoroutine.Append(spriteRenderer.DOColor(Color.white, flashOnDamageDuration/2));
            colorSpriteCoroutine.onComplete += () => spriteRenderer.color = Color.white;

            colorSpriteCoroutine.Play();

            break;
        }

        return true;
    }
    // public bool isPlaying => animator.is

    private string getAnimationStringKey(AnimationType animationType)
    {
        switch (animationType)
        {
        case AnimationType.ATTACK:         return "Attack";
        case AnimationType.ATTACK_FORCE:   return "AttackForce";
        case AnimationType.DEFENCE:        return "Defence";
        case AnimationType.ATTACK_FIRE:    return "MagicAttackFire";
        case AnimationType.ATTACK_FROST:   return "MagicAttackFrost";
        case AnimationType.ATTACK_PSYONIC: return "AttackPsyonic";
        case AnimationType.ATTACK_POISON:  return "Psyonic";
        case AnimationType.HEAL:           return "Heal";
        case AnimationType.DEATH:          return "Death";
        }

        return "Idle";
    }
    
    
}
