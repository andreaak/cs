import { Component } from "@angular/core";
import { User } from "../user";
import { NgForm } from "@angular/forms";

@Component({
    moduleId: module.id,
    selector: "form-errors",
    templateUrl: "form-errors.component.html",
    styleUrls: [
        "../../../node_modules/bootstrap/dist/css/bootstrap.css",
        "form-errors.component.css"
    ]
})
export class FormErrorsComponent {

   roles: string[] = ["Guest", "Moderator", "Administartor"];

    model: User = new User(1, "", "", null);

    submitted: boolean = false;

    onSubmit() {
        this.submitted = true;
    }
}