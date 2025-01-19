using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue;
    [SerializeField] Sprite[] sprites; // 0 for clubs, 1 for diamond, 2 for heart, 3 for spade
    SpriteRenderer spriteRenderer;
    public bool isInHand = false;
    public bool isPlayerCard = true;
    private HandManager handManager;

    private void Start()
    {
     spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        handManager = FindFirstObjectByType<HandManager>();
    }



    private void OnMouseDown()
    {

        Debug.Log(name);

        if (gameObject.GetComponent<Card>().isPlayerCard)
        {
            Card currentCard = gameObject.GetComponent<Card>();


            if (currentCard.isInHand)
            {
                handManager.ChangeCurrentValue(cardValue);
            }
        }
        if (!gameObject.GetComponent<Card>().isPlayerCard && handManager.currentValue == gameObject.GetComponent<Card>().cardValue) 
        {
            print("cards neutralised");
        }





    }
    private void CardNeutralised() // if cards have same value
    {

    }
    private void CardDefeated() // if card used has bigger value than other card
    {

    }
    private void CardDefense() // if card used to defend
    {

    }
}


