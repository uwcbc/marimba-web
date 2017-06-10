import express = require('express');
import mysql = require('mysql');
import CASAuthentication = require('cas-authentication');
import whiskers = require('whiskers');

class Server {
	private express : express.Application;
	private cas;

	constructor() {
		this.configureExpress();
		this.configureRoutes();
		this.configureCAS();
	}

	private configureExpress() : void {
		this.express = express();
		this.express.engine('.html', whiskers.__express);
	}

	private configureRoutes() : void {
		let router = express.Router();

		router.get('/', (req, resp, next) => {
			resp.redirect('/home');
		});

		router.get('/home', (req, resp, next) => {
			resp.send('Hello world!');
		});

		this.express.use('/', router);
	}

	private configureCAS() : void {
		this.cas = new CASAuthentication({
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