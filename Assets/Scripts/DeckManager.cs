using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] bool playerDeck = true;


   [SerializeField] GameObject[] cardsToPickFrom;

  [SerializeField]  List<GameObject> deck;

    private int numberOfCardsInDeck = 26;


    [SerializeField] Transform deckPosition;

    [SerializeField] Transform[] playingCardPosition;

    private int handSize = 14;

    [SerializeField] Sprite backOfCardSprite;

    [SerializeField] float timeInBetweenCard = 0.3f;

    private void Start()
    {
        for (int i = 0; i < playingCardPosition.Length; i++) // deactivates the playing cards visuals
        {
            playingCardPosition[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < numberOfCardsInDeck; i++)  // creates the deck
        {

            GameObject cardPrefab = cardsToPickFrom[i % cardsToPickFrom.Length];

            GameObject instantiatedCard = Instantiate(cardPrefab, deckPosition);

            deck.Add(instantiatedCard);
           
            
        }
      

        ShuffleDeck();

        StartCoroutine(PlaceCardsInHand(playerDeck));
    }

    void ShuffleDeck()
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
        int cardsToPlace = Mathf.Min(handSize, deck.Count, playingCardPosition.Length);
        for (int i = 0; i < cardsToPlace; i++) 
        {
            yield return new WaitForSeconds(timeInBetweenCard);

        

            if(i >= 10)
            {
                if (!playerDeck)
                {
                    deck[i].gameObject.GetComponent<SpriteRenderer>().sprite = backOfCardSprite;
                }
                deck[i].gameObject.GetComponent<Card>().isInHand = true;
            }
            deck[i].gameObject.GetComponent<Card>().isPlayerCard = isPlayerCard; 


            deck[i].transform.position = playingCardPosition[i].transform.position;
        }
       

       
       

     
    }
}

