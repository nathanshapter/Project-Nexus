using UnityEngine;

public class GameManager : MonoBehaviour
{
    // script is mainly used to turn the PC turn off an on

    public static GameManager instance { get; private set; }
    [SerializeField]  DeckManager playerDeckManager, computerDeckManager;
    public bool playerTurn = true;
    PCBrain npc;

    private void Awake()
    {
        if(instance !=null && instance == this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        npc = FindFirstObjectByType<PCBrain>();        
    }

   public void FlipTurn()
    {
        playerTurn = !playerTurn;

        if (!playerTurn)
        {
            print("computers turn now");
            npc.ProcessPCTurn();
        }

    }
}
