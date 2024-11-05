
namespace BowlingGameV2;

public class Frame(int firstRoll)
{
    private int? _secondRoll;
    private readonly IList<int> _bonusRoll = new List<int>();

    public int Score()
    {
        return firstRoll + _secondRoll.GetValueOrDefault()+ _bonusRoll.Aggregate(0, (total, bonus) => total + bonus);
    }

    public bool IsCompleted()
    {
        return IsStrike() || _secondRoll.HasValue;
    }

    public void AddSecondRoll(int pins)
    {
        _secondRoll = pins;
    }

    internal bool IsStrike()
    {
        return firstRoll == 10;
    }

    internal void AddBonusRoll(int pins)
    {
        if (_bonusRoll.Count < 2) {
            _bonusRoll.Add(pins);
        }
    }

    internal bool IsSpare()
    {
        return !IsStrike() && (firstRoll+_secondRoll == 10);
    }
}
public class BowlingGame
{
    IList<Frame> frames = new List<Frame>();

    public void Roll(int pins)
    {
        if (frames.Count == 10 ) {
            frames.Last().AddBonusRoll(pins);
            if (frames[^2].IsStrike()) {
                frames[^2].AddBonusRoll(pins);
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

        if (frames.Count > 1) {
            if (frames[^2].IsSpare() ) {
                frames[^2].AddBonusRoll(pins);
            }
        }

        if (frames.Count > 1)
        {
            if (frames[^2].IsStrike())
            {
                frames[^2].AddBonusRoll(pins);
            }
        }
        if (frames.Count > 2)
        {
            if (frames[^3].IsStrike())
            {
                frames[^3].AddBonusRoll(pins);
            }
        }
    }

    public int Score()
    {
        return frames.Aggregate(0, (total, frame) => total + frame.Score());
    }
}
