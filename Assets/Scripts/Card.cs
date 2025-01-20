using UnityEngine;

public class Card : MonoBehaviour
{
    // card holds almost all of its data, it is clicked with a box collider and takes care of itseld from there

    public int cardValue; // 2 is 0, etc
    [SerializeField] Sprite[] sprites; // 0 for clubs, 1 for diamond, 2 for heart, 3 for spade
    SpriteRenderer spriteRenderer; // in case I want to specify what sprite is being used
    public bool isInHand = false; // if the card is in the hand of the PC or the player, and can be used
    public bool isPlayerCard = true; // so the card knows whether or not it is owned by the player or the PC
    private HandManager handManager;

   public DeckManager deckManager;

    

    
    private void Start()
    {
     spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; // chooses a random suit for the card
        handManager = FindFirstObjectByType<HandManager>();
        
        
    }



    private void OnMouseDown()
    {
        deckManager = GetComponentInParent<DeckManager>(); // easiest to put the deck manager here, as they spawn at different times, doesnt need deckmanager before it is clicked



        Debug.Log(name);

        if (gameObject.GetComponent<Card>().isPlayerCard) // selets the current card as the one to loo kat
        {
            Card currentCard = gameObject.GetComponent<Card>();
            handManager.cardInUse = currentCard;
            

            if (currentCard.isInHand)  // if the card is in the players hand then it is the card used for the next action
            {
                handManager.ChangeCurrentValue(cardValue);
            }
        }

       

        if (!gameObject.GetComponent<Card>().isPlayerCard   )  
        {

            if(handManager.currentValue == gameObject.GetComponent<Card>().cardValue) // if the card selected cardvalue is equal to that of the 
                //opponents card, neutralise them
            {
                CardNeutralised();
            }
            if (handManager.currentValue > gameObject.GetComponent<Card>().cardValue) // if selected card value is higher than opponents card,
                // defeat it
            {
               CardDefeated();
            }


        }    
    }
    private void CardNeutralised() // if cards have same value
    {
        if (!isPlayerCard) // only works if selected card of player can interact with enemy card by being equal
        {
            deckManager.deck.Remove(this.gameObject); // removes it from deck, puts into discarded deck
            deckManager.discardedCards.Add(this.gameObject);
            GameObject nextCard = (deckManager.deck[13]); // always plays the card in position 13 as that is the next card
            this.gameObject.transform.position = deckManager.deckPosition.position; // moves it out of view
            this.gameObject.transform.parent = deckManager.deckPosition; // gives it its new parent


            // at end of players turn, they will draw their new cards
            print("next card to be played from PC" + nextCard.name);


            // player card logic

          

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


