$(document).ready(function () {
    $('.changeAccess').on('click', function () {
      
        var id = $(this).attr('data-id');
        var rid = $("#rid").val();
        var id = $(this).attr('data-id');
        var ftr1 = $("#chk_vi_" + id).prop("checked") ? "Y" : "N";
        var ftr2 = $("#chk_ad_" + id).prop("checked") ? "Y" : "N";
        var ftr3 = $("#chk_ed_" + id).prop("checked") ? "Y" : "N";
        var ftr4 = $("#chk_del_" + id).prop("checked") ? "Y" : "N";
        $.ajax({
            type: 'POST',
            url: "manage-role-access.aspx/ChangeStatus",
            data: "{pid: '" + id + "',rid: '" + rid + "',v: '" + ftr1 + "',a: '" + ftr2 + "',e: '" + ftr3 + "',d: '" + ftr4 + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data2) { 

                if (data2.d.toString() == "Success") {
                    Snackbar.show({ pos: 'top-right', text: 'Updated successfully.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                }
                else if (data2.d.toString() == "Permission") {
                    Snackbar.show({ pos: 'top-right', text: 'Oops! Permission denied. Please contact to your administrator', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
                else {
                    Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
            }
        });
    });
     
});