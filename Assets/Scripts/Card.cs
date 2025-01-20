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
            handManager.cardInUse = currentCard;

            if (currentCard.isInHand)
            {
                handManager.ChangeCurrentValue(cardValue);
            }
        }

       

        if (!gameObject.GetComponent<Card>().isPlayerCard   ) 
        {

            if(handManager.currentValue == gameObject.GetComponent<Card>().cardValue)
            {
                CardNeutralised();
            }
            if (handManager.currentValue > gameObject.GetComponent<Card>().cardValue)
            {
               CardDefeated();
            }








        }
       





    }
    private void CardNeutralised() // if cards have same value
    {
        // these need to not be set to inactive, but instead to paly the next card

        this.gameObject.SetActive(false);
        handManager.cardInUse.gameObject.SetActive(false);


        print("cards neutralised");
    }
    private void CardDefeated() // if card used has bigger value than other card
    {
        this.gameObject.SetActive(false);
        handManager.cardInUse.gameObject.SetActive(false);

        print("card defeated, your turn is over");

        GameManager.instance.FlipTurn();
    }
    private void CardDefense() // if card used to defend
    {

    }

 

  
}


