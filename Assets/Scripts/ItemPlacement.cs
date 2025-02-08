using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPlacement : MonoBehaviour
{
    public string requiredItemTag = "Pickup"; // Tag of the item that can be placed
    public GameObject playerHead; // Assign this in the Inspector for raycasting
    public EnigmeChecker enigmeChecker; // Add this field

    private GameObject currentItem;
    public float placementRayDistance = 4f; // Maximum distance for detecting a placement


    private string extractItemSuffix(string item_name)
    {
        Debug.Log("neededobject_name = " + item_name + " neededobject_name.Length = " + item_name.Length);
        return (item_name[item_name.Length - 2].ToString() + item_name[item_name.Length - 1].ToString());
    }

    public bool IsGoodItem(string PlacementName, string ItemName)
    {
        return extractItemSuffix(PlacementName) == (extractItemSuffix(ItemName));
    }

    public bool PlaceItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerHead.transform.position, playerHead.transform.forward, out hit, placementRayDistance))
        {
            if(hit.transform.CompareTag("Placement") && currentItem != null && currentItem.CompareTag(requiredItemTag))
            {
                if(hit.transform.childCount > 0)
                {
                    return false;
                }
                currentItem.transform.SetParent(hit.transform);
                currentItem.transform.position = hit.transform.position;
                currentItem.transform.rotation = hit.transform.rotation;
                currentItem.GetComponent<Rigidbody>().isKinematic = false;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                // Use the public enigmeChecker instead of FindObjectOfType
                if(enigmeChecker != null && IsGoodItem(hit.transform.name, currentItem.name))
                {
                    enigmeChecker.IncrementCurrent();
                }
                Debug.Log("Current item = " + currentItem + " needed object = " + currentItem.name);
                currentItem = null;
                return true;
            }
        }
        return false;
    }

    public void SetCurrentItem(GameObject item)
    {
        currentItem = item;
    }
}

