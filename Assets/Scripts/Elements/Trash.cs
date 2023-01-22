using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : BaseElement {
    Player001 playerScript;
    // Start is called before the first frame update
    void Start() {
        playerScript = player.GetComponent<Player001>();
    }
}
