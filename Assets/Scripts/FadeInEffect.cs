using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///FadeInEffect used for logo/textures presentation during application run
public class FadeInEffect : MonoBehaviour
{
    //RawImage to attach the logo
    [SerializeField] private RawImage rawImage;
    //BlackSphere to set in front of the user or to cover the environment if already loaded
    [SerializeField] private GameObject blackSphere; 

    void Start() => StartCoroutine(FadeImage(false));
    
    IEnumerator FadeImage(bool fadeAway)
    {
        yield return new WaitForSecondsRealtime(3.0f);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                rawImage.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            Debug.Log("time: " + Time.deltaTime);
            // loop over 1 second
            for (float i = 0; i <= 1; i += 0.005f)
            {
                // set color with i as alpha
                rawImage.color = new Color(1, 1, 1, i);
                yield return null;
            }
            for (float i = 1; i >= 0; i -= 0.003f)
            {
                // set color with i as alpha
                rawImage.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }

        blackSphere.SetActive(false);
    }
}
