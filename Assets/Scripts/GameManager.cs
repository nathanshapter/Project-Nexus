using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance { get; private set; }
  [SerializeField]  DeckManager playerDeckManager, computerDeckManager;
    public bool playerTurn = true;

    private void Awake()
    {
        if(instance !=null && instance == this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);


        
    }

   public void FlipTurn()
    {
        playerTurn = !playerTurn;


        if (!playerTurn)
        {
            print("computers turn");
        }



    }
}
