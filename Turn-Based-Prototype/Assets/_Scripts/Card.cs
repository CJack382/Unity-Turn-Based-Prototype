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
        
        public List<CardType> cardType;

        public int health;
        public int damageMin;
        public int damageMax;
        public Sprite cardSprite;

        public List<DamageType> damageType;

        public GameObject prefab;

        public int range;
        public AttackPattern attackPattern;
        public PriorityTarget priorityTarget;

        //Basic Placeholder typings
        public enum CardType
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
    }
}