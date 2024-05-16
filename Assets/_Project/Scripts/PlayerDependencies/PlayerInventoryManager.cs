using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private List<ItemBase> _inventory;

    [SerializeField] private Transform _dropSpawnPoint;

    public void AddItem(ItemBase item)
    {
        foreach (ItemBase currentItem in _inventory)
        {
            if (item.ID == currentItem.ID)
            {
                currentItem.ChangeQuantityCallback(true);
                _inventoryView.AddNewItem(true, currentItem);
                return;
            }
        }

        item.ChangeQuantityCallback(true);
        _inventory.Add(item);
        _inventoryView.AddNewItem(false, item);
    }

    private void Start()
    {
        Initializations();
    }

    private void Initializations()
    {
        _inventory = new List<ItemBase>();
        _inventoryView.DropItemButton.onClick.AddListener(() => RemoveItem(_inventoryView.CurrentItemSelected));
    }

    private void RemoveItem(ItemBase item = null)
    {
        if (_inventoryView.CurrentItemSelected == null)
        { return; }

        foreach (ItemBase currentItem in _inventory)
        {
            if (item.ID == currentItem.ID)
            {
                bool s = currentItem.GetItemQuantity > 1;
                
                currentItem.ChangeQuantityCallback(false);
                _inventoryView.RemoveItem(s, currentItem);

                Instantiate(item.Prefab, _dropSpawnPoint.position, Quaternion.identity);
            }
        }
    }
}
