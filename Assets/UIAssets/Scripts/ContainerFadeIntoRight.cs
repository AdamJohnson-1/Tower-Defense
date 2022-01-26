using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerFadeIntoRight : MonoBehaviour
{

    public bool mFaded = true;

    public float Duration = 0.1f;
    

    public void Fade()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0 ));

        mFaded = !mFaded;
        canvGroup.interactable = !mFaded;
        canvGroup.blocksRaycasts = !mFaded;
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {

        float counter = 0f;

        while(counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
    }
}
