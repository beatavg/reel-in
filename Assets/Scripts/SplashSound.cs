using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSound : MonoBehaviour
{

    public AudioSource splash;

    public int wait = 10;

    bool keepPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SoundOut());
        splash = GetComponent<AudioSource>();
    }

    IEnumerator SoundOut()
     {
         while (keepPlaying){
             splash.Play();  
             Debug.Log("SPLASH");
             yield return new WaitForSeconds(wait);
         }


}
