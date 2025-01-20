using UnityEngine;

public class PCBrain : MonoBehaviour // script is used to take care of all of the information the PC has
{

    DeckManager deckManager; 
    [SerializeField] GameObject PCHand; // holds the PC hand

    [SerializeField] Card[] cardsInPCHand; // references the cards in PC hand

    [SerializeField] GameObject[] playerRow; // references the rows of the player

    [SerializeField] Card[] cardsInPlayerFirstRow; // references the cards in the first row of the player, set in inspector

   

    private void Start()
    {
        deckManager = GetComponent<DeckManager>();
    }
    public void ProcessPCTurn()
    {
        print("pc turn has started");

        // gets cards in computer hand and player first row
        cardsInPCHand = PCHand.GetComponentsInChildren<Card>();
        cardsInPlayerFirstRow = playerRow[0].GetComponentsInChildren<Card>();




        FindMatches();

        LookForLargerCard();


    }

    private void FindMatches() // looks for cards that it can equalise
    {
        foreach (Card pcCard in cardsInPCHand)
        {
            bool matchFound = false;

            foreach (Card playerCard in cardsInPlayerFirstRow) // for every card in PC hand, see if it can equalise, if it can, do that
            {
                if (pcCard.cardValue == playerCard.cardValue)
                {
                    print($"Match found! {pcCard.name} matches {playerCard.name}");
                    NeutraliseCards(playerCard, pcCard, false);
                    matchFound = true;
                    
                }
              
            }

            // If no match is found for this PC card, you can look for larger cards or handle other logic
            if (!matchFound )
            {
                print($"No match found for {pcCard.name}. Looking for a larger card.");
               
            }
        }
    }

    void LookForLargerCard() // looks for cards that it can defeat by virture of having a larger card
        // the goal is to have it randomised with values. if they are on the last row the the pc is more likely to protect than to attack
    {
     

        foreach (Card pcCard in cardsInPCHand) 
        {
            bool foundStrongerCard = false;

            foreach (Card playerCard in cardsInPlayerFirstRow) // for every card in PC hand, try and see if it has a larger card than palyer
            {
                if (pcCard.cardValue > playerCard.cardValue) // once it has found a stronger card it will use it to destroy both cards
                {
                    print($"higher card found {pcCard.name} is bigger than {playerCard.name}");
                    NeutraliseCards(playerCard, pcCard, true);
                    foundStrongerCard = true;
                    return;

                }
                if(!foundStrongerCard) // if it does not have a stronger card then it will go to defend
                {
                    print("no larger card found");

                    LookForCardToDefendWith();
                }
            }
        }

       

    }

    void LookForCardToDefendWith()
    {
        // to be implemented
    }
    void NeutraliseCards(Card playerCard, Card PCCard, bool turnOver) // turn continues if cards are equal, if not, it will end, bool used to indicate turn over or not
    {
        // if the card has already been used to equalise or as a larger card, return

        if (PCCard.CardUsedByPC)
            return;

        PCCard.CardUsedByPC = true;

        PCCard.deckManager.deck.Remove(PCCard.gameObject);
        PCCard.deckManager.discardedCards.Add(PCCard.gameObject);

        PCCard.deckManager.nextCardsToPlay.Add(deckManager.deck[13]);
        PCCard.deckManager.deck.Remove(deckManager.deck[13]);

        PCCard.gameObject.transform.position = PCCard.deckManager.deckPosition.position;
        PCCard.gameObject.transform.parent = PCCard.deckManager.deckPosition;




        playerCard.deckManager.deck.Remove(playerCard.gameObject);
        playerCard.deckManager.discardedCards.Add(playerCard.gameObject);

        playerCard.deckManager.nextCardsToPlay.Add(deckManager.deck[13]);
        playerCard.deckManager.deck.Remove(deckManager.deck[13]);

        playerCard.transform.parent = playerCard.deckManager.deckPosition.transform;
        playerCard.transform.position = playerCard.deckManager.deckPosition.position;

     //   playerCard.gameObject.SetActive(false);
     //   PCCard.gameObject.SetActive(false);
        if(turnOver)
        {
            print("PC turn is over");
        }


    }
}
