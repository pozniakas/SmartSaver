
export class Goal {
    id: number;
    title: string;
    description: string;
    amount: number;
    creationdate: Date;
    deadlinedate: Date;

    constructor(id: number, title: string, description: string, amount: number, creationdate: Date, deadlinedate: Date) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.amount = amount;
        this.creationdate = creationdate;
        this.deadlinedate = deadlinedate;
    }

}