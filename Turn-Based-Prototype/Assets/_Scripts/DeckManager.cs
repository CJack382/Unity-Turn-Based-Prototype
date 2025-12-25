using NUnit.Framework;
using UnityEngine;
using SinuousProductions;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    private int currentIndex = 0;

    private void Start()
    {
        //Load All card assets from the Resources folders
        Card[] cards = Resources.LoadAll<Card>("Cards"); //More Resources.Load, very helpful tool. This takes ALL objects with the Card component in the Resources/Cards folder (I checked and it does go through several folders
                                                         //including the 'CardData' folder which has all of the cards, despite me not hard coding it) then loads them into the cards array

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards); //AddRange simply functions as if you were just adding all elements of an Array or List to the end of an existing list .AddRange takes every element from the input list and
                                  //adds it to the end of the existing list

        HandManager hand = FindAnyObjectByType<HandManager>(); //Assume only 1 will be in scene, may need to change later
        for (int i = 0; i < 6; i++)
        {
            DrawCard(hand);
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if(allCards.Count == 0)
        {
            return;
        }

        Card nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex = (currentIndex + 1) % (allCards.Count + 1); //For some reason, the math in the video had it to where the deck cannot access the last card, adding 1 to the list count fixes it, but its patchwork
    }
}
