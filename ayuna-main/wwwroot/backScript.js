let count = 4
let value = $("#portfolioId").val();

$(document).on("click", "#loadMore", function () {
    $.ajax({
        url: "/Portfolio/LoadMore/",
        type: "get",
        data: {
            "skip": count
        },
        success: function (res) {
            $("#portfolioWrapper").append(res)
            count += 4
            if (value <= count) {
                $("#loadMore").remove();
            }
        }
    })
});