using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;

    public int mySelectedCharacter;

    public GameObject[] allCharacters;

    private void Awake()
    {
        if (PI == null)
        {
            PI = this;
        }
        else
        {
            if (PI != this)
            {
                Destroy(PI.gameObject);
                PI = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedCharacter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
