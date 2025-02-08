using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public string requiredItemTag = "Pickup"; // Tag of the item that can be placed
    public GameObject neededobject; // Object to trigger an action on
    public GameObject playerHead; // Assign this in the Inspector for raycasting
    public EnigmeChecker enigmeChecker; // Add this field

    private GameObject currentItem;
    public float placementRayDistance = 4f; // Maximum distance for detecting a placement

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
                if(enigmeChecker != null && currentItem == neededobject)
                {
                    enigmeChecker.IncrementCurrent();
                }
                Debug.Log("Current item = " + currentItem + " needed object = " + neededobject);
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

