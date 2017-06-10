import express = require("express");

declare class CASAuthentication {
    constructor (options: {
            cas_url: string,
            service_url: string,
            cas_version?: "1.0" | "2.0" | "3.0" | "saml1.1",
            renew?: boolean,
            is_dev_mode?: boolean,
            dev_mode_user?: string,
            dev_mode_info?: object,
            session_name?: string,
            session_info?: string,
            destroy_session?: boolean
        });

    bounce(req: express.Request, resp: express.Response, next: express.Handler): void;
    bounce_redirect(req: express.Request, resp: express.Response, next: express.Handler): void;
    block(req: express.Request, resp: express.Response, next: express.Handler): void;
    logout(req: express.Request, resp: express.Response, next: express.Handler): void;
}

export = CASAuthentication;
