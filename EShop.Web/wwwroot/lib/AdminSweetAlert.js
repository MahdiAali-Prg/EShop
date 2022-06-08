function GenericFormSubmitWarning(formId, text) {
    swal({
        title: 'مطمعن هستید ؟',
        text: text,
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0CC27E',
        cancelButtonColor: '#FF586B',
        confirmButtonText: 'بله',
        cancelButtonText: "لغو"
    }).then(function (isConfirm) {
        if (isConfirm) {
            $("#" + formId).submit();
        }
    }).catch(swal.noop);
}