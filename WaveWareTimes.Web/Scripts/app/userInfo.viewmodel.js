function UserInfoViewModel(app, userInfo, dataModel) {
    var self = this;

    // Data
    self.name = ko.observable(userInfo.userName);
    self.isAdmin = ko.observable(userInfo.isAdmin || false);

    // Operations
    self.logOff = function () {
        dataModel.logout().done(function () {
            app.navigateToLoggedOff();
        }).fail(function () {
            app.errors.push("Log off failed.");
        });
    };

    self.manage = function () {
        app.navigateToManage();
    };

    self.register = function () {
        app.navigateToRegister();
    };
    self.navigateToUsers = function () {
        app.navigateToHome();
    };

    dataModel.getUserInfo()
                .done(function (data) {
                    if (typeof (data.userName) !== "undefined"
                        && typeof (data.hasRegistered) !== "undefined"
                        && typeof (data.loginProvider) !== "undefined") {
                        if (data.hasRegistered) {
                            self.isAdmin(data.isAdmin);
                        }                        
                        else {
                            self.navigateToLogin();
                        }
                    } else {
                        self.navigateToLogin();
                    }
                })
                .fail(function () {
                    self.navigateToLogin();
                });

}
