import { Component } from "@angular/core";

@Component({
    moduleId: module.id,
    selector: "timer-host",
    templateUrl: "timer-host.component.html"
})
export class TimerHostComponent {
    tickHandler1(value) {
        console.log("Tick Hanlder 1 - " + value);
    }

    tickHandler2(value) {
        console.log("Tick Hanlder 2 - " + value);
    }
}