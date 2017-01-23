using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionScript : MonoBehaviour {

    public GameObject movieField;
    public double minReactionTime = 0.0005;
    public double maxReactionTime = 0.01;
    public int maxViews = 5000;


    private MovieTexture[] movies;
    private int currentMovieIndex;
    private bool shown;
    private bool showing;
    private bool hiding;
    private double animTime = 0;
    private bool animUpdated;
    private GameObject viewsField;

    private int viewCount;

    private float delay = 15.0f;

    private double frequency = 0.0005;
    MovieTexture randomizeTexture()
    {
        return movies[Random.Range(0, movies.Length)];
        
    }

	// Use this for initialization
	void Start () {
        movies = Resources.LoadAll<MovieTexture>("");
        currentMovieIndex = 0;
        shown = false;
        showing = false;
        hiding = false;


        viewsField = GameObject.Find("ViewsField");

        //GetComponent<Animator>().SetTrigger("show");
        //GetComponent<RawImage>().texture = movies[1];
        //((MovieTexture)GetComponent<RawImage>().texture).Play();
        //bool isplaying = ((MovieTexture)GetComponent<RawImage>().texture).isPlaying;


        //((MovieTexture)GetComponent<RawImage>().texture).Stop();
        //GetComponent<RawImage>().texture = movies[0];
        //((MovieTexture)GetComponent<RawImage>().texture).Play();
        //isplaying = ((MovieTexture)GetComponent<RawImage>().texture).isPlaying;
    }

    double getFrequency(int currentViews)
    {
        return (Mathf.Clamp01((float)currentViews / maxViews)) * maxReactionTime + minReactionTime;
    }
	
	// Update is called once per frame
	void Update () {

        delay -= Time.deltaTime;
        if(delay >= 0)
        {
            return;
        }
        else
        {
            delay = 0.0f;
        }

        bool test = int.TryParse(viewsField.GetComponent<Text>().text, out viewCount);
        if(test)
        {
            frequency = getFrequency(viewCount);
        }
        if (!shown && Random.value < frequency)
        {
            GetComponent<Transform>().localScale = new Vector3(1, 1);
            currentMovieIndex = Random.Range(0, movies.Length);
            GetComponent<RawImage>().texture = movies[currentMovieIndex];
            ((MovieTexture)GetComponent<RawImage>().texture).Play();
            shown = true;
        }
        if(shown && !(((MovieTexture)GetComponent<RawImage>().texture).isPlaying))
        {
            ((MovieTexture)GetComponent<RawImage>().texture).Stop();
            GetComponent<Transform>().localScale = new Vector3(0, 0);
            shown = false;
        }


        //if(animTime > GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
        //{
        //    animUpdated = true;
            
        //}
        //if (!shown && !showing && !hiding && Random.value < reactionTime)
        //{
        //    currentMovieIndex = Random.Range(0, movies.Length);

        //    GetComponent<RawImage>().texture = movies[currentMovieIndex];
        //    //movies[currentMovieIndex].Play();


        //    GetComponent<Animator>().SetTrigger("show");
        //    showing = true;
        //    animUpdated = false;
        //}
        //float test = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
        ////if (((MovieTexture)GetComponent<RawImage>().texture).isPlaying)
        ////{
        ////    bool test = true;
        ////}
        //if (showing && !hiding && !shown && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0) && animUpdated)
        //{
        //    ((MovieTexture)GetComponent<RawImage>().texture).Play();
        //    shown = true;
        //    showing = false;
        //}

        //test = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
        //if (hiding && !shown && !showing && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0) && animUpdated)
        //{
        //    hiding = false;
        //}
        //if (shown && !hiding && !showing && !((MovieTexture)GetComponent<RawImage>().texture).isPlaying )
        //{
        //    ((MovieTexture)GetComponent<RawImage>().texture).Stop();
        //    GetComponent<Animator>().SetTrigger("hide");
        //    hiding = true;
        //    shown = false;
        //    animUpdated = false;
        //}
        //animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
