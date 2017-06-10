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
			let user = req.session[ self.cas.session_name ];
			let loggedIn = user != null;
			resp.render(
				'page-template.html',
				{
					partials: {
						header: 'header.html',
						body: loggedIn ? 'homepage-loggedin.html' : 'homepage-not-loggedin.html'
					},
					title: 'Marimba Home',
					user: null,
					nextRehearsalDate: loggedIn ? 'tomorrow' : null,
					history: loggedIn ? [ 'history 1', 'history 2' ] : null
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