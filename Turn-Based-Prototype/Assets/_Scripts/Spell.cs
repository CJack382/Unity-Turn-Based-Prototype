using UnityEngine;
using SinuousProductions;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Spell Card", menuName = "Card/Spell")]
public class Spell : Card
{
    public SpellType spellType;
    public List<AttributeTarget> attributeTargets;
    public List<int> attributeChangeAmount;
}
