using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheatOrbs : MonoBehaviour
{
    public string[] cheatCodeOrbs = new string[] { "o", "r", "b", "p", "l", "s" };
    private int indexCheat;
    public GameObject[] orbs;
    // Start is called before the first frame update
    void Start()
    {
        indexCheat = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCodeOrbs[indexCheat]))
            {
                indexCheat++;
            }
            else
            {
                indexCheat = 0;
            }
        }

        if (indexCheat == cheatCodeOrbs.Length)
        {
            print("Spawning all orbs from cheats");
            Vector3 nowPosition = transform.position;
            
            // Distance from the player
            float distance = 2.0f; 
            Quaternion rotation = Quaternion.Euler(0, 0, 0);

            for (int i = 0; i < orbs.Length; i++)
            {
                GameObject orb = orbs[i];
                Vector3 position = nowPosition;

                if (i == 0) position += transform.forward * distance;
                else if (i == 1) position += - transform.right * distance;
                else if (i == 2) position += transform.right * distance;
                
                position.y +=0.5f;
                Instantiate(orb, position, rotation);
            }
            indexCheat = 0;
        }
    }
}
