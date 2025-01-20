using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] bool playerDeck = true; 

   [SerializeField] GameObject[] cardsToPickFrom; // holds the 13 types of cards that are used

  public  List<GameObject> deck; // holds the deck of player or PC cards

    private int numberOfCardsInDeck = 26;


    public Transform deckPosition; // holds the position of cards that are not in play

    [SerializeField] Transform[] playingCardPosition; // holds the position of where to put the cards when they are in play

    private int handSize = 14;

    [SerializeField] Sprite backOfCardSprite;

    [SerializeField] float timeInBetweenCard = 0.3f;

    [SerializeField] GameObject[] row; // holds the rows

    public List<GameObject> discardedCards; // holds cards that have been taken out of play, will re - enter after a shuffle

    public List<GameObject> nextCardsToPlay; // makes a list of the next cards that will come in to play

    
    private void Start()
    {
        for (int i = 0; i < playingCardPosition.Length; i++) // deactivates the playing cards positions visuals (alll the default 2 on screen)
        {
            playingCardPosition[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < numberOfCardsInDeck; i++)  // creates the deck for player and PC
        {

            GameObject cardPrefab = cardsToPickFrom[i % cardsToPickFrom.Length];

            GameObject instantiatedCard = Instantiate(cardPrefab, deckPosition);

            deck.Add(instantiatedCard);
           
            
        }
      

        ShuffleDeck();

        StartCoroutine(PlaceCardsInHand(playerDeck));
    }
    void ChooseRowPosition(GameObject row)
    {

    }
    void ShuffleDeck() // shuffles the cards so they are not in order
    {
       

        for (int i = deck.Count - 1; i >= 0; i--) 
        {
            int randomIndex = Random.Range(0, i + 1); // gets a random number of remaining values

            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
    private IEnumerator PlaceCardsInHand(bool isPlayerCard)
    {
        int cardsToPlace = Mathf.Min(handSize, deck.Count, playingCardPosition.Length); // places all cards into play
        for (int i = 0; i < cardsToPlace; i++)
        {
            yield return new WaitForSeconds(timeInBetweenCard);



            if (i >= 10)
            {
                if (!playerDeck)
                {
                    deck[i].gameObject.GetComponent<SpriteRenderer>().sprite = backOfCardSprite; // places cards for PC in a way they cannot be seen by player
                }
                deck[i].gameObject.GetComponent<Card>().isInHand = true;
            }
            deck[i].gameObject.GetComponent<Card>().isPlayerCard = isPlayerCard;


            deck[i].transform.position = playingCardPosition[i].transform.position;

            PlaceCardsInRow(i);
        }






    }

    private void PlaceCardsInRow(int i)
    {
        if (i <= 3) // puts them in the correct row based on their sequence in play
        {
            deck[i].transform.parent = row[0].transform;
        }


        if (i > 3 && i <= 6)
        {
            deck[i].transform.parent = row[1].transform;
        }

        if (i > 6 && i <= 8)
        {
            deck[i].transform.parent = row[2].transform;
        }
        if (i == 9)
        {
            deck[i].transform.parent = row[3].transform;
        }
        if (i > 9)
        {
            deck[i].transform.parent = row[4].transform;
        }
    }

}

