using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using ListViewExample.Model;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ListViewExample.Tile
{
   public class TileNote
         {

       private static string TaskLists(int result)
            
        {
           
            using (var db = DBConnection.DbConnection)
            {
                List<Task> NoteTask;
                NoteTask = new List<Task>();
                List<Task> list = (from p in db.Table<Task>() where p.completed == false select p).Take(3).ToList();
                string textstring = "";
                if (result < list.Count())
                {
                    textstring += list[result].datetime.Date.ToString("MM/dd/yy") +" "+ list[result].title;
                }

                return textstring;

            }
        }
       public static void Note()
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);

            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = TaskLists(0);
            XmlNodeList tileTextAttributes1 = tileXml.GetElementsByTagName("text");
            tileTextAttributes[1].InnerText = TaskLists(1);
            XmlNodeList tileTextAttributes2 = tileXml.GetElementsByTagName("text");
            tileTextAttributes[2].InnerText = TaskLists(2);

            XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appx:///assets/Square150x150Logo.png");

            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageCollection02);

            XmlNodeList squareTileTextAttributes = squareTileXml.GetElementsByTagName("text");
            squareTileTextAttributes[0].AppendChild(squareTileXml.CreateTextNode(TaskLists(0)));

            XmlNodeList squareTileTextAttributes3 = squareTileXml.GetElementsByTagName("text");
            squareTileTextAttributes[1].AppendChild(squareTileXml.CreateTextNode(TaskLists(1)));

            XmlNodeList squareTileTextAttributes4 = squareTileXml.GetElementsByTagName("text");
            squareTileTextAttributes[2].AppendChild(squareTileXml.CreateTextNode(TaskLists(2)));

            IXmlNode node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            TileNotification tileNotification = new TileNotification(tileXml);

            tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddDays(365);

            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);

            XmlDocument badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            XmlElement badgeElement = (XmlElement)badgeXml.SelectSingleNode("/badge");
            badgeElement.SetAttribute("value", BadgeNum());
            BadgeNotification badge = new BadgeNotification(badgeXml);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
            if (BadgeNum() == "0")
            {
                BadgeUpdateManager.CreateBadgeUpdaterForApplication().Clear();
            }
        }

        private static string BadgeNum()

        {

            using (var db = DBConnection.DbConnection)
            {
                List<Task> badge;
                badge = new List<Task>();
                List<Task> list = (from p in db.Table<Task>() where p.completed == false select p).ToList();
                int CountB = list.Count();
                string countB = CountB.ToString();

                return countB;
            }
        }

    }
}
