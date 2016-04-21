function WorkTimeRecordViewModel(app, dataModel) {
    var self = this;

    self.records = ko.observableArray();
    

    ko.bindingHandlers.kendoGrid.options = {
        groupable: {
            messages: {
                empty: "Drop columns here for grouping"
            }
        },        
        editable: {
            mode: "popup",
            window: {
                animation: true,
                width: '415px',
                title: "Add or edit"
                //,open: myOpenEventHandler
            }
        },
        toolbar: ["create"],//"excel", "save", "cancel"
        excel: {
            allPages: true,
            paperSize: "A4",
            landscape: false
        },
        filterable: true,
        navigatable: true,
        resizable: true,
        scrollable: true,        
        selectable: true,
        sortable: true,
        pageable: { pageSize: 10 },
        height: 510,
        noRecords: true,
        messages: {
            commands: {
                create: "Add new"
            },
            noRecords: "There is no data"
        },
        //,detailTemplate: "<div>User: #: UserName #</div><textarea>Description: #: Description #</textarea>"

        schema: {           
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number" },
                    Start: { type: "date", validation: { required: true } },
                    End: { type: "date", validation: { required: true } },
                    Description: { type: "string", validation: { required: true } },
                    UserName: { type: "string", validation: { required: true } }
                }
            }
        },
        columns: [
            {
                field: "UserName", title: "User",// width: "250px"                
                editor: function (container, options) {
                    var input = $('<input type="text" readonly class="k-input k-textbox" class name="' + options.field + '" />');
                    input.appendTo(container);
                }
            },
            {
                field: "Description",
                //template: "<textarea>#: Description # </textarea>",
                editor: function (container, options) {
                    var input = $('<textarea rows="8" cols="31" name="' + options.field + '" required="required" />');
                    input.appendTo(container);
                    var tooltipElement = $('<span class="k-invalid-msg" data-for="' + options.field + '"></span>');
                    tooltipElement.appendTo(container);
                }
            },
            {
                field: "Start", title: "Start date",// width: "250px",
                template: "<strong># var date = (typeof Start !== 'undefined' ? Start : new Date()) ##= common.toDateTimeString(date, 'MMM dd, yyyy hh:mm tt') #</strong>",
                editor: function (container, options) {
                    var input = $('<input name="' + options.field + '" required="required" />');
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        min: new Date(2016, 0, 1, 12, 0, 0), culture: "de-DE",
                        format: "yyyy-MM-dd hh:mm tt"
                    });
                    var tooltipElement = $('<span class="k-invalid-msg" data-for="' + options.field + '"></span>');
                    tooltipElement.appendTo(container);
                }
            },
            {
                field: "End", title: "End date",// width: "250px",
                template: "<strong># var date = (typeof End !== 'undefined' ? End : new Date()) ##=  common.toDateTimeString(date, 'MMM dd, yyyy hh:mm tt') #</strong>",
                editor: function (container, options) {
                    var input = $('<input name="' + options.field + '" required="required" />');
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        min: new Date(2016, 0, 1, 12, 0, 0), culture: "de-DE",
                        format: "yyyy-MM-dd hh:mm tt"
                    });
                    var tooltipElement = $('<span class="k-invalid-msg" data-for="' + options.field + '"></span>');
                    tooltipElement.appendTo(container);
                }
            },
            {
                command: [

                    { name: "edit", text: "edit" }
                  , { name: "destroy", text: "Remove" }
                ]
            }
        ],        
        save: function (e) {
            if (e.model) {
                addEditRecord(e.model);
            } else {
                getWorkTimeRecords();
            }            
            e.preventDefault();
        },
        remove: function (e) {
            if (confirm("Are you sure you want to remove this record?")) {
                if (e.model.Id > 0) {
                    removeRecord(e.model.Id);
                }
            } else {
                getWorkTimeRecords();
            }
            e.preventDefault();
        },
        dataBound: function (e) {

            e.preventDefault();
        }

    };

    function getWorkTimeRecords() {
        dataModel.getWorkTimeRecords()
                    .done(function (data) {
                        if (data.length) {
                            var recs = [];
                            data.forEach(function (rec, i) {
                                recs.push({
                                    Id: rec.id,
                                    Start: new Date(rec.start),//take into consoderation GMT time
                                    End: new Date(rec.end),
                                    Description: rec.description,
                                    UserName: rec.userName
                                });
                            });
                            self.records(recs);
                        } else {

                        }
                    })
                    .fail(function () {

                    });
    };
    getWorkTimeRecords();
    function addEditRecord(model) {
        var record = {
            Id: model.Id ? model.Id : 0,
            Description: model.Description,
            Start: model.Start,
            End: model.End,
            UserName: model.UserName
        };
        dataModel.saveWorkTimeRecord(record)
                    .done(function (data) {
                        if (data.length) {
                            app.errors.removeAll();
                            data.forEach(function (err, i) {
                                app.errors.push(err);
                            });
                            //e.preventDefault();
                        } else {
                            
                        }
                        getWorkTimeRecords();
                    })
                    .fail(function () {

                    });
    };

    function removeRecord(id) {
        dataModel.removeWorkTimeRecord(id)
                    .done(function (data) {
                        if (data && data.length) {
                            app.errors.removeAll();
                            data.forEach(function (err, i) {
                                app.errors.push(err);
                            });
                        } else {
                            
                        }
                        getWorkTimeRecords();
                    })
                    .fail(function () {

                    });
    };

}

app.addViewModel({
    name: "WorkTimeRecord",
    bindingMemberName: "workTimeRecord",
    factory: WorkTimeRecordViewModel
});
