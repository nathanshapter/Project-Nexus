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

    public bool CardUsedByPC = false;

    private int nextCardID = 13; // as there are always 14 cards in play, this ensures the next card will be played referencing the array



    public int rowIndex, playerRowIndex; // index of the row where the card was placed
    public Vector3 previousPosition, playerCardPreviousPosition; // world space position of the card before neutralised
    private void Start()
    {
     spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; // chooses a random suit for the card
        handManager = FindFirstObjectByType<HandManager>();
        
        deckManager = GetComponentInParent<DeckManager>();

        if(deckManager.GetComponent<PCBrain>() != null)
        {
            isPlayerCard = false;
        }
    }



    private void OnMouseDown()
    {
        Debug.Log($"Card selected is {name}");


        ProcessPlayerCard();
        ProcessNonPlayerCard();
    }

    private void ProcessNonPlayerCard()
    {
        // the point of this method is for the player to select the opponents cards after having chosen their own card to work with
        if (!gameObject.GetComponent<Card>().isPlayerCard)
        {

            if (handManager.currentValue == gameObject.GetComponent<Card>().cardValue) 
            {
                // if the card selected cardvalue is equal to that of the opponents card, neutralise them
                CardNeutralised();
            }
            if (handManager.currentValue > gameObject.GetComponent<Card>().cardValue) 
            {
                // if selected card value is higher than opponents card, defeat it
                CardDefeated();
            }
            else if (handManager.currentValue == 2 && gameObject.GetComponent<Card>().cardValue == 14)
            {
                // same as above except it allows 2 to take out an ACE card
                CardDefeated();
            }
        }
    }

    private void ProcessPlayerCard()
    {
        if (gameObject.GetComponent<Card>().isPlayerCard) // selets the current card as the one to consider when touching opponent card
        {
            Card currentCard = gameObject.GetComponent<Card>();
            handManager.cardInUse = currentCard;


            if (currentCard.isInHand)
            // if the card is in the players hand then it is the card used for the next action
            // stops the player from selecting a defensive card
            {
                handManager.ChangeCurrentValue(cardValue);
            }
        }
    }

    void ProcessCardRemoval(DeckManager deckManager, GameObject cardGO, string removalType)
    {
        //remove it from the playable deck, and put it into the discarded cards pile
        deckManager.deck.Remove(cardGO);
        deckManager.discardedCards.Add(cardGO);

        //remove the next card to play, and putting it into the next card to play 
        deckManager.deck.Remove(deckManager.deck[nextCardID]);
        deckManager.nextCardsToPlay.Add(deckManager.deck[nextCardID]);

        // removes the card off the playing field by position, and sets its parent to the deck
        cardGO.transform.position =deckManager.deckPosition.position;
        cardGO.transform.parent = deckManager.deckPosition;

        Debug.Log($"Card Neutralised{ deckManager.name + cardGO.name} by type of {removalType}");
    }

    private void CardNeutralised() // if cards have same value
    {
        string type = "Neutralised";
        if (!isPlayerCard) // only works if selected card of player can interact with enemy card by being equal
        {         

            ProcessCardRemoval(deckManager, this.gameObject, type);

            DeckManager deckManagerToUseForPlayer = handManager.cardInUse.deckManager;
            GameObject cardInHand = handManager.cardInUse.gameObject;

            ProcessCardRemoval(deckManagerToUseForPlayer, cardInHand, type);     

        }   
    }

   
  
    private void CardDefeated() // if card used has bigger value than other card
    {
        string type = "Defeated";
        ProcessCardRemoval(deckManager, this.gameObject, type);

        DeckManager deckManagerToUseForPlayer = handManager.cardInUse.deckManager;
        GameObject cardInHand = handManager.cardInUse.gameObject;

        ProcessCardRemoval(deckManagerToUseForPlayer, cardInHand, type);

    

        print("card defeated, your turn is over");

        GameManager.instance.FlipTurn();
    }
    private void CardDefense() // if card used to defend
    {

    }

 

  
}


