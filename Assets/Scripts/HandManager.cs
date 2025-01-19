using UnityEngine;

public class HandManager : MonoBehaviour
{
    public int currentValue;


   public void ChangeCurrentValue(int i)
    {
        currentValue = i;
        print("Holding card" + currentValue);
    }



}
