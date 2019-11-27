var io = require('socket.io')(process.env.PORT || 52300);
//Custom Classes
var Player = require('./Classes/Player');
var Shell = require('./Classes/Shell')
console.log('Server has start');

var players = [];
var sockets = [];
var shells = [];
//updates
setInterval(() => {
    shells.forEach(shell => {
        var isDestroyed = shell.onUpdate();

        if (isDestroyed) {
            var index = shells.indexOf(shell);
            if (index > -1) {
                shells.splice(index, 1);

                var returnData = {
                    id: shell.id
                }

                for (var playerID in players) {
                    sockets[playerID].emit('serverUnspawn', returnData);
                }
            }
        } else {
            var returnData = {
                id: shell.id,
                position: {
                    x: shell.position.x,
                    y: shell.position.y,
                    z: shell.position.z
                }
            }
            for (var playerID in players) {
                sockets[playerID].emit('updatePosition', returnData);
            }

        }
    });

}, 100, 0);
io.on('connection', function (socket) {
    console.log('Connection made!');
    var player = new Player();
    var thisPlayerID = player.id;

    players[thisPlayerID] = player;
    sockets[thisPlayerID] = socket;
    console.log(players);
    //Tell the client that this is our id for the server

    socket.emit('register', {
        id: thisPlayerID
    });

    socket.emit('spawn', player); //Tell myselft I have spawned
    socket.broadcast.emit('spawn', player); //Tell other I have spawned

    //Tell myself about everyone else in the game

    for (var playerId in players) {
        if (playerId != thisPlayerID) {
            socket.emit('spawn', players[playerId]);

        }
    }
    //positional data from client
    socket.on('updatePosition', function (data) {
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


    socket.on('fireShell', function (data) {
        var shell = new Shell();
        shell.name = 'Shell';
        shell.position.x = data.position.x;
        shell.position.y = data.position.y;
        shell.position.z = data.position.z;
        shell.direction.x = data.direction.x;
        shell.direction.y = data.direction.y;
        shell.direction.z = data.direction.z;

        shells.push(shell);
        var returnData = {
            name: shell.name,
            id: shell.id,
            position: {
                x: shell.position.x,
                y: shell.position.y,
                z: shell.position.z,

            },
            direction: {
                x: shell.direction.x,
                y: shell.direction.y,
                z: shell.direction.z,
            }
        }

        socket.emit('serverSpawn', returnData);
        socket.broadcast.emit('serverSpawn', returnData);

    });


});


function interval(func, wait, times) {
    var interval = function (w, t) {
        return function () {
            if (typeof t === "undefined" || t-- > 0) {
                setTimeout(interv, w);
                try {
                    func.call(null)
                } catch (error) {
                    t = 0;
                    throw error.toString();
                }
            }
        };
    }(wait, times);

    setTimeout(interv, wait);
}