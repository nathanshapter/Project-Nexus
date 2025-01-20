using UnityEngine;

public class PCBrain : MonoBehaviour
{


    DeckManager deckManager;
  [SerializeField]  GameObject PCHand;

  [SerializeField]  Card[] cardsInPCHand;

    [SerializeField] GameObject[] opponentRow;

    [SerializeField] Card[] cardsInOpponentFirstRow;

    private void Start()
    {
        deckManager = GetComponent<DeckManager>();
    }
    public void PCTurnStarted()
    {
        print("pc turn");
        cardsInPCHand = PCHand.GetComponentsInChildren<Card>();

        cardsInOpponentFirstRow = opponentRow[0].GetComponentsInChildren<Card>();




        FindMatches();


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
                    MatchFound(playerCard, pcCard);
                    matchFound = true;
                }
            }

            // If no match is found for this PC card, you can look for larger cards or handle other logic
            if (!matchFound)
            {
                print($"No match found for {pcCard.name}. Looking for a larger card.");
                //LookForLargerCard(pcCard);
            }
        }
    }


    void MatchFound(Card playerCard, Card PCCard)
    {
        playerCard.gameObject.SetActive(false);
        PCCard.gameObject.SetActive(false);
      
    }

}
