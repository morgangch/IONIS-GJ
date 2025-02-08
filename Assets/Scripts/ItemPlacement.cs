using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public string requiredItemTag = "Pickup"; // Tag of the item that can be placed
    public GameObject actionObject; // Object to trigger an action on
    public GameObject playerHead; // Assign this in the Inspector for raycasting

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
                TriggerAction(hit.transform);
                currentItem = null;
                return true;
            }
        }
        return false;
    }

    void TriggerAction(Transform target)
    {
        if (actionObject != null)
        {
            actionObject.SetActive(true);
        }
    }

    public void SetCurrentItem(GameObject item)
    {
        currentItem = item;
    }
}

