var io = require('socket.io')(process.env.PORT || 52300);
//Custom Classes
var Player = require('./Classes/Player');
console.log('Server has start');

var players = [];
var sockets = [];
io.on('connection', function (socket) {
    console.log('Connection made!');
    var player = new Player();
    var thisPlayerID = player.id;

    players[thisPlayerID] = player;
    sockets[thisPlayerID] = socket;

    //Tell the client that this is our id for the server

    socket.emit('register', {
        id: thisPlayerID
    });

    socket.emit('spawn', player); //Tell myselft I have spawned
    socket.broadcast.emit('spawn', player); //Tell other I have spawned

    //Tell myself about everyone else in the game

    for(var playerId in players) {
        if(playerId != thisPlayerID) {
            socket.emit('spawn', players[playerId]);
            
        }
    }
    //positional data from client
    socket.on('updatePosition', function(data) {
        player.position.x = data.position.x;
        player.position.y = data.position.y;

        player.position.z = data.position.z;

        socket.broadcast.emit('updatePosition', player);
        player.position.ConsoleOutPut();

    });
    socket.on('disconnect', function () {
        console.log('A player has disconnected!')
        delete players[thisPlayerID];
        delete sockets[thisPlayerID];
        socket.broadcast.emit('disconnected', player)
    });
});