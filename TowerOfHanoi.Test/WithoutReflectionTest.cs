using System;
using System.Collections.Generic;
using System.Text;
using TowerOfHanoi.Model;
using Xunit;

namespace TowerOfHanoi.Test
{
    public class WithoutReflectionTest
    {
        [Fact(DisplayName = "The class model and the tests are compilable")]
        public void AlsDezeTestSlaagtIsTheModelCompileerbaar()
        {
        }

        [Fact(DisplayName = "Class exists: Hanoi")]
        public void ClassHanoiExists()
        {
            Hanoi hanoi = null;
            Assert.Null(hanoi);
        }

        [Fact(DisplayName = "Class exists: Disc")]
        public void ClassDiscExists()
        {
            Disc Disc = null;
            Assert.Null(Disc);
        }

        [Fact(DisplayName = "Class exists: DiscTooGreatException")]
        public void ClassDiscTooGreatExceptionExists()
        {
            DiscTooGreatException ex = null;
            Assert.Null(ex);
        }

        [Fact(DisplayName = "Class exists: Tower")]
        public void ClassTowerExists()
        {
            Tower Tower = null;
            Assert.Null(Tower);
        }

        [Fact(DisplayName = "Class exists: TowerEmptyException")]
        public void ClassTowerEmptyExceptionExists()
        {
            TowerEmptyException ex = null;
            Assert.Null(ex);
        }

        [Fact(DisplayName = "Type is subclass of Exception: DiscTooGreatException")]
        public void DiscTooGreatExceptionIsExceptionType()
        {
            var ex = new DiscTooGreatException();
            Assert.True(ex is Exception);
        }

        [Fact(DisplayName = "Type is subclass of Exception: TowerEmptyException")]
        public void TowerEmptyExceptionIsAnExceptionType()
        {
            var ex = new TowerEmptyException();
            Assert.True(ex is Exception);
        }

        [Fact(DisplayName = "Hanoi has a constructor with 1 int parameter")]
        public void HanoiHasCtorWithIntParameter()
        {
            new Hanoi(7);
        }

        [Fact(DisplayName = "Hanoi has property with type Tower and name StartTower")]
        public void HanoiHasStartTower()
        {
            var hanoi = new Hanoi(5);
            Tower Tower = hanoi.StartTower;
            Assert.NotNull(Tower);
        }

        [Fact(DisplayName = "Hanoi has property with type Tower and name MiddleTower")]
        public void HanoiHasMiddleTower()
        {
            var hanoi = new Hanoi(5);
            Tower Tower = hanoi.MiddleTower;
            Assert.NotNull(Tower);
        }

        [Fact(DisplayName = "Hanoi has property with type Tower and name EndTower")]
        public void HanoiHasEndTower()
        {
            var hanoi = new Hanoi(5);
            Tower Tower = hanoi.EndTower;
            Assert.NotNull(Tower);
        }

        [Fact(DisplayName = "TowerEmptyException when trying to create Hanoi with 0 Discs")]
        public void TowerEmptyExceptionWhenCreatingHanoiWith0Discs()
        {
            Assert.Throws<TowerEmptyException>(() => new Hanoi(0));
        }

        [Theory(DisplayName = "InvalidDiscCountException when trying to create Hanoi with negative number or more than 100 Discs")]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(101)]
        [InlineData(1000)]
        public void InvalidDiscCountExceptionWhenCreatingHanoi(int count)
        {
            Assert.Throws<InvalidDiscCountException>(()=>new Hanoi(count));
        }

