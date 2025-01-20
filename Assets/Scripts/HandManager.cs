using UnityEngine;

public class HandManager : MonoBehaviour
{
    public int currentValue;
    public Card cardInUse;

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
