/// <summary>
/// Gun class - example Item
/// </summary>
public class Gun : BaseElement {
    private void Start() {
    }

    private void OnDestroy() {
        playerScript.PutOnGun();
    }
}
