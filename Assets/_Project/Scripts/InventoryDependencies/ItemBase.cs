using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObject/Item", order = 1)]
[Serializable]
public class ItemBase : ScriptableObject
{
    [SerializeField] protected int _id;
    [SerializeField] protected Sprite _itemAppear;

    [SerializeField] protected string _itemName;
    [SerializeField] protected int _quantity;

    [SerializeField] protected GameObject _itemPrefab;

    public int ID => _id;
    public Sprite GetItemAppear => _itemAppear;
    public string GetItemName => _itemName;
    public int GetItemQuantity => _quantity;

    public GameObject Prefab => _itemPrefab;

    public void ChangeQuantityCallback(bool isAdding)
    {
        if (isAdding)
        {
            _quantity++;
            return;
        }

        _quantity--;
    }
}
