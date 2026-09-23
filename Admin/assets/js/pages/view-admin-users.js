$(document).ready(function () {

    $('.markBlocked').on('click', function () {

        var id = $(this).attr('data-id');
        var ftr = $("#chk_u_" + id).prop("checked") ? "Yes" : "No";
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
            cancelButtonClass: "btn btn-danger w-xs mt-2",
            confirmButtonText: "Yes, " + (ftr == "Yes" ? "Block" : "Unblock") + " it!",
            buttonsStyling: !1,
            showCloseButton: !0,
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    type: 'POST',
                    url: "view-admin-users.aspx/BlockUsers",
                    data: "{id: '" + id + "',ftr: '" + ftr + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                         
                        if (data2.d.toString().split('-')[0] == "Success") {
                            Snackbar.show({ pos: 'top-right', text: 'Updated successfully.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                            if (ftr === "Yes") {
                                $("#sts_" + id).removeAttr("class");
                                $("#sts_" + id).attr("class", "badge badge-soft-danger");
                                $("#sts_" + id).text("Blocked");
                            }
                            else {
                                $("#sts_" + id).text("Active");
                                $("#sts_" + id).removeAttr("class");
                                $("#sts_" + id).attr("class", "badge badge-soft-success");
                            } 
                        }
                       else if (data2.d.toString().split('-')[0] == "Permission") {
                            Snackbar.show({ pos: 'top-right', text: 'Oops! Permission denied. Please contact to your administrator', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                        }
                        else {
                            Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                        }
                    }
                });
            }
        }); 
    });

    $(document.body).on('click', '.deleteItem', function () {
        var elem = $(this);
        var id = elem.attr('data-id');
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
            cancelButtonClass: "btn btn-danger w-xs mt-2",
            confirmButtonText: "Yes, delete it!",
            buttonsStyling: !1,
            showCloseButton: !0,
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    type: 'POST',
                    url: "view-admin-users.aspx/Delete",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                        if (data2.d.toString() == "Success") {
                            Swal.fire({ title: "Deleted!", text: "User has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
                            elem.parent().parent().remove();
                        }
                        else if (data2.d.toString() == "Permission") {

                            Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                        }
                        else {
                            Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                        }
                    }
                });

            }
        })
    });
});