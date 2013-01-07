using System;
using Xunit;
using FluentAssertions;

namespace BowlingKata
{
    public class BowlingGame
    {
        private int[] rolls = new int[21];
        private int currentRoll;

        public void Roll(int pins)
        {
            rolls[currentRoll++] = pins;
        }

        public int Score()
        {
            int score = 0;
            int roll = 0;

            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(roll))
                {
                    score += 10 + StrikeBonus(roll);
                    roll++;
                }
                else if (IsSpare(roll))
                {
                    score += 10 + SpareBonus(roll);
                    roll += 2;
                }
                else
                {
                    score += SumOfBallsInFrame(roll);
                    roll += 2;
                }
            }

            return score;
        }

        private int SumOfBallsInFrame(int roll)
        {
            return rolls[roll] + rolls[roll + 1];
        }

        private int SpareBonus(int roll)
        {
            return rolls[roll + 2];
        }

        private int StrikeBonus(int roll)
        {
            return rolls[roll + 1] + rolls[roll + 2];
        }

        private bool IsStrike(int roll)
        {
            return rolls[roll] == 10;
        }

        private bool IsSpare(int roll)
        {
            return rolls[roll] + rolls[roll + 1] == 10;
        }
    }
 
    public class BowlingGameTests
    {
        readonly BowlingGame g = new BowlingGame();

        [Fact]
        public void test_game()
        {
            RollMany(20, 0);
            g.Score().Should().Be(0);
        }

        [Fact]
        public void test_all_ones()
        {
            RollMany(20, 1);
            g.Score().Should().Be(20);
        }

        [Fact]
        public void test_game1()
        {
            int rolls = 20;
            int pins = 0;

            RollMany(rolls, pins);

            g.Score().Should().Be(0);
        }

        [Fact]
        public void test_one_spare()
        {
            RollSpare();
            g.Roll(3);
            RollMany(17, 0);
            g.Score().Should().Be(16);
        }

        private void RollSpare()
        {
            g.Roll(5);
            g.Roll(5);
        }

        private void RollMany(int rolls, int pins)
        {
            for (int i = 0; i < rolls; i++)
            {
                g.Roll(pins);
            }
        }
    }
}
