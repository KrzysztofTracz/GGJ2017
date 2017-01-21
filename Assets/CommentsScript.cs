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

    private float frequency;
    private GameObject lastMessage;
    private string[] likeComments;
    private string[] dislikeComments;
    private string[] nicknames;

    private float likeDislikeRatio;
    
    private int likeCount, dislikeCount;
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
        likeDislikeRatio = likeCount / (likeCount + dislikeCount);
    }

    // Use this for initialization
    void Start () {
        frequency = 0.03f;
    }
	
	// Update is called once per frame
	void Update () {
		if(Random.value < frequency)
        {
            if(lastMessage != null)
            {
                Animator anim = lastMessage.GetComponent<Animator>();
                anim.SetTrigger("pass");
                //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
                //{
                //    lastMessage = Instantiate(messagePrefab, new Vector3(0, 0), Quaternion.identity);
                //    lastMessage.transform.parent = panel;
                //}
            }
            //else
            {
                lastMessage = Instantiate(messagePrefab, new Vector3(0, 0), Quaternion.identity);
                lastMessage.transform.parent = panel;
                lastMessage.transform.localScale = new Vector3(1, 1);
                lastMessage.transform.GetChild(0).gameObject.GetComponent<Text>().text = randomizeNickname();
                lastMessage.transform.GetChild(1).gameObject.GetComponent<Text>().text = randomizeComment();
            }
        }
	}
}
