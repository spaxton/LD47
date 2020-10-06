using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_popup : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text content;
    [SerializeField] Button gotit;
    public int skullScore;

    // Start is called before the first frame update
    void Start()
    {
        skullScore = GameObject.Find("PlayerGhost").GetComponent<scr_player_movement>().skullScore;
        if(skullScore == 4)
        {

            gotit.onClick.AddListener(theEndClick);
        } else
        {

            gotit.onClick.AddListener(gotItClick);
        }
        messaging();
        Time.timeScale = 0.00001f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void gotItClick()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }

    void theEndClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void messaging()
    {
        if (skullScore == 4)
        {
            title.text = "Finally Rest in Peace";
            content.text = "That was a lot of exorcise! You've earned a dirt nap: pull up a tombstone and see what dreams may come.";
        } else
        {
            content.text = "Looks like you made some... questionable decisions in life, now you're stuck in limbo! Don't panic: help move old bones to where they want to go and you can rest in peace.";
        }
    }


}
