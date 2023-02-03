/// <summary>
/// Gun class - example Item
/// </summary>
public class Gun : BaseElement {
    protected override void AdjustEffect() {
        playerScript.PutOnGun();
    }
}
