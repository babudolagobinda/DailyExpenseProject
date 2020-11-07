window.onload = function () {
    var duration = 2000;
    setTimeout(function () {
        $("#successMsg").hide();
    }, duration);
    //reset();
    $('.btn').on('click', function () {
        var $this = $(this);
        $this.button('loading');
        setTimeout(function () {
            $this.button('reset');
        }, 8000);
    });
}
function reset() {
    $("#Name").val("");
    $("#EmailId").val("");
    $("#PhoneNo").val("");
    $("#UserName").val("");
}