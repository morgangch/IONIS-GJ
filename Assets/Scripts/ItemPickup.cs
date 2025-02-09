using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public Transform playerHand; // Assign this in the Inspector
    public GameObject playerHead; // Assign this in the Inspector
    private GameObject currentItem;
    private ItemPlacement itemPlacement;

    void Start()
    {
        itemPlacement = FindObjectOfType<ItemPlacement>();
    }

    public GameObject GetCurrentItem()
    {
        return currentItem;
    }

    public void SetCurrentItem(GameObject item)
    {
        currentItem = item;
    }

    public ItemPlacement GetItemPlacement()
    {
        return itemPlacement;
    }

    public void PickupItem(RaycastHit hit)
    {
        GameObject targetItem = hit.transform.gameObject;
        if(targetItem.transform.parent != null && targetItem.transform.parent.CompareTag("Placement"))
        {
            if(itemPlacement.enigmeChecker != null && itemPlacement.IsGoodItem(targetItem.transform.parent.name, targetItem.name))
            {
                itemPlacement.enigmeChecker.DecrementCurrent();
            }
            targetItem.transform.SetParent(null);
        }
        currentItem = targetItem;
        currentItem.transform.SetParent(playerHand);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
        currentItem.GetComponent<Rigidbody>().isKinematic = true;
        currentItem.GetComponent<Rigidbody>().useGravity = true;
        currentItem.GetComponent<MeshCollider>().enabled = false;
        if (itemPlacement)
            itemPlacement.SetCurrentItem(currentItem);
}

    public void DropItem()
    {
        currentItem.transform.SetParent(null);
        currentItem.GetComponent<Rigidbody>().isKinematic = false;
        currentItem.GetComponent<Rigidbody>().useGravity = true;
        currentItem.GetComponent<MeshCollider>().enabled = true;
        currentItem = null;
    }
}
