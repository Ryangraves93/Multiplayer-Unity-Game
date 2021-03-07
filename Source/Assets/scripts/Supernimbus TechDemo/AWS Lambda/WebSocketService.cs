using UnityEngine;
using NativeWebSocket;

/*Class is written to handle any communication between AWS Lambda and the frontend client using websockets.
 The websockets allow for a constant connection between client and AWS. The class will pass the current 
 player score to Lambda, which well then be return with a new value + 10 from AWS. Currently has a single
 op code which could be extended to use more functionality*/
public class WebSocketService : Singleton<WebSocketService>
{
    public const string MessageOp = "11";
    private bool intentionalClose = false;
    private WebSocket _websocket;

    private string _webSocketDns = "wss://o0w3wgpszk.execute-api.us-east-1.amazonaws.com/TechDemo";

    // Establishes the connection's lifecycle callbacks.


    /*Sends a GameMessage to the Lambda with with a message type, op and the value to change.*/
    public void SendMessage(string messageType, int score)
    {
        GameMessage startRequest = new GameMessage(messageType, MessageOp, score);

        SendWebSocketMessage(JsonUtility.ToJson(startRequest));
    }

    /*Responds with callbacks depending on action.*/
    private void SetupWebsocketCallbacks()
    {
        _websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            intentionalClose = false;
        };

        _websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        _websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");

        };

        //Receiving message from lambda
        _websocket.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");
            string message = System.Text.Encoding.UTF8.GetString(bytes);

            ProcessReceivedMessage(message);
        };
    }

    // Connects to the websocket
    async public void Connect()
    {
        // waiting for messages
        await _websocket.Connect();
    }

    /*Parses returned message to a JSON and constructs a game message from the response */
    private void ProcessReceivedMessage(string message)
    {
        GameMessage gameMessage = JsonUtility.FromJson<GameMessage>(message);
        GameSparksManager.Instance.SetPlayerScore(gameMessage.score);
    }

    /*Sends a plain text message to Lambda */
    public async void SendWebSocketMessage(string message)
    {
        if (_websocket.State == WebSocketState.Open)
        {

            await _websocket.SendText(message);
        }
    }

    /*Quit game */
    public async void QuitGame()
    {
        intentionalClose = true;
        await _websocket.Close();
    }

    private async void OnApplicationQuit()
    {
        await _websocket.Close();
    }

    void Start()
    {
        Debug.Log("Websocket start");
        intentionalClose = false;

        _websocket = new WebSocket(_webSocketDns);
        SetupWebsocketCallbacks();
        Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _websocket.DispatchMessageQueue();
#endif
    }

    public void init() { }

    protected WebSocketService() { }
}

