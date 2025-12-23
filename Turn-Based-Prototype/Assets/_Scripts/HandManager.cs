using UnityEngine;
using SinuousProductions;
using NUnit.Framework;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //Assign Card prefab in inspector
    public Transform handTransform; //Root of hand position, Also I hate public variable, but I can't go off script
    public float fanSpread = 5f;

    public List<GameObject> cardsInHand = new List<GameObject>(); //List of Card objects in hand

    void Start()
    {
        AddCardToHand();
    }

    public void AddCardToHand()
    {
        //Card Instantiation
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform); //You've seen it before, but just to really hit it home, Instantiate takes the desired gameObject, position,
                                                                                                                  //rotation (Quaternion), and transform to 'spawn' the object
        cardsInHand.Add(newCard);

        UpdateHandVisuals();
    }

    void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;
        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1)) / 2f);
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle); //x, y, z, Euler is very complicated, it is the user view of the rotation for the developers, however the computer reads it as Quaternions,
                                                                                              //Quaternion.Euler rotates the values along the respective axis relative to the amount put in, I'm not certain as to the exact mechanics
                                                                                              //however, I do sorta understand what's happening
        }
    }
}
