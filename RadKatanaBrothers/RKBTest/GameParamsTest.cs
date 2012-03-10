using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for GameParamsTests and is intended
    ///to contain all GameParamsTests Unit Tests
    ///</summary>
    [TestClass()]
    public class GameParamsTests
    {
        /// <summary>
        ///A test for GameParams with indexing and dictionary syntax.
        ///</summary>
        [TestMethod()]
        public void GameParamsConstructorTest()
        {
            GameParams target = new GameParams {{"test", "value"},{"test2", "value2"}};
            Assert.AreEqual(target["test"] as String, "value");
            Assert.AreEqual(target["test2"] as String, "value2");
        }
    }
}
