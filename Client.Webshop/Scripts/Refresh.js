function setRefresh() {
    setTimeout("refresh()", 300000);
}

function refresh() {
    $.get(
        "/SessionRefresh.ashx",
        null,
        function (data) {
            setRefresh();
        },
    );
}
