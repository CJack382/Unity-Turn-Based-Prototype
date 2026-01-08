using UnityEngine;
using System.Collections.Generic;
using SinuousProductions;
using TMPro;

public class DrawPileManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();

    private int currentIndex = 0;

    public int startingHandSize;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;

    private DiscardManager discardManager;

    public TextMeshProUGUI drawPileCounter;
    private void Start()
    {
        handManager = FindAnyObjectByType<HandManager>();
    }

    private void Update()
    {
        currentHandSize = handManager.cardsInHand.Count;
    }

    public void MakeDrawPile(List<Card> cardsToAdd)
    {
        drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(drawPile);
        UpdateDrawPileCount();
    }

    public void BattleSetup(int numberOfCardsToDraw, int setMaxHandSize)
    {
        maxHandSize = setMaxHandSize;
        for (int i = 0; i < numberOfCardsToDraw; i++)
        {
            DrawCard(handManager);
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (drawPile.Count == 0)
        {
            RefillDeckFromDiscard();
        }

        if (currentHandSize < maxHandSize)
        {
            Card nextCard = drawPile[currentIndex];
            handManager.AddCardToHand(nextCard);

            drawPile.RemoveAt(currentIndex);
            UpdateDrawPileCount();

            if (drawPile.Count > 0)
            {
                currentIndex %= drawPile.Count;
            }
        }
    }

    private void RefillDeckFromDiscard()
    {
        if (discardManager == null)
        {
            discardManager = FindAnyObjectByType<DiscardManager>();
        }

        if (discardManager != null && discardManager.discardCardsCount > 0)
        {
            drawPile = discardManager.PullAllFromDiscard();
            Utility.Shuffle(drawPile);
            currentIndex = 0;
        }
    }

    private void UpdateDrawPileCount()
    {
        drawPileCounter.text = drawPile.Count.ToString();
    }
}
