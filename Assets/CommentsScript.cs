using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CommentsScript : MonoBehaviour {

    public Transform panel;
    public GameObject messagePrefab;

    public TextAsset likeCommentsAsset;
    public TextAsset dislikeCommentsAsset;
    public TextAsset nicknamesAsset;

    public double maxFrequency = 3;
    public int maxSubs = 20000;

    public float minSpeed = 0.5f;

    private float frequency;
    private GameObject lastMessage;
    private List<GameObject> loweringMessages;
    private string[] likeComments;
    private string[] dislikeComments;
    private string[] nicknames;

    private float likeDislikeRatio;
    
    private int likeCount, dislikeCount, subsCount;
    private GameObject likesField, dislikesField, subsField;
    void loadFiles()
    {
        string likeString = likeCommentsAsset.text;
        List<string> likeCommentsList = new List<string>();
        likeCommentsList.AddRange(likeString.Split("\n"[0]));
        likeComments = likeCommentsList.ToArray();

        string dislikeString = dislikeCommentsAsset.text;
        List<string> dislikeCommentsList = new List<string>();
        dislikeCommentsList.AddRange(dislikeString.Split("\n"[0]));
        dislikeComments = dislikeCommentsList.ToArray();

        string nicknameString = nicknamesAsset.text;
        List<string> nicknamesList = new List<string>();
        nicknamesList.AddRange(nicknameString.Split("\n"[0]));
        nicknames = nicknamesList.ToArray();
    }

    string randomizeLikeComment()
    {
        return likeComments[Random.Range(0, likeComments.Length)];
    }

    string randomizeDislikeComment()
    {
        return dislikeComments[Random.Range(0, dislikeComments.Length)];
    }

    string randomizeNickname()
    {
        return nicknames[Random.Range(0, nicknames.Length)];
    }

    string randomizeComment()
    {
        if(Random.value < likeDislikeRatio)
        {
            return randomizeLikeComment();
        }
        else
        {
            return randomizeDislikeComment();
        }
    }

    void countLikeDislikeRatio()
    {
        if (likeCount == 0 && dislikeCount == 0)
        {
            likeDislikeRatio = 0.5f;
        }
        else
        {
            likeDislikeRatio = (float)likeCount / (likeCount + dislikeCount);
        }
    }

    // Use this for initialization
    void Start () {
        loadFiles();
        frequency = 0.03f;
        likesField = GameObject.Find("LikesField");
        dislikesField = GameObject.Find("DislikesField");
        subsField = GameObject.Find("SubsField");
        loweringMessages = new List<GameObject>();
    }

    void countFrequency()
    {
        frequency = Mathf.Clamp(((float)subsCount / maxSubs), minSpeed, (float)maxFrequency);
    }

    // Update is called once per frame
    void Update () {

        bool test = true;
        test &= int.TryParse(likesField.GetComponent<Text>().text, out likeCount);
        test &= int.TryParse(dislikesField.GetComponent<Text>().text, out dislikeCount);
        test &= int.TryParse(subsField.GetComponent<Text>().text, out subsCount);
        if(test)
        {
            countLikeDislikeRatio();
            countFrequency();

            //if (Random.value < frequency)
            //{
            if (lastMessage != null)
            {
                Animator anim = lastMessage.GetComponent<Animator>();
                anim.SetTrigger("pass");
                anim.speed = frequency;
                loweringMessages.Add(lastMessage);
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("MessageAnim"))
                {
                    lastMessage = Instantiate(messagePrefab, new Vector3(0, 0), Quaternion.identity);
                    lastMessage.transform.parent = panel;
                    lastMessage.transform.localScale = new Vector3(1, 1);
                    lastMessage.transform.GetChild(0).gameObject.GetComponent<Text>().text = randomizeNickname();
                    lastMessage.transform.GetChild(1).gameObject.GetComponent<Text>().text = randomizeComment();

                    anim = lastMessage.GetComponent<Animator>();
                    anim.speed = frequency;

                    foreach(var m in loweringMessages)
                    {
                        anim = m.GetComponent<Animator>();
                        anim.speed = frequency;
                    }

                }
                foreach(var m in loweringMessages.ToArray())
                {
                    if (m.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                    {
                        Destroy(m);
                        loweringMessages.Remove(m);
                    }
                }
                
            }
            else
            {
                lastMessage = Instantiate(messagePrefab, new Vector3(0, 0), Quaternion.identity);
                lastMessage.transform.parent = panel;
                lastMessage.transform.localScale = new Vector3(1, 1);
                lastMessage.transform.GetChild(0).gameObject.GetComponent<Text>().text = randomizeNickname();
                lastMessage.transform.GetChild(1).gameObject.GetComponent<Text>().text = randomizeComment();

                Animator anim = lastMessage.GetComponent<Animator>();
                anim.speed = frequency;
            }

            //}
        } 
	}
}
