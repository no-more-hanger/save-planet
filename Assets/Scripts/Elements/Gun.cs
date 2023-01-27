/// <summary>
/// Gun class - example Item
/// </summary>
public class Gun : BaseElement {
    BaseCharacter playerScript;
    private void Start() {
        playerScript = this.player.GetComponent<BaseCharacter>();
    }

    private void OnDestroy() {
        playerScript.PutOnGun();
    }
}
