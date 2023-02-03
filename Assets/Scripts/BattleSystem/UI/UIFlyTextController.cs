using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIFlyTextController : MonoBehaviour
{
   [SerializeField] private UIFlyText goFlyText = null;

   [SerializeField] private List<UIFlyText> flyTextsPoolDefault = new List<UIFlyText>();

   private List<UIFlyText> currentFlying = new List<UIFlyText>();
   private Stack<UIFlyText> flyTextsPoolStack = null;

   //Scale settings
   private const float TWEEN_SCALE_TIME_IN  = 0.1f;
   private const float TWEEN_SCALE_TIME_OUT = 0.3f;
   private static readonly Vector3 SCALE_TEXT_MIN      = Vector3.one * 0.01f;
   private static readonly Vector3 SCALE_TEXT_MAX      = Vector3.one * 1.5f;
   private static readonly Vector3 SCALE_TEXT          = Vector3.one;
   //Move settings
   private const float TIME_TWEEN_TEXT = 0.6f;


   public void init()
   {
      flyTextsPoolStack = new Stack<UIFlyText>(flyTextsPoolDefault);
   }

   public void spawnFlyText(Vector3 startPos, float damageAmount, bool isHeal, bool isCrit = false)
   {
      if (flyTextsPoolStack.Count == 0)
      {
         UIFlyText newFlyText = Instantiate(goFlyText, new Vector3(999999, 999999), Quaternion.identity);
         flyTextsPoolStack.Push(newFlyText);
      }

      UIFlyText flyText = flyTextsPoolStack.Pop();
      currentFlying.Add(flyText);

      flyText.init(damageAmount, isHeal, isCrit);
      flyText.gameObject.SetActive(true);
      Transform trans = flyText.transform;
      trans.position = startPos;
      trans.localScale = SCALE_TEXT_MIN;
      trans.SetAsLastSibling();


      Sequence sequenceScale = DOTween.Sequence();
      sequenceScale.Append(trans.DOScale(SCALE_TEXT_MAX, TWEEN_SCALE_TIME_IN));
      sequenceScale.Append(trans.DOScale(SCALE_TEXT, TWEEN_SCALE_TIME_OUT));


      Vector3 random_offset = new Vector3( Random.Range( -30, 30 ), Random.Range( 30, 60 ) );
      var coroutine = trans.DOLocalMove(trans.localPosition + random_offset, TIME_TWEEN_TEXT).Play();
      coroutine.onComplete += onFlyEnded;

      void onFlyEnded()
      {
         flyText.gameObject.SetActive(false);
         flyTextsPoolStack.Push(flyText);
         currentFlying.Remove(flyText);
      }
   }

   
}
