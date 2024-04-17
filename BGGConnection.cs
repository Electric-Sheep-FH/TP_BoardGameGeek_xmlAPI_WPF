using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TP_BoardGameGeek_xmlAPI_WPF
{
    public class BGGConnection
    {
        const string CONNECTION_URL = "https://boardgamegeek.com/xmlapi2/";

        public BGGConnection()
        {
        }

        public static string GetGameById(int id)
        {
            string idPath = CONNECTION_URL + $"thing?id={id}";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(idPath);

            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                XmlNode firstChildNode = node.ChildNodes[2];
                return firstChildNode.Attributes["value"].InnerText;
            }
            return "null";
        }

        public static List<string> GetGameByName(string name)
        {
            string namePath = CONNECTION_URL + $"search?query={name}";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(namePath);

            List<string> gameNames = new List<string>();

            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                XmlNode firstChildNode = node.ChildNodes[0];
                gameNames.Add(firstChildNode.Attributes["value"].InnerText);
            }
            return gameNames;
        }

        public static int GetGameId(string name)
        {
            string stringId = "0";
            int parsedId;

            string namePath = CONNECTION_URL + $"search?query={name}";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(namePath);

            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                if (node.ChildNodes[0].Attributes["value"].InnerText.ToLower() == name.ToLower())
                {
                        stringId = node.Attributes["id"].InnerText;
                }
            }

            int.TryParse(stringId, out parsedId);

            return parsedId;
        }

        public static string GetGameDetails(string gameName)
        {
            int gameId = GetGameId(gameName);
            string gameTitle = "";
            string gameDescription = "";
            string minPlayers = "";
            string maxPlayers = "";
            string yearPublished = "";

            string idPath = CONNECTION_URL + $"thing?id={gameId}";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(idPath);

            foreach(XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                foreach (XmlNode childNode in node)
                {
                    if (childNode.Name == "name" && childNode.Attributes["type"].InnerText == "primary")
                    {
                        gameTitle = childNode.Attributes["value"].InnerText;
                    }
                    if (childNode.Name == "description")
                    {
                        gameDescription = childNode.InnerText;
                    }
                    if (childNode.Name == "minplayers")
                    {
                        minPlayers = childNode.Attributes["value"].InnerText;
                    }
                    if (childNode.Name == "maxplayers")
                    {
                        maxPlayers = childNode.Attributes["value"].InnerText;
                    }
                    if (childNode.Name == "yearpublished")
                    {
                        yearPublished = childNode.Attributes["value"].InnerText;
                    }
                }
            }

            return $"Nom de jeu :\n{gameTitle}\n\nDescription :\n{gameDescription}\n\nNombres joueurs :\n{minPlayers} à {maxPlayers} joueurs\n\nAnnée de publication :\n{yearPublished}";
        }
    }
}
