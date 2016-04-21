function HomeViewModel(app, dataModel) {
    var self = this;
    //self.myHometown = ko.observable("Germany");
    // HomeViewModel currently does not require data binding, so there are no visible members.

    self.usersRegistered = ko.observableArray();
    dataModel.getRegisteredUsers()
            .done(function (data) {
                if (data && data.length) {
                    var users = [];
                    data.orderBy('userName', 'asc')
                    .forEach(function (u, i) {
                        users.push({
                            Id: i+1,
                            UserName: u.userName,
                            FirstName: u.firstName,
                            LastName: u.lastName,
                            Email: u.email,
                            Roles: u.roles
                        });
                    });
                    self.usersRegistered(users);
                } else {
                    app.navigateToLogin();
                }
            })
            .fail(function () {
                app.navigateToLogin();
            });
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});
