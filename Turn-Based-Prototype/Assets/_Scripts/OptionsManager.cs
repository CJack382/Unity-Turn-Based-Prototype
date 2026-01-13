using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class OptionsManager : MonoBehaviour
{
    private AudioManager audioManager;
    public bool muteAudio;

    public List<TMP_FontAsset> fontList;
    public static event Action FontUpdated; //Event Time YIPPEE!!!
    void Start()
    {
        audioManager = GameManager.Instance.AudioManager;
    }

    void Update()
    {
        
    }

    public TMP_FontAsset GetFontClass(string classID)
    {
        switch (classID)
        {
            case "MenuText":
                return fontList[0];
            case "CardTitle":
                return fontList[1];
            case "CardBody":
                return fontList[2];
            case "CardBodyBold":
                return fontList[3];
            case "MenuTextBold":
                return fontList[4];
            default:
                return fontList[0];
        }
    }

    public void UpdateFont()
    {
        FontUpdated?.Invoke();
    }
}
