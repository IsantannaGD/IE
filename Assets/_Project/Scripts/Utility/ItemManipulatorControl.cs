using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemManipulatorControl : MonoBehaviour, IInteractable
{
   [SerializeField] private string _messageToShow;

   [SerializeField] private ItemBase _itemData;
   [SerializeField] private List<PersonalOutline> _itemOutlines;

   private void Start()
   {
      Initializations();
   }

   private void Initializations()
   {

   }

   public void HighLightInteractableObject(bool status)
   {
      GameManager.OnShowMessage?.Invoke(_messageToShow, status);
      OutlineControlCallback(status);
   }

   private void OutlineControlCallback(bool status)
   {
      if (status)
      {
         foreach (PersonalOutline outline in _itemOutlines)
         {
            outline.OutlineWidth = 10f;
         }

         return;
      }

      foreach (PersonalOutline outline in _itemOutlines)
      {
         outline.OutlineWidth = 0f;
      }
   }

   public void InteractionCallback(Player player = null)
   {
      player.PlayerInventoryManager.AddItem(_itemData);
      gameObject.SetActive(false);
      Destroy(this);
   }
}
