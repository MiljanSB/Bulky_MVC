var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'email', "width": "15%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'company.name', "width": "15%" },
            { data: 'role', "width": "15%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `<div class="text-center">
                                <a onclick="LockUnlock('${data.id}')" class="btn btn-success text-white">
                                    
                                    <i class="bi bi-unlock-fill"></i> Unlock
                                </a>
                        <a href="/admin/user/RoleManagment?userId=${data.id}" class="btn btn-danger text-white">
                                    <i class="bi bi-pencil-square"></i> Permissions
                                </a>
                        </div>`
                    } else {
                        return `<div class="text-center">
                                <a onclick="LockUnlock('${data.id}')" class="btn btn-danger text-white">
                                    <i class="bi bi-lock-fill"></i> Lock
                                </a>
                        <a href="/admin/user/RoleManagment?userId=${data.id}"  class="btn btn-danger text-white">
                                    <i class="bi bi-pencil-square"></i> Permissions
                                </a>
                        </div>`
                    }

                },
                "width": "35%"
            }
        ]
    });
}


function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/admin/user/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            } else {
                toastr.error(data.message);
            }
        }
    });
}
