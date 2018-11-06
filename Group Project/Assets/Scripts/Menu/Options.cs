using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject graphicsPanel, audioPanel, controlsPanel;
    public enum WhichMenu
    {
        Graphics, Audio, Controls
    }
    public WhichMenu whichMenu;

    // Use this for initialization
    void Start()
    {
        graphicsPanel = GameObject.Find("Graphics Panel");
        audioPanel = GameObject.Find("Audio Panel");
        controlsPanel = GameObject.Find("Controls Panel");
    }

    // Update is called once per frame
    void Update()
    {
        switch (whichMenu)
        {
            case WhichMenu.Graphics:
                graphicsPanel.SetActive(true);
                audioPanel.SetActive(false);
                controlsPanel.SetActive(false);
                break;
            case WhichMenu.Audio:
                graphicsPanel.SetActive(false);
                audioPanel.SetActive(true);
                controlsPanel.SetActive(false);
                break;
            case WhichMenu.Controls:
                graphicsPanel.SetActive(false);
                audioPanel.SetActive(false);
                controlsPanel.SetActive(true);
                break;
            default:
                graphicsPanel.SetActive(true);
                audioPanel.SetActive(false);
                controlsPanel.SetActive(false);
                break;
        }
    }

    public void ChangePanel(int a)
    {
        switch (a)
        {
            case 1:
                whichMenu = WhichMenu.Graphics;
                break;
            case 2:
                whichMenu = WhichMenu.Audio;
                break;
            case 3:
                whichMenu = WhichMenu.Controls;
                break;
            default:
                whichMenu = WhichMenu.Graphics;
                break;
        }
        
    } 
}
