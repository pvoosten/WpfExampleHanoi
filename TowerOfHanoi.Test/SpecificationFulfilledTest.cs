using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace TowerOfHanoi.Test
{
    public class SpecificationFulfilledTest
    {

        private Assembly assembly;

        public SpecificationFulfilledTest()
        {
            assembly = Assembly.LoadFrom(@"..\..\..\..\TowerOfHanoi.Model\bin\Debug\netstandard2.0\TowerOfHanoi.Model.dll");
        }

        const string ns = "TowerOfHanoi.Model";

        private string Fq(string shortName) => $"{ns}.{shortName}";
        private Type Tp(string shortName) => assembly.GetType(Fq(shortName));

        [Fact(DisplayName = "The class model and the tests compile")]
        public void IfThisTestSucceedsTheModelIsCompilable()
        {
        }

        [Theory(DisplayName = "Class exists")]
        [InlineData("Hanoi")]
        [InlineData("Disc")]
        [InlineData("DiscTooGreatException")]
        [InlineData("Tower")]
        [InlineData("TowerEmptyException")]
        public void ClassExists(string className)
        {
            Assert.NotNull(Tp(className));
        }

        [Theory(DisplayName = "Is an exception type")]
        [InlineData("DiscTooGreatException")]
        [InlineData("TowerEmptyException")]
        public void IsExceptionType(string exception)
        {
            var exceptionType = assembly.GetType(Fq(exception));
            Assert.NotNull(exceptionType);
            exceptionType.IsSubclassOf(typeof(Exception));
        }

        [Fact(DisplayName = "Hanoi has no default constructor")]
        public void HanoiHasNoDefaultConstructor()
        {
            var ctor = Tp("Hanoi").GetConstructor(Array.Empty<Type>());
            Assert.Null(ctor);
        }

        [Fact(DisplayName = "Hanoi has a constructor with 1 int parameter")]
        public void HanoiHasCtorWithIntParameter()
        {
            var ctor = Tp("Hanoi").GetConstructor(new Type[] { typeof(int) });
            Assert.NotNull(ctor);
        }

        [Fact(DisplayName = "Hanoi has one single constructor")]
        public void HanoiHasSingleCtor()
        {
            var ctors = Tp("Hanoi").GetConstructors();
            Assert.Single(ctors);
        }

        [Fact(DisplayName = "Hanoi has 3 properties")]
        public void HanoiHas4Members()
        {
            var members = Tp("Hanoi").GetMembers();
            Assert.Equal<int>(3, members.Count(m => m.MemberType == MemberTypes.Property));
        }

        [Theory(DisplayName = "Hanoi has property of type Tower")]
        [InlineData("StartTower")]
        [InlineData("MiddleTower")]
        [InlineData("EndTower")]
        public void HanoiHasTower(string TowerNaam)
        {
            var prop = Tp("Hanoi").GetProperty(TowerNaam);
            Assert.Equal(Tp("Tower"), prop.PropertyType);
        }

        [Fact(DisplayName = "TowerEmptyException thrown when creating Hanoi with 0 Discs")]
        public void TowerEmptyExceptionWhenCreatingHanoiWith0Discs()
        {
            try
            {
                Activator.CreateInstance(Tp("Hanoi"), 0);
            }
            catch (TargetInvocationException ex) // wrapper around exception because of reflection
            {
                Assert.Equal("TowerEmptyException", ex.InnerException.GetType().Name);
            }
        }

        [Theory(DisplayName = "InvalidDiscCountException when trying to create Hanoi with negative number or more than 100 discs")]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(101)]
        [InlineData(1000)]
        public void InvalidDiscCountExceptionWhenCreatingHanoi(int count)
        {
            try
            {
                Activator.CreateInstance(Tp("Hanoi"), count);
            }
            catch (TargetInvocationException ex) // wrapper around exception because of reflection
            {
                Assert.Equal("InvalidDiscCountException", ex.InnerException.GetType().Name);
            }
        }

        // Disc has 1 ctor
        [Fact(DisplayName = "Disc has one single constructor")]
        public void DiscHasOneSingleCtor()
        {
            var ctors = Tp("Hanoi").GetConstructors();
            Assert.Single(ctors);
        }

        [Fact(DisplayName = "Disc has a constructor with 1 int parameter")]
        public void DiscHasCtorWithIntParameter()
        {
            var ctor = Tp("Disc").GetConstructor(new Type[] { typeof(int) });
            Assert.NotNull(ctor);
        }

        [Theory(DisplayName = "System.ArgumentException when you try to create Disc with negative diameter or diameter > 100")]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(0)]
        [InlineData(101)]
        [InlineData(1010)]
        public void InvalidDiameterWhenCreatingDisc(int diameter)
        {
            try
            {
                Activator.CreateInstance(Tp("Disc"), diameter);
            }
            catch (TargetInvocationException ex)
            {
                Assert.IsType<ArgumentException>(ex.InnerException);
            }
        }

        [Theory(DisplayName = "Disc can be created with diameter ranging from 1 to 100")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(99)]
        public void ValidDiameterWhenCreatingDisc(int diameter)
        {
            var Disc = Activator.CreateInstance(Tp("Disc"), diameter);
        }

        [Fact(DisplayName = "Disc has a property named Diameter")]
        public void DiscHasPropertyDiameter()
        {
            var prop = Tp("Disc").GetProperty("Diameter");
            Assert.NotNull(prop);
        }

        [Fact(DisplayName = "Disc.Diameter is readonly, has no setter")]
        public void DiscDiameterIsReadOnly()
        {
            var prop = Tp("Disc").GetProperty("Diameter");
            Assert.Null(prop.SetMethod);
        }

        [Theory(DisplayName = "The property Diameter gets the value of the parameter in the constructor of Disc")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(99)]
        public void DiscDiameterValueViaConstructor(int diameter)
        {
            dynamic Disc = Activator.CreateInstance(Tp("Disc"), diameter);
            Assert.Equal(diameter, Disc.Diameter);
        }

        [Fact(DisplayName = "Tower has a public default constructor, that executes the constructor with one parameter with parameter value 0")]
        public void TowerPublicDefaultCtor()
        {
            Assert.True(Tp("Tower").GetConstructor(Array.Empty<Type>()).IsPublic);
        }

        [Fact(DisplayName = "Tower has a second constructor with 1 parameter of type int, but that constructor is not public")]
        public void TowerInternalConstructorWithIntParameter()
        {
            var ctor = (ConstructorInfo)Tp("Tower").FindMembers(MemberTypes.Constructor, BindingFlags.Instance | BindingFlags.NonPublic, (mi, o) => true, null).First();
            Assert.Single(ctor.GetParameters());
            Assert.Equal(typeof(int), ctor.GetParameters().First().ParameterType);
        }

        [Fact(DisplayName = "Tower has a read-only property TopLevelDisc")]
        public void TowerTopLevelDiscIsReadOnlyProperty()
        {
            Assert.Null(Tp("Tower").GetProperty("TopLevelDisc").SetMethod);
        }

        [Fact(DisplayName = "Tower.TopLevelDisc has type Disc")]
        public void TowerTopLevelDiscHasTypeDisc()
        {
            Assert.Equal(Tp("Disc"), Tp("Tower").GetProperty("TopLevelDisc").PropertyType);
        }

        [Fact(DisplayName = "Tower has a public method LayDiscOnTop with 1 parameter of type Disc")]
        public void TowerLayDiscOnTopIsPublicMethodWith1Parameter()
        {
            var parameters = Tp("Tower").GetMethod("LayDiscOnTop").GetParameters();
            Assert.Single(parameters);
        }

        [Fact(DisplayName = "The parameter of method Tower.LayDiscOnTop has type Disc")]
        public void TowerLayDiscOnTopIsPublicMethodWithParameterTypeDisc()
        {
            var parameters = Tp("Tower").GetMethod("LayDiscOnTop").GetParameters();
            Assert.Equal(Tp("Disc"), parameters[0].ParameterType);
        }

        [Fact(DisplayName = "Tower.TopLevelDisc is null in an empty Tower")]
        public void TopLevelDiscIsNullInEmptyTower()
        {
            dynamic Tower = Activator.CreateInstance(Tp("Tower"));
            Assert.Null(Tower.TopLevelDisc);
        }

        [Fact(DisplayName = "Tower: LayDiscOnTop with Disc greater than TopLevelDisc => DiscTooGreatException")]
        public void LayDiscOnTopWithDiscGreaterThanTopLevelDisc()
        {
            dynamic Tower = Activator.CreateInstance(Tp("Tower"));
            dynamic smallDisc = Activator.CreateInstance(Tp("Disc"), 3);
            dynamic groteDisc = Activator.CreateInstance(Tp("Disc"), 6);
            Tower.LayDiscOnTop(smallDisc);
            void layGreatDisc() => Tower.LayDiscOnTop(groteDisc);
            Assert.Throws(Tp("DiscTooGreatException"), () => layGreatDisc());
        }

        [Fact(DisplayName = "Tower: LayDiscOnTop on empty Tower: TopLevelDisc becomes gelegde Disc")]
        public void LayDiscOnTopOpEmptyTower()
        {
            dynamic Tower = Activator.CreateInstance(Tp("Tower"));
            dynamic smallDisc = Activator.CreateInstance(Tp("Disc"), 3);
            Tower.LayDiscOnTop(smallDisc);
            Assert.Equal(smallDisc, Tower.TopLevelDisc);
        }

        [Fact(DisplayName = "Tower:LayDiscOnTop on non-empty Tower with Disc smaller than top level Disc => laid Disc becomes TopLevelDisc")]
        public void LayDiscOnTopOpNonEmptyTowerCheckTopLevelDisc()
        {
            dynamic Tower = Activator.CreateInstance(Tp("Tower"));
            dynamic bigDisc = Activator.CreateInstance(Tp("Disc"), 6);
            dynamic smallDisc = Activator.CreateInstance(Tp("Disc"), 3);
            Tower.LayDiscOnTop(bigDisc);
            Tower.LayDiscOnTop(smallDisc);
            Assert.Equal(smallDisc, Tower.TopLevelDisc);
        }

        [Fact(DisplayName = "Tower has method TakeDisc without parameters with returntype Disc")]
        public void TowerHasMethodTakeDiscWithoutParameters()
        {
            Assert.Empty(Tp("Tower").GetMethod("TakeDisc").GetParameters());
            Assert.Equal(Tp("Disc"), Tp("Tower").GetMethod("TakeDisc").ReturnType);
        }


        [Fact(DisplayName = "Execute Tower.TakeDisc on empty Tower => TowerEmptyException")]
        public void TakeDiscFromEmptyTower()
        {
            dynamic Tower = Activator.CreateInstance(Tp("Tower"));
            void neem() => Tower.TakeDisc();
            Assert.Throws(Tp("TowerEmptyException"), () => neem());
        }

        [Fact(DisplayName = "Execute Tower.TakeDisc on Tower with 1 Disc: taken Disc was TopLevelDisc")]
        public void TakeDiscTakenDiscWasTopLevelDisc()
        {
            dynamic Tower = Activator.CreateInstance(Tp("Tower"));
            dynamic groteDisc = Activator.CreateInstance(Tp("Disc"), 6);
            Tower.LayDiscOnTop(groteDisc);
            var Disc = Tower.TopLevelDisc;
            Assert.Equal(Disc, Tower.TakeDisc());
        }

        [Fact(DisplayName = "Execute Tower.TakeDisc on Tower with 1 Disc: Tower is now empty (TopLevelDisc is null)")]
        public void TowerEmptyAfterTakingLastDisc()
        {
            dynamic Tower = Activator.CreateInstance(Tp("Tower"));
            dynamic groteDisc = Activator.CreateInstance(Tp("Disc"), 6);
            Tower.LayDiscOnTop(groteDisc);
            Tower.TakeDisc();
            Assert.Null(Tower.TopLevelDisc);
        }

        [Theory(DisplayName = "Hanoi.StartTower has correct Disc count")]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void HanoiStartTowerHasCorrectDiscCount(int discCount)
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), discCount);
            var Tower = hanoi.StartTower;
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
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), discCount);
            var Tower = hanoi.StartTower;
            Assert.Equal(1, Tower.TopLevelDisc.Diameter);
        }


        [Fact(DisplayName = "A Hanoi game can be solved with TakeDisc and LayDiscOnTop for 1 Discs")]
        public void SolveHanoiWith1Disc()
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), 1);
            var Disc = hanoi.StartTower.TakeDisc();
            hanoi.EndTower.LayDiscOnTop(Disc);
        }

        [Fact(DisplayName = "A Hanoi game can be solved with TakeDisc and LayDiscOnTop for 2 Discs")]
        public void SolveHanoiWith2Discs()
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), 2);
            var Disc = hanoi.StartTower.TakeDisc();
            hanoi.MiddleTower.LayDiscOnTop(Disc);
            Disc = hanoi.StartTower.TakeDisc();
            hanoi.EndTower.LayDiscOnTop(Disc);
            Disc = hanoi.MiddleTower.TakeDisc();
            hanoi.EndTower.LayDiscOnTop(Disc);
        }

        [Fact(DisplayName = "A Hanoi game can be solved with TakeDisc and LayDiscOnTop for 3 Discs")]
        public void SolveHanoiWith3Discs()
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), 3);
            void moveDisc(dynamic fromTower, dynamic toTower) => toTower.LayDiscOnTop(fromTower.TakeDisc());
            moveDisc(hanoi.StartTower, hanoi.EndTower);
            moveDisc(hanoi.StartTower, hanoi.MiddleTower);
            moveDisc(hanoi.EndTower, hanoi.MiddleTower);
            moveDisc(hanoi.StartTower, hanoi.EndTower);
            moveDisc(hanoi.MiddleTower, hanoi.StartTower);
            moveDisc(hanoi.MiddleTower, hanoi.EndTower);
            moveDisc(hanoi.StartTower, hanoi.EndTower);
        }

        [Fact(DisplayName = "Optional: solve the problem recursively in Tower.VerplaatsNaar(int offset, Tower target, int targetOffset, Tower via, int viaOffset")]
        public void Optional() { }

    }
}
