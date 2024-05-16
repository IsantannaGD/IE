using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void HighLightInteractableObject(bool status);
    public void InteractionCallback(Player player = null);
}
