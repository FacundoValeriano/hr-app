import { ValidatorFn, FormControl } from "@angular/forms";

export const trimValidator: ValidatorFn = (control: FormControl) => {
    if (control.value != null) {
        let validationRuleStart = /^\s/;
        if (validationRuleStart.test(control.value.toString())) {
            return {
                'trimBeginError': true
            };
        }
        let validationRuleEnd = /\s$/;
        if (validationRuleEnd.test(control.value.toString())) {
            return {
                'trimEndError': true
            };
        }
    }
    return null;
};