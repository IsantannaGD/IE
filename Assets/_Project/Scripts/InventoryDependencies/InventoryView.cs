using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public Action OnDropItem;
    public Action<int> OnSelectSlot;

    [SerializeField] private PanelMovementController _mainPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _dropItemButton;
    [SerializeField] private List<SlotBehavior> _inventorySlots = new List<SlotBehavior>();

    [SerializeField] private SlotBehavior _currentSlotSelected;
    
    public Button DropItemButton => _dropItemButton;
    [CanBeNull] public ItemBase CurrentItemSelected => _currentSlotSelected.GetCurrentItem();

    public void CallInventory()
    {
        if(_mainPanel.IsClosed && GameManager.GamePaused)
        {return;}

        GameManager.OnGamePause?.Invoke();
        _mainPanel.MovementManagerCallback(ActiveControlInventoryPanel);
    }

    public void AddNewItem(bool isIncreasingQuantity, ItemBase item)
    {
        if (isIncreasingQuantity)
        {
            foreach (SlotBehavior slot in _inventorySlots)
            {
                if (slot.GetCurrentItem() != null && item.ID == slot.AllocatedItemID)
                {
                    slot.ItemQuantityChangeCallback();
                    return;
                }
            }
        }

        foreach (SlotBehavior slot in _inventorySlots)
        {
            if (slot.EmptySlot)
            {
                slot.AddItemToInventory(item);
                return;
            }
        }
    }

    public void RemoveItem(bool isDecreaseQuantity, ItemBase item)
    {
        foreach (SlotBehavior slot in _inventorySlots)
        {
            if (!slot.EmptySlot && slot.AllocatedItemID == item.ID)
            {
                if (isDecreaseQuantity)
                {
                    slot.ItemQuantityChangeCallback();
                    return;
                }

                _currentSlotSelected = null;
                OnSelectSlot?.Invoke(0);
                slot.RemoveItemFromInventory();
            }
        }
    }

    private void Start()
    {
        Initializations();
    }

    private void OnEnable()
    {
        _currentSlotSelected = null;
        OnSelectSlot?.Invoke(0);
    }

    private void Initializations()
    {
        _closeButton.onClick.AddListener(CallInventory);
        SetupInventorySlots();
    }

    private void ActiveControlInventoryPanel()
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeInHierarchy);
    }

    private void SetupInventorySlots()
    {
        foreach (SlotBehavior slot in _inventorySlots)
        {
            slot.OnItemSelected += ItemSelectedCallback;
            OnSelectSlot += slot.OnSelectItemCallback;
        }

        ActiveControlInventoryPanel();
    }

    private void ItemSelectedCallback(SlotBehavior slotSelected)
    {
        _currentSlotSelected = slotSelected;
        OnSelectSlot?.Invoke(_currentSlotSelected.AllocatedItemID);
    }
}
