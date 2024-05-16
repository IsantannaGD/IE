using System;
using System.Net.Mime;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotBehavior : MonoBehaviour
{
    public Action<SlotBehavior> OnItemSelected;

    [SerializeField] private ItemBase _currentItem;
    [SerializeField] private Image _imageBackground;
    [SerializeField] private Image _imageDisplay;
    [SerializeField] private TextMeshProUGUI _quantityDisplay;
    [SerializeField] private Button _button;

    [SerializeField] private Color _itemSelectedColor;

    [SerializeField] private bool _emptySlot = true;

    public int AllocatedItemID => _currentItem.ID; 
    public ItemBase GetCurrentItem() => _currentItem;
    public bool EmptySlot => _emptySlot;
    
    public void AddItemToInventory(ItemBase itemAdded)
    {
        _currentItem = itemAdded;
        _emptySlot = false;
        UpdateItemDisplay();
    }

    public void ItemQuantityChangeCallback()
    {
        UpdateItemDisplay();
    }

    public void RemoveItemFromInventory()
    {
        _currentItem = null;
        _emptySlot = true;
        UpdateItemDisplay();
    }

    public void OnSelectItemCallback(int itemSelectedID)
    {
        if(_emptySlot)
        {return;}

        if (_currentItem.ID != itemSelectedID)
        {
            ItemSelectVisualCallback(false);
        }
    }

    private void Start()
    {
        Initializations();
    }

    private void Initializations()
    {
        _button.onClick.AddListener(SelectItemHandler);
    }

    private void SelectItemHandler()
    {
        if (_emptySlot)
        { return;}

        OnItemSelected?.Invoke(this);
        ItemSelectVisualCallback(true);
    }

    private void UpdateItemDisplay()
    {
        if (_currentItem == null)
        {
            _imageDisplay.sprite = null;
            _quantityDisplay.enabled = false;
            return;
        }

        _imageDisplay.sprite = _currentItem.GetItemAppear;
        _quantityDisplay.text = _currentItem.GetItemQuantity.ToString("00");
        _quantityDisplay.enabled = true;
    }

    private void ItemSelectVisualCallback(bool status)
    {
        Color c = status ? _itemSelectedColor : Color.gray;
        _imageBackground.color = c;
    }
}
