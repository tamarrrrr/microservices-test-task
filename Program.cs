using helloworldserver;

var url = "http://localhost:8080/";
// Create a Http server and start listening for incoming connections
SimpleServer.serverListener = new System.Net.HttpListener();
SimpleServer.serverListener.Prefixes.Add(url);
SimpleServer.serverListener.Start();
Console.WriteLine("Listening for connections on {0}", url);

// Handle requests
Task listenTask = SimpleServer.HandleIncomingConnections();
listenTask.GetAwaiter().GetResult();

// Close the listener
SimpleServer.serverListener.Close();
