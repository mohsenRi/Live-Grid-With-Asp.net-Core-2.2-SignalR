var connection = new signalR.HubConnectionBuilder().withUrl('/RealTimeGrid').build();
var $table = $('#table');
function startConnection() {
    connection.start().catch(function (err) {
        console.log(err);
    });
}
//to start connection with signalR
$(document).ready(function () {
    startConnection();
});

//bind signalR methods
connection.on("lockPerson", lock);
connection.on("updatePersonInfo", unlock);
connection.on("removePerson", remove);

//to remove updating record from others browser
function lock(personId) {
    $table.bootstrapTable('removeByUniqueId', personId);
}

//to show updated record to others
function unlock(personId, name, salary) {
    var row = ({
        personId: personId,
        name: name,
        salary: salary
    });
    $table.bootstrapTable('append', row);
}

//remove selected row form bootstrap table and database
function remove(personId) {
    $table.bootstrapTable('removeByUniqueId', personId);
}

function operateFormatter(value, row, index) {

    return [
        //Delete Record
        '<a class="Remove" href="javascript:void(0)"' +
        ' title="Remove">',
        '<i class="fa fa-trash-o" aria-hidden="true"></i>',
        '</a>  ',
        //Edit record
        '<a class="Edit" href="javascript:void(0)" title="Edit"' +
        ' data-toggle="modal" data-target="#exampleModal">',
        '<i class="fa fa-pencil-square-o" aria-hidden="true"></i>',
        '</a>'
    ].join('');

}


window.operateEvents = {
    //edit record
    'click .Edit': function (e, value, row, index) {
        var lockPerson = row.personId;
        connection.invoke("lockPerson", lockPerson);
        $("#editName").val(row.name);
        $("#editSalary").val(row.salary);
        $('#personId').val(row.personId);
    },
    //remove record
    'click .Remove': function (e, value, row, index) {
        var personId = row.personId;
        connection.invoke("DeletePerson", personId);
    }
};

//to save person record changes
$('#save').click(function () {
    var personId = $('#personId').val();
    var name = $("#editName").val();
    var salary = $("#editSalary").val();
    personId = parseInt(personId);

    //close modal
    $('#exampleModal').modal('toggle');
    //update current browser record
    $table.bootstrapTable('updateByUniqueId',
        {
            id: personId,
            row: {
                name: name,
                salary: salary
            }
        });
    //save changes
    connection.invoke("UpdatePersonInfo", personId, name, salary);
})