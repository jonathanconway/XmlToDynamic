using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlToDynamic;
using System.Xml.Linq;
using System.Dynamic;

namespace XmlToDynamic.Tests
{
    [TestClass]
    public class XElementExtensionsTests
    {
        [TestMethod]
        public void ToDynamic_Translates_Simple_Properties()
        {
            // Arrange
            var xml = XElement.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<testcontainer>
    <testchild1>one</testchild1>
    <testchild2>two</testchild2>
</testcontainer>");
           
            // Act
            dynamic result = xml.ToDynamic();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.testchild1);
            Assert.AreEqual("one", result.testchild1);
            Assert.IsNotNull(result.testchild2);
            Assert.AreEqual("two", result.testchild2);
        }

        [TestMethod]
        public void ToDynamic_Translates_Nested_Elements()
        {
            // Arrange
            var xml = XElement.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<testcontainer>
    <testchild1>
        <testsubchild>one</testsubchild>
    </testchild1>
    <testchild2>
        <testsubchild>two</testsubchild>
    </testchild2>
</testcontainer>");

            // Act
            dynamic result = xml.ToDynamic();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.testchild1);
            Assert.IsNotNull(result.testchild1.testsubchild);
            Assert.AreEqual("one", result.testchild1.testsubchild);
            Assert.IsNotNull(result.testchild2);
            Assert.IsNotNull(result.testchild2.testsubchild);
            Assert.AreEqual("two", result.testchild2.testsubchild);
        }

        [TestMethod]
        public void ToDynamic_Translates_Repeated_Properties_Into_Collections()
        {
            // Arrange
            var xml = XElement.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<testcontainer>
    <testchild1>A</testchild1>
    <testchild1>B</testchild1>
    <testchild2>C</testchild2>
    <testchild2>D</testchild2>
</testcontainer>");

            // Act
            dynamic result = xml.ToDynamic();

            // Assert
            Assert.IsNotNull(result);
            
            Assert.IsNotNull(result.testchild1s);
            Assert.AreEqual(2, result.testchild1s.Count);
            Assert.AreEqual("A", result.testchild1s[0]);
            Assert.AreEqual("B", result.testchild1s[1]);

            Assert.IsNotNull(result.testchild2s);
            Assert.AreEqual(2, result.testchild2s.Count);
            Assert.AreEqual("C", result.testchild2s[0]);
            Assert.AreEqual("D", result.testchild2s[1]);
        }

        [TestMethod]
        public void ToDynamic_Translates_Repeated_Properties_Into_Collections_With_SubObjects()
        {
            // Arrange
            var xml = XElement.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<testcontainer>
    <testchild1><name>Jon</name><age>13</age></testchild1>
    <testchild1><name>Esther</name><age>18</age></testchild1>
</testcontainer>");

            // Act
            dynamic result = xml.ToDynamic();

            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.testchild1s);
            Assert.AreEqual(2, result.testchild1s.Count);
            Assert.AreEqual("Jon", result.testchild1s[0].name);
            Assert.AreEqual("13", result.testchild1s[0].age);
            Assert.AreEqual("Esther", result.testchild1s[1].name);
            Assert.AreEqual("18", result.testchild1s[1].age);
        }
    }
}
