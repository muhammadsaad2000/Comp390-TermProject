using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potions Object", menuName = "Inventory System/Items/Potions")]
public class PotionsObject : ItemObject
{
    public float restoreHPValue;

    private void Awake()
    {
        type = ItemType.Potions;
    }
}
