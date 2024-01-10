using System;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ServerGoldMine
{
    public class RequestListner
    {
        private PlayerList _playerList;
        private HttpListener listener;
        private ResponseCollection _responseCollection;

        public RequestListner(PlayerList playerList, ResponseCollection responseCollection, string prefix)
        {
            _playerList = playerList;
            _responseCollection = responseCollection;

            listener =  new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Start();
            listener.AuthenticationSchemes = AuthenticationSchemes.Basic;
        }

        public async void StartRequestListen()
        {

            listener.Start();
            while (true)
            {
               
                 //Получение запроса
                 HttpListenerContext context = await listener.GetContextAsync();

                HttpListenerBasicIdentity identity = (HttpListenerBasicIdentity) context.User.Identity;

                PlayerInfo playerInfo = new PlayerInfo(identity.Name, identity.Password);

                if (_playerList.ExistPlayer(playerInfo) == false)
                {
                    Console.WriteLine($"Request was received from a user {identity.Name} who is not in database");

                    continue;
                }

                if (context.Request.HttpMethod == "GET")
                {
                    string responseMessage = _responseCollection.GetResponseForGET(context.Request.RawUrl, playerInfo);

                    if (responseMessage != "")
                        SendResponseAsync(context.Response, playerInfo, responseMessage);

                    await context.Response.OutputStream.FlushAsync();
                }

                if (context.Request.HttpMethod == "POST")
                {
                    string content = "";

                    StreamReader inputStream = new StreamReader(context.Request.InputStream);
                    content = inputStream.ReadToEnd();
                    Console.WriteLine($" CONTENT: {content};");

                    string responseMessage = _responseCollection.ResponseForPOST(context.Request.RawUrl, content, playerInfo);
                    if (responseMessage != "")
                        SendResponseAsync(context.Response, playerInfo, responseMessage);
                }
            }
        }

        public async void SendResponseAsync(HttpListenerResponse response, PlayerInfo info, string responseText)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(responseText);

            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            await output.WriteAsync(buffer, 0, buffer.Length);

            Console.WriteLine($"RESPONCE: USER: {info.Name}; METHOD: POST; CONTENT: {responseText};");
        }
    }
}
