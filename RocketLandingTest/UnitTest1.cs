using System;
using Xunit;
using RocketLanding;
namespace RocketLandingTest
{
    public class UnitTest1
    {
        /// <summary>
        /// Test of wrong landing platform settings
        /// </summary>
        [Fact]
        public void Test0()
        {
            LangingChecker langingChecker = new LangingChecker();
            
            Assert.Throws<Exception>(() => {
                langingChecker.UpdateSettings(new LandingAreaSettings()
                {
                    LandingAreaSize = 60,
                    LandingPlatformSize = 12,
                    LandingPlatformStartX = 50,
                    LandingPlatformStartY = 40
                });
            });
        }
        /// <summary>
        /// Test of one rocket "ok for landing" 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [Theory]
        [InlineData(5,5)]
        [InlineData(10,13)]
        [InlineData(14,14)]
        public void Test1(int x, int y)
        {
            LangingChecker langingChecker = new LangingChecker();
            Assert.Equal("ok for landing", langingChecker.Check(1,x, y));
        }
        /// <summary>
        /// Test for Case should "out of platform" 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(4, 16)]
        [InlineData(15, 15)]
        public void Test2(int x, int y)
        {
            LangingChecker langingChecker = new LangingChecker();
            Assert.Equal("out of platform", langingChecker.Check(1,x, y));
        }
        /// <summary>
        /// Test For multiple rockets with default platform settings
        /// </summary>
        [Fact]
        public void Test3()
        {
            LangingChecker langingChecker = new LangingChecker();
            Assert.Equal("ok for landing", langingChecker.Check(1,5, 6));
            Assert.Equal("ok for landing", langingChecker.Check(1,5, 5));
            Assert.Equal("clash", langingChecker.Check(2, 5, 5));
            Assert.Equal("clash", langingChecker.Check(2, 5, 6));
            Assert.Equal("out of platform", langingChecker.Check(2,4, 6));
            Assert.Equal("clash", langingChecker.Check(2,6, 6));
            Assert.Equal("ok for landing", langingChecker.Check(2,10, 11));
            Assert.Equal("clash", langingChecker.Check(3,9, 12));
            Assert.Equal("clash", langingChecker.Check(3,11, 12));
            Assert.Equal("clash", langingChecker.Check(3,5, 5));
            Assert.Equal("ok for landing", langingChecker.Check(3,7, 5));
            Assert.Equal("clash", langingChecker.Check(1,7, 5));
            Assert.Equal("clash", langingChecker.Check(1,6, 5));
            Assert.Equal("ok for landing", langingChecker.Check(1,5, 5));
        }
        /// <summary>
        /// Test For multiple rockets with custom platfourm settings
        /// </summary>
        [Fact]
        public void Test4()
        {
            LangingChecker langingChecker = new LangingChecker(new LandingAreaSettings()
            {
                LandingAreaSize = 60,
                LandingPlatformSize =12,
                LandingPlatformStartX = 35,
                LandingPlatformStartY = 40
            }) ;
            Assert.Equal("out of platform", langingChecker.Check(1, 5, 5));
            Assert.Equal("out of platform", langingChecker.Check(2, 0, 0));
            Assert.Equal("ok for landing", langingChecker.Check(2, 40, 40));
            Assert.Equal("clash", langingChecker.Check(3, 41, 40));
            Assert.Equal("ok for landing", langingChecker.Check(3, 41, 42));
            Assert.Equal("clash", langingChecker.Check(1, 39, 40));
            Assert.Equal("out of platform", langingChecker.Check(1, 37, 52));
            Assert.Equal("ok for landing", langingChecker.Check(1, 35, 50));
        }
    }
}
