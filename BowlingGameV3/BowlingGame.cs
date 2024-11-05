namespace BowlingGameV3;

public interface BowlingFrame
{
    int Score();
    bool IsCompleted();
    void AddSecondRoll(int pins);
    bool IsStrike();
    void AddBonusRoll(int pins);
    bool IsSpare();
}

public class Frame : BowlingFrame
{
    private readonly int _firstRoll;
    int? _secondRoll;
    private readonly IList<int> _bonusRoll;

    public Frame(int firstRoll)
    {
        _firstRoll = firstRoll;
        _bonusRoll = new List<int>();
    }

    public int Score()
    {
        return _firstRoll + _secondRoll.GetValueOrDefault()+ _bonusRoll.Aggregate(0, (total, bonus) => total + bonus);
    }

    public bool IsCompleted()
    {
        return _secondRoll.HasValue;
    }

    public void AddSecondRoll(int pins)
    {
        _secondRoll = pins;
    }

    public bool IsStrike()
    {
        return false;
    }

    public void AddBonusRoll(int pins)
    {
        if (_bonusRoll.Count < 2) {
            _bonusRoll.Add(pins);
        }
    }

    public bool IsSpare()
    {
        return (_firstRoll+_secondRoll == 10);
    }
}

public class StrikeFrame : BowlingFrame
{
    private readonly IList<int> _bonusRoll= new List<int>();
    
    public int Score()
    {
        return 10 + _bonusRoll.Aggregate(0, (total, bonus) => total + bonus);
    }

    public bool IsCompleted()
    {
        return true;
    }

    public void AddSecondRoll(int pins)
    {
        throw new NotImplementedException();
    }

    public bool IsStrike()
    {
        return true;
    }

    public void AddBonusRoll(int pins)
    {
        if (_bonusRoll.Count < 2) {
            _bonusRoll.Add(pins);
        }
    }

    public bool IsSpare()
    {
        return false;
    }
}

public class BowlingGame
{
    IList<BowlingFrame> _frames = new List<BowlingFrame>();

    private BowlingFrame _createFrame(int pins)
    {
        if (pins == 10)
        {
            return new StrikeFrame();
        }
        
        return new Frame(pins);
    }
    public void Roll(int pins)
    {
        if (_frames.Count == 10 ) {
            _frames.Last().AddBonusRoll(pins);
            if (_frames[^2].IsStrike()) {
                _frames[^2].AddBonusRoll(pins);
            }
            return;
        }

        if (_frames.Count > 0)
        {
            var lastFrame = _frames.Last();

            if (lastFrame.IsCompleted())
            {
                var frame = _createFrame(pins);
                _frames.Add(frame);
            }
            else
            {
                lastFrame.AddSecondRoll(pins);
            }
        }
        else
        {
            var frame = _createFrame(pins);
            _frames.Add(frame);
        }

        if (_frames.Count - 1 > 0) {
            if (_frames[^2].IsSpare() ) {
                _frames[^2].AddBonusRoll(pins);
            }
        }

        if (_frames.Count - 1 > 0)
        {
            if (_frames[^2].IsStrike())
            {
                _frames[^2].AddBonusRoll(pins);
            }
        }
        if (_frames.Count - 2 > 0)
        {
            if (_frames[^3].IsStrike())
            {
                _frames[^3].AddBonusRoll(pins);
            }
        }
    }

    public int Score()
    {
        return _frames.Aggregate(0, (total, frame) => total + frame.Score());
    }
}
