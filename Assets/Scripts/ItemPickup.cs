using UnityEngine;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentItem == null)
            {
                PickupItem();
            }
            else
            {
                if (itemPlacement.PlaceItem())
                {
                    currentItem = null;
                }
                else
                {
                    DropItem();
                }
            }
        }
    }

    void PickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerHead.transform.position, playerHead.transform.forward, out hit, 4f))
        {
            if (hit.transform.CompareTag("Pickup"))
            {
                currentItem = hit.transform.gameObject;
                currentItem.transform.SetParent(playerHand);
                currentItem.transform.localPosition = Vector3.zero;
                currentItem.transform.localRotation = Quaternion.identity;
                currentItem.GetComponent<Rigidbody>().isKinematic = true;
                currentItem.GetComponent<Rigidbody>().useGravity = true;
                itemPlacement.SetCurrentItem(currentItem);
            }
        }
    }

    public void DropItem()
    {
        currentItem.transform.SetParent(null);
        currentItem.GetComponent<Rigidbody>().isKinematic = false;
        currentItem = null;
    }
}
