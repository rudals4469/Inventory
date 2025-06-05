using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Character Player { get; private set; }

    [SerializeField] private CharacterData characterData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetData();
    }

    private void SetData()
    {
        Player = new Character(characterData);
        UIManager.Instance.InitAllUI(Player);
    }
}