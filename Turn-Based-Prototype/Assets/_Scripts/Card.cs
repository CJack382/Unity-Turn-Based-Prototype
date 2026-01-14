using UnityEngine;
using System.Collections.Generic;

//Uses own namespace for organizational purposes, don't know if good idea or not yet
namespace SinuousProductions
{
    //Very cool, allows you to add to the create menu ("Right clicking inside of your assets"), fileName is the basic name of the new Asset, and menuName is the name of the item in the Create dropdown
    //Allows you to literally create objects in Unity, with an appropriate menu and whatnot
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        
        public List<ElementType> cardType;

        public Sprite cardSprite;

        public string description;

        //Basic Placeholder typings
        public enum ElementType
        {
            Fire,
            Earth,
            Water,
            Dark,
            Light,
            Air
        }

        public enum DamageType
        {
            Fire,
            Earth,
            Water,
            Dark,
            Light,
            Air
        }

        public enum AttackPattern
        {
            Single,
            Multitarget,
            Cross,
            Column,
            Row,
            TwoByTwo,
            FourByFour
        }

        public enum PriorityTarget
        {
            Close,
            Far,
            LeastCurrentHealth,
            MostCurrentHealth,
            MostMaxHealth,
            MostDamage
        }

        public enum SpellType
        {
            Buff,
            Debuff
        }

        public enum AttributeTarget
        {
            health,
            damage,
            range,
            attackPattern,
            damageType,
            cardType,
            priorityTarget
        }
    }
}