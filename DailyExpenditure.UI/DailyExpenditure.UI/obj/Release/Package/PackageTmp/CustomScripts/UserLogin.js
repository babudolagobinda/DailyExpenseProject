window.onload = function () {
    var duration = 2000;
    setTimeout(function () {
        $("#alertMsg").hide();
    }, duration);
}
$(document).ready(function () {
    $('.btn').on('click', function () {
        var $this = $(this);
        $this.button('loading');
        setTimeout(function () {
            $this.button('reset');
        }, 8000);
    });
})

