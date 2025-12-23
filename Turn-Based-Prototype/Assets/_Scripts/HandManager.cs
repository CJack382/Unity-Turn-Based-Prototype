using UnityEngine;
using SinuousProductions;
using System.Collections.Generic;
using System.Collections;
using System;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //Assign Card prefab in inspector
    public Transform handTransform; //Root of hand position, Also I hate public variable, but I can't go off script
    public float fanSpread = 7.5f;

    public float cardSpacing = -150f;
    public float verticalSpacing = 75f;
    public List<GameObject> cardsInHand = new List<GameObject>(); //List of Card objects in hand

    void Start()
    {
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
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

    private void Update()
    {
        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1) //Catches an error that occurs when there is only 1 card in hand, which has trouble being calculated. Simply sets basic values and returns from method.
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f)); //As i gets higher, the resulting value will become a smaller NEGATIVE NUMBER i.e i = 0, then rotation angle = fanSpread * -2.5, i = 5 rotation angle =
                                                                            //fanSpread * 2.5, each increase in i, increases the resulting value by 1, creating a linear equation

            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle); //x, y, z, Euler is very complicated, it is the user view of the rotation for the developers, however the computer reads it as Quaternions,
                                                                                              //Quaternion.Euler rotates the values along the respective axis relative to the amount put in, I'm not certain as to the exact mechanics
                                                                                              //however, I do sorta understand what's happening, It is creating the 'direction' for the object to look, then uses quaternions to direct it that way
            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f)); 

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f); //Normalize card position between -1 and 1, i = 0 will result in negative 1 (0/# - 1f = 0 - 1f), i = 5 results in 1 (2 * 5 = 10 / 5
                                                                        //[assuming CardCount is 6 and i is max] = 2 - 1 = 1)

            float verticalOffset = (verticalSpacing * (1 - normalizedPosition * normalizedPosition)); //multiplies normalized numbers by themselves to get a normalized value (standard normalization), subtracts it from 1 to get
                                                                                                      //a negative quadratic equation which is what's needed for the 'hand arch' look

            //Set Card position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
