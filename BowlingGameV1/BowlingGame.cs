namespace BowlingGameV1;

public class BowlingGame {
    IList<int> rolls;
    public BowlingGame() {
        rolls = new List<int>();
    }

    public void Roll(int pins)
    {
        rolls.Add(pins);
    }

    public int Score()
    {
        var total = 0;
        var newFrame = true;
        var currentFrame = 1;
        foreach (var(roll, index) in rolls.Select((f, idx)=> (f,idx))) {
            if (currentFrame > 10) {
                break;
            }

            total += roll;

            // We handle strikes
            if (roll == 10) {
                if (index + 1 < rolls.Count) {
                total += rolls[index + 1];
                }
                if (index + 2 < rolls.Count) {
                    total += rolls[index + 2];
                }
                newFrame = true;
                currentFrame++;
            } else {
                // We handle spares
                if (!newFrame) {
                    if (roll + rolls[index -1] == 10) {
                        if (index + 1 < rolls.Count) {
                            total += rolls[index+1];
                        }
                    }
                }
                newFrame = !newFrame;
                if (newFrame) {
                    currentFrame++;
                }
            }
        }
        return  total;
    }
}
