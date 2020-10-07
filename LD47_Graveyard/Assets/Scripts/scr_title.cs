using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_title : MonoBehaviour
{
    public Button startButt;
    public Button controlButt;
    public Button endButt;
    public Canvas controlPrefab;
    private bool controlled = false;

    // Start is called before the first frame update
    void Start()
    {
        startButt.onClick.AddListener(startOnClick);
        controlButt.onClick.AddListener(controlOnClick);
        endButt.onClick.AddListener(endOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        // exit strategy
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void startOnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void controlOnClick()
    {
        if (controlled == false)
        {
            controlPrefab.enabled = true;
            //Debug.Log("controlled turned from false to true");
            controlled = true;
        } else
        {
            controlPrefab.enabled = false;
            //Debug.Log("controlled turned from true to false");
            controlled = false;
        }
    }

    public void endOnClick()
    {
        Application.Quit();
    }
}
