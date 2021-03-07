[System.Serializable]

/*Class is written to send messages between AWS Lambda and Unity front-end. Contains an op code
  which can be expanded upon to have different functionality, an action to call a route and the 
  user score which Lambda manipulates and returns*/
public class GameMessage
{
    public string opcode;
    public string message;
    public string action;
    public int score;

    public GameMessage(string actionIn, string opcodeIn, int scoreIn)
    {
        action = actionIn;
        opcode = opcodeIn;
        score = scoreIn;
    }

    public GameMessage(string actionIn, string opcodeIn, string messageIn)
    {
        action = actionIn;
        opcode = opcodeIn;
        message = messageIn;
    }
}