
export class Transaction {
    id: number;
    time: Date;
    amount: number;
    details: string;

    constructor(id: number, time: Date, amount: number, details: string) {
        this.id = id;
        this.amount = amount;
        this.time = time;
        this.details = details;
    }

}