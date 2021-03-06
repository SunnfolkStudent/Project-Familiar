using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class InventorySystem : MonoBehaviour
{
    public GameObject[] ItemPrefabs;
    public GameObject currentItem;
    public bool canPickup;
    public bool canDrop;
    public AudioClip PickUp;
    public AudioClip Drop;
    private AudioSource m_Audiosource;

    private PlayerInput _input;

   [SerializeField] private GameObject prefabObject;
   [SerializeField] private GameObject colliderObject;
   [SerializeField] private float itemDespawnTimer= 5f;
   public Health Health;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        m_Audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Health.IsDead) return;
        if (_input.interact && canPickup && currentItem == null)
        {
            currentItem = prefabObject;
            Destroy(colliderObject);
            m_Audiosource.PlayOneShot(PickUp);

            prefabObject = null;
            colliderObject = null;
            canDrop = true;
        }
        else if (_input.interact && canDrop)
        {
            var clone = Instantiate(currentItem, transform.position, quaternion.identity);
            Destroy(clone,itemDespawnTimer);
            currentItem = null;
            canDrop = false;
            m_Audiosource.PlayOneShot(Drop);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentItem != null) return;
        
        foreach (var item in ItemPrefabs)
        {
            if (item == null) continue;
            if (other.CompareTag(item.tag))
            {
                canPickup = true;
                prefabObject = item;
                colliderObject = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPickup = false;
        prefabObject = null;
        colliderObject = null;
    }
}
