using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SinuousProductions; //The namespace of Card.cs, did not know you could do this, nor do I yet understand the implications

public class CardDisplay : MonoBehaviour
{
    public Card cardData;

    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages;

    private Color[] cardColors =
    {
        Color.red, //Fire
        new Color(0.0f, 0.52f, 0.24f),
        Color.blue, //Water
        new Color(0.23f, 0.06f, 0.21f), //Dark
        Color.yellow, //Light
        Color.cyan // Air
    };
    
    private Color[] typeColors =
    {
        Color.red, //Fire
        new Color(0.0f, 0.52f, 0.24f),
        Color.blue, //Water
        new Color(0.899371f, 0.2008029f, 0.8248572f), //Dark
        Color.yellow, //Light
        Color.cyan // Air
    };
    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        //Update the main card image color based on the first card type
        cardImage.color = cardColors[(int)cardData.cardType[0]];

        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = $"{cardData.damageMin}-{cardData.damageMax}";

        //Update TypeImages
        for (int i = 0; i < typeImages.Length; i++)
        {
            if (i < cardData.cardType.Count)
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }
    }
}
