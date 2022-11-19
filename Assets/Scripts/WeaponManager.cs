using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Variables
    public PlayerController controller;
    public List<Gun> guns;
    public int count;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.ChangeUp() && count < guns.Count)
        {
            // Play Animation && Sound
            count++;
        }
        
        else if (controller.ChangeDown() && count > 0)
        {
            // Play Animation && Sound
            count--;
        }
    }
}
