using UnityEngine;
using SinuousProductions;
using System.Collections.Generic;

//Very cool, allows you to add to the create menu ("Right clicking inside of your assets"), fileName is the basic name of the new Asset, and menuName is the name of the item in the Create dropdown
//Allows you to literally create objects in Unity, with an appropriate menu and whatnot
[CreateAssetMenu(fileName = "New Character Card", menuName = "Card/Character")]
public class Character : Card
{
    public int health;
    public int damageMin;
    public int damageMax;

    public List<ElementType> damageType;

    public GameObject prefab;

    public int range;
    public AttackPattern attackPattern;
    public PriorityTarget priorityTarget;
}
