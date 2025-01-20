using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue;
    [SerializeField] Sprite[] sprites; // 0 for clubs, 1 for diamond, 2 for heart, 3 for spade
    SpriteRenderer spriteRenderer;
    public bool isInHand = false;
    public bool isPlayerCard = true;
    private HandManager handManager;

   public DeckManager deckManager;

    

    
    private void Start()
    {
     spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        handManager = FindFirstObjectByType<HandManager>();
        
        
    }



    private void OnMouseDown()
    {
        deckManager = GetComponentInParent<DeckManager>();


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
        if (!isPlayerCard) 
        {
            deckManager.deck.Remove(this.gameObject);
            deckManager.discardedCards.Add(this.gameObject);
            GameObject nextCard = (deckManager.deck[13]); // always plays the card in position 13 as that is the next card
            this.gameObject.transform.position = deckManager.deckPosition.position;
            this.gameObject.transform.parent = deckManager.deckPosition;
            print("next card to be played from PC" + nextCard.name);


            // player card logic

          //  handManager.cardInUse.gameObject.SetActive(false);

            handManager.cardInUse.deckManager.deck.Remove(handManager.cardInUse.gameObject);
            handManager.cardInUse.deckManager.discardedCards.Add(handManager.cardInUse.gameObject);


        } // not owrking for player card because it is not being clicked on
    
       

     
        




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


