﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UserStorage.Interfaces.Entities;

namespace UserStorage.Interfaces.XmlEntities
{
    [Serializable]
    public class XmlUser : IXmlSerializable
    {
        public int PersonalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public List<XmlVisa> Visas { get; set; }

        public override bool Equals(object obj)
        {
            XmlUser user = obj as XmlUser;
            if (user == null)
            {
                return false;
            }

            if (ReferenceEquals(this, user))
            {
                return true;
            }

            if (PersonalId == user.PersonalId && FirstName == user.FirstName && LastName == user.LastName)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = PersonalId.GetHashCode();
            if (FirstName != null)
            {
                hashCode ^= FirstName.GetHashCode();
            }

            if (LastName != null)
            {
                hashCode ^= LastName.GetHashCode();
            }

            return hashCode ^ DateOfBirth.GetHashCode() ^ Gender.GetHashCode();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(nameof(XmlUser));
            PersonalId = reader.ReadElementContentAsInt();
            FirstName = reader.ReadElementContentAsString();
            LastName = reader.ReadElementContentAsString();
            DateOfBirth = reader.ReadElementContentAsDateTime();
            Gender = (Gender)reader.ReadElementContentAsInt();
            var serializer = new XmlSerializer(typeof(List<XmlVisa>));
            Visas = (List<XmlVisa>)serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString(nameof(PersonalId), PersonalId.ToString());
            writer.WriteElementString(nameof(FirstName), FirstName);
            writer.WriteElementString(nameof(LastName), LastName);
            writer.WriteElementString(nameof(DateOfBirth), DateOfBirth.ToString("yyyy-MM-dd"));
            writer.WriteElementString(nameof(Gender), ((int)Gender).ToString());
            var serializer = new XmlSerializer(typeof(List<XmlVisa>));
            serializer.Serialize(writer, Visas);
        }
    }
}
