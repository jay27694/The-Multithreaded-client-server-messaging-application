# The-Multithreaded-Client-Server-Messaging-Application

The Multithreaded Client-Server Messaging Application     	
                                                                                                                                                               
•	Implemented the multithreaded server in such way that it can handle manage multiple clients at same time.
•	Server is capable enough to keep the traffic separate for different connections among the clients.

System Requirements:
Windows OS, Visual Studio, WAMP server, C#

Assumption: 
Length of the Username should be 20 characters. It is not the limitation because I am using the local MySql server and database table has the field length which is 20. Clients can register with long username if we change that size.

How to run?

1) 	Start WAMP server for MySql Database
2)	Run the server process. 
3)	Run the client processes (as many as we want)
4)	Connect each client to the server
5)	Register each client with Username by marking the check box if it wants to show the online status to other clients.
6)	Send the Username from client to which with it wants to connect
7)	If connection established, now they both can chat 
8)	Server can handle as many paired connections as we made and keep the traffic separate.  


