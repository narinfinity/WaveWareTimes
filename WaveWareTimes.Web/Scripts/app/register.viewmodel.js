function RegisterViewModel(app, dataModel) {
    var self = this;

    // Data
    self.userName = ko.observable("").extend({ required: true, maxLength: 100 });
    self.firstName = ko.observable("").extend({ required: true, maxLength: 100 });
    self.lastName = ko.observable("").extend({ required: true, maxLength: 100 });
    self.email = ko.observable("").extend({ required: true, email: true, maxLength: 100 });
    self.password = ko.observable("").extend({ required: true, maxLength: 100 });
    self.confirmPassword = ko.observable("").extend({ required: true, equal: self.password, maxLength: 100 });

    // Other UI state
    self.registering = ko.observable(false);
    self.errors = ko.observableArray();
    self.validationErrors = ko.validation.group([self.userName, self.firstName, self.lastName, self.email, self.password, self.confirmPassword]);

    // Operations
    self.register = function () {
        self.errors.removeAll();
        if (self.validationErrors().length > 0) {
            self.validationErrors.showAllMessages();
            return;
        }
        self.registering(true);

        dataModel.register({
            userName: self.userName(),
            firstName: self.firstName(),
            lastName: self.lastName(),
            email: self.email(),
            password: self.password(),
            confirmPassword: self.confirmPassword()
        }).done(function (data) {
            dataModel.login({
                grant_type: "password",
                username: self.userName(),
                password: self.password()
            }).done(function (data) {
                self.registering(false);

                if (data.userName && data.access_token) {
                    //app.navigateToLoggedIn(data, data.access_token, false /* persistent */);
                    app.navigateToHome();
                } else {
                    self.errors.push("An unknown error occurred.");
                }
            }).failJSON(function (data) {
                self.registering(false);

                if (data && data.error_description) {
                    self.errors.push(data.error_description);
                } else {
                    self.errors.push("An unknown error occurred.");
                }
            });
        }).failJSON(function (data) {
            var errors;

            self.registering(false);
            errors = dataModel.toErrorsArray(data);

            if (errors) {
                self.errors(errors);
            } else {
                self.errors.push("An unknown error occurred.");
            }
        });
    };

    self.login = function () {
        app.navigateToLogin();
    };
}

app.addViewModel({
    name: "Register",
    bindingMemberName: "register",
    factory: RegisterViewModel
});