        [Theory(DisplayName = "System.ArgumentException when trying to create Hanoi with negative number or more than 100 Discs")]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(0)]
        [InlineData(101)]
        [InlineData(1010)]
        public void InvalidDiameterWhenCreatingDisc(int diameter)
        {
            Assert.Throws<ArgumentException>(()=> new Disc(diameter));
        }

        [Theory(DisplayName = "Disc can be created with diameter ranging from 1 to 100")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(99)]
        public void ValidDiameterWhenCreatingDisc(int diameter)
        {
            var Disc = new Disc(diameter);
        }

        [Theory(DisplayName = "The property Diameter gets the value of the parameter in the constructor of Disc")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(99)]
        public void DiscDiameterValueViaConstructor(int diameter)
        {
            var Disc = new Disc(diameter);
            Assert.Equal(diameter, Disc.Diameter);
        }

        [Fact(DisplayName = "Tower has a property TopLevelDisc with type Disc")]
        public void TowerTopLevelDiscIsReadOnlyProperty()
        {
            var Tower = new Tower();
            Disc Disc = Tower.TopLevelDisc;
        }

        [Theory(DisplayName = "A Disc with diameter from 1 to 100 can always be laid on an empty Tower")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(97)]
        [InlineData(98)]
        [InlineData(99)]
        [InlineData(100)]
        public void LayDiscOnTopWithCertainDiameterOnEmptyTower(int diameter)
        {
            var Tower = new Tower();
            var Disc = new Disc(diameter);
            Tower.LayDiscOnTop(Disc);
        }


        [Fact(DisplayName = "Tower.TopLevelDisc is null in an empty Tower")]
        public void TopLevelDiscIsNullInAnEmptyTower()
        {
            var Tower = new Tower();
            Assert.Null(Tower.TopLevelDisc);
        }

        [Theory(DisplayName = "Tower: LayDiscOnTop with Disc greater than TopLevelDisc => DiscTooGreatException")]
        [InlineData(3, 4, 5)]
        [InlineData(3, 4, 1)]
        [InlineData(3, 2, 3)]
        [InlineData(10, 1, 2)]
        public void LayDiscOnTopWithDiscGreaterThanTopLevelDisc(int firstDiameter, int secondDiameter, int thirdDiameter)
        {
            var Tower = new Tower();
            Assert.Throws<DiscTooGreatException>(()=> {
                var firstDisc = new Disc(firstDiameter);
                Tower.LayDiscOnTop(firstDisc);
                var secondDisc = new Disc(secondDiameter);
                Tower.LayDiscOnTop(secondDisc);
                var thirdDisc = new Disc(thirdDiameter);
                Tower.LayDiscOnTop(thirdDisc);
            });
        }

        [Fact(DisplayName = "Tower: LayDiscOnTop on empty Tower: TopLevelDisc becomes laid Disc")]
        public void LayDiscOnTopOpEmptyTower()
        {
            var Tower = new Tower();
            var Disc = new Disc(77);
            Tower.LayDiscOnTop(Disc);
            Assert.Same(Disc, Tower.TopLevelDisc);
        }

        [Theory(DisplayName = "Tower:LayDiscOnTop on non-empty Tower with Disc smaller than top level Disc => laid Disc becomes TopLevelDisc")]
        [InlineData(55, 22)]
        [InlineData(5, 2)]
        [InlineData(2, 1)]
        [InlineData(100, 99)]
        public void LayDiscOnTopOpNonEmptyTowerCheckTopLevelDisc(int ondersteDiscDiameter, int bovensteDiscDiameter)
        {
            var Tower = new Tower();
            var groteDisc =  new Disc(ondersteDiscDiameter);
            var kleineDisc = new Disc(bovensteDiscDiameter);
            Tower.LayDiscOnTop(groteDisc);
            Tower.LayDiscOnTop(kleineDisc);
            Assert.Same(kleineDisc, Tower.TopLevelDisc);
        }

        [Fact(DisplayName = "Tower has method TakeDisc without parameters and with return type Disc")]
        public void TowerHasMethodTakeDiscWithoutParameters()
        {
            Tower Tower = new Tower();
            Tower.LayDiscOnTop(new Disc(55)); // make sure that the tower is not empty.
            Disc Disc = Tower.TakeDisc();
        }


        [Fact(DisplayName = "Execute Tower.TakeDisc on empty Tower => TowerEmptyException")]
        public void TakeDiscFromEmptyTower()
        {
            var Tower = new Tower();
            Assert.Throws<TowerEmptyException>(() => Tower.TakeDisc());
        }

        [Fact(DisplayName = "Execute Tower.TakeDisc on Tower with 1 Disc: taken Disc was TopLevelDisc")]
        public void TakeDiscTakenDiscWasTopLevelDisc()
        {
            var Tower = new Tower();
            var dsc = new Disc(6);
            Tower.LayDiscOnTop(dsc);
            var Disc = Tower.TopLevelDisc;
            var takenDisc = Tower.TakeDisc();
            Assert.Same(Disc, takenDisc);
        }

        [Fact(DisplayName = "Execute Tower.TakeDisc on Tower with 1 Disc: Tower is now empty (TopLevelDisc is null)")]
        public void TowerEmptyAfterTakingLastDisc ()
        {
            var Tower = new Tower();
            var groteDisc = new Disc(6);
            Tower.LayDiscOnTop(groteDisc);
            Tower.TakeDisc();
            Assert.Null(Tower.TopLevelDisc);
        }

        [Theory(DisplayName = "Hanoi.StartTower has correct count of Discs")]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void HanoiStartTowerContainsDiscCount(int discCount)
        {
            var hanoi = new Hanoi(discCount);
            var Tower = hanoi.StartTower;
            // Take disks off of the tower until the tower should be empty
            for (int i = 0; i < discCount; i++)
            {
                Assert.NotNull(Tower.TopLevelDisc);
                Tower.TakeDisc();
            }
            Assert.Null(Tower.TopLevelDisc);
        }

        [Theory(DisplayName = "Top level Disc of Hanoi.StartTower has diameter 1.")]
        [InlineData(1)]
        [InlineData(11)]
        [InlineData(97)]
        public void TopLevelDiscInNewStartTowerHasDiameter1(int discCount)
        {
            var hanoi = new Hanoi(discCount);
            var Tower = hanoi.StartTower;
            Assert.Equal(1, Tower.TopLevelDisc.Diameter);
        }

        [Fact(DisplayName = "A Hanoi game can be solved with TakeDisc and LayDiscOnTop for 1 Disc")]
        public void SolveHanoiWith1Disc()
        {
            var hanoi = new Hanoi(1);
            var Disc = hanoi.StartTower.TakeDisc();
            hanoi.EndTower.LayDiscOnTop(Disc);
            Assert.Null(hanoi.StartTower.TopLevelDisc); // the first Tower is empty when solution is found
            Assert.Null(hanoi.MiddleTower.TopLevelDisc); // the second Tower is empty when solution is found
            Assert.NotNull(hanoi.EndTower.TopLevelDisc); // the third Tower contains at least 1 Disc when solution is found
            Assert.Same(Disc, hanoi.EndTower.TopLevelDisc); // the top level Disc of the third Tower is the Disc from the start of the game
        }

        [Fact(DisplayName = "A Hanoi game can be solved with TakeDisc and LayDiscOnTop for 2 Disc")]
        public void SolveHanoiWith2Discs()
        {
            var hanoi = new Hanoi(2);
            var Disc = hanoi.StartTower.TakeDisc();

            // solve
            hanoi.MiddleTower.LayDiscOnTop(Disc);
            Disc = hanoi.StartTower.TakeDisc();
            hanoi.EndTower.LayDiscOnTop(Disc);
            Disc = hanoi.MiddleTower.TakeDisc();
            hanoi.EndTower.LayDiscOnTop(Disc);

            // Check whether the solution is correct
            Assert.Null(hanoi.StartTower.TopLevelDisc); // the first Tower is empty when solution is found
            Assert.Null(hanoi.MiddleTower.TopLevelDisc); // the second Tower is empty when solution is found
            Assert.NotNull(hanoi.EndTower.TopLevelDisc); // the third Tower contains at least 1 Disc when solution is found
        }

        [Fact(DisplayName = "Een Hanoi-spel kan opgelost worden met TakeDisc and LayDiscOnTop voor 3 Discs")]
        public void SolveHanoiWith3Discs()
        {
            var hanoi = new Hanoi(3);

            // Solve the problem with three disks
            void verplaatsDisc(Tower vanTower, Tower naarTower) => naarTower.LayDiscOnTop(vanTower.TakeDisc());
            verplaatsDisc(hanoi.StartTower, hanoi.EndTower);
            verplaatsDisc(hanoi.StartTower, hanoi.MiddleTower);
            verplaatsDisc(hanoi.EndTower, hanoi.MiddleTower);
            verplaatsDisc(hanoi.StartTower, hanoi.EndTower);
            verplaatsDisc(hanoi.MiddleTower, hanoi.StartTower);
            verplaatsDisc(hanoi.MiddleTower, hanoi.EndTower);
            verplaatsDisc(hanoi.StartTower, hanoi.EndTower);

            // Controleer of the oplossing juist kan are
            Assert.Null(hanoi.StartTower.TopLevelDisc); // the first Tower is empty when solution is found
            Assert.Null(hanoi.MiddleTower.TopLevelDisc); // the second Tower is empty when solution is found
            Assert.NotNull(hanoi.EndTower.TopLevelDisc); // the third Tower contains at least 1 Disc when solution is found
        }

    }
}
