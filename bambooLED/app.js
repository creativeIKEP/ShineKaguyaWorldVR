'use strict';

const five = require('johnny-five');
const express = require('express');
const http = require('http');
const WebSocket = require('ws');

const app = express();
app.use(express.static('www'));

const server = http.createServer(app);
const wss = new WebSocket.Server({server});

server.listen(3000);

const board = new five.Board({port: 'COM6'});

board.on('ready', () => {
    const led = {
        'slight': new five.Led(8),
        'medium': new five.Led(10),
        'bright': new five.Led(12),
    };

    function LedOn(state) {
        Object.keys(led).forEach((key) => {
            led[key].off();
        });
        switch (state) {
        case 1:
            led.bright.on();
            break;
        case 2:
            led.slight.on();
            led.medium.on();
            break;
        case 3:
            led.bright.on();
            led.medium.on();
            break;
        case 4:
            led.slight.on();
            led.medium.on();
            led.bright.on();
            break;
        default:
            break;
        }
    }

    wss.on('connection', (ws) => {
        ws.on('open', () => {
            ws.send('connected');
        });
        ws.on('close', () => {
        });
        ws.on('message', (stateID) => {
            LedOn(parseInt(stateID));
            ws.send(`The StateID is ${stateID}`);
        });
    });

});
