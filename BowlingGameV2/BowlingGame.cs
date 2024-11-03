
namespace BowlingGameV2;

public class Frame
{
    int FirstRoll;
    int? SecondRoll;
    IList<int> BonusRoll;

    public Frame(int firstRoll)
    {
        FirstRoll = firstRoll;
        BonusRoll = new List<int>();
    }

    public int Score()
    {
        return FirstRoll + SecondRoll.GetValueOrDefault()+ BonusRoll.Aggregate(0, (total, bonus) => total + bonus);
    }

    public bool IsCompleted()
    {
        return IsStrike() || SecondRoll.HasValue;
    }

    public void AddSecondRoll(int pins)
    {
        SecondRoll = pins;
    }

    internal bool IsStrike()
    {
        return FirstRoll == 10;
    }

    internal void AddBonusRoll(int pins)
    {
        if (BonusRoll.Count < 2) {
            BonusRoll.Add(pins);
        }
    }

    internal bool IsSpare()
    {
        return !IsStrike() && (FirstRoll+SecondRoll == 10);
    }
}
public class BowlingGame
{
    IList<Frame> frames;
    public BowlingGame()
    {
        frames = new List<Frame>();
    }

    public void Roll(int pins)
    {
        if (frames.Count == 10 ) {
            frames.Last().AddBonusRoll(pins);
            if (frames[frames.Count -2].IsStrike()) {
                frames[frames.Count -2].AddBonusRoll(pins);
            }
            return;
        }

        if (frames.Count > 0)
        {
            var lastFrame = frames.Last();

            if (lastFrame.IsCompleted())
            {
                var frame = new Frame(pins);
                frames.Add(frame);
            }
            else
            {
                lastFrame.AddSecondRoll(pins);
            }
        }
        else
        {
            var frame = new Frame(pins);
            frames.Add(frame);
        }

        if (frames.Count - 1 > 0) {
            if (frames[frames.Count-2].IsSpare() ) {
                frames[frames.Count - 2].AddBonusRoll(pins);
            }
        }

        if (frames.Count - 1 > 0)
        {
            if (frames[frames.Count - 2].IsStrike())
            {
                frames[frames.Count - 2].AddBonusRoll(pins);
            }
        }
        if (frames.Count - 2 > 0)
        {
            if (frames[frames.Count - 3].IsStrike())
            {
                frames[frames.Count - 3].AddBonusRoll(pins);
            }
        }
    }

    public int Score()
    {
        return frames.Aggregate(0, (total, frame) => total + frame.Score());
    }
}
