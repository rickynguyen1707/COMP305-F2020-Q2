using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class LevelController : MonoBehaviour
{
    // Singleton
    private static LevelController _instance;
    public static LevelController Instance 
    { 
        get 
        { 
            return _instance; 
        } 
    }

    // Public variables
    [SerializeField] private Text itemUIText;
    [SerializeField] private PlayableDirector pDir;
    [SerializeField] private PlayableDirector pDir2;

    // Private variables
    private int totalItemsQty = 0, itemsCollectedQty = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        pDir.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalItemsQty = GameObject.FindGameObjectsWithTag("Item").Length;
        // Debug.Log("There are " + totalItemsQty + " items in this level.");
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        itemUIText.text = itemsCollectedQty + " / " + totalItemsQty;
    }

    public void PickedUpItem()
    {
        itemsCollectedQty++;
        // Debug.Log("Number of items collected: " + itemsCollectedQty);
        UpdateItemUI();
    }

    public void CheckLevelEnd()
    {
        if(itemsCollectedQty == totalItemsQty)
        {
            // Play an animation of the character jumping up and down
            // Play level end audio
            pDir2.Stop();
            // Show level end UI
            pDir.enabled = true;
        }
    }
}
