
$(document).ready(function () {
    ReadTodoData();
});


//function GetTodosData() {
//    $.ajax({
//        url: '/Home/GetTodosData',
//        type: 'post',
//        dataType: 'json',
//        success: OnSuccess
//    });
//}

function ReadTodoData(res) {
    $('#usersDetailOne').DataTable({
        bProcessing: true,
        bLenghtChange: true,
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
        bfilter: true,
        bSort: true,
        bPaginate: true,
        //data: res,
        //columns: [
        //    {
        //        data: 'Name',
        //        render: function (data, type, row, meta) {
        //            return row.name
        //        }
        //    },
        //    {
        //        data: 'CnName',
        //        render: function (data, type, row, meta) {
        //            return row.cnName
        //        }
        //    },
        //    {
        //        data: 'ItemName',
        //        render: function (data, type, row, meta) {
        //            return row.itemName
        //        }
        //    },
        //    {
        //        data: 'DeadLine',
        //        render: function (data, type, row, meta) {
        //            return row.deadLine
        //        }
        //    },
        //    {
        //        data: 'InsertDate',
        //        render: function (data, type, row, meta) {
        //            return row.insertDate
        //        }
        //    },
        //    {
        //        data: 'UpdateDate',
        //        render: function (data, type, row, meta) {
        //            return row.updateDate
        //        }
        //    }
        //]
    });
}
