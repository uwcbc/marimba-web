import express = require('express');
import session = require('express-session');
import mysql = require('mysql');
import CASAuthentication = require('cas-authentication');
import whiskers = require('whiskers');

class Server {
	private express : express.Application;
	private cas: CASAuthentication;

	constructor() {
		this.configureExpress();
		this.configureCAS();
		this.configureRoutes();
	}

	private configureExpress() : void {
		this.express = express();
		
		// Whiskers JS setup
		this.express.engine('.html', whiskers.__express);
		this.express.set('views', __dirname + '/templates');

		// Express sessions setup
		this.express.use(session({ secret: 'lololololol', resave: false, saveUninitialized: false }));

		// Static resource setup
		this.express.use(express.static('resources'));
	}

	private configureRoutes() : void {
		let router = express.Router();
		let self = this;

		router.get('/', (req, resp) => {
			resp.redirect('/home');
		});

		router.get('/home', (req, resp) => {
			resp.render(
				'page-template.html',
				{
					partials: {
						header: 'header.html',
						body: 'homepage-not-loggedin.html'
					},
					title: 'Marimba Home',
					user: null
				}
			);
		});

		router.get('/login', self.cas.bounce, (req, resp) => {
			resp.redirect('/home');
		});

		this.express.use('/', router);
	}

	private configureCAS() : void {
		this.cas = new CASAuthentication({
			cas_url: 'https://cas-dev.uwaterloo.ca/cas',
			service_url: 'http://localhost:3000',
			cas_version: '2.0'
		});
	}

	public start() : void {
		this.express.listen(3000);
	}
}

export = Server;