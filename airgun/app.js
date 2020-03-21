'use strict';

const five = require('johnny-five');
const WebSocket = require('ws');

const wss = new WebSocket.Server({port: 5500});
const board = new five.Board({port: 'COM11'});

board.on('ready', () => {
    wss.on('connection', (ws) => {
        ws.on('open', () => {
            board.digitalWrite(13, 0);
            ws.send('connected!');
        });
        ws.on('close', () => {
        });
        ws.on('message', (status) => {
            board.digitalWrite(13, parseInt(status));
            ws.send(`I set solenoid status ${status}`);
        });
    });
});
