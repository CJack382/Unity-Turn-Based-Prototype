using NUnit.Framework;
using UnityEngine;
using SinuousProductions;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int startingHandSize;
    public int maxHandSize = 12;
    public int currentHandSize;
    private HandManager handManager;
    private DrawPileManager drawPileManager;
    private bool startBattleRun = true;

    private void Start()
    {
        //Load All card assets from the Resources folders
        Card[] cards = Resources.LoadAll<Card>("Cards"); //More Resources.Load, very helpful tool. This takes ALL objects with the Card component in the Resources/Cards folder (I checked and it does go through several folders
                                                         //including the 'CardData' folder which has all of the cards, despite me not hard coding it) then loads them into the cards array

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards); //AddRange simply functions as if you were just adding all elements of an Array or List to the end of an existing list .AddRange takes every element from the input list and
                                  //adds it to the end of the existing list
    }

    private void Awake()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindAnyObjectByType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindAnyObjectByType<HandManager>();
        }
    }

    private void Update()
    {
        if (startBattleRun)
        {
            BattleSetup();
        }
    }

    public void BattleSetup()
    {
        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(allCards);
        drawPileManager.BattleSetup(startingHandSize, maxHandSize);
        startBattleRun = false;
    }
}
