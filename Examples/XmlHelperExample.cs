using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

#pragma warning disable 414, 219
namespace UnityUtilities.Examples
{
    public class XmlHelperExample : MonoBehaviour
    {
        public class TestData
        {
            // A public field - will be serialized
            public int field;

            // A private field with public property - will be serialized
            float property;
            public float Property
            {
                get { return property; }
                set { property = value;}
            }

            // An auto property - will be serialized
            public bool AutoProperty { get; set; }

            // A private field - will *not* be serialized
            string privateField = "Test";

            // A public field marked "XmlIgnore" - will *not* be serialized
            [XmlIgnore]
            public double publicNonSerialized = 5.5;

            // The public default constructor is needed for the XmlSerializer.
            public TestData()
            {
            }

            public TestData(int field, float property, bool autoProperty)
            {
                this.field = field;
                this.property = property;
                AutoProperty = autoProperty;
            }
        }

        void Awake()
        {
            SerializationExamples();
            XmlExamples();
        }

        void SerializationExamples()
        {
            // Create a new TestData object
            TestData data = new TestData(1, 2.3f, true);

            // Serialize the TestData object into a string
            string xmlString = data.SerializeToXmlString();

            /* Output:

                <?xml version="1.0" encoding="utf-16"?>
                <TestData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                    <field>1</field>
                    <Property>2.3</Property>
                    <AutoProperty>true</AutoProperty>
                </TestData>
             */
            Debug.Log(xmlString);

            // Get the data back from the string
            TestData deserializedData = xmlString.DeserializeFromXmlString<TestData>();
        }

        void XmlExamples()
        {
            // Create an XmlDocument with test data
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<enemyList>" +
                                "   <enemyData>" +
                                "       <name>Grunt</name>" +
                                "       <position x='5' y='3'/>" +
                                "   </enemyData>" +
                                "   <enemyData>" +
                                "       <name>Tank</name>" +
                                "       <position x='7' y='1'/>" +
                                "       <ranged>true</ranged>" +
                                "   </enemyData>" +
                                "</enemyList>");

            // Read each enemyData element in the enemyList
            foreach (XmlNode enemyData in xmlDocument["enemyList"].ChildNodes)
            {
                // Get the name element content, if it exists, else set "???"
                string name = enemyData.GetElementString("name", "???");

                // Get the position element and then its attributes
                XmlNode position = enemyData["position"];
                int x = position.GetAttributeInt("x");
                int y = position.GetAttributeInt("y");

                // Get the ranged element content, if it exists, else set "false"
                bool ranged = enemyData.GetElementBool("ranged", false);

                // Output the result
                Debug.Log(string.Format("{0} at {1}|{2} is {3}",
                                        name,
                                        x,
                                        y,
                                        ranged ? "ranged" : "not ranged"));
            }

            /* Grunt at 5|3 is not ranged
               Tank at 7|1 is ranged
             */
        }
    }
}
#pragma warning restore 414, 219