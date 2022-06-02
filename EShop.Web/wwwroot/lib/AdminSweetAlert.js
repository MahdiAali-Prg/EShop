function SubmitEditBrand() {
    swal({
        title: 'مطمعن هستید ؟',
        text: "آیا از ویرایش این برند مطمعن هستند ؟",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0CC27E',
        cancelButtonColor: '#FF586B',
        confirmButtonText: 'بله',
        cancelButtonText: "لغو"
    }).then(function (isConfirm) {
        if (isConfirm) {
            $("#Edit-Brand-Form").submit();
        }
    }).catch(swal.noop);
}