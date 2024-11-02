using BowlingGameV1;

namespace BowlingTests;

public class BowlingGameV1Tests
{
    [Fact]
    public void TestSingleRoll()
    {
        var bowling = new BowlingGame();
        bowling.Roll(5);
        Assert.Equal(5, bowling.Score());

    }

    [Fact]
    public void TestTwoRolls() {
        var bowling = new BowlingGame();
        bowling.Roll(5);
        bowling.Roll(3);
        Assert.Equal(8, bowling.Score());
    }

    [Fact]
   public void TestWithStrikeAndTwoRolls() {
        var bowling = new BowlingGame();
        bowling.Roll(10);
        bowling.Roll(4);
        bowling.Roll(3);
        Assert.Equal(24, bowling.Score());
    }

   [Fact]
   public void TestWithStrikeAndSingleRollFollowUp() {
        var bowling = new BowlingGame();
        bowling.Roll(10);
        bowling.Roll(4);
        Assert.Equal(18, bowling.Score());
    }
     
    [Fact]
   public void TestWithStrikeAndNoFollowUp() {
        var bowling = new BowlingGame();
        bowling.Roll(10);
        Assert.Equal(10, bowling.Score());
    }

    [Fact]
    public void TestWithSpareAndFollowUpRoll(){
        var bowling = new BowlingGame();
        bowling.Roll(4);
        bowling.Roll(6);
        bowling.Roll(3);
        Assert.Equal(16, bowling.Score());
    }
    
    [Fact]
    public void TestWithSpareAndNoFollowUpRoll(){
        var bowling = new BowlingGame();
        bowling.Roll(4);
        bowling.Roll(6);
        Assert.Equal(10, bowling.Score());
    }

    [Fact]
    public void TestFullGame() {
        var bowling = new BowlingGame();
        bowling.Roll(10);
        bowling.Roll(4);
        bowling.Roll(3);
        bowling.Roll(8);
        bowling.Roll(2);
        bowling.Roll(10);
        bowling.Roll(8);
        bowling.Roll(1);
        bowling.Roll(3);
        bowling.Roll(3);
        bowling.Roll(10);
        bowling.Roll(10);
        bowling.Roll(10);
        bowling.Roll(6);
        bowling.Roll(3);
        Assert.Equal(162, bowling.Score());
    }

    [Fact]
    public void TestWithAllStrikes() {
        var bowling = new BowlingGame();
        foreach (int v in Enumerable.Range(1,12)) {
            bowling.Roll(10);
        }
        Assert.Equal(300, bowling.Score());
    }

    [Fact]
    public void TestStrikesOnFinalFrame() {
        var bowling = new BowlingGame();
        foreach (int v in Enumerable.Range(1,18)) {
            bowling.Roll(1);
        }
        bowling.Roll(10);
        bowling.Roll(10);
        bowling.Roll(10);
        Assert.Equal(48, bowling.Score());
    }

    [Fact]
    public void TestSpareOnFinalFrame() {
        var bowling = new BowlingGame();
        foreach (int v in Enumerable.Range(1,18)) {
            bowling.Roll(1);
        }
        bowling.Roll(6);
        bowling.Roll(4);
        bowling.Roll(3);
        Assert.Equal(31, bowling.Score());
    }
}