//JAY AMISHKUMAR PATEL - 1001357017
//https://www.youtube.com/watch?v=cHq2lYLA4XY&list=PLAC179D21AF94D28F&index=7
//https://www.youtube.com/watch?v=p8Nlxtj0sV4&index=8&list=PLAC179D21AF94D28F

using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

//it basically prepares the servesr to accept client requests
namespace Mul_Con_server
{
    class Listner
    {
        //local variables for server socket, and its port
        Socket s;
        public int Port
        {
            get;
            private set;
        }

        //boolen variable to know whether the server is runnig or not   
        public bool listening
        {
            get;
            private set;
        }


        /// <summary>
        /// consructor to create server at the particular port specified in the parametere.
        /// </summary>
        /// <param name="port">server port number</param>       
        public Listner(int port)
        {
            Port = port;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Binds the server to the Specified port and IPAddress
        /// </summary>
        public void start()
        {
            if (listening)
            {
                return;
            }
            s.Bind(new IPEndPoint(0, Port));
            s.Listen(0);
            s.BeginAccept(callback, null);
            listening = true;
        }

        /// <summary>
        /// stops running the server and close that socket connection
        /// </summary>     
        public void stop()
        {
            if (!listening)
                return;
            s.Close();
            s.Dispose();
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// trigger when it accepts the request to connect the client to the server 
        /// delegate the handler to proceed with next step
        /// </summary>
        /// <param name="ar">object that contains the received data of socket</param>
        void callback(IAsyncResult ar)
        {
            try
            {
                Socket s = this.s.EndAccept(ar);              
               
                if (SocketAccepted != null)
                {                 
                    //calls the event handler for what to do with particular socket request 
                    SocketAccepted(s);
                }

                //start accepting new connection again
                this.s.BeginAccept(callback, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //event-delegate conncept listens the socket accept event for every client.
        //it will trigger as soon as it receives the socket connection from new client 
        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;
    }
}
