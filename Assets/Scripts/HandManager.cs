using UnityEngine;

public class HandManager : MonoBehaviour
{
    // This script is used only to store the value of the card that is currently selected to try and see what the card can do be that
    // neutralise, defend or attack with

    public int currentValue;
    public Card cardInUse;
   [SerializeField] DeckManager deckManager;

    private void Start()
    {
        cardInUse = null;
    }

    public void ChangeCurrentValue(int i)
    {
        currentValue = i;
        print("Holding card" + currentValue);

        

    }

   

}
