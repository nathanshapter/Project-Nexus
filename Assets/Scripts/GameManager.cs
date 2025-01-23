using System.Collections.Generic;
using System.Linq;
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
        if (instance == null)
        {
            // If no instance exists, set this one as the instance and prevent it from being destroyed.
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // If an instance exists and it's not this one, destroy this duplicate.
            Destroy(gameObject);
        }
        InitializeNPC();

         
    }

    private void InitializeNPC()
    {
        // Find PCBrain in the current scene
        npc = FindFirstObjectByType<PCBrain>();
        if (npc == null)
        {
            Debug.LogWarning("PCBrain not found in the current scene.");
        }
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
        DrawCardForPlayer();
    }

    void ReloadScene()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
           
        }

       
         
            playerDeckManager = FindDeckManager("PlayerDeck");
        
        
       
           computerDeckManager = FindDeckManager("PCDeck");
        
    }

    private DeckManager FindDeckManager(string managerName)
    {
        GameObject managerObject = GameObject.FindGameObjectWithTag(managerName);
        return managerObject != null ? managerObject.GetComponent<DeckManager>() : null;
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
    public void DrawCardForPlayer() // for debugging purposes
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            print("player attempted to draw cards");

            CheckRowPositions(4);
        }
    }
    private void CheckRowPositions(int rowIndex)
    {
        if (playerDeckManager.cardPositions.ContainsKey(rowIndex))
        {
            List<Vector3> positions = playerDeckManager.cardPositions[rowIndex];
            Debug.Log($"Row {rowIndex} has {positions.Count} positions stored.");

            for (int i = 0; i < playerDeckManager.nextCardsToPlay.Count; i++)
            {
                var item = playerDeckManager.nextCardsToPlay[i];
                print($"Next cards {item.name}");

                if(positions.Count > 0)
                {
                    Vector3 rowPosition = positions[0];
                    item.GetComponent<Card>().rowIndex = rowIndex;

                    item.transform.parent = playerDeckManager.row[4].transform;
                    item.transform.position = rowPosition;

                    item.GetComponent<Card>().isInHand = true;

                    positions.RemoveAt(0);

                    if(positions.Count == 0)
                    {
                        playerDeckManager.cardPositions.Remove(rowIndex);
                    }

                    if(playerDeckManager.nextCardsToPlay.Count > 0)
                    {
                        playerDeckManager.nextCardsToPlay.RemoveAt(i);
                    }
                    
                }



            }
        }
        else
        {
            Debug.Log($"Row {rowIndex} does not exist in the dictionary.");
        }
    }

}
