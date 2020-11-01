
export class Transaction {
    id: number;
    time: Date;
    amount: number;
    details: string;
    categoryid: number;

    constructor(id: number, time: Date, amount: number, details: string, categoryid: number) {
        this.id = id;
        this.amount = amount;
        this.time = time;
        this.details = details;
        this.categoryid = categoryid;
    }

}