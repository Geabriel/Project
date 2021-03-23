using Lowadi.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lowadi.Core
{
    class UsersLoad
    {
        readonly List<User> usersList;
        public UsersLoad(string pachXmlFIle)
        {
            if (pachXmlFIle == string.Empty)
            {
                throw new ArgumentNullException(nameof(pachXmlFIle), "Is empty");
            }
            usersList = new List<User>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pachXmlFIle);
            XmlElement elements = xmlDoc.DocumentElement;
            foreach (XmlNode element in elements.ChildNodes)
            {
                var login = element.Attributes["Login"].Value;
                var password = element.Attributes["Password"].Value;
                usersList.Add(new User { Login = login, Password = password });
            }
        }

        public IEnumerable<User> Users
        { get { return usersList; } }
    }
}
