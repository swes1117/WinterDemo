using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Authentication;
using MongoDB.Bson;
using HtmlAgilityPack;
using System.Linq;
using System.Xml;

namespace Bot_Application1
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// 
        string[] data=new string[100];
        MongoClient client;
        public bool Isteam(string teamName)
        {
            string[] stringArray = new string[4];
            stringArray[0]= "Warrior";
            stringArray[1]= "Thunder";
            stringArray[2]= "Spurs";
            stringArray[3]= "Rocket";


            for (var i = 0; i < 3; i++)
            {
                if (teamName == stringArray[i])
                    return true;
            }
            return false;
        }

        public int Isplayer(string playerName)
        {
            string[] stringArray = new string[25];
            stringArray[0] = "Chris Paul";
            stringArray[1] = "James Harden";
            stringArray[2] = "Nene";
            stringArray[3] = "Clint Capela";
            stringArray[4] = "Eric Gordon";
            stringArray[5] = "Andre Iguodala";
            stringArray[6] = "Stephen Curry";
            stringArray[7] = "Kevin Durant";
            stringArray[8] = "Draymond Green";
            stringArray[9] = "Klay Thompson";
            stringArray[10] = "Russel Westbrook";
            stringArray[11] = "Steven Adams";
            stringArray[12] = "Andre Roberson";
            stringArray[13] = "Carmelo Anthony";
            stringArray[14] = "Paul George";
            stringArray[15] = "Kawhi Leonard";
            stringArray[16] = "Pau Gasol";
            stringArray[17] = "Tony Parker";
            stringArray[18] = "Rudy Gay";
            stringArray[19] = "LaMarcus Aldridge";
            for (var i = 0; i < 19; i++)
            {
                if (playerName == stringArray[i])
                {
                    if (i >= 0 && i <= 4)
                    {
                        //Represnet for Rocket
                        return 0;
                    }
                    else if (i >= 5 && i <= 9)
                    {
                        //Represent for Warrior
                        return 1;
                    }
                    else if (i >= 10 && i <= 14)
                    {
                        //Represent for Thunder
                        return 2;
                    }
                    else if (i >= 15 && i <= 19)
                    {
                        //Represent for  Spurs
                        return 3;
                    }                  
                }                    
            }
            return -1;
        }
        public MessagesController()
        {
            string userName = "projectdemomsp";
            string host = "projectdemomsp.documents.azure.com";
            string password = "xWF0xfKAhEPvzZ3fP19uB1etEcXJoUYslc09Iy6kw2C1T8mrJulyZBK3gfGjnwp2q91Gjv60DBO2uQprREhbTw==";
            string dbName = "League";

            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credentials = new List<MongoCredential>()
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };

            client = new MongoClient(settings);
        }
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                Activity reply = activity.CreateReply();
               
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                if (activity.Text.StartsWith("Tim"))
                {
                    reply.Attachments.Add(new Attachment()
                    {
                        Content = "Info",
                        Name = "Tim",
                        ContentType = "image/png",
                        ContentUrl = "https://github.com/swes1117/2017-10-18Demo/blob/master/1018BotDemo/Dialogs/img/thunder.jpg?raw=true"
                    });
                    
                }
                if (activity.Text.StartsWith("Demo"))
                {
                    GetNba();
                    reply.Text = data[2];
                   // reply.Text = "Hello";
                }

                if (Isteam(activity.Text))
                {

                    if (activity.Text.StartsWith("Thunder"))
                    {
                        List<CardImage> images = new List<CardImage>();
                        CardImage ci = new CardImage("https://github.com/swes1117/2017-10-18Demo/blob/master/1018BotDemo/Dialogs/img/thunder.jpg?raw=true");
                        images.Add(ci);
                        CardAction ca1 = new CardAction()
                        {
                            Title = "Westbrook",
                            Type = ActionTypes.ImBack,
                            Value = "Russel Westbrook"
                        };
                        CardAction ca2 = new CardAction()
                        {
                            Title = "Anthony",
                            Type = ActionTypes.ImBack,
                            Value = "Carmelo Anthony"
                        };
                        CardAction ca3 = new CardAction()
                        {
                            Title = "Roberson",
                            Type = ActionTypes.ImBack,
                            Value = "Andre Roberson"
                        };
                        CardAction ca4 = new CardAction()
                        {
                            Title = "George",
                            Type = ActionTypes.ImBack,
                            Value = "Paul George"
                        };
                        CardAction ca5 = new CardAction()
                        {
                            Title = "Adams",
                            Type = ActionTypes.ImBack,
                            Value = "Steven Adams"
                        };
                        HeroCard hc = new HeroCard()
                        {
                            Images = images,
                            Title = "Oklahoma City Thunder",
                            Text = "Choose the player you want to know",
                            Buttons = new List<CardAction>()
                        };
                        hc.Buttons.Add(ca1);
                        hc.Buttons.Add(ca2);
                        hc.Buttons.Add(ca3);
                        hc.Buttons.Add(ca4);
                        hc.Buttons.Add(ca5);
                        reply.Attachments.Add(hc.ToAttachment());
                    }
                    else if (activity.Text.StartsWith("Warrior"))
                    {
                        List<CardImage> images = new List<CardImage>();
                        CardImage ci = new CardImage("https://github.com/swes1117/2017-10-18Demo/blob/master/1018BotDemo/Dialogs/img/warrior.jpg?raw=true");
                        images.Add(ci);
                        CardAction ca1 = new CardAction()
                        {
                            Title = "Curry",
                            Type = ActionTypes.ImBack,
                            Value = "Stephen Curry"
                        };
                        CardAction ca2 = new CardAction()
                        {
                            Title = "Durant",
                            Type = ActionTypes.ImBack,
                            Value = "Kevin Durant"
                        };
                        CardAction ca3 = new CardAction()
                        {
                            Title = "Thompson",
                            Type = ActionTypes.ImBack,
                            Value = "Klay Thompson"
                        };
                        CardAction ca4 = new CardAction()
                        {
                            Title = "Green",
                            Type = ActionTypes.ImBack,
                            Value = "Draymond Green"
                        };
                        CardAction ca5 = new CardAction()
                        {
                            Title = "Iguodala",
                            Type = ActionTypes.ImBack,
                            Value = "Andre Iguodala"
                        };
                        HeroCard hc = new HeroCard()
                        {
                            Images = images,
                            Title = "Gorden State Warrior",
                            Text = "Choose the player you want to know",
                            Buttons = new List<CardAction>()
                        };
                        hc.Buttons.Add(ca1);
                        hc.Buttons.Add(ca2);
                        hc.Buttons.Add(ca3);
                        hc.Buttons.Add(ca4);
                        hc.Buttons.Add(ca5);
                        reply.Attachments.Add(hc.ToAttachment());
                    }

                    else if (activity.Text.StartsWith("Rocket"))
                    {
                        List<CardImage> images = new List<CardImage>();
                        CardImage ci = new CardImage("https://github.com/swes1117/2017-10-18Demo/blob/master/1018BotDemo/Dialogs/img/rocket.jpg?raw=true");
                        images.Add(ci);
                        CardAction ca1 = new CardAction()
                        {
                            Title = "Paul",
                            Type = ActionTypes.ImBack,
                            Value = "Chris Paul"
                        };
                        CardAction ca2 = new CardAction()
                        {
                            Title = "Harden",
                            Type = ActionTypes.ImBack,
                            Value = "James Harden"
                        };
                        CardAction ca3 = new CardAction()
                        {
                            Title = "Nene",
                            Type = ActionTypes.ImBack,
                            Value = "Nene"
                        };
                        CardAction ca4 = new CardAction()
                        {
                            Title = "Capela",
                            Type = ActionTypes.ImBack,
                            Value = "Clint Capela"
                        };
                        CardAction ca5 = new CardAction()
                        {
                            Title = "Gordon",
                            Type = ActionTypes.ImBack,
                            Value = "Eric Gordon"
                        };
                        HeroCard hc = new HeroCard()
                        {
                            Images = images,
                            Title = "Houston Rocket",
                            Text = "Choose the player you want to know",
                            Buttons = new List<CardAction>()
                        };
                        hc.Buttons.Add(ca1);
                        hc.Buttons.Add(ca2);
                        hc.Buttons.Add(ca3);
                        hc.Buttons.Add(ca4);
                        hc.Buttons.Add(ca5);
                        reply.Attachments.Add(hc.ToAttachment());
                    }
                    else if (activity.Text.StartsWith("Spurs"))
                    {
                        List<CardImage> images = new List<CardImage>();
                        CardImage ci = new CardImage("https://github.com/swes1117/2017-10-18Demo/blob/master/1018BotDemo/Dialogs/img/spurs.jpg?raw=true");
                        images.Add(ci);
                        CardAction ca1 = new CardAction()
                        {
                            Title = "Leonard",
                            Type = ActionTypes.ImBack,
                            Value = "Kawhi Leonard"
                        };
                        CardAction ca2 = new CardAction()
                        {
                            Title = "Gasol",
                            Type = ActionTypes.ImBack,
                            Value = "Pau Gasol"
                        };
                        CardAction ca3 = new CardAction()
                        {
                            Title = "Parker",
                            Type = ActionTypes.ImBack,
                            Value = "Tony Parker"
                        };
                        CardAction ca4 = new CardAction()
                        {
                            Title = "Gay",
                            Type = ActionTypes.ImBack,
                            Value = "Rudy Gay"
                        };
                        CardAction ca5 = new CardAction()
                        {
                            Title = "Aldridge",
                            Type = ActionTypes.ImBack,
                            Value = "LaMarcus Aldridge"
                        };
                        HeroCard hc = new HeroCard()
                        {
                            Images = images,
                            Title = "Oklahoma City Thunder",
                            Text = "Choose the player you want to know",
                            Buttons = new List<CardAction>()
                        };
                        hc.Buttons.Add(ca1);
                        hc.Buttons.Add(ca2);
                        hc.Buttons.Add(ca3);
                        hc.Buttons.Add(ca4);
                        hc.Buttons.Add(ca5);
                        reply.Attachments.Add(hc.ToAttachment());
                    }
                }              
                if (Isplayer(activity.Text)!=-1)
                {
                    var db = client.GetDatabase("League");
                    string teamName="";
                    if (Isplayer(activity.Text)==0)
                    {
                        teamName = "Houston Rocket";
                    }
                    else if (Isplayer(activity.Text) == 1)
                    {
                        teamName = "Golden State Warriors";
                    }
                    else if (Isplayer(activity.Text) == 2)
                    {
                        teamName = "Oklahoma City Thunder";
                    }
                    else if (Isplayer(activity.Text) == 3)
                    {
                        teamName = "Sam Antonio Spurs";
                    }

                    var playerName = activity.Text;
                    string reb="";
                    string pts="";
                    string ast = "";
                    string stl = "";
                    string blk = "";
                    string fg = "";
                    string to = "";

                    var coll = db.GetCollection<Player>(teamName);

                    var outcome = coll
                        .Find(b => b.Name == playerName)
                        .ToListAsync()
                        .Result;
                    foreach (var attr in outcome)
                    {
                        reb = attr.REB;
                        pts = attr.PTS;
                        ast = attr.AST;
                        stl = attr.STL;
                        blk = attr.BLK;
                        fg = attr.FG;
                        to = attr.TO;       
                    }

                    List<CardImage> images = new List<CardImage>();
                    CardImage ci = new CardImage("https://github.com/swes1117/2017-10-18Demo/blob/master/1018BotDemo/Dialogs/img/"+playerName+".jpg?raw=true");
                    images.Add(ci);
                    CardAction ca1 = new CardAction()
                    {
                        Title = "PTS : " + pts,
                        Type="ipenUrl",
                    };
                    CardAction ca2 = new CardAction()
                    {
                        Title = "REB : " + reb,
                        Type = "ipenUrl",
                    };
                    CardAction ca3 = new CardAction()
                    {
                        Title = "AST : " + ast,
                        Type = "ipenUrl",
                    };
                    CardAction ca4 = new CardAction()
                    {
                        Title = "STL : " + stl,
                        Type = "ipenUrl",
                    };
                    CardAction ca5 = new CardAction()
                    {
                        Title = "BLK : " + blk,
                        Type = "ipenUrl",
                    };
                    CardAction ca6 = new CardAction()
                    {
                        Title = "FG : " + fg+"%",
                        Type = "ipenUrl",
                    };
                    CardAction ca7 = new CardAction()
                    {
                        Title = "To : " + to,
                        Type = "ipenUrl",
                    };
                    
                    HeroCard hc = new HeroCard()
                    {
                        Title =playerName,
                        Subtitle = "2016-2017 season record",
                        Images = images,
                        Buttons = new List<CardAction>()
                    };

                    hc.Buttons.Add(ca1);
                    hc.Buttons.Add(ca2);
                    hc.Buttons.Add(ca3);
                    hc.Buttons.Add(ca4);
                    hc.Buttons.Add(ca5);
                    hc.Buttons.Add(ca6);
                    hc.Buttons.Add(ca7);

                    reply.Attachments.Add(hc.ToAttachment());
                }


                if (activity.Text.StartsWith("Hello"))
                {
                    CardAction ca1 = new CardAction()
                    {
                        Title = "Warrior",
                        Type = ActionTypes.ImBack,
                        Value = "Warrior",
                    };
                    CardAction ca2 = new CardAction()
                    {
                        Title = "Thunder",
                        Type = ActionTypes.ImBack,
                        Value = "Thunder"
                    };
                    CardAction ca3 = new CardAction()
                    {
                        Title = "Spurs",
                        Type = ActionTypes.ImBack,
                        Value = "Spurs"
                    };
                    CardAction ca4 = new CardAction()
                    {
                        Title = "Rocket",
                        Type = ActionTypes.ImBack,
                        Value = "Rocket"
                    };
                    List<CardImage> images = new List<CardImage>();
                    CardImage ci = new CardImage("https://github.com/swes1117/2017-10-18Demo/blob/master/1018BotDemo/Dialogs/img/nba.jpg?raw=true");
                    images.Add(ci);
                    ThumbnailCard tc = new ThumbnailCard()
                    {
                        Images = images,
                        Title = "Welcome to NBA",
                        Subtitle = "Choose the team you like",
                        Text = "fantastic",
                        Buttons = new List<CardAction>()

                    };
                    tc.Buttons.Add(ca1);
                    tc.Buttons.Add(ca2);
                    tc.Buttons.Add(ca3);
                    tc.Buttons.Add(ca4);
                    reply.Attachments.Add(tc.ToAttachment());
                }



                else if (activity.Text.StartsWith("Test")) {

                   reply.Attachments.Add(new Attachment()
                    {
                        Name = "GOOD",
                        ContentType = "image/png",
                        ContentUrl= "https://github.com/swes1117/2017-10-18Demo/blob/master/1018BotDemo/Dialogs/img/nba.jpg?raw=true"
                   });                  
                }
                connector.Conversations.ReplyToActivityAsync(reply);
                //await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }


        public async Task<int> GetNba()
        {
            
            



            return 0;
        }
    }
    public class Player
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string REB { get; set; }
        public string PTS { get; set; }
        public string AST { get; set; }
        public string STL { get; set; }
        public string BLK { get; set; }
        public string FG { get; set; }
        public string TO { get; set; }
    }
    
     

}