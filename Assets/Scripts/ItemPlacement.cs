using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public string requiredItemTag = "Pickup"; // Tag of the item that can be placed
    public GameObject actionObject; // Object to trigger an action on
    public GameObject playerHead; // Assign this in the Inspector for raycasting

    private GameObject currentItem;
    public float placementRayDistance = 4f; // Maximum distance for detecting a placement

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed");
            PlaceItem();
        }
    }

    void PlaceItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerHead.transform.position, playerHead.transform.forward, out hit, placementRayDistance))
        {
            // Expect placement targets to have the "Placement" tag.
            if(hit.transform.CompareTag("Placement") && currentItem != null && currentItem.CompareTag(requiredItemTag))
            {
                // Place the item at the hit transform.
                currentItem.transform.SetParent(null);
                currentItem.transform.position = hit.transform.position;
                currentItem.transform.rotation = hit.transform.rotation;
                currentItem.GetComponent<Rigidbody>().isKinematic = false;
                Debug.Log("Placed item: " + currentItem.name + " at " + hit.transform.name);
                TriggerAction(hit.transform);
                currentItem = null;
            }
            else
            {
                Debug.Log("No valid placement target in sight.");
            }
        }
        else
        {
            Debug.Log("Placement raycast did not hit any target.");
        }
    }

    void TriggerAction(Transform target)
    {
        if (actionObject != null)
        {
            // Example action: Activate the actionObject.
            actionObject.SetActive(true);
            Debug.Log("Action triggered on: " + actionObject.name + " by placement " + target.name);
        }
    }

    public void SetCurrentItem(GameObject item)
    {
        currentItem = item;
    }
}
