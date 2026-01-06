using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //Singleton time yippee!!!

    private int playerHealth;
    private int playerXP;
    private int difficulty = 5;

    public OptionsManager OptionsManager {  get; private set; }
    public AudioManager AudioManager { get; private set; }
    public DeckManager DeckManager { get; private set; }

    public bool PlayingCard = false;

    private void Awake()
    {
        /* A Singleton is an instance that there can only be one of within the lifespan of an application, ideal for managing game states, configs, and data, that need to be managed from different parts of the game.
         The data it holds can ONLY be stored during the session, if persistant data is necessary, other means of persistant storage must be utilized. */

        if (Instance == null) //If the instance for GameManager doesn't exist in the scene yet, THIS instance will become the sole gameManager
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else if (Instance != this) //If the instance exists, but this is not the instance, the object this script exists within WILL be destroyed
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        //These are just GetComponent for EXCLUSIVELY, children of the specified gameObject (In this case, the game object this script is attached to).
        OptionsManager = GetComponentInChildren<OptionsManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
        DeckManager = GetComponentInChildren<DeckManager>();

        if (OptionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if (prefab == null)
            Debug.Log($"OptionsManager Prefab Not Found");
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                OptionsManager = GetComponentInChildren<OptionsManager>();
            }
        }

        if (AudioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
                Debug.Log($"AudioManager Prefab Not Found");
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                AudioManager = GetComponentInChildren<AudioManager>();
            }
        }

        if (DeckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
                Debug.Log($"DeckManager Prefab Not Found");
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                DeckManager = GetComponentInChildren<DeckManager>();
            }
        }
    }

    public int PlayerHealth //Standard Getter/Setter method. Never seen this before but it seems like it'd slightly help speed up coding. essentially just makes a setter and getter take up less space in code. When calling this, no ()
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public int PlayerXP
    { 
        get { return playerXP; }
        set {  playerXP = value; } 
    }

    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }
}
