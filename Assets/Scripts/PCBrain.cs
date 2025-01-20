using UnityEngine;

public class PCBrain : MonoBehaviour
{


    DeckManager deckManager;
  [SerializeField]  GameObject PCHand;

  [SerializeField]  Card[] cardsInPCHand;



    private void Start()
    {
        deckManager = GetComponent<DeckManager>();
    }
    public void PCTurnStarted()
    {
        print("pc turn");
        cardsInPCHand = PCHand.GetComponentsInChildren<Card>();


    }
}
