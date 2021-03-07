const AWS = require('aws-sdk');
require('./patch.js');
let send = undefined;


function init(event) {
  const apigwManagementApi = new AWS.ApiGatewayManagementApi({
      apiVersion: '2018-11-29',
      endpoint: event.requestContext.domainName + '/' + event.requestContext.stage
  });
  // Send message to the client
  send = async (connectionId, data) => {
      await apigwManagementApi.postToConnection({
         ConnectionId: connectionId,
         Data: `${data}`
      }).promise();
  }
}

//Parses the event body and stores the connection ID. Checks if message is not null
//and contains an op code, then updates the score and returns a json object to the request.

exports.handler = (event, context, callback) => {
  console.log("Event received: %j", event);
  init(event);

  let message = JSON.parse(event.body);
  console.log("message: %j", message);
  console.log("message op: %j", message.opcode);

  let connectionIdForCurrentRequest = event.requestContext.connectionId;
  console.log("Current connection id: " + connectionIdForCurrentRequest);

  if (message && message.opcode) {
      switch (message.opcode) {
        case "11":
            let UserScore = message.score; // "10"
            UserScore = UserScore + 10// add one to 10
            console.log("Passed in " + UserScore);
            send(connectionIdForCurrentRequest,`{"opcode":11,"action":"Print HI", "score":${UserScore}}`).then(() => {
                callback(null, {
                   statusCode: 200
                })}).catch((err)=>console.error(err));
         default:
         console.error("Default hit");
      }
  }

};