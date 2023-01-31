using Assets.Scripts.SkillTree;
using RuneStack;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelRunes : MonoBehaviour
{
   [SerializeField] private UIRuneStack uiRuneStack = null;
   [SerializeField] private UIRuneTree  uiRuneTree  = null;

   private void Awake()
   {
      uiRuneTree.init();

      uiRuneStack.init(uiRuneTree.RuneTree);
      uiRuneStack.OnStackChanged += runes => uiRuneTree.trySelectSequence(runes);
   }
}
