using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SinuousProductions; //The namespace of Card.cs, did not know you could do this, nor do I yet understand the implications

public class CardDisplay : MonoBehaviour
{



    //All card elements
    public Card cardData;

    public Image cardImage;
    public TMP_Text nameText;
    public Image[] typeImages;
    public Image displayImage;

    public GameObject characterElements;
    public GameObject spellElements;

    public GameObject characterCardLabel;
    public GameObject spellCardLabel;

    public TMP_Text descriptionText;

    //Character card elements
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image damageImage;

    //Spell card elements
    public GameObject[] spellTypeLabels;
    public GameObject[] attributeTargetSymbols;
    public float attributeSymbolSpacing = 10f;
    public TMP_Text attributeChangeAmountText;

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

    public void UpdateCardDisplay()
    {
        //All Card Changes
        cardImage.color = cardColors[(int)cardData.cardType[0]];
        nameText.text = cardData.cardName;
        displayImage.sprite = cardData.cardSprite;
        descriptionText.text = cardData.description;

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

        //Specific Card Changes
        if (cardData is Character characterCard)
        {
            UpdateDisplayCharacterCard(characterCard);
        }
        if (cardData is Spell spellCard)
        {
            UpdateDisplaySpellCard(spellCard);
        }
    }

    private void UpdateDisplayCharacterCard(Character characterCard)
    {
        spellElements.SetActive(false);
        characterElements.SetActive(true);
        characterCardLabel.SetActive(true);

        damageImage.color = typeColors[(int)characterCard.damageType[0]];
        healthText.text = characterCard.health.ToString();
        damageText.text = $"{characterCard.damageMin}-{characterCard.damageMax}";
    }

    private void UpdateDisplaySpellCard(Spell spellCard)
    {
        characterElements.SetActive(false);
        spellElements.SetActive(true);
        spellCardLabel.SetActive(true);

        //Set correct spell type label
        foreach(GameObject label in spellTypeLabels)
        {
            label.SetActive(false);
        }
        spellTypeLabels[(int)spellCard.spellType].SetActive(true);

        //Reset and update attribute target symbols
        foreach(GameObject symbol in attributeTargetSymbols)
        {
            symbol.SetActive(false);
        }

        for (int i = 0; i < spellCard.attributeTargets.Count; i++)
        {
            GameObject currentSymbol = attributeTargetSymbols[(int)spellCard.attributeTargets[i]];
            currentSymbol.SetActive(true);
            float newYPosition = i * attributeSymbolSpacing;
            currentSymbol.transform.localPosition = new Vector3(0, newYPosition, 0);
        }

        //Display attribute change amounts
        attributeChangeAmountText.text = string.Join(", ", spellCard.attributeChangeAmount);
    }
}
