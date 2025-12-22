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
        new Color(0.8238993f, 0.2903206f, 0f), //Fire
        new Color(0.0f, 0.52f, 0.24f), //Earth
        new Color(0f, 0f, 0.5408804f), //Water
        new Color(0.23f, 0.06f, 0.21f), //Dark
        new Color(0.7861634f, 0.6659856f, 0f), //Light
        new Color(0.1259839f, 0.6163521f, 0.6163521f) // Air
    };
    
    private Color[] typeColors =
    {
        Color.red, //Fire
        new Color(0.4402515f, 0.2920384f, 0.0733752f), //Earth
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
