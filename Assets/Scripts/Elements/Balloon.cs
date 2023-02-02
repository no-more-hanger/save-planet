public class Balloon : BaseElement {
    private int balloonCnt = 1;

    void Start() {
        destroyDelay = 2f;
    }

    protected override void AdjustEffect() {
        playerScript.AddBalloon(balloonCnt);
    }
}
