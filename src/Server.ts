import express = require('express');
import mysql = require('mysql');

class Server {
	private express : express.Application;

	constructor() {
		this.express = express();
		this.configureRoutes();
	}

	private configureRoutes() : void {
		let router = express.Router();
		router.get('/', (req, resp, next) => {
			resp.json({ message: 'Hello world!' });
		});
		this.express.use('/', router);
	}

	public start() : void {
		this.express.listen(3000);
	}
}

export = Server;