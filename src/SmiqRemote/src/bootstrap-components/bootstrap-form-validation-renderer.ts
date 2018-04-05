import {
    RenderInstruction,
    ValidateResult,
    ValidationRenderer
} from 'aurelia-validation';

export class BootstrapFormValidationRenderer {
    render(instruction: RenderInstruction) {
        for (const { result, elements } of instruction.unrender) {
            for (const element of elements) {
                this.remove(element, result);
            }
        }

        for (const { result, elements } of instruction.render) {
            for (const element of elements) {
                this.add(element, result);
            }
        }
    }

    add(element: Element, result: ValidateResult) {
        const formGroup = element.closest('.form-group');
        if (!formGroup) {
          return;
        }

        if (result.valid) {
            return;
        }

        element.classList.add("is-invalid");

        const message = document.createElement('div');
        message.className = 'invalid-feedback';
        message.textContent = result.message;
        message.id = `validation-message-${result.id}`;
        formGroup.appendChild(message);
    }

    remove(element: Element, result: ValidateResult) {
        if (result.valid) {
            if (element.classList.contains("is-invalid")) {
                element.classList.remove("is-invalid");
            }
            return;
        }

        const formGroup = element.closest('.form-group');
        if (!formGroup) {
            return;
        }

        const message = formGroup.querySelector(`#validation-message-${result.id}`);
        if (message) {
            formGroup.removeChild(message);
        }
    }
}