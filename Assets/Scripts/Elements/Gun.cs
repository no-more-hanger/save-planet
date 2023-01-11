/// <summary>
/// Gun class - example Item
/// </summary>
public class Gun : BaseElement {
    Player001 playerScript;
    private void Start() {
        playerScript = this.player.GetComponent<Player001>();
    }

    private void OnDestroy() {
        playerScript.PutOnGun();
    }
}
