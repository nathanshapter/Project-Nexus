using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        ReloadScene();
    }

    void ReloadScene()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            SceneManager.LoadScene(0);
        }

        
    }

    public void PlayersTakeNextCards(DeckManager deckManager)
    {
        foreach (var item in deckManager.nextCardsToPlay)
        {
            print($"Card in reserve for {deckManager.name} + {item.name}");
        }
        // when the card is taking out of the hand, the next card needs to be put into the player hand row. the previous card needs to have
        // its vector 3 saved. once the card is ready to be placed there it needs only to access the list of vector3 to be replaced in order


    }
}
