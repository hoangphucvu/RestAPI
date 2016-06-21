// refresh button
$("#refresh").click(function () {
    location.reload();
});
//post status
$("#postTweet").click(function () {
    var status = $("#status").val();
    if (!status) {
        alert("Plase input your status");
        return false;
    }
});
//check text remain in textarea
$(document).ready(function () {
    var text_length = 140;
    $("#feedback").html(text_length + " characters remaining");
    $("#status").keyup(function () {
        var actual_text = $("#status").val().length;
        var text_remain = text_length - actual_text;
        $("#feedback").html(text_remain + " characters remaining");
        if (text_remain == 0) {
            $("#feedback").html(" 0 characters remaining");
        }
    });
});