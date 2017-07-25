//JAY AMISHKUMAR PATEL - 1001357017
//https://www.youtube.com/watch?v=cHq2lYLA4XY&list=PLAC179D21AF94D28F&index=7
//https://www.youtube.com/watch?v=p8Nlxtj0sV4&index=8&list=PLAC179D21AF94D28F

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//client application
namespace Mul_Con_C
{
    public partial class Form1 : Form
    {
        //declare socket for new cooncetion to server
        Socket sck = null;

        //event-delegate concept when it receives the data from the server
        public delegate void DataAcceptedHandler(Socket e, byte[] data);
        public event DataAcceptedHandler DataAccepted;
        
        /// <summary>
        /// define the socket varible for new connection
        /// start receiving the data and triggers when it actually receives
        /// </summary>
        public Form1()
        {
            InitializeComponent();                        
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);           
            DataAccepted += Form1_DataAccepted;                     
        }     
        
        /// <summary>
        /// as soon as it receives the data from server it update the listbox
        /// </summary>
        /// <param name="e">client itsef</param>
        /// <param name="data">the message</param>
        private void Form1_DataAccepted(Socket e, byte[] data)
        {
            string s = Encoding.Default.GetString(data);
            int lastindex = s.LastIndexOf("\n");
            string a = s.Substring(lastindex);
            Invoke((MethodInvoker)delegate
            {
                lstchat.Items.Add(a);
            });
        }

        /// <summary>
        /// establish the connection with the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {            
            try
            {               
                sck.Connect("127.0.0.1",8080);         
                MessageBox.Show("connected");
                Start();
                Invoke((MethodInvoker)delegate
                {
                    btnRegister.Enabled = true;
                });               
            }
            catch
            {                               
                MessageBox.Show("Problem to connect with given server");
                sck = null;
            }          
        }

        /// <summary>
        /// strat receiving the data from the server and triggers when it receives
        /// </summary>
        private void Start()
        {
            sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
        }

        /// <summary>
        /// get the data and delegate the handler to proceed with next
        /// </summary>
        /// <param name="ar">object that contains the data over socket</param>
        private void callback(IAsyncResult ar)
        {
            try
            {
                sck.EndReceive(ar);
                byte[] buf = new byte[8192];
                int rec = sck.Receive(buf, buf.Length, 0);
                if (rec < buf.Length)
                {
                    Array.Resize<byte>(ref buf, rec);
                }
                var str = System.Text.Encoding.Default.GetString(buf);
                if (str[0].ToString() == "A")
                {
                    str = str.Substring(1, str.Length - 1);
                    string[] olusers = str.Split(',');
                    Invoke((MethodInvoker)delegate
                    {
                        olusrs.Items.Clear();
                        foreach (string s in olusers)
                        {
                            olusrs.Items.Add(s);
                        }
                    });
                }
                else if (DataAccepted != null)
                {
                    DataAccepted(sck, buf);
                }
                sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message +": SERVER WENT OFF");
            }           
        }

        /// <summary>
        /// triggers when the send button clicks
        /// send the message to the server
        /// add the message to its own text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsend_Click(object sender, EventArgs e)
        {
            string msg = "POST /Server HTTP/1.1\n";
            msg += "Host: localhost:8080\n";
            msg += "Timestamp: " + DateTime.Now.ToString() + "\n";  
            msg += "Content-Length: " + textBox1.Text.Length +"\n";
            msg += textBox1.Text;
            //MessageBox.Show(msg);
            int s = sck.Send(Encoding.Default.GetBytes(msg));
            Invoke((MethodInvoker)delegate
            {
                lstchat.Items.Add("Me:" + textBox1.Text);
            });
            Invoke((MethodInvoker)delegate
            {
                textBox1.Text = "";
            });
        }

        /// <summary>
        /// triggers when register button clicks
        /// send the username to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string sts = null;
            if (status.Checked)
            {
                sts = "1";
            }
            else
            {
                sts = "0";
            }
            int s = sck.Send(Encoding.Default.GetBytes("R"+name.Text+sts));
            if (s > 0)
                MessageBox.Show("Registerd");
            Invoke((MethodInvoker)delegate
            {
                this.Text = name.Text;
            });
            Invoke((MethodInvoker)delegate
            {
                btnConnectTo.Enabled = true;
            });
               
        }

        /// <summary>
        /// sends the username to the server to which clients want to connect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnectTo_Click(object sender, EventArgs e)
        {           
            int s = sck.Send(Encoding.Default.GetBytes("T" + clientName.Text));           
        }    
    }
}
