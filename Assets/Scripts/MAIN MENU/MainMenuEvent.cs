using System.Collections;
using UnityEngine;

public class MainMenuEvent : MonoBehaviour
{
    public Animator textAnim;
    public Animator play;
    public Animator options;
    public Animator quit;
    public Animator indev;

    void Awake()
    {
        MainMenuGlobals.isMatchOff = true;
        StartCoroutine(MatchBegin());
    }

    IEnumerator MatchBegin()
    {
        yield return new WaitForSeconds(1.25f);
        MainMenuGlobals.matchSrc.PlayOneShot(MainMenuGlobals.fireOn);
        yield return new WaitForSeconds(0.75f);

        textAnim.SetBool("Play?", true);
        play.SetBool("Play?", true);
        options.SetBool("Play?", true);
        quit.SetBool("Play?", true);
        indev.SetBool("Play?", true);

        LightMenu.Max = 2.0f;
        MainMenuGlobals.isMatchOff = false;
        
        yield return new WaitForSeconds(0.5f);
        
        LightMenu.Max = 1.1f;
    }
}
