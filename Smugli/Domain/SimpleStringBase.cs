namespace Smugli.Domain
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    #endregion

    public abstract class SimpleStringBase : IXmlSerializable,
                                             IComparable
    {
        private string _value;

        protected SimpleStringBase(string id)
        {
            Value = id;
        }

        protected SimpleStringBase(int id)
            : this(id.ToString(CultureInfo.InvariantCulture))
        {
        }

        protected SimpleStringBase()
        {
        }

        protected SimpleStringBase(long id)
            : this(id.ToString(CultureInfo.InvariantCulture))
        {
        }

        protected SimpleStringBase(Guid id)
            : this(id.ToString())
        {
        }

        public string Value
        {
            get { return _value; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("id");
                if (value == string.Empty)
                    throw new ArgumentException("id");

                _value = value;
            }
        }

        public static bool operator ==(SimpleStringBase operand, SimpleStringBase operand2)
        {
            if (ReferenceEquals(operand, operand2))
                return true;

            return !ReferenceEquals(null, operand) && operand.Equals(operand2);
        }

        public static bool operator !=(SimpleStringBase operand, SimpleStringBase operand2)
        {
            return !(operand == operand2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null ||
                GetType() != obj.GetType())
                return false;

            return Value.Equals(((SimpleStringBase)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        #region IComparable members

        public int CompareTo(object other)
        {
            return Value.CompareTo(((SimpleStringBase)other).Value);
        }

        #endregion

        #region IXmlSerializable members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Value = reader.GetAttribute("Id");
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Id", Value);
        }

        #endregion
    }
}