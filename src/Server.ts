import express = require('express');
import mysql = require('mysql');
import CAS = require('cas-authentication');

class Server {
	private express : express.Application;
	private cas;

	constructor() {
		this.express = express();
		this.configureRoutes();
		this.configureCAS();
	}

	private configureRoutes() : void {
		let router = express.Router();
		router.get('/', (req, resp, next) => {
			resp.json({ message: 'Hello world!' });
		});
		this.express.use('/', router);
	}

	private configureCAS() : void {
		this.cas = new CAS({
			cas_url: 'https://cas-dev.uwaterloo.ca/cas',
			service_url: 'https://redir-loo.com',
			cas_version: '2.0'
		});
	}

	public start() : void {
		this.express.listen(3000);
	}
}

export = Server;