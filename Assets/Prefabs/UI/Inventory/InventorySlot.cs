using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    InventoryComponent item;
    [SerializeField]Image buttonImage;
    [SerializeField] Sprite emptySprite;

    private void Start()
    {
        if(buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
            if(buttonImage == null)
            {
                buttonImage = GetComponentInChildren<Image>();
            }
        }
        if(buttonImage == null)
        {
            Debug.LogError($"Inventory slot: {gameObject.name} cannot find an image component");
        }
        else
        {
            emptySprite = buttonImage.sprite;
        }
    }
    public bool IsSlotEmpty()
    {
        return item == null;
        
    }

    public void StoreItem(InventoryComponent newItem)
    {
        item = newItem;
        buttonImage.sprite = item.GetIcon();
        item.DroppedDown();
    }

    public InventoryComponent TakeOutItem()
    {
        item.PickedUp();
        buttonImage.sprite = emptySprite;
        InventoryComponent returnItem = item;
        item = null;
        return returnItem;
    }
}
