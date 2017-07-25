//JAY AMISHKUMAR PATEL - 1001357017
//https://www.youtube.com/watch?v=cHq2lYLA4XY&list=PLAC179D21AF94D28F&index=7
//https://www.youtube.com/watch?v=p8Nlxtj0sV4&index=8&list=PLAC179D21AF94D28F

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//maintains the clients' connection to the server
namespace Mul_Con_server
{
    class Client
    {
        //local variavles of the clients such as ID, Name, EndPoint, Socket, and to which clients it wants to connect
        public string ID
        {
            get;
            private set;
        }
        public string Client_Name
        {
            get;
            set;
        }
        public Client Connected_To
        {
            get;
            set;
        }
        public IPEndPoint EndPoint
        {
            get;
            private set;
        }

        public Socket sck;


        /// <summary>
        /// sets the end point for the conncetiom specified in parameter
        /// and start receiving the data from the particular client
        /// </summary>
        /// <param name="accepted">socket connetion for specific client</param>
        public Client(Socket accepted)
        {
            sck = accepted;
            //sets the temporary ID to client
            ID = Guid.NewGuid().ToString();
            ConnectedTo = null;
            EndPoint = (IPEndPoint)sck.RemoteEndPoint;
            sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
        }
       
        /// <summary>
        /// triggers when the it receives the data from the client
        /// </summary>
        /// <param name="ar"></param>
        private void callback(IAsyncResult ar)
        {
           try
            {
                sck.EndReceive(ar);

                //prepares the varible for receiving the data
                byte[] buf = new byte[8192];
                int rec = sck.Receive(buf, buf.Length, 0);
                if (rec < buf.Length)
                {
                    Array.Resize<byte>(ref buf, rec);
                }
                var str = System.Text.Encoding.Default.GetString(buf); 
                
                //trigges when clients request for registraion                              
                if (str[0].ToString().Equals("R"))
                {
                    str = str.Substring(1, str.Length - 1);
                    string sts = str[str.Length-1].ToString();
                    str = str.Remove(str.Length - 1);
                    if (Registered != null)
                    {                       
                        Registered(this, str, sts);
                    }
                }
                //trigges when clients request for connection with other client  
                else if (str[0].ToString().Equals("T"))
                {
                    str = str.Substring(1, str.Length - 1);
                    if (ConnectedTo != null)
                    {
                        ConnectedTo(this, str);
                    }
                }
                //trigges when clients sends the data to client with which it is connected  
                else if (Received != null)
                {                                       
                    Received(this, str);
                }
                //start receiving again 
                sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
            }
            catch(Exception e)
            {              
                close();
                if (Disconnected != null)
                {
                    Disconnected(this);
                }
            }
        }

        /// <summary>
        /// called when the client closes the application
        /// </summary>
        public void close()
        {
            sck.Close();
            sck.Dispose();
            db.UserDelete(this.Client_Name);         
        }

        /// <summary>
        /// evet-delegate concept for haldling the client requests 
        /// </summary>
        public delegate void ClientReceivedHandeler(Client sender, string data);
        public delegate void ClientDisconnected(Client sender);
        public delegate void ClientRegisterHandler(Client sender, string data, string sts);
        public delegate void ClientConnectHandler(Client sender, string data);

        public event ClientReceivedHandeler Received;
        public event ClientDisconnected Disconnected;
        public event ClientRegisterHandler Registered;
        public event ClientConnectHandler ConnectedTo;
    }
}
