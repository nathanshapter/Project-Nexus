using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue;
    [SerializeField] Sprite[] sprites; // 0 for clubs, 1 for diamond, 2 for heart, 3 for spade
    SpriteRenderer spriteRenderer;
    public bool isInHand = false;
    public bool isPlayerCard = true;
    private HandManager handManager;

   [SerializeField] DeckManager deckManager;

    

    
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
        

        // PC card
       
        deckManager.deck.Remove(this.gameObject);
        deckManager.deck.Add(this.gameObject);
        GameObject nextCard = (deckManager.deck[13]); // always plays the card in position 13 as that is the next card

        print("next card to be played " +  nextCard.name);

       // this.gameObject.SetActive(false); // this is PC card
       this.gameObject.transform.position = deckManager.deckPosition.position;
      
        
        handManager.cardInUse.gameObject.SetActive(false); // player card


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


