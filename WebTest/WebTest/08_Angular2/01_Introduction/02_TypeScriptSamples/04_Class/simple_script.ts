class Human {
    name: string;
    age: number;
    messageElementId: any;

    constructor(nameValue: string, ageValue: number, selector: string) {
        this.name = nameValue;
        this.age = ageValue;
        this.messageElementId = document.querySelector(selector);
    }

    say() {
        let message = `My name is ${this.name} I am ${this.age} years old`;
        this.messageElementId.innerHTML = message;
    }
}

let h = new Human("Ivan", 25, "#output");
h.say();