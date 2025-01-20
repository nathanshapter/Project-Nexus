using UnityEngine;

public class PCBrain : MonoBehaviour
{


    DeckManager deckManager;
    [SerializeField] GameObject PCHand;

    [SerializeField] Card[] cardsInPCHand;

    [SerializeField] GameObject[] opponentRow;

    [SerializeField] Card[] cardsInOpponentFirstRow;

   

    private void Start()
    {
        deckManager = GetComponent<DeckManager>();
    }
    public void ProcessPCTurn()
    {
        print("pc turn has started");
        cardsInPCHand = PCHand.GetComponentsInChildren<Card>();

        cardsInOpponentFirstRow = opponentRow[0].GetComponentsInChildren<Card>();




        FindMatches();

        LookForLargerCard();


    }

    private void FindMatches()
    {
        foreach (Card pcCard in cardsInPCHand)
        {
            bool matchFound = false;

            foreach (Card playerCard in cardsInOpponentFirstRow)
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

    void LookForLargerCard()
    {
     

        foreach (Card pcCard in cardsInPCHand)
        {
            bool foundStrongerCard = false;

            foreach (Card playerCard in cardsInOpponentFirstRow)
            {
                if (pcCard.cardValue > playerCard.cardValue)
                {
                    print($"higher card found {pcCard.name} is bigger than {playerCard.name}");
                    NeutraliseCards(playerCard, pcCard, true);
                    foundStrongerCard = true;
                    return;

                }
                if(!foundStrongerCard)
                {
                    print("no larger card found");
                }
            }
        }

       

    }
    void NeutraliseCards(Card playerCard, Card PCCard, bool turnOver) // turn continues if cards are equal, if not, it will end
    {
        playerCard.gameObject.SetActive(false);
        PCCard.gameObject.SetActive(false);
        if(turnOver)
        {
            print("PC turn is over");
        }


    }
}
