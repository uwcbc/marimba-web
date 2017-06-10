declare class Whiskers {
    static render(template: string, context: object): string;
    static __express(filename: string, options: any, callback: Function): any;
}

export = Whiskers;