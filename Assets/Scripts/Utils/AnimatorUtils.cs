using System.Collections;
using UnityEngine;

using static GameObjectUtils;

public class AnimatorUtils
{
    public static IEnumerator WaitForAnimationCompletion(Transform transform)
    {
        SetInteractability(transform, false);

        var animator = transform.GetComponent<Animator>();

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);

        SetInteractability(transform);
    }

    public static IEnumerator ExecuteTriggerThenWait(Transform transform, string triggerName)
    {
        Animator animator = transform.GetComponent<Animator>();

        SetInteractability(transform, false);

        animator.SetTrigger(triggerName);
        yield return new WaitForSeconds(0.1f);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);

        SetInteractability(transform);
    }
}