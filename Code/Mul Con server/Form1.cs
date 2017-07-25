//JAY AMISHKUMAR PATEL - 1001357017
//https://msdn.microsoft.com/en-us/library/ms171728(VS.80).aspx 
//https://www.youtube.com/watch?v=cHq2lYLA4XY&list=PLAC179D21AF94D28F&index=7
//https://www.youtube.com/watch?v=p8Nlxtj0sV4&index=8&list=PLAC179D21AF94D28F

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//This is the server application
namespace Mul_Con_server
{
    public partial class Server : Form
    {       
        Listner listner;
     
        //variable to set the postion for chat box dor every paired connetions
        int fromTop = 220;
        int fromLeft = 10;

        string name = null;

        /// <summary>
        /// initialization of db class to perform the databse operation
        /// binds the server to specific port and make ready for accepting clients
        /// transfer the handleing as soon as it gets the new clients request 
        /// </summary>
        public Server()
        {
            InitializeComponent();
            db.InitializeDB();
                    
            listner = new Listner(8080);
            listner.SocketAccepted += L_SocketAccepted;
            Load += new EventHandler(Server_Load);
        }
       
        /// <summary>
        /// calls when server is loaded to start the listner and background thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_Load(object sender, EventArgs e)
        {
            listner.start();
            backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// handles with every request of clients 
        /// </summary>
        /// <param name="e">socket object</param>
        private void L_SocketAccepted(System.Net.Sockets.Socket e)
        {           
            //handles the client connections on server side
            Client c = new Client(e);

            //handles when to proceed with what for the each event specified for the client connection           
            c.Registered += C_Registered;
            c.ConnectedTo += C_ConnectedTo;
            c.Received += C_Received;
            c.Disconnected += C_Disconnected;      
                 
            //prepares the listview for client connections 
            Invoke((MethodInvoker) delegate
            {
                ListViewItem l = new ListViewItem();
                l.Tag = c; 
                l.Text = c.EndPoint.ToString();
                l.SubItems.Add(c.ID); //item1 - username/id
                l.SubItems.Add("XX"); //item2 - last msg
                l.SubItems.Add("XX"); //item3 - time stamp
                l.SubItems.Add("XX"); //item4 - connected to
                listView1.Items.Add(l);
            });
        }

        /// <summary>
        /// handler for request "client wansts to connect with other client" event 
        /// update the listview if the requested clients is found and is not connected to other
        /// prepares the chatbox on server side to show the full converstion 
        /// </summary>
        /// <param name="sender">requesting client</param>
        /// <param name="data">name of the requested client</param>
        private void C_ConnectedTo(Client sender, string data)
        {          
            Invoke((MethodInvoker)delegate
            {
                bool found = false, Already_Connected=false;               
                Client Set_Receiver = null, Set_Sender = null; 
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    Set_Receiver = listView1.Items[i].Tag as Client;
                    if(Set_Receiver.Client_Name == data)
                    {
                        if(listView1.Items[i].SubItems[4].Text == "")
                        {
                            Set_Receiver.Connected_To = sender;
                            listView1.Items[i].SubItems[4].Text = Set_Receiver.Connected_To.Client_Name;
                            found = true;                         
                            break;
                        }
                        else
                        {                           
                            Already_Connected = true;
                            break;
                        }                       
                    }
                }

                if (found == false && Already_Connected == false)
                {
                    MessageBox.Show("Client Not Present");
                }                   
                else if (Already_Connected == true)
                {
                    MessageBox.Show("Client is already connected to another client");
                }
                else
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        Set_Sender = listView1.Items[i].Tag as Client;
                        if (Set_Sender.Client_Name == sender.Client_Name)
                        {
                            Set_Sender.Connected_To = Set_Receiver;
                            listView1.Items[i].SubItems[4].Text = Set_Sender.Connected_To.Client_Name;
                            name = Set_Sender.Client_Name + " Connected To " + Set_Receiver.Client_Name;                            
                        }
                    }

                    new Thread(() =>
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            ListBox lb = new ListBox();
                            lb.Height = 250;
                            lb.Width = 250;
                            lb.Left = fromLeft;
                            fromLeft += 270;
                            lb.Top = fromTop;
                            lb.Name = name;
                            lb.Items.Add(name);
                            lb.Items.Add("------------------------------------------------------------------------------------------------------------------------------------");
                            this.Controls.Add(lb);
                        });
                    }).Start();
                }                
            });
        }

        /// <summary>
        /// update the listview specific name in the record of the requesting client 
        /// </summary>
        /// <param name="sender">requesting client</param>
        /// <param name="data">Username for the requesting client</param>
        private void C_Registered(Client sender, string data, string sts)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    Client client = listView1.Items[i].Tag as Client;
                    if (client.ID == sender.ID)
                    {
                        client.Client_Name = data;                        
                        listView1.Items[i].SubItems[1].Text = client.Client_Name;
                        listView1.Items[i].SubItems[4].Text = "";
                        break;
                    }
                }
            });

            new Thread(() =>
            {
                db.InsertData(data, sts);
            }).Start();
        }

        /// <summary>
        /// delete the record from the listview if requesting client exits the application
        /// and delete the chat box associated with its chat.
        /// dalete the record of "connected to" field associted with connected client
        /// </summary>
        /// <param name="sender">the requsting client</param>
        private void C_Disconnected(Client sender)
        {
            //db.UserDelete(sender.Client_Name);
            Invoke((MethodInvoker)delegate
            {
                Client dis_connected=null, client =null;
                 
                for (int i = 0;i<listView1.Items.Count;i++)
                {
                    dis_connected = listView1.Items[i].Tag as Client;
                    if(dis_connected.ID == sender.ID)
                    {                       
                        listView1.Items.RemoveAt(i);
                        break;
                    }
                }

                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    client = listView1.Items[i].Tag as Client;
                    if (listView1.Items[i].SubItems[4].Text == sender.Client_Name)
                    {
                        listView1.Items[i].SubItems[4].Text = "";                       
                        client.Connected_To = null;
                        break;
                    }
                }
            });

            new Thread(() =>
            {
                if (sender.Connected_To != null)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        string s = sender.Client_Name + " Connected To " + sender.Connected_To.Client_Name;
                        string s1 = sender.Connected_To.Client_Name + " Connected To " + sender.Client_Name;

                        ListBox newlb = this.Controls.Find(s, true).FirstOrDefault() as ListBox;
                        if (newlb == null)
                        {
                            newlb = this.Controls.Find(s1, true).FirstOrDefault() as ListBox;
                        }
                        newlb.Dispose();
                    });
                    fromLeft -= 270;
                }               
            }).Start();
        }

        /// <summary>
        /// when message received, upadet the last message and time column of the listview for sender
        /// update the chat box associated with connected_to client for sender
        /// server sends the message to the recipent asscociated with sender
        /// </summary>
        /// <param name="sender">client who is sending message</param>
        /// <param name="data">the message</param>
        private void C_Received(Client sender, string data)
        {
            int lastindex = data.LastIndexOf("\n");
            string a = data.Substring(lastindex);
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    Client client = listView1.Items[i].Tag as Client;
                    if (client.ID == sender.ID)
                    {
                        listView1.Items[i].SubItems[2].Text = a;
                        listView1.Items[i].SubItems[3].Text = DateTime.Now.ToString();
                        new Thread(() =>
                        {
                            send_message(client, data);
                        }).Start();
                        break;
                    }
                }

            });

            new Thread(() =>
            {
                string[] words = data.Split('\n');
                Invoke((MethodInvoker)delegate
                {
                    string s = sender.Client_Name + " Connected To " + sender.Connected_To.Client_Name;
                    string s1 = sender.Connected_To.Client_Name + " Connected To " +  sender.Client_Name;

                    ListBox newlb = this.Controls.Find(s, true).FirstOrDefault() as ListBox;
                    if (newlb == null)
                    {
                        newlb = this.Controls.Find(s1, true).FirstOrDefault() as ListBox;
                    }
                    newlb.Items.Add(sender.Client_Name+ ": ");
                    foreach(string x in words)
                        newlb.Items.Add("   "+x);
                });

            }).Start();
        }

        /// <summary>
        /// method called for sending the message to receipent client 
        /// </summary>
        /// <param name="c">sender client</param>
        /// <param name="data">the message</param>
        private void send_message(Client c, string data)
        {                      
            byte[] data1 = Encoding.Default.GetBytes(data);
            c.Connected_To.sck.Send(data1, 0, data.Length, 0);
        }

        /// <summary>
        /// finds the online users who agree to show online status from the databse.
        /// get the string from the online users from the db class
        /// send the string to the particular client 
        /// </summary>
        private void LoopThroughListUsers()
        {
            if (listView1.InvokeRequired)
            {
                // This will re-call LoopThroughListItems - on the UI Thread
                listView1.Invoke(new Action(LoopThroughListUsers));
                return;
            }
            string s = null;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                Thread.Sleep(500);
                Client client = listView1.Items[i].Tag as Client;
                s = db.GetOnlineUsers(client.Client_Name);
                
                if(client.sck.Connected)
                    client.sck.Send(Encoding.Default.GetBytes(s));  

                Console.WriteLine("server: "+s);             
            }
        }

        /// <summary>
        /// created for sending the list of online users to the perticular client       
        /// </summary>
        /// <param name="sender">default para</param>
        /// <param name="e">default para</param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                LoopThroughListUsers();
                Thread.Sleep(500);
            }           
        }
    }
}
